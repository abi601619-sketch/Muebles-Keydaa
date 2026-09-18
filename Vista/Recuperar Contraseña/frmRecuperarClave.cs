using Modelo.Entidades;
using Modelo.Servicios;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vista.Recuperar_Contraseña
{
    public partial class frmRecuperarClave : Form
    {
        private int idUsuarioActual;
        public frmRecuperarClave()
        {
            InitializeComponent();

            txtNuevaContrasena.UseSystemPasswordChar = true;
            txtConfirmarContrasena.UseSystemPasswordChar = true;
        }

        private void btnCodigoRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                string correo = txtCorreoRecuperacion.Text.Trim();

                // =========================================
                // VALIDAR CORREO
                // =========================================

                if (string.IsNullOrWhiteSpace(correo))
                {
                    MessageBox.Show(
                        "Ingrese su correo electrónico.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCorreoRecuperacion.Focus();
                    return;
                }

                // =========================================
                // BUSCAR USUARIO
                // =========================================

                DataTable usuario =
                    DbRecuperacion.BuscarUsuarioPorCorreo(correo);

                if (usuario.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No existe un usuario activo asociado a este correo.",
                        "Correo no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // =========================================
                // OBTENER ID DEL USUARIO
                // =========================================

                int idUsuario =
                    Convert.ToInt32(
                        usuario.Rows[0]["IdUsuario"]
                    );

                // =========================================
                // GENERAR CÓDIGO
                // =========================================

                string codigo =
                    GeneradorCodigo.GenerarCodigo();

                // =========================================
                // GUARDAR CÓDIGO EN SQL
                // =========================================

                DbRecuperacion.GuardarCodigo(
                    idUsuario,
                    codigo
                );

                // =========================================
                // ENVIAR CORREO
                // =========================================

                ServicioCorreo.EnviarCodigo(
                    correo,
                    codigo
                );

                // =========================================
                // GUARDAR ID PARA EL SIGUIENTE PASO
                // =========================================

                idUsuarioActual = idUsuario;

                MessageBox.Show(
                    "Se ha enviado un código de verificación a su correo.",
                    "Código enviado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtCodigo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al enviar el código:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnRecuperarClave_Click(object sender, EventArgs e)
        {
            try
            {
                // =========================================
                // VERIFICAR QUE SE HAYA ENVIADO UN CÓDIGO
                // =========================================

                if (idUsuarioActual == 0)
                {
                    MessageBox.Show(
                        "Primero debe ingresar su correo y enviar el código de verificación.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCorreoRecuperacion.Focus();
                    return;
                }

                // =========================================
                // OBTENER DATOS
                // =========================================

                string codigo = txtCodigo.Text.Trim();

                string nuevaContraseña =
                    txtNuevaContrasena.Text;

                string confirmarContraseña =
                    txtConfirmarContrasena.Text;

                // =========================================
                // VALIDAR CÓDIGO
                // =========================================

                if (string.IsNullOrWhiteSpace(codigo))
                {
                    MessageBox.Show(
                        "Ingrese el código de verificación.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCodigo.Focus();
                    return;
                }

                // =========================================
                // VALIDAR LONGITUD DEL CÓDIGO
                // =========================================

                if (codigo.Length != 6)
                {
                    MessageBox.Show(
                        "El código debe tener 6 dígitos.",
                        "Código inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCodigo.Focus();
                    return;
                }

                // =========================================
                // VALIDAR NUEVA CONTRASEÑA
                // =========================================

                if (string.IsNullOrWhiteSpace(nuevaContraseña))
                {
                    MessageBox.Show(
                        "Ingrese una nueva contraseña.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtNuevaContrasena.Focus();
                    return;
                }

                // =========================================
                // VALIDAR CONFIRMACIÓN
                // =========================================

                if (string.IsNullOrWhiteSpace(confirmarContraseña))
                {
                    MessageBox.Show(
                        "Confirme la nueva contraseña.",
                        "Advertencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtConfirmarContrasena.Focus();
                    return;
                }

                // =========================================
                // COMPARAR CONTRASEÑAS
                // =========================================

                if (nuevaContraseña != confirmarContraseña)
                {
                    MessageBox.Show(
                        "Las contraseñas no coinciden.",
                        "Contraseñas diferentes",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtConfirmarContrasena.Clear();
                    txtConfirmarContrasena.Focus();

                    return;
                }

                // =========================================
                // VERIFICAR CÓDIGO EN LA BASE DE DATOS
                // =========================================

                DataTable resultado =
                    DbRecuperacion.VerificarCodigo(
                        idUsuarioActual,
                        codigo
                    );

                if (resultado.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "El código es incorrecto, ya fue utilizado o ha expirado.",
                        "Código inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtCodigo.Clear();
                    txtCodigo.Focus();

                    return;
                }

                // =========================================
                // OBTENER ID DE LA RECUPERACIÓN
                // =========================================

                int idRecuperacion =
                    Convert.ToInt32(
                        resultado.Rows[0]["IdRecuperacion"]
                    );

                // =========================================
                // CAMBIAR CONTRASEÑA
                // =========================================

                DbRecuperacion.CambiarContraseña(
                    idUsuarioActual,
                    nuevaContraseña
                );

                // =========================================
                // MARCAR CÓDIGO COMO UTILIZADO
                // =========================================

                DbRecuperacion.MarcarCodigoUsado(
                    idRecuperacion
                );

                // =========================================
                // MENSAJE DE ÉXITO
                // =========================================

                MessageBox.Show(
                    "La contraseña se cambió correctamente.",
                    "Contraseña actualizada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // =========================================
                // LIMPIAR CAMPOS
                // =========================================

                txtCorreoRecuperacion.Clear();
                txtCodigo.Clear();
                txtNuevaContrasena.Clear();
                txtConfirmarContrasena.Clear();

                idUsuarioActual = 0;

                // =========================================
                // CERRAR FORMULARIO
                // =========================================

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al cambiar la contraseña:\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnVerContrasena_Click(object sender, EventArgs e)
        {
            if (txtNuevaContrasena.UseSystemPasswordChar)
            {
                txtNuevaContrasena.UseSystemPasswordChar = false;
                btnVerContrasena.Text = "🙈";
            }
            else
            {
                txtNuevaContrasena.UseSystemPasswordChar = true;
                btnVerContrasena.Text = "👁";
            }
        }
    }
}
