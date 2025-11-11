using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Agro_UES.FormLogin;


namespace Agro_UES
{
    public static class SesionUsuario
    {
        private static int formulariosAbiertos = 0;

        public static void NotificarFormularioAbierto()
        {
            if (formulariosAbiertos == 0)
            {
                AbrirSesion();
            }

            formulariosAbiertos++;
        }

        public static void NotificarFormularioCerrado()
        {
            formulariosAbiertos--;

            if (formulariosAbiertos <= 0)
            {
                CerrarSesion();
            }
        }

        private static void AbrirSesion()
        {
            string abrirSesion = "UPDATE usuarios SET sesion_activa = TRUE WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(abrirSesion, conexion))
            {
                comando.Parameters.AddWithValue("@idUsuario", UsuarioSesion.IdUsuarioActual);
                comando.ExecuteNonQuery();
            }

            Console.WriteLine("🔓 Sesión iniciada.");
        }

        private static void CerrarSesion()
        {
            string cerrarSesion = "UPDATE usuarios SET sesion_activa = FALSE WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(cerrarSesion, conexion))
            {
                comando.Parameters.AddWithValue("@idUsuario", UsuarioSesion.IdUsuarioActual);
                comando.ExecuteNonQuery();
            }

            Console.WriteLine("🔒 Sesión cerrada automáticamente.");
        }
    }
}
