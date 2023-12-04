using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Conexion2
    {
        private string cadenaConexion = "Server=tiusr23pl.cuc-carrera-ti.ac.cr.\\MSSQLSERVER2019;Database=ProyectoGrupo5;User Id=Grupo5_Dreamplace;Password=grupo512345;";

        // Asegúrate de usar async/await si tus métodos son asíncronos
        public DataTable ObtenerInmueblesBaratos()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("ObtenerInmuebleBaratos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
