using CapaNegocio.Models;
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

        public DataTable ObtenerDenunciasPorCedula2(string cedula)
        {
            DataTable dtDenuncias = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerDenunciasPorCedula", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Añadir el parámetro de cédula al comando
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dtDenuncias.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones según tus necesidades
                        Console.WriteLine("Error al obtener las denuncias: " + ex.Message);
                    }
                }
            }

            return dtDenuncias;
        }

        public List<Denuncia> ObtenerDenunciasPorCedula(string cedula)
        {
            List<Denuncia> denuncias = new List<Denuncia>();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT D.Denuncia, I.Nombre FROM Denuncias D INNER JOIN Inmuebles I ON D.IdReserva = I.IdReserva WHERE D.IdCedula = @Cedula;", connection))
                {
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Denuncia denuncia = new Denuncia
                                {
                                    ContenidoDenuncia = reader["Denuncia"].ToString(),
                                    NombreInmueble = reader["Nombre"].ToString()
                                };

                                denuncias.Add(denuncia);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener las denuncias: " + ex.Message);
                    }
                }
            }

            return denuncias;
        }

    }
}
