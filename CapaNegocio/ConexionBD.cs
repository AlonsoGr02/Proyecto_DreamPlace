﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CapaNegocio.Models;
namespace CapaNegocio
{
    public class ConexionBD
    {
        private string cadenaConexion = "Server=tiusr23pl.cuc-carrera-ti.ac.cr.\\MSSQLSERVER2019;Database=ProyectoGrupo5;User Id=Grupo5_Dreamplace;Password=grupo512345;";
        public static string cadenaCon = "Server=tiusr23pl.cuc-carrera-ti.ac.cr.\\MSSQLSERVER2019;Database=ProyectoGrupo5;User Id=Grupo5_Dreamplace;Password=grupo512345;";

        #region Estadisticas DREAMPLACE
        public int ContarHuespedes()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM usuarios WHERE IdRol = 1", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int ContarAnfitriones()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM usuarios WHERE IdRol = 2", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int ContarInmuebles()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Inmuebles", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int ContarCategorias()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Categorias", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int ContarRoles()
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Roles", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        #endregion



        //Inicio
        public DataTable ObtenerInmueblesPorNombre(string nombreInmueble)
        {
            DataTable dtInmuebles = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("FiltrarInmueblesPorNombre", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@NombreInmueble", nombreInmueble);

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(dtInmuebles);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener inmuebles por nombre: {ex.Message}");
            }

            return dtInmuebles;
        }
        //METODOS INMUEBLES
        public void InsertarUbicacion(string provincia, string canton, string posicionGPS)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("InsertarUbicacion", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Provincia", provincia);
                    cmd.Parameters.AddWithValue("@Canton", canton);
                    cmd.Parameters.AddWithValue("@PosicionGPS", posicionGPS);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerDatosHuesped(string IdCedula)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT r.IdReserva, r.IdCedula, r.IdInmueble, r.CantidadHuespedes, 
                                   r.IdNTarjeta, r.IdFechaReservada, i.Nombre AS NombreHuesped, i.Apellidos AS ApellidosHuesped,
                                   inm.Nombre AS NombreInmueble, inm.Descripcion AS DescripcionInmueble,
                                   inm.CantidadPersonas, inm.CantidadDormitorios, inm.CantidadBanos, inm.CantidadCamas, f.FechaI, f.FechaF
                            FROM Reservas r
                            INNER JOIN Inmuebles inm ON r.IdInmueble = inm.IdInmueble
                            INNER JOIN Identificaciones i ON r.IdCedula = i.IdCedula
                            INNER JOIN FechasReservadas f ON r.IdFechaReservada = f.IdFechaReservada
                            WHERE inm.IdCedula = @IdCedula
                            AND NOT EXISTS (
                                SELECT 1
                                FROM CalificacionHuesped ch
                                WHERE ch.IdReserva = r.IdReserva)";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }



        public DataTable ObtenerInmuebles(string IdCedula)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT *
                                FROM Inmuebles
                                WHERE IdCedula = @IdCedula";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }

        public int ObtenerUltimoIdInmueble()
        {
            int idInmueble = 0;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 IdInmueble FROM Inmuebles ORDER BY IdInmueble DESC", connection))
                {
                    connection.Open();

                    // Ejecuta la consulta y obtiene el valor
                    object result = cmd.ExecuteScalar();

                    // Verifica si el resultado no es nulo y es convertible a int
                    if (result != null && int.TryParse(result.ToString(), out idInmueble))
                    {
                        return idInmueble;
                    }
                }
            }

            // Si no se encuentra el IdInmueble, se devuelve 0
            return idInmueble;
        }

