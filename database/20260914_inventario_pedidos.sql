-- Ejecutar sobre MueblesKeyda. Reejecutable; conserva los registros existentes.
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @Restriccion sysname;
DECLARE @Sql nvarchar(max);
DECLARE restricciones CURSOR LOCAL FAST_FORWARD FOR
    SELECT name FROM sys.check_constraints
    WHERE parent_object_id = OBJECT_ID('dbo.Pedido')
      AND definition LIKE '%Estado%';
OPEN restricciones;
FETCH NEXT FROM restricciones INTO @Restriccion;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Sql = N'ALTER TABLE dbo.Pedido DROP CONSTRAINT ' + QUOTENAME(@Restriccion);
    EXEC sp_executesql @Sql;
    FETCH NEXT FROM restricciones INTO @Restriccion;
END;
CLOSE restricciones;
DEALLOCATE restricciones;
ALTER TABLE dbo.Pedido WITH CHECK ADD CONSTRAINT CK_Pedido_Estado
    CHECK (Estado IN ('Finalizado', 'En proceso', 'Cancelado'));
EXEC(N'CREATE OR ALTER VIEW dbo.VerProveedores AS
    SELECT IdProveedor, Nombre_Proveedor AS Proveedor, Telefono, Correo, Ubicacion,
        CASE WHEN Estado = 1 THEN ''Activo'' ELSE ''Inactivo'' END AS Estado
    FROM dbo.Proveedor;');
COMMIT TRANSACTION;
