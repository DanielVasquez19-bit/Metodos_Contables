using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Agro_UES.Formularios.FormLogin
{
    public static class SesionHelper
    {
        public static void MarcarSesionInactiva(int idUsuario)
        {
            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                const string sql = @"
                  UPDATE usuarios
                     SET sesion = 'inactiva'
                   WHERE id_usuario = @uid;";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", idUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
