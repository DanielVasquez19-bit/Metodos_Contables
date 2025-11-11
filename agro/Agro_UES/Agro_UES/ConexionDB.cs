using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Agro_UES
{
    internal class ConexionDB
    {
        public static MySqlConnection Conexion()//esta es la clase que el ingeniero crea
        {
            // Parametros de conexion
            string servidor = "127.0.0.1"; // Direccion del servidor
            string database = "agro_ues"; // Nombre de la base de datos
            string usuario = "root"; // Usuario de MySQL(no hay que cambiar este nomber si no no accesde XD)
            string clave = ""; // Clave de acceso (por si le ponemos clave aqui hay que meterlo)

            // Construccion de la cadena de conexion
            string cadenaConexion = $"Database={database}; Data Source={servidor}; User Id={usuario}; password={clave};";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}