using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Modelo.Conexión_DB;
using Modelo.Entidades;

// Ejecutar desde la raíz del repositorio. Usa una base temporal y la elimina al finalizar.
internal static class InventoryWorkflowTests
{
    static void Assert(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
        Console.WriteLine("PASS: " + message);
    }

    static object Sql(string sql)
    {
        using (var connection = Conexion.Conectar())
        using (var command = new SqlCommand(sql, connection))
            return command.ExecuteScalar();
    }

    static int Number(string sql) { return Convert.ToInt32(Sql(sql)); }
    static int Stock() { return Number("SELECT Stock FROM Material WHERE IdMaterial = 1"); }
    static DetalleCompraMaterial Item(int id, int quantity, int material = 1)
    {
        return new DetalleCompraMaterial(id, 0, material, quantity, 2m);
    }

    static void MustFail(Action action, string message)
    {
        bool failed = false;
        try { action(); } catch (Exception) { failed = true; }
        Assert(failed, message);
    }

    static DataTable Consumption(params int[] quantities)
    {
        var table = new DataTable();
        table.Columns.Add("IdMaterial", typeof(int));
        table.Columns.Add("Cantidad_Utilizada", typeof(int));
        foreach (int quantity in quantities) table.Rows.Add(1, quantity);
        return table;
    }

