using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Modelo.Servicios
{
    public class ServicioCorreo
    {
        public static void EnviarCodigo(string correoDestino, string codigo)
        {
            var mensaje = new MimeMessage();

            mensaje.From.Add(new MailboxAddress("Sistema de Ventas", "abi601619@gmail.com"));

            mensaje.To.Add(MailboxAddress.Parse(correoDestino));

            mensaje.Subject = "wrsj cwmi vsyq pwoh";

            mensaje.Body = new TextPart("html")
            {
                Text = $@" <html>
                <body>

                    <h2>Recuperación de contraseña</h2>

                    <p>
                        Hemos recibido una solicitud
                        para recuperar tu contraseña.
                    </p>

                    <p>
                        Tu código de verificación es:
                    </p>

                    <h1>{codigo}</h1>

                    <p>
                        Este código es válido durante
                        <strong>10 minutos</strong>.
                    </p>

                    <p>
                        Si no solicitaste recuperar tu contraseña,
                        puedes ignorar este correo.
                    </p>

                </body>
                </html>"
            };

            using (var cliente = new SmtpClient())
            {
                cliente.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                cliente.Authenticate("abi601619@gmail.com", "wrsj cwmi vsyq pwoh");

                cliente.Send(mensaje);

                cliente.Disconnect(true);
            }
        }
    }
}