        public void InsertarInmuebleCategoria(int idCategoria, string idCedula, int idEstado, int idDescuento)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Inmuebles (IdCategoria, IdCedula, IdEstado, IdDescuento) " +
                    "VALUES (@IdCategoria, @IdCedula, @IdEstado, @IdDescuento);", connection))
                {
                    // Agrega los parámetros con sus respectivos valores
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdCedula", idCedula);
                    cmd.Parameters.AddWithValue("@IdEstado", idEstado);
                    cmd.Parameters.AddWithValue("@IdDescuento", idDescuento);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarTipoInmueble(int idInmueble, string tipoInmueble)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Inmuebles SET TipoInmueble = @TipoInmueble WHERE IdInmueble = @IdInmueble;", connection))
                {
                    // Agrega los parámetros con sus respectivos valores
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@TipoInmueble", tipoInmueble);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarDetallesInmueble(int idInmueble, int cantidadPersonas, int cantidadDormitorios, int cantidadBanos, int cantidadCamas, int nuevaIdUbicacion)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Inmuebles SET " +
                    "CantidadPersonas = @CantidadPersonas, " +
                    "CantidadDormitorios = @CantidadDormitorios, " +
                    "CantidadBanos = @CantidadBanos, " +
                    "CantidadCamas = @CantidadCamas, " +
                    "IdUbicacion = @NuevaIdUbicacion " +
                    "WHERE IdInmueble = @IdInmueble;", connection))
                {
                    // Agrega los parámetros con sus respectivos valores
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@CantidadPersonas", cantidadPersonas);
                    cmd.Parameters.AddWithValue("@CantidadDormitorios", cantidadDormitorios);
                    cmd.Parameters.AddWithValue("@CantidadBanos", cantidadBanos);
                    cmd.Parameters.AddWithValue("@CantidadCamas", cantidadCamas);
                    cmd.Parameters.AddWithValue("@NuevaIdUbicacion", nuevaIdUbicacion);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarNombreInmueble(int idInmueble, string nuevoNombre)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Inmuebles SET Nombre = @NuevoNombre WHERE IdInmueble = @IdInmueble;", connection))
                {
                    // Agrega los parámetros con sus respectivos valores
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarDescripcionInmueble(int idInmueble, string nuevaDescripcion)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Inmuebles SET Descripcion = @NuevaDescripcion WHERE IdInmueble = @IdInmueble;", connection))
                {
                    // Agrega los parámetros con sus respectivos valores
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@NuevaDescripcion", nuevaDescripcion);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ObtenerUltimoIdUbicacion()
        {
            int idUbicacion = 0;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 IdUbicacion FROM Ubicaciones ORDER BY IdUbicacion DESC", connection))
                {
                    connection.Open();

                    var result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        idUbicacion = Convert.ToInt32(result);
                    }
                }
            }

            return idUbicacion;
        }

        public DataTable ObtenerInfoInmueblesMain()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaConexion))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ObtenerInfoInmueblesMain", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener información de inmuebles: {ex.Message}");
            }

            return dataTable;
        }
        public static string ObtenerNumTarjetaPorCedula(string IdCedula)
        {
            string obtenerIdCedulaQuery = "SELECT IdNTarjeta FROM MetodosPago WHERE IdCedula = @IdCedula";
            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerIdCedulaCommand = new SqlCommand(obtenerIdCedulaQuery, connection))
                {
                    obtenerIdCedulaCommand.Parameters.AddWithValue("@IdCedula", IdCedula);

                    object idCedulaObject = obtenerIdCedulaCommand.ExecuteScalar();

                    return idCedulaObject?.ToString();
                }
            }
        }
        public List<byte[]> ObtenerImagenesInmueble(int idInmueble)
        {
            List<byte[]> imagenes = new List<byte[]>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    string consulta = "SELECT Imagen FROM ImagenInmueble WHERE IdInmueble = @IdInmueble";
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdInmueble", idInmueble);

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                if (!lector.IsDBNull(0))
                                {
                                    byte[] imagenBytes = (byte[])lector["Imagen"];
                                    imagenes.Add(imagenBytes);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener imágenes: {ex.Message}");
            }

            return imagenes;
        }

        public DataTable ObtenerInmueblesPorCategoria(int idCategoria)
        {
            DataTable dtInmuebles = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand("FiltrarInmueblesPorCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;

                        // Agregar el parámetro de la categoría
                        comando.Parameters.AddWithValue("@IdCategoria", idCategoria);

                        // Crear un adaptador de datos para ejecutar el procedimiento almacenado
                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            // Llenar el DataTable con los resultados
                            adaptador.Fill(dtInmuebles);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine($"Error al obtener inmuebles por categoría: {ex.Message}");
            }

            return dtInmuebles;
        }

        #region Metodos de Pago
        public static void InsertarMetodoPago(string correo, string numeroTarjeta, string cvv, DateTime fechaVencimiento)
        {
            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                string obtenerCedulaQuery = "SELECT IdCedula FROM Usuarios WHERE Correo = @Correo";

                connection.Open();
                using (SqlCommand obtenerCedulaCommand = new SqlCommand(obtenerCedulaQuery, connection))
                {
                    obtenerCedulaCommand.Parameters.AddWithValue("@Correo", correo);


                    object idCedulaObject = obtenerCedulaCommand.ExecuteScalar();

                    if (idCedulaObject != null)
                    {

                        string idCedula = idCedulaObject.ToString();

                        string insertarMetodoPagoQuery = "INSERT INTO MetodosPago (IdNTarjeta, CVV, FechaV, IdCedula) " +
                                                         "VALUES (@IdNTarjeta, @CVV, @FechaV, @IdCedula)";

                        using (SqlCommand command = new SqlCommand(insertarMetodoPagoQuery, connection))
                        {

                            command.Parameters.AddWithValue("@IdNTarjeta", numeroTarjeta);
                            command.Parameters.AddWithValue("@CVV", cvv);
                            command.Parameters.AddWithValue("@FechaV", fechaVencimiento);
                            command.Parameters.AddWithValue("@IdCedula", idCedula);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception("No se encontró una coincidencia en la tabla Usuarios para el correo proporcionado.");
                    }
                }
            }
        }
        public void InsertarDatosMiBanco(int numeroCuenta, string cedula, int numeroTarjeta)
        {
            // Definir la consulta SQL con parámetros
            string consulta = "INSERT INTO MiBanco(IdNumeroCuenta, IdCedula, IdNTarjeta) " +
                              "VALUES(@NCuenta, @Cedula, @NTarjeta)";

            // Crear la conexión a la base de datos
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Crear el comando SQL con la consulta y la conexión
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    // Agregar parámetros a la consulta
                    comando.Parameters.AddWithValue("@NCuenta", numeroCuenta);
                    comando.Parameters.AddWithValue("@Cedula", cedula);
                    comando.Parameters.AddWithValue("@NTarjeta", numeroTarjeta);

                    try
                    {
                        // Abrir la conexión
                        conexion.Open();

                        // Ejecutar la consulta
                        comando.ExecuteNonQuery();

                        Console.WriteLine("Datos insertados correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al insertar datos: " + ex.Message);
                    }
                }
            }
        }



        #endregion


        public int ObtenerIdInmueblePorNombre(string Nombre)
        {
            int Inmueble = 0;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT IdInmueble FROM Inmuebles WHERE Nombre = @Nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", Nombre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtiene el valor de la columna IdInmueble
                            Inmueble = Convert.ToInt32(reader["IdInmueble"]);
                        }
                    }
                }
            }

            return Inmueble;
        }

        public static string ObtenerIdCedulaPorCorreo(string correo)
        {
            string obtenerIdCedulaQuery = "SELECT IdCedula FROM Usuarios WHERE Correo = @Correo";
            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerIdCedulaCommand = new SqlCommand(obtenerIdCedulaQuery, connection))
                {
                    obtenerIdCedulaCommand.Parameters.AddWithValue("@Correo", correo);

                    object idCedulaObject = obtenerIdCedulaCommand.ExecuteScalar();

                    return idCedulaObject?.ToString();
                }
            }
        }



        public static string ObtenerIdCedulayNumTarjetaPorCorreo(string correo)
        {
            string obtenerIdCedulaQuery = "SELECT IdNTarjeta FROM MetodosPago WHERE IdCedula = @IdCedula";
            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerIdCedulaCommand = new SqlCommand(obtenerIdCedulaQuery, connection))
                {
                    obtenerIdCedulaCommand.Parameters.AddWithValue("@Correo", correo);

                    object idCedulaObject = obtenerIdCedulaCommand.ExecuteScalar();

                    return idCedulaObject?.ToString();
                }
            }
        }

        public static string[] ObtenerNombreYApellidoPorIdCedula(string idCedula)
        {
            string obtenerNombreYApellidoQuery = "SELECT Nombre, Apellidos FROM Identificaciones WHERE IdCedula = @IdCedula";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerNombreYApellidoCommand = new SqlCommand(obtenerNombreYApellidoQuery, connection))
                {
                    obtenerNombreYApellidoCommand.Parameters.AddWithValue("@IdCedula", idCedula);

                    SqlDataReader reader = obtenerNombreYApellidoCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string nombre = reader["Nombre"].ToString();
                        string apellido = reader["Apellidos"].ToString();

                        return new string[] { nombre, apellido };
                    }

                    return new string[] { "", "" };
                }
            }
        }

        public MiBanco ObtenerInfoMiBancoPorCorreo(string cedula)
        {
            MiBanco infoMiBanco = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"SELECT i.IdCedula, CONCAT(i.Nombre, ' ', i.Apellidos) as NombreCompleto, m.IdNTarjeta, m.Saldo
                         FROM MiBanco m
                         JOIN Identificaciones i ON i.IdCedula = m.IdCedula
                         WHERE i.IdCedula = @Cedula";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            infoMiBanco = new MiBanco
                            {
                                IdCedula = reader["IdCedula"].ToString(),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                IdNTarjeta = reader["IdNTarjeta"].ToString(),
                                Saldo = Convert.ToDecimal(reader["Saldo"])
                            };
                        }
                    }
                }
            }

            return infoMiBanco;
        }


        public DataTable ObtenerCategoriasActivas()
        {
            DataTable result = new DataTable();
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_ObtenerCategoriasActivas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener categorías: " + ex.Message);
                }
            }
            return result;
        }
        public static string[] ObtenerDatosInmueblePorIdInmueble(int idInmueble)
        {
            string obtenerDatosInmuebleQuery = @"
        SELECT i.Nombre, i.Descripcion, i.CantidadPersonas, i.CantidadDormitorios,
               i.CantidadBanos, i.CantidadCamas, i.IdUbicacion, i.IdCategoria,
               i.TipoInmueble, d.Porcentaje
        FROM Inmuebles i
        JOIN Descuentos d ON d.IdDescuentos = i.IdDescuento
        WHERE IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerDatosInmuebleCommand = new SqlCommand(obtenerDatosInmuebleQuery, connection))
                {
                    obtenerDatosInmuebleCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    SqlDataReader reader = obtenerDatosInmuebleCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        string nombre = reader["Nombre"].ToString();
                        string descripcion = reader["Descripcion"].ToString();
                        string cantidadPersonas = reader["CantidadPersonas"].ToString();
                        string cantidadDormitorios = reader["CantidadDormitorios"].ToString();
                        string cantidadBanos = reader["CantidadBanos"].ToString();
                        string cantidadCamas = reader["CantidadCamas"].ToString();
                        string idUbicacion = reader["IdUbicacion"].ToString();
                        string idCategoria = reader["IdCategoria"].ToString();
                        string tipoInmueble = reader["TipoInmueble"].ToString();
                        string porcentajeDescuento = reader["Porcentaje"].ToString();

                        return new string[] { nombre, descripcion, cantidadPersonas, cantidadDormitorios, cantidadBanos, cantidadCamas, idUbicacion, idCategoria, tipoInmueble, porcentajeDescuento };
                    }

                    return new string[] { "", "", "", "", "", "", "", "", "", "" };
                }
            }
        }

        public void InsertarTotal(decimal precio, decimal iva, decimal total, int idInmueble)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarTotal", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = precio;
                        cmd.Parameters.Add("@IVA", SqlDbType.Decimal).Value = iva;
                        cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = total;
                        cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = idInmueble;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarIdentificacion(string idCedula, string nombre, string apellidos, DateTime fechaNac, string telefono) //, byte[] imagenFrontal, byte[] imagenTrasera
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarIdentificacion", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@IdCedula", SqlDbType.Char, 20).Value = idCedula;
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nombre;
                        cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = apellidos;
                        cmd.Parameters.Add("@FechaNac", SqlDbType.Date).Value = fechaNac;
                        cmd.Parameters.Add("@Telefono", SqlDbType.VarChar, 20).Value = telefono;
                        //cmd.Parameters.Add("@IFrontal", SqlDbType.VarBinary, -1).Value = imagenFrontal; // -1 indica el tamaño máximo
                        //cmd.Parameters.Add("@ITrasera", SqlDbType.VarBinary, -1).Value = imagenTrasera;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar la excepción según tus necesidades
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarImagenInmueble(byte[] imagen, int idInmueble)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarImagenInmueble", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@Imagen", SqlDbType.VarBinary, -1).Value = imagen; // -1 indica el tamaño máximo
                        cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = idInmueble;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarInmueble(string nombre, string descripcion, int cantidadPersonas, int cantidadDormitorios, int cantidadBanos, int cantidadCamas, int idUbicacion, int idCategoria, int idDescuento, string idCedula, int idEstado, string descripcionEstado)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarInmueble", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 255).Value = nombre;
                        cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar, 500).Value = descripcion;
                        cmd.Parameters.Add("@CantidadPersonas", SqlDbType.Int).Value = cantidadPersonas;
                        cmd.Parameters.Add("@CantidadDormitorios", SqlDbType.Int).Value = cantidadDormitorios;
                        cmd.Parameters.Add("@CantidadBanos", SqlDbType.Int).Value = cantidadBanos;
                        cmd.Parameters.Add("@CantidadCamas", SqlDbType.Int).Value = cantidadCamas;
                        cmd.Parameters.Add("@IdUbicacion", SqlDbType.Int).Value = idUbicacion;
                        cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategoria;
                        cmd.Parameters.Add("@IdDescuento", SqlDbType.Int).Value = idDescuento;
                        cmd.Parameters.Add("@IdCedula", SqlDbType.Char, 20).Value = idCedula;
                        cmd.Parameters.Add("@IdEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@DescripcionEstado", SqlDbType.VarChar, 500).Value = descripcionEstado;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarServicio(string nombre, int idInmueble)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarServicio", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 255).Value = nombre;
                        cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = idInmueble;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarServicioInmueble(int idServiciosAlojamientos, int idInmueble)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarServicioInmueble", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@IdServiciosAlojamientos", SqlDbType.Int).Value = idServiciosAlojamientos;
                        cmd.Parameters.Add("@IdInmueble", SqlDbType.Int).Value = idInmueble;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void InsertarUsuario(string correo, string clave, int idRol, string idCedula, string contrasena)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100).Value = correo;
                        cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = clave;
                        cmd.Parameters.Add("@IdRol", SqlDbType.Int).Value = idRol;
                        cmd.Parameters.Add("@IdCedula", SqlDbType.Char, 20).Value = idCedula;
                        cmd.Parameters.Add("@Contrasena", SqlDbType.VarChar, 50).Value = contrasena;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void ActualizarClaveUsuario(string nuevaClave, string correo)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("ActualizarClaveUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros al comando
                    comando.Parameters.AddWithValue("@NuevaClave", nuevaClave);
                    comando.Parameters.AddWithValue("@Correo", correo);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Maneja la excepción según tus necesidades (por ejemplo, loguea el error)
                        Console.WriteLine("Error al ejecutar el stored procedure: " + ex.Message);
                    }
                }
            }
        }

        public bool ExisteCorreo(string correo)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo", conexion))
                {
                    comando.Parameters.AddWithValue("@Correo", correo);

                    try
                    {
                        conexion.Open();
                        int count = Convert.ToInt32(comando.ExecuteScalar());
                        return count > 0; // Si count es mayor a 0, el correo existe en la base de datos
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine("Error al verificar correo: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool ValidarCodigoL(string correo, string codigo)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo AND Clave = @Codigo", conexion))
                {
                    comando.Parameters.AddWithValue("@Correo", correo);
                    comando.Parameters.AddWithValue("@Codigo", codigo);

                    try
                    {
                        conexion.Open();
                        int count = Convert.ToInt32(comando.ExecuteScalar());
                        return count > 0; // Si count es mayor a 0, el código coincide en la base de datos
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine("Error al verificar código: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool ValidarLogin(string correo, string contrasena)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo AND Contrasena = @Contrasena", conexion))
                {
                    comando.Parameters.AddWithValue("@Correo", correo);
                    comando.Parameters.AddWithValue("@Contrasena", contrasena);

                    try
                    {
                        conexion.Open();
                        int count = Convert.ToInt32(comando.ExecuteScalar());
                        return count > 0; // Si count es mayor a 0, el código coincide en la base de datos
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine("Error al verificar código: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public DataTable ObtenerTodosLosServicios()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand("ObtenerTodosLosServicios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener servicios: " + ex.Message);
                    }
                }
            }

            return dataTable;
        }

        public int ObtenerIdRol(string correo)
        {
            int idRol = 0; // Valor predeterminado o algún valor que indique que el rol no se encontró

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();


                string query = "SELECT IdRol FROM Usuarios WHERE Correo = @Correo";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);


                    var result = cmd.ExecuteScalar();


                    if (result != null)
                    {
                        idRol = Convert.ToInt32(result);
                    }
                }
            }

            return idRol;
        }

        public Usuario ObtenerDatosUsuario(string correo)
        {
            Usuario usuario = null;

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"SELECT i.IdCedula, i.Nombre, i.Apellidos, i.FechaNac, i.Telefono, u.Correo, u.Clave, r.Nombre as Rol
                        FROM Usuarios u
                        JOIN Identificaciones i ON i.IdCedula = u.IdCedula
                        JOIN Roles r ON r.IDRol = u.IdRol
                        WHERE u.Correo = @Correo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                Cedula = reader["IdCedula"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                FechaNac = (DateTime)reader["FechaNac"],
                                Correo = reader["Correo"].ToString(),
                                Clave = reader["Clave"].ToString(),
                                Rol = reader["Rol"].ToString()
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        #region Descuentos
        public void CargarDescuentos(DropDownList ddlDescuentos)
        {
            string query = "SELECT IdDescuentos, Porcentaje FROM Descuentos"; // Seleccionar también el IdDescuento

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ddlDescuentos.Items.Clear();

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            int idDescuento = Convert.ToInt32(reader["IdDescuentos"]); // Obtener el IdDescuento
                            string porcentaje = reader["Porcentaje"].ToString();
                            ddlDescuentos.Items.Add(new ListItem(porcentaje, idDescuento.ToString())); // Agregar ListItem con valor entero
                        }
                    }
                }
            }
        }


        public Inmueble ObtenerDetallesAlojamientosConDescuento(string ddlAlojamientos)
        {
            Inmueble detalles = null;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DatosInmuebleDescuento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", ddlAlojamientos);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalles = new Inmueble
                            {
                                IdInmueble = Convert.ToInt32(reader["IdInmueble"]),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Total = GetSafeDecimalD(reader, "Total")
                            };
                        }
                    }
                }
            }

            return detalles;
        }

        private decimal GetSafeDecimalD(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToDecimal(reader[columnName]) : 0m;
        }

        public void ActualizarDescuentoEnInmueble(int idInmueble, int idDescuento)
        {
            string query = "UPDATE Inmuebles SET IdDescuento = @IdDescuento WHERE IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    command.Parameters.AddWithValue("@IdDescuento", idDescuento);

                    command.ExecuteNonQuery();
                }
            }
        }




        #endregion


        #region Mantenimiento Alojamientos
        public static void ActualizarInmueble(int idInmueble, string nombre, string descripcion, int personas, int dormitorios, int banos, int camas, int idCategoria, int idEstado)
        {
            string actualizarInmuebleQuery = @"
                                                UPDATE Inmuebles
                                                SET Nombre = @Nombre,
                                                    Descripcion = @Descripcion,
                                                    CantidadPersonas = @Personas,
                                                    CantidadDormitorios = @Dormitorios,
                                                    CantidadBanos = @Banos,
                                                    CantidadCamas = @Camas,
                                                    IdCategoria = @Categoria,
                                                    IdEstado = @Estado
                                                WHERE IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(actualizarInmuebleQuery, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@Personas", personas);
                    command.Parameters.AddWithValue("@Dormitorios", dormitorios);
                    command.Parameters.AddWithValue("@Banos", banos);
                    command.Parameters.AddWithValue("@Camas", camas);
                    command.Parameters.AddWithValue("@Categoria", idCategoria);
                    command.Parameters.AddWithValue("@Estado", idEstado);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Inmueble ObtenerDetallesAlojamientos(string ddlAlojamientos)
        {
            Inmueble detalles = null;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DatosInmueblePersonal", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nombre", ddlAlojamientos);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detalles = new Inmueble
                            {
                                IdInmueble = GetSafeInt(reader, "IdInmueble"),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                CantidadPersonas = GetSafeInt(reader, "CantidadPersonas"),
                                CantidadDormitorios = GetSafeInt(reader, "CantidadDormitorios"),
                                CantidadBanos = GetSafeInt(reader, "CantidadBanos"),
                                CantidadCamas = GetSafeInt(reader, "CantidadCamas"),
                                IdCategoria = GetSafeInt(reader, "Categoria"),
                                IdEstado = GetSafeInt(reader, "NombreEstado"),
                                DescripcionEstado = reader["DescripcionEstado"].ToString(),
                                Imagen = reader["Imagen"] as byte[]
                            };
                        }
                    }
                }
            }

            return detalles;
        }
        private int GetSafeInt(SqlDataReader reader, string columnName)
        {
            int result;
            return int.TryParse(reader[columnName].ToString(), out result) ? result : 0;
        }

        public void CargarNombresInmuebles(DropDownList ddlAlojamientos, string correoUsuario)
        {
            // Obtener los nombres de los alojamientos
            string query = "SELECT IdInmueble,Nombre " +
                           "FROM Inmuebles i " +
                           "JOIN Usuarios u ON u.IdCedula = i.IdCedula " +
                           "WHERE u.Correo = @Correo";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Correo", correoUsuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Agrega la opción inicial
                        ddlAlojamientos.Items.Add(new ListItem("Selecciona un alojamiento", string.Empty));

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            ddlAlojamientos.DataValueField = "IdInmueble";
                            string nombreAlojamiento = reader["Nombre"].ToString();
                            ddlAlojamientos.Items.Add(nombreAlojamiento);
                        }
                    }
                }
            }
        }

        public void CargarCategoriasInmueble(DropDownList ddlCategorias, string nombreInmueble)
        {
            string query = @"
                   SELECT c.IdCategoria, c.Categoria
                     FROM Categorias c
                     LEFT JOIN Inmuebles i ON c.IdCategoria = i.IdCategoria
                     LEFT JOIN Usuarios u ON u.IdCedula = i.IdCedula
                       WHERE i.Nombre = @Nombre;";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreInmueble);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ddlCategorias.Items.Clear(); // Limpiar las opciones existentes

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            string categoria = reader["Categoria"].ToString();
                            ddlCategorias.Items.Add(categoria);
                        }
                    }
                }
            }
        }


        public void CargarCategorias(DropDownList ddlCategoria)
        {
            string query = "SELECT DISTINCT IdCategoria, Categoria FROM Categorias";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ddlCategoria.Items.Clear(); // Limpiar las opciones existentes

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            string categoria = reader["Categoria"].ToString();
                            ddlCategoria.Items.Add(categoria);
                        }
                    }
                }
            }
        }

        public void CargarEstadosInmueble(DropDownList ddlEstado, string nombreInmueble)
        {
            string query = @"
                   SELECT DISTINCT e.IdEstado, e.NombreEstado
                        FROM Estados e
                        LEFT JOIN Inmuebles i ON e.IdEstado = i.IdEstado
                        LEFT JOIN Usuarios u ON u.IdCedula = i.IdCedula
                        WHERE i.Nombre = @Nombre;";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombreInmueble);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ddlEstado.Items.Clear(); // Limpiar las opciones existentes

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            string categoria = reader["NombreEstado"].ToString();
                            ddlEstado.Items.Add(categoria);
                        }
                    }
                }
            }
        }


        public void CargarEstados(DropDownList ddlEstados)
        {
            string query = "SELECT  distinct IdEstado, NombreEstado FROM Estados";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ddlEstados.Items.Clear(); // Limpiar las opciones existentes

                        // Verificar si hay filas devueltas
                        while (reader.Read())
                        {
                            string categoria = reader["NombreEstado"].ToString();
                            ddlEstados.Items.Add(categoria);
                        }
                    }
                }
            }
        }


        #endregion





        public void ActualizarUsuario(string cedula, string nombre, string apellidos, string telefono)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"UPDATE Identificaciones
                             SET Nombre = @Nombre,
                                 Apellidos = @Apellidos,
                                 Telefono = @Telefono
                             WHERE IdCedula = @Cedula";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerNotificacionesPorCorreo(string correo)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT n.Notificacion FROM Notificaciones n " +
                                   "LEFT JOIN Identificaciones i ON n.IdCedula = i.IdCedula " +
                                   "LEFT JOIN Usuarios u ON i.IdCedula = u.IdCedula " +
                                   "WHERE u.Correo = @Correo OR u.IdCedula IS NULL";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Correo", correo);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener notificaciones: " + ex.Message);
                }
            }

            return result;
        }
        public string ObtenerIdCedulaPorNombre(string Apellidos)
        {
            string IdCedula = null;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT IdCedula FROM Identificaciones WHERE (Apellidos) = @Apellidos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar el parámetro de nombre completo
                    command.Parameters.AddWithValue("@Apellidos", Apellidos);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtener el IdCedula
                            IdCedula = reader["IdCedula"].ToString();
                        }
                    }
                }
            }

            return IdCedula;
        }

        public string ObtenerIdCedulaPorN(string Nombre)
        {
            string IdCedula = null;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT IdCedula FROM Identificaciones WHERE (Nombre) = @Nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar el parámetro de nombre completo
                    command.Parameters.AddWithValue("@Nombre", Nombre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtener el IdCedula
                            IdCedula = reader["IdCedula"].ToString();
                        }
                    }
                }
            }

            return IdCedula;
        }

        public int ObtenerInmueble(string IdCedula)
        {
            int IdInmueble = 0;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT IdInmueble FROM Inmuebles WHERE IdCedula = @IdCedula";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar el parámetro IdCedula
                    command.Parameters.AddWithValue("@IdCedula", IdCedula);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si el valor no es nulo antes de convertir
                            if (!reader.IsDBNull(reader.GetOrdinal("IdInmueble")))
                            {
                                // Obtener el IdInmueble como entero
                                IdInmueble = reader.GetInt32(reader.GetOrdinal("IdInmueble"));
                            }
                        }
                    }
                }
            }

            return IdInmueble;
        }

        public string ObtenerIdCedulaHuesped(int IdInmueble)
        {
            string IdCedulaHuesped = null;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT IdCedula FROM Reservas WHERE IdInmueble = @IdInmueble";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", IdInmueble);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Verificar si el valor no es nulo antes de obtenerlo
                            if (!reader.IsDBNull(reader.GetOrdinal("IdCedula")))
                            {
                                // Obtener el IdCedula como cadena de caracteres
                                IdCedulaHuesped = reader.GetString(reader.GetOrdinal("IdCedula"));
                            }
                        }
                    }
                }
            }

            return IdCedulaHuesped;
        }


        public List<string> ObtenerNombreHuesped(string IdCedula)
        {
            List<string> nombresHuesped = new List<string>();

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT Nombre FROM Identificaciones WHERE IdCedula = @IdCedula";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar el parámetro IdCedula
                    command.Parameters.AddWithValue("@IdCedula", IdCedula);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Verificar si el valor no es nulo antes de obtenerlo
                            if (!reader.IsDBNull(reader.GetOrdinal("Nombre")))
                            {
                                // Obtener el nombre del huésped y agregarlo a la lista
                                string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                                nombresHuesped.Add(nombre);
                            }
                        }
                    }
                }
            }

            return nombresHuesped;
        }


        public List<string> Obtener_Info_Hespedes_Reservaron(string IdCedula)
        {
            List<string> nombresAnfitriones = new List<string>();

            string queryInmueble = "SELECT IdInmueble FROM Inmuebles WHERE IdCedula = @IdCedula";
            string queryCedulaHuesped = "SELECT IdCedula AS IdCedulaHuesped FROM Reservas WHERE IdInmueble = @IdInmueble";
            string queryIdentificaciones = "SELECT Nombre, Apellidos FROM Identificaciones WHERE IdCedula = @IdCedula";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                // Obtener IdInmueble
                using (SqlCommand cmdInmueble = new SqlCommand(queryInmueble, connection))
                {
                    cmdInmueble.Parameters.AddWithValue("@IdCedula", IdCedula);
                    string idInmueble = "";

                    try
                    {
                        connection.Open();
                        SqlDataReader readerInmueble = cmdInmueble.ExecuteReader();

                        if (readerInmueble.Read())
                        {
                            idInmueble = readerInmueble["IdInmueble"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción
                    }

                    // Si se encontró el IdInmueble, proceder con la consulta de IdCedulaHuesped
                    if (!string.IsNullOrEmpty(idInmueble))
                    {
                        using (SqlCommand cmdCedulaHuesped = new SqlCommand(queryCedulaHuesped, connection))
                        {
                            cmdCedulaHuesped.Parameters.AddWithValue("@IdInmueble", idInmueble);

                            try
                            {
                                SqlDataReader readerCedulaHuesped = cmdCedulaHuesped.ExecuteReader();

                                if (readerCedulaHuesped.Read())
                                {
                                    string idCedulaHuesped = readerCedulaHuesped["IdCedulaHuesped"].ToString();

                                    // Consulta de Identificaciones con la IdCedulaHuesped obtenida
                                    using (SqlCommand cmdIdentificaciones = new SqlCommand(queryIdentificaciones, connection))
                                    {
                                        cmdIdentificaciones.Parameters.AddWithValue("@IdCedulaHuesped", idCedulaHuesped);

                                        try
                                        {
                                            SqlDataReader readerIdentificaciones = cmdIdentificaciones.ExecuteReader();

                                            while (readerIdentificaciones.Read())
                                            {                                                
                                                string nombreAnfitrion = readerIdentificaciones["Nombre"].ToString();
                                                nombresAnfitriones.Add(nombreAnfitrion);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // Manejar la excepción
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Manejar la excepción
                            }
                        }
                    }
                }
            }

            return nombresAnfitriones;
        }



        public List<Mensajes> ObtenerMensajesEntreUsuarios(string IdCedula, string IdCedulaR)
        {
            List<Mensajes> mensajes = new List<Mensajes>();

            string query = @"
                            SELECT Fecha, Mensaje, IdCedula AS Remitente, IdCedulaR AS Destinatario
                            FROM Mensajes
                            WHERE (IdCedula = @IdCedula AND IdCedulaR = @IdCedulaR)
                            OR (IdCedula = @IdCedulaR AND IdCedulaR = @IdCedula)
                            ORDER BY Fecha ASC";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);
                    cmd.Parameters.AddWithValue("@IdCedulaR", IdCedulaR);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Mensajes mensaje = new Mensajes
                            {
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Mensaje = reader["Mensaje"].ToString(),
                                IdCedula = reader["Remitente"].ToString(),
                                IdCedulaR = reader["Destinatario"].ToString()
                            };

                            mensajes.Add(mensaje);
                        }
                    }
                    catch (Exception ex)
                    {
                        

                    }
                }
            }

            return mensajes;
        }

        public void Huespedes(DropDownList ddlhuesped, string IdCedula)
        {
            
            string query = " select Nombre, Apellidos From Identificaciones where IdCedula=@IdCedula";

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCedula", IdCedula);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        ddlhuesped.Items.Add(new ListItem("Selecciona un huesped", string.Empty));
                        
                        while (reader.Read())
                        {
                            string nombreAlojamiento = reader["Nombre"].ToString();
                            ddlhuesped.Items.Add(nombreAlojamiento);
                        }
                    }
                }
            }
        }


        public DataTable ObtenerNombre_IdCedula(string IdCedula)
        {
            DataTable datos = new DataTable();

            string consulta = @" select Nombre, Apellidos From Identificaciones where IdCedula=@IdCedula";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }


        public object ObtenerIdFechaReservada(int IdInmueble)
        {
            object IdCedula = null;

            string selectQuery = "select IdCedula From Reservas where IdInmueble=@IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdInmueble", IdInmueble);

                    try
                    {
                        connection.Open();
                        IdCedula = selectCommand.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error de SQL: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return IdCedula;
        }


        public DataTable ObtenerAnfitriones(string IdCedula)
        {
            DataTable result = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    string query = @"
                                        SELECT r.IdReserva, r.IdCedula, r.IdInmueble,
                                            i.IdCedula AS IdCedula,
                                            u.Nombre AS Nombre,
                                            u.Apellidos AS Apellidos,
                                            u.IdCedula AS IdCedula
                                        FROM Reservas r
                                        INNER JOIN Inmuebles i ON r.IdInmueble = i.IdInmueble
                                        INNER JOIN Identificaciones u ON i.IdCedula = u.IdCedula
                                        WHERE r.IdCedula = @IdCedula";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener notificaciones: " + ex.Message);
                }
            }

            return result;
        }



     
        public void CargarHuespedEnDropDownList(DropDownList ddlHuesped)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"SELECT CONCAT(i.Nombre, ' ', i.Apellidos) AS NombreCompleto
                         FROM Identificaciones i
                         JOIN Usuarios u ON i.IdCedula = u.IdCedula 
                         WHERE u.IdRol = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Agrega la opción inicial
                        ddlHuesped.Items.Add(new ListItem("Selecciona un huésped", string.Empty));

                        while (reader.Read())
                        {
                            string nombreCompleto = reader["NombreCompleto"].ToString();
                            ddlHuesped.Items.Add(nombreCompleto);
                        }
                    }
                }
            }
        }
        public void CargarAnfitrionesEnDropDownList(DropDownList ddlAnfitriones)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"SELECT CONCAT(i.Nombre, ' ', i.Apellidos) AS NombreCompleto
                         FROM Identificaciones i
                         JOIN Usuarios u ON i.IdCedula = u.IdCedula 
                         WHERE u.IdRol = 2";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Agrega la opción inicial
                        ddlAnfitriones.Items.Add(new ListItem("Selecciona un anfitrión", string.Empty));

                        while (reader.Read())
                        {
                            string nombreCompleto = reader["NombreCompleto"].ToString();
                            ddlAnfitriones.Items.Add(nombreCompleto);
                        }
                    }
                }
            }
        }
        //public void InsertarMensaje(string nombreCompleto, string mensaje, string correo)
        //{
        //    using (SqlConnection con = new SqlConnection(cadenaConexion))
        //    {
        //        con.Open();

        //        string obtenerIdCedulaQuery = @"SELECT i.IdCedula
        //                               FROM Identificaciones i
        //                               JOIN Usuarios u ON i.IdCedula = u.IdCedula
        //                               WHERE CONCAT(i.Nombre, ' ', i.Apellidos) = @NombreCompleto";


        //        string[] nombreApellidos = nombreCompleto.Split(' ');
        //        string nombre = nombreApellidos[0];
        //        string apellidos = nombreApellidos[1];

        //        using (SqlCommand obtenerIdCedulaCmd = new SqlCommand(obtenerIdCedulaQuery, con))
        //        {
        //            obtenerIdCedulaCmd.Parameters.AddWithValue("@Nombre", nombre);
        //            obtenerIdCedulaCmd.Parameters.AddWithValue("@Apellidos", apellidos);

        //            string idCedula = obtenerIdCedulaCmd.ExecuteScalar()?.ToString();

        //            if (idCedula != null)
        //            {
        //                string obtenerIdCedulaRQuery = @"SELECT m.IdCedulaR
        //                                     FROM Mensajes m
        //                                     JOIN Identificaciones i ON i.IdCedula = m.IdCedulaR
        //                                     JOIN Usuarios u ON u.IdCedula = m.IdCedulaR
        //                                     WHERE u.Correo = @Correo";

        //                using (SqlCommand obtenerIdCedulaRCmd = new SqlCommand(obtenerIdCedulaRQuery, con))
        //                {
        //                    obtenerIdCedulaRCmd.Parameters.AddWithValue("@Correo", correo);

        //                    string idCedulaR = obtenerIdCedulaRCmd.ExecuteScalar()?.ToString();

        //                    if (idCedulaR != null)
        //                    {
        //                        // Insertar el mensaje en la tabla Mensajes
        //                        string insertarMensajeQuery = @"INSERT INTO Mensajes (Mensaje, Fecha, IdCedula, IdCedulaR)
        //                                            VALUES (@Mensaje, GETDATE(), @IdCedula, @IdCedulaR)";

        //                        using (SqlCommand insertarMensajeCmd = new SqlCommand(insertarMensajeQuery, con))
        //                        {
        //                            insertarMensajeCmd.Parameters.AddWithValue("@Mensaje", mensaje);
        //                            insertarMensajeCmd.Parameters.AddWithValue("@IdCedula", idCedula);
        //                            insertarMensajeCmd.Parameters.AddWithValue("@IdCedulaR", idCedulaR);
        //                            insertarMensajeCmd.ExecuteNonQuery();
        //                        }
        //                    }
        //                    else
        //                    {

        //                    }
        //                }
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //}
        public void InsertarMensajeSP(string nombreCompleto, string mensaje, string correo)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("sp_InsertarMensaje", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    cmd.Parameters.AddWithValue("@Mensaje", mensaje);
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insert_Mensajes(string Mensaje, DateTime Fecha, string IdCedula, string @IdCedulaR)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Mensajes ( Mensaje, Fecha, IdCedula,IdCedulaR)"+
                        "VALUES( @Mensaje, @Fecha, @IdCedula, @IdCedulaR) ; ", connection))
                {
                    // Agrega los parámetros con sus respectivos valores                    
                    cmd.Parameters.AddWithValue("@Mensaje", Mensaje);
                    cmd.Parameters.AddWithValue("@Fecha", Fecha);
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);
                    cmd.Parameters.AddWithValue("@IdCedulaR", IdCedulaR);

                    connection.Open();

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public static string[] ObtenerPrecioInmueble(int idInmueble)
        {
            string obtenerDatosInmuebleQuery = "SELECT  Total FROM Total WHERE IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerDatosInmuebleCommand = new SqlCommand(obtenerDatosInmuebleQuery, connection))
                {
                    obtenerDatosInmuebleCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    SqlDataReader reader = obtenerDatosInmuebleCommand.ExecuteReader();

                    if (reader.Read())
                    {

                        string Total = reader["Total"].ToString();


                        return new string[] { Total };
                    }

                    return new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "" };
                }
            }
        }

        public static List<byte[]> ObtenerImagenesPorIdInmueble(int idInmueble)
        {
            List<byte[]> listaImagenes = new List<byte[]>();

            string obtenerImagenesQuery = "SELECT Imagen FROM ImagenInmueble WHERE IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerImagenesCommand = new SqlCommand(obtenerImagenesQuery, connection))
                {
                    obtenerImagenesCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    SqlDataReader reader = obtenerImagenesCommand.ExecuteReader();

                    while (reader.Read())
                    {

                        byte[] imagen = (byte[])reader["Imagen"];
                        listaImagenes.Add(imagen);
                    }
                }
            }

            return listaImagenes;
        }

        public static List<Servicio> ObtenerServicios(int idInmueble)
        {
            List<Servicio> listaServicios = new List<Servicio>();

            string obtenerImagenesQuery = "SELECT s.IdServiciosAlojamientos, s.Nombre, s.Icono FROM Servicios s INNER JOIN ServiciosInmuebles si ON s.IdServiciosAlojamientos = si.IdServiciosAlojamientos WHERE si.IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerImagenesCommand = new SqlCommand(obtenerImagenesQuery, connection))
                {
                    obtenerImagenesCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    SqlDataReader reader = obtenerImagenesCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        string nombre = reader["Nombre"].ToString();
                        byte[] imagen = (byte[])reader["Icono"];

                        listaServicios.Add(new Servicio
                        {
                            Nombre = nombre,
                            Icono = imagen
                        });
                    }
                }
            }

            return listaServicios;
        }


        public List<Mensajes> ObtenerMensajesPorCorreo(string correo)
        {
            List<Mensajes> mensajes = new List<Mensajes>();

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                con.Open();

                string query = @"SELECT m.Mensaje, m.Fecha
                         FROM Mensajes m
                         JOIN Identificaciones i ON i.IdCedula = m.IdCedula
                         JOIN Usuarios u ON i.IdCedula = u.IdCedula
                         WHERE u.correo = @Correo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Mensajes mensaje = new Mensajes
                            {
                                Mensaje = reader["Mensaje"].ToString(),
                                Fecha = Convert.ToDateTime(reader["Fecha"])
                                // Puedes agregar más propiedades si es necesario
                            };

                            mensajes.Add(mensaje);
                        }
                    }
                }
            }

            return mensajes;
        }


        public static void InsertarFechaReservada(DateTime fechaInicio, DateTime fechaFin, int idInmueble, string IdCedula)
        {
            string insertQuery = "INSERT INTO FechasReservadas (FechaI, FechaF, IdInmueble, IdCedula) VALUES (@FechaInicio, @FechaFin, @IdInmueble, @IdCedula)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    insertCommand.Parameters.AddWithValue("@FechaFin", fechaFin);
                    insertCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        public static int ObtenerIdFechaReservada(string idCedula)
        {
            int idFechaReservada = 0;

            string selectQuery = "SELECT IdFechaReservada FROM FechasReservadas WHERE IdCedula = @IdCedula";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdCedula", idCedula);

                    var result = selectCommand.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idFechaReservada = Convert.ToInt32(result);
                    }
                }
            }

            return idFechaReservada;
        }



        public static void InsertarReserva(string idCedula, int idInmueble, int cantidadHuespedes, string idNTarjeta, int IdFechaReservada)
        {
            string insertQuery = "INSERT INTO Reservas (IdCedula, IdInmueble, CantidadHuespedes, IdNTarjeta, IdFechaReservada) " +
                                 "VALUES (@IdCedula, @IdInmueble, @CantidadHuespedes, @IdNTarjeta, @IdFechaReservada)";


            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@IdCedula", idCedula);
                    insertCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    insertCommand.Parameters.AddWithValue("@CantidadHuespedes", cantidadHuespedes);
                    insertCommand.Parameters.AddWithValue("@IdNTarjeta", idNTarjeta);
                    insertCommand.Parameters.AddWithValue("@IdFechaReservada", IdFechaReservada);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        public static bool TieneSaldoSuficiente(string IdNTarjeta, decimal montoAPagar)
        {
            string selectQuery = "SELECT Saldo FROM MiBanco WHERE IdNTarjeta = @IdNTarjeta";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdNTarjeta", IdNTarjeta);

                    object result = selectCommand.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        decimal saldo = Convert.ToDecimal(result);
                        return saldo >= montoAPagar;
                    }
                }
            }

            return false; // Si no se encuentra la tarjeta o hay un problema de conexión
        }
        public static decimal ObtenerSaldo(string IdNTarjeta)
        {
            string selectQuery = "SELECT Saldo FROM MiBanco WHERE IdNTarjeta = @IdNTarjeta";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdNTarjeta", IdNTarjeta);

                    object result = selectCommand.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToDecimal(result);
                    }
                }
            }

            return 0; // Si no se encuentra la tarjeta o hay un problema de conexión
        }


        public static void InsertarPagos(string IdNTarjeta, decimal TotalAPagar, int IdInmueble, string IdCedula)
        {

            string insertQuery = "INSERT INTO HistorialPagos (IdNTarjeta, TotalAPagar, IdInmueble, IdCedula) " +
                                 "VALUES (@IdNTarjeta, @TotalAPagar, @IdInmueble, @IdCedula)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@IdNTarjeta", IdNTarjeta);
                    insertCommand.Parameters.AddWithValue("@TotalAPagar", TotalAPagar);
                    insertCommand.Parameters.AddWithValue("@IdInmueble", IdInmueble);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);

                    insertCommand.ExecuteNonQuery();
                }

                decimal saldoActual = ObtenerSaldo(IdNTarjeta);
                decimal nuevoSaldo = saldoActual - TotalAPagar;

                string updateQuery = "UPDATE MiBanco SET Saldo = @NuevoSaldo WHERE IdNTarjeta = @IdNTarjeta";
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@NuevoSaldo", nuevoSaldo);
                    updateCommand.Parameters.AddWithValue("@IdNTarjeta", IdNTarjeta);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }


        public static bool FechasReservadasExisten(DateTime FechaI, DateTime FechaF, int idInmueble)
        {
            string consulta = "SELECT COUNT(*) FROM FechasReservadas " +
                              "WHERE IdInmueble = @IdInmueble AND " +
                              "((FechaI BETWEEN @FechaI AND @FechaF) OR " +
                              "(FechaF BETWEEN @FechaI AND @FechaF))";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmd.Parameters.AddWithValue("@FechaI", FechaI);
                    cmd.Parameters.AddWithValue("@FechaF", FechaF);

                    conexion.Open();
                    int cantidadReservas = (int)cmd.ExecuteScalar();

                    return cantidadReservas > 0;
                }
            }
        }

        public static List<string> ObtenerFechasReservadas(int idInmueble)
        {
            List<string> fechasReservadas = new List<string>();

            string consulta = "SELECT FechaI, FechaF FROM FechasReservadas WHERE IdInmueble = @IdInmueble";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    try
                    {
                        conexion.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DateTime fechaI = Convert.ToDateTime(reader["FechaI"]);
                            DateTime fechaF = Convert.ToDateTime(reader["FechaF"]);

                            for (DateTime fecha = fechaI; fecha <= fechaF; fecha = fecha.AddDays(1))
                            {
                                fechasReservadas.Add(fecha.ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener fechas reservadas: " + ex.Message);
                    }
                }
            }
            return fechasReservadas;
        }

        public class Restriccion
        {
            public string Descrip_Restricciones { get; set; }
        }

        public static List<Restriccion> ObtenerRestricciones(int idInmueble)
        {
            List<Restriccion> restricciones = new List<Restriccion>();

            string consulta = "SELECT Descrip_Restricciones FROM Restricciones_espacio WHERE IdInmueble = @IdInmueble";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    try
                    {
                        conexion.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Restriccion restriccion = new Restriccion();
                            restriccion.Descrip_Restricciones = reader["Descrip_Restricciones"].ToString();
                            restricciones.Add(restriccion);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener restricciones: " + ex.Message);
                    }
                }
            }
            return restricciones;
        }


        public DataTable Obtener_Reservas_IdCedula(string IdCedula)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT 
                                    Reservas.IdReserva, Reservas.IdFechaReservada,
                                    Inmuebles.Nombre,
                                    Inmuebles.IdInmueble,
                                    Identificaciones.Nombre AS NombrePropietario, 
                                    Identificaciones.Apellidos AS ApellidoPropietario,
                                    Inmuebles.IdCedula As CedulaPropietario, 
	                                f.FechaI,f.FechaF
                                FROM Reservas
                                INNER JOIN Inmuebles ON Inmuebles.IdInmueble = Reservas.IdInmueble
                                INNER JOIN Identificaciones ON Identificaciones.IdCedula = Reservas.IdCedula
                                INNER JOIN FechasReservadas f ON Reservas.IdFechaReservada = f.IdFechaReservada
                                WHERE Reservas.IdCedula = @IdCedula;
                                ";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }

        public DataTable Obtener_ReservasDenuncia_IdReserva(int IdReserva)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT 
                                    Reservas.IdReserva, 
                                    Reservas.IdFechaReservada,
                                    Inmuebles.Nombre,
                                    Inmuebles.IdInmueble,
                                    Identificaciones.Nombre AS NombrePropietario, 
                                    Identificaciones.Apellidos AS ApellidoPropietario,
                                    Inmuebles.IdCedula As CedulaPropietario, 
                                    f.FechaI,
                                    f.FechaF
                                FROM Reservas
                                INNER JOIN Inmuebles ON Inmuebles.IdInmueble = Reservas.IdInmueble
                                INNER JOIN Identificaciones ON Identificaciones.IdCedula = Reservas.IdCedula
                                INNER JOIN FechasReservadas f ON Reservas.IdFechaReservada = f.IdFechaReservada
                                WHERE Reservas.IdReserva = @IdReserva
                                AND NOT EXISTS (
                                    SELECT 1 FROM Denuncias WHERE Denuncias.IdReserva = Reservas.IdReserva
                                );";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdReserva);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }


        public DataTable Obtener_ReservasDenuncia_IdCedula(string IdCedula)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT 
                                    Reservas.IdReserva, 
                                    Reservas.IdFechaReservada,
                                    Inmuebles.Nombre,
                                    Inmuebles.IdInmueble,
                                    Identificaciones.Nombre AS NombrePropietario, 
                                    Identificaciones.Apellidos AS ApellidoPropietario,
                                    Inmuebles.IdCedula As CedulaPropietario, 
                                    f.FechaI,
                                    f.FechaF
                                FROM Reservas
                                INNER JOIN Inmuebles ON Inmuebles.IdInmueble = Reservas.IdInmueble
                                INNER JOIN Identificaciones ON Identificaciones.IdCedula = Reservas.IdCedula
                                INNER JOIN FechasReservadas f ON Reservas.IdFechaReservada = f.IdFechaReservada
                                WHERE Reservas.IdCedula = @IdCedula
                                AND NOT EXISTS (
                                    SELECT 1 FROM Denuncias WHERE Denuncias.IdReserva = Reservas.IdReserva
                                );";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdCedula", IdCedula);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }



        public DataTable Obtener_Reservas_IdReserva(int IdReserva)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT 
                                    Reservas.IdReserva, Reservas.IdFechaReservada,
                                    Inmuebles.Nombre,
                                    Inmuebles.IdInmueble,
                                    Identificaciones.Nombre AS NombrePropietario, 
                                    Identificaciones.Apellidos AS ApellidoPropietario,
                                    Inmuebles.IdCedula As CedulaPropietario, 
	                                f.FechaI,f.FechaF
                                FROM Reservas
                                INNER JOIN Inmuebles ON Inmuebles.IdInmueble = Reservas.IdInmueble
                                INNER JOIN Identificaciones ON Identificaciones.IdCedula = Reservas.IdCedula
                                INNER JOIN FechasReservadas f ON Reservas.IdFechaReservada = f.IdFechaReservada
                                WHERE Reservas.IdReserva = @IdReserva;
                                ";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", IdReserva);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }




        public List<int> Obtener_ReservasHuesped(string idCedula)
        {
            List<int> reservas = new List<int>();

            string selectQuery = "SELECT IdReserva FROM Reservas WHERE IdCedula = @IdCedula";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdCedula", idCedula);

                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["IdReserva"] != DBNull.Value)
                            {
                                reservas.Add(Convert.ToInt32(reader["IdReserva"]));
                            }
                        }
                    }
                }
            }

            return reservas;
        }


        public DataTable ObtenerDatosAnfitrion(int IdReserva)
        {
            DataTable datos = new DataTable();

            string consulta = @"SELECT r.IdReserva, r.IdCedula AS IdCedulaHuesped, r.IdInmueble, r.CantidadHuespedes, 
                                       r.IdNTarjeta, r.IdFechaReservada, i.Nombre AS NombreHuesped, i.Apellidos AS ApellidosHuesped,
                                       inm.Nombre AS NombreInmueble, inm.Descripcion AS DescripcionInmueble,
                                       inm.CantidadPersonas, inm.CantidadDormitorios, inm.CantidadBanos, inm.CantidadCamas, inm.IdCedula,
	                                   f.FechaI, f.FechaF
                                FROM Reservas r
                                INNER JOIN Inmuebles inm ON r.IdInmueble = inm.IdInmueble
                                INNER JOIN Identificaciones i ON r.IdCedula = i.IdCedula
                                INNER JOIN FechasReservadas f ON r.IdFechaReservada = f.IdFechaReservada
                                WHERE r.IdReserva = @IdReserva
                                AND NOT EXISTS (
                                    SELECT 1
                                    FROM CalificacionAlojamiento ch
                                    WHERE ch.IdReserva = r.IdReserva)";

            using (SqlConnection conexion = new SqlConnection(cadenaCon))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    cmd.Parameters.AddWithValue("@IdReserva", IdReserva);

                    conexion.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(datos);
                }
            }

            return datos;
        }

        public static void InsertarCalificacion(int Calificacion, string IdCedula, int IdInmueble, int IdReserva)
        {
            string insertQuery = "INSERT INTO CalificacionAlojamiento (Calificacion, IdCedula, IdInmueble, IdReserva) " +
                                 "VALUES (@Calificacion, @IdCedula, @IdInmueble, @IdReserva)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Calificacion", Calificacion);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);
                    insertCommand.Parameters.AddWithValue("@IdInmueble", IdInmueble);
                    insertCommand.Parameters.AddWithValue("@IdReserva", IdReserva);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }

        public static void InsertarDenuncia( string Denuncia, string IdCedula, int IdReserva)
        {
            string insertQuery = "INSERT INTO Denuncias (Denuncia, IdCedula, IdReserva) " +
                                 "VALUES (@Denuncia, @IdCedula, @IdReserva)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Denuncia", Denuncia);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);
                    insertCommand.Parameters.AddWithValue("@IdReserva", IdReserva);                    

                    insertCommand.ExecuteNonQuery();
                }
            }
        }
        public static void InsertarNotificacion(string Notificacion, string IdCedula)
        {
            string insertQuery = "INSERT INTO Notificaciones(Notificacion,IdCedula)" +
                                 "values(@Notificacion,@IdCedula)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Notificacion", Notificacion);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }


        public static void CalificacionHuesped(int Calificacion, string IdCedula, int IdReserva)
        {
            string insertQuery = "INSERT INTO CalificacionHuesped (Calificacion, IdCedula, IdReserva) " +
                                 "VALUES (@Calificacion, @IdCedula, @IdReserva)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Calificacion", Calificacion);
                    insertCommand.Parameters.AddWithValue("@IdCedula", IdCedula);
                    insertCommand.Parameters.AddWithValue("@IdReserva", IdReserva);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }



        public DataTable ObtenerServiciosVistaP(int idInmueble)
        {
            DataTable dtServicios = new DataTable();
            dtServicios.Columns.Add("Nombre", typeof(string));
            dtServicios.Columns.Add("Icono", typeof(byte[]));

            string obtenerServiciosQuery = @"
            SELECT S.Nombre, S.Icono
            FROM ServiciosInmuebles SI
            JOIN Servicios S ON SI.IdServiciosAlojamientos = S.IdServiciosAlojamientos
            WHERE SI.IdInmueble = @IdInmueble";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerServiciosCommand = new SqlCommand(obtenerServiciosQuery, connection))
                {
                    obtenerServiciosCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    using (SqlDataReader reader = obtenerServiciosCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["Nombre"].ToString();
                            byte[] icono = reader["Icono"] as byte[];

                            dtServicios.Rows.Add(nombre, icono);
                        }
                    }
                }
            }

            return dtServicios;
        }

        public DataTable ObtenerDatosVistaPrevia(int idInmueble)
        {
            DataTable resultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand("Grupo5_Dreamplace.sp_ObtenerDatosVistaPrevia", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;


                    comando.Parameters.AddWithValue("@idInmueble", idInmueble);

                    try
                    {
                        conexion.Open();

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            adaptador.Fill(resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el stored procedure: " + ex.Message);
                    }
                }
            }

            return resultado;
        }

        public void InsertarFavorito(string idCedula, int idInmueble)
        {
            string insertarFavoritoQuery = "INSERT INTO Favoritos (IdCedula, IdInmueble) VALUES (@IdCedula, @IdInmueble)";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand insertarFavoritoCommand = new SqlCommand(insertarFavoritoQuery, connection))
                {
                    insertarFavoritoCommand.Parameters.AddWithValue("@IdCedula", idCedula);
                    insertarFavoritoCommand.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    insertarFavoritoCommand.ExecuteNonQuery();
                }
            }
        }

        public DataTable ObtenerInmueblesFavoritos(string idCedula)
        {
            DataTable dtInmuebles = new DataTable();
            dtInmuebles.Columns.Add("IdInmueble", typeof(int));
            dtInmuebles.Columns.Add("NombreInmueble", typeof(string));
            dtInmuebles.Columns.Add("TipoInmueble", typeof(string));
            dtInmuebles.Columns.Add("Provincia", typeof(string));
            dtInmuebles.Columns.Add("Canton", typeof(string));
            dtInmuebles.Columns.Add("Total", typeof(decimal));

            string obtenerInmueblesFavoritosQuery = "Grupo5_Dreamplace.ObtenerInmueblesFavoritos";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();
                using (SqlCommand obtenerInmueblesFavoritosCommand = new SqlCommand(obtenerInmueblesFavoritosQuery, connection))
                {
                    obtenerInmueblesFavoritosCommand.CommandType = CommandType.StoredProcedure;
                    obtenerInmueblesFavoritosCommand.Parameters.AddWithValue("@IdCedula", idCedula);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(obtenerInmueblesFavoritosCommand))
                    {
                        adapter.Fill(dtInmuebles);
                    }
                }
            }

            return dtInmuebles;
        }


        public void InsertarComentario(string idCedula, int idInmueble, string comentarioTexto)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                string query = "INSERT INTO Comentarios (IdCedula, IdInmueble, ComentarioTexto, Fecha) " +
                               "VALUES (@IdCedula, @IdInmueble, @ComentarioTexto, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@IdCedula", SqlDbType.Char, 20));
                    command.Parameters["@IdCedula"].Value = idCedula;

                    command.Parameters.Add(new SqlParameter("@IdInmueble", SqlDbType.Int));
                    command.Parameters["@IdInmueble"].Value = idInmueble;

                    command.Parameters.Add(new SqlParameter("@ComentarioTexto", SqlDbType.NVarChar));
                    command.Parameters["@ComentarioTexto"].Value = comentarioTexto;

                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable ObtenerComentariosPorInmueble(int idInmueble)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                string query = "SELECT * FROM Comentarios WHERE IdInmueble = @Inmueble";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Inmueble", SqlDbType.Int));
                    command.Parameters["@Inmueble"].Value = idInmueble;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }

            return resultTable;
        }



        public DataTable ObtenerUbicacion(int IdUbicacion)
        {
            DataTable resultTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                string query = "SELECT Provincia, Canton, PosicionGPS FROM Ubicaciones WHERE IdUbicacion = @IdUbicacion";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@IdUbicacion", SqlDbType.Int));
                    command.Parameters["@IdUbicacion"].Value = IdUbicacion;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(resultTable);
                    }
                }
            }

            return resultTable; 
        }


        
        public void InsertarInmuebleConmpleto(
        string cedAnfitrion,
        int idEstado,
        int idDescuento,
        int idCategoria,
        string tipoInmueble,
        string provincia,
        string canton,
        string direccion,
        int canHuespedes,
        int canHabitaciones,
        int canCamas,
        int canBanos,
        int idServicio1,
        int idServicio2,
        string tituloInmueble,
        string descripcionIn,
        decimal precioBase,
        decimal iva,
        decimal precioTotal,
        string listaImagenes)
        {
            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand command = new SqlCommand("InsertarInmuebleCompleto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@CedAnfitrion", cedAnfitrion);
                    command.Parameters.AddWithValue("@IdEstado", idEstado);
                    command.Parameters.AddWithValue("@IdDescuento", idDescuento);
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    command.Parameters.AddWithValue("@TipoInmueble", tipoInmueble);
                    command.Parameters.AddWithValue("@Provincia", provincia);
                    command.Parameters.AddWithValue("@Canton", canton);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@CanHuespedes", canHuespedes);
                    command.Parameters.AddWithValue("@CanHabitaciones", canHabitaciones);
                    command.Parameters.AddWithValue("@CanCamas", canCamas);
                    command.Parameters.AddWithValue("@CanBanos", canBanos);
                    command.Parameters.AddWithValue("@IdServicio1", idServicio1);
                    command.Parameters.AddWithValue("@IdServicio2", idServicio2);
                    command.Parameters.AddWithValue("@TituloInmueble", tituloInmueble);
                    command.Parameters.AddWithValue("@DescripcionIn", descripcionIn);
                    command.Parameters.AddWithValue("@PrecioBase", precioBase);
                    command.Parameters.AddWithValue("@Iva", iva);
                    command.Parameters.AddWithValue("@PrecioTotal", precioTotal);
                    command.Parameters.AddWithValue("@ListaImagenes", listaImagenes);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        public string ObtenerNombreServicioPorId(int idServicio)
        {
            string nombreServicio = null;

            using (SqlConnection connection = new SqlConnection(cadenaConexion))
            {
                connection.Open();

                string query = "SELECT Nombre FROM Servicios WHERE IdServiciosAlojamientos = @IdServicio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdServicio", idServicio);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtiene el nombre del servicio
                            nombreServicio = reader["Nombre"].ToString();
                        }
                    }
                }
            }

            return nombreServicio;
        }

        public string ObtenerNombreCategoriaPorId(int idCategoria)
        {
            string nombreCategoria = null;

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT Categoria FROM Categorias WHERE IdCategoria = @IdCategoria";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Obtiene el nombre de la categoría
                            nombreCategoria = reader["Categoria"].ToString();
                        }
                    }
                }
            }

            return nombreCategoria;
        }

        public DataTable ObtenerMovimientosPorCuenta(string idNumeroCuenta)
        {
            DataTable movimientosDataTable = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("ObtenerMovimientosPorCuenta", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdNumeroCuenta", idNumeroCuenta);

                    try
                    {
                        conexion.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(movimientosDataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar la excepción según tus necesidades
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return movimientosDataTable;
        }

        public void RealizarDeposito(string idNumeroCuenta, decimal montoDeposito, string descripcion)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(cadenaCon))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("RealizarDeposito", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros del Stored Procedure
                        cmd.Parameters.Add(new SqlParameter("@IdNumeroCuenta", idNumeroCuenta));
                        cmd.Parameters.Add(new SqlParameter("@MontoDeposito", montoDeposito));
                        cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));

                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Depósito realizado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al realizar el depósito: " + ex.Message);
                // Puedes manejar el error según tus necesidades
            }
        }

        public void EliminarFavorito(string idCedula, int idInmueble)
        {
            string consultaSql = "DELETE FROM Favoritos WHERE IdCedula = @IdCedula AND IdInmueble = @IdInmueble;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(consultaSql, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCedula", idCedula);
                    comando.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al eliminar el favorito: " + ex.Message);
                    }
                }
            }
        }

        public string ObtenerCorreoAnfitrionPorIdInmueble(int idInmueble)
        {
            string correoAnfitrion = "";

            using (SqlConnection connection = new SqlConnection(cadenaCon))
            {
                connection.Open();

                string query = "SELECT U.Correo " +
                               "FROM Usuarios U " +
                               "JOIN Inmuebles I ON U.IdCedula = I.IdCedula " +
                               "WHERE I.IdInmueble = @IdInmueble";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdInmueble", idInmueble);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            correoAnfitrion = reader["Correo"].ToString();
                        }
                    }
                }
            }

            return correoAnfitrion;
        }

    } // Fin de la clase conexion
}