    [STAThread]
    static int Main()
    {
        string database = "KeydaTests_" + Guid.NewGuid().ToString("N");
        var field = typeof(Conexion).GetField("baseDeDatos", BindingFlags.Static | BindingFlags.NonPublic);
        object original = field.GetValue(null);
        using (var admin = Conexion.Conectar())
        {
            admin.ChangeDatabase("master");
            using (var create = new SqlCommand("CREATE DATABASE [" + database + "]", admin))
                create.ExecuteNonQuery();
            try
            {
                field.SetValue(null, database);
                Sql(@"
                    CREATE TABLE Proveedor (IdProveedor int IDENTITY PRIMARY KEY, Nombre_Proveedor varchar(50),
                        Telefono varchar(9), Correo varchar(100), Ubicacion varchar(200), Estado bit);
                    INSERT INTO Proveedor VALUES ('Prueba', '123456789', 'prueba@example.com', 'Prueba', 1);
                    CREATE TABLE Material (IdMaterial int IDENTITY PRIMARY KEY, Stock int NOT NULL CHECK (Stock >= 0));
                    INSERT INTO Material VALUES (100), (100);
                    CREATE TABLE Compras (IdCompra int IDENTITY PRIMARY KEY, FechaCompra date NOT NULL,
                        TotalCompra decimal(10,2) NOT NULL, IdProveedor int REFERENCES Proveedor(IdProveedor));
                    CREATE TABLE DetalleCompraMaterial (IdDetalleCompraMaterial int IDENTITY PRIMARY KEY,
                        IdCompra int REFERENCES Compras(IdCompra), IdMaterial int REFERENCES Material(IdMaterial),
                        Cantidad int NOT NULL, PrecioUnitario decimal(10,2) NOT NULL);
                    CREATE TABLE Pedido (IdPedido int IDENTITY PRIMARY KEY, Estado varchar(10)
                        CHECK (Estado IN ('En proceso', 'Finalizado')));
                    INSERT INTO Pedido VALUES ('En proceso');
                    CREATE TABLE DetallePedido (IdDetallePedido int IDENTITY PRIMARY KEY,
                        IdPedido int REFERENCES Pedido(IdPedido), Mueble varchar(150), Cantidad int, Medidas nvarchar(100));
                    INSERT INTO DetallePedido VALUES (1, 'Mesa', 1, '1x1x1'), (1, 'Silla', 1, '1x1x1');
                    CREATE TABLE Produccion (IdProduccion int PRIMARY KEY, IdPedido int REFERENCES Pedido(IdPedido));
                    INSERT INTO Produccion VALUES (1, 1);
                    CREATE TABLE MaterialUtilizado (IdMaterialUtilizado int IDENTITY PRIMARY KEY,
                        IdMaterial int REFERENCES Material(IdMaterial), IdProduccion int REFERENCES Produccion(IdProduccion),
                        Cantidad_Utilizada int NOT NULL);");
                string migration = File.ReadAllText("database/20260914_inventario_pedidos.sql");
                Sql(migration);
                Sql(migration);
                Assert(DbProveedor.CargarProveedor().Rows[0]["Estado"].ToString() == "Activo", "Vista de proveedores y migración reejecutable");

                MaterialUtilizado.GuardarConsumo(1, Consumption(3, 7));
                Assert(Stock() == 90 && Number("SELECT COUNT(*) FROM MaterialUtilizado") == 2,
                    "Consumo de varios materiales descuenta exactamente una vez");
                MustFail(() => MaterialUtilizado.GuardarConsumo(1, Consumption(5, 100)), "Rechaza consumo acumulado sin stock");
                Assert(Stock() == 90 && Number("SELECT COUNT(*) FROM MaterialUtilizado") == 2, "Fallo revierte todo el lote de consumo");
                MustFail(() => MaterialUtilizado.GuardarConsumo(999, Consumption(1)), "Rechaza producción inexistente");
                MustFail(() => MaterialUtilizado.GuardarConsumo(1, Consumption(0)), "Rechaza cantidad cero");
                Assert(Stock() == 90, "Errores de consumo conservan inventario");

                var items = new List<DetalleCompraMaterial> { Item(0, 10), Item(0, 5), Item(0, 4, 2) };
                int purchase = ComprasDb.GuardarCompleta(0, DateTime.Today, 1, items);
                Assert(Stock() == 105, "Compra suma todas las filas del mismo material");
                int first = Number("SELECT MIN(IdDetalleCompraMaterial) FROM DetalleCompraMaterial WHERE IdCompra=" + purchase);
                items = new List<DetalleCompraMaterial> { Item(first, 12) };
                ComprasDb.GuardarCompleta(purchase, DateTime.Today, 1, items);
                Assert(Stock() == 102 && Number("SELECT COUNT(*) FROM DetalleCompraMaterial") == 1
                    && Number("SELECT TotalCompra FROM Compras WHERE IdCompra=" + purchase) == 24,
                    "Quitar productos actualiza inventario, detalles y total juntos");
                Assert(Number("SELECT Stock FROM Material WHERE IdMaterial=2") == 100, "Restaura inventario del material quitado");
                ComprasDb.GuardarCompleta(purchase, DateTime.Today, 1, items);
                Assert(Stock() == 102, "Guardar edición dos veces no duplica el movimiento");
                Sql("UPDATE Material SET Stock=1 WHERE IdMaterial=1");
                MustFail(() => ComprasDb.GuardarCompleta(purchase, DateTime.Today, 1,
                    new List<DetalleCompraMaterial> { Item(first, 1) }), "Rechaza reversión sin existencias suficientes");
                Assert(Stock() == 1 && Number("SELECT TotalCompra FROM Compras WHERE IdCompra=" + purchase) == 24,
                    "Edición fallida conserva cabecera, detalles e inventario");
                Sql("UPDATE Material SET Stock=102 WHERE IdMaterial=1");
                Sql("UPDATE Material SET Stock=Stock+20 WHERE IdMaterial=1");
                Assert(new ComprasDb { IdCompra1 = purchase }.EliminarCompra(), "Elimina compra completa");
                Assert(Stock() == 110 && Number("SELECT COUNT(*) FROM Compras") == 0,
                    "Reversión conserva movimientos ajenos a la compra");
                purchase = ComprasDb.GuardarCompleta(0, DateTime.Today, 1,
                    new List<DetalleCompraMaterial> { Item(0, 3), Item(0, 8) });
                Assert(new ComprasDb { IdCompra1 = purchase }.EliminarCompra() && Stock() == 110,
                    "Borrar compra con material repetido revierte la suma completa");
                MustFail(() => ComprasDb.GuardarCompleta(0, DateTime.Today, 1,
                    new List<DetalleCompraMaterial> { Item(0, 1, 999) }), "Compra inválida no deja una cabecera huérfana");
                Assert(Number("SELECT COUNT(*) FROM Compras") == 0, "Fallo al crear revierte toda la compra");

                Assert(!DetallePedidos.EliminarDetalle(1, 1, false), "Quitar primer producto conserva pedido activo");
                MustFail(() => DetallePedidos.EliminarDetalle(1, 2, false), "Último producto requiere confirmar cancelación");
                Assert(Number("SELECT COUNT(*) FROM DetallePedido") == 1, "Sin confirmación se conserva el último producto");
                Assert(DetallePedidos.EliminarDetalle(1, 2, true), "Último producto cancela pedido");
                Assert(Sql("SELECT Estado FROM Pedido WHERE IdPedido=1").ToString() == "Cancelado"
                    && Number("SELECT COUNT(*) FROM DetallePedido") == 0 && Stock() == 110
                    && Number("SELECT COUNT(*) FROM MaterialUtilizado") == 2,
                    "Pedido cancelado conserva registro, producción e inventario");
                Assert(!DbPedidos.ActualizarPedidoEstado(1, "En proceso"), "Pedido vacío no se reactiva al guardar");
                Console.WriteLine("ALL TESTS PASSED");
                return 0;
            }
            catch (Exception error)
            {
                Console.Error.WriteLine(error);
                return 1;
            }
            finally
            {
                field.SetValue(null, original);
                SqlConnection.ClearAllPools();
                using (var drop = new SqlCommand("ALTER DATABASE [" + database +
                    "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [" + database + "];", admin))
                    drop.ExecuteNonQuery();
            }
        }
    }
}
