using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_RFC
    {
        private string CadenaConexion = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

        public List<E_RFC> ObtenerRFCs()
        {
            //creamos la lista para almacenar los rfcs
            List<E_RFC> lista = new List<E_RFC>();
            //creamos el objeto conexion
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                //abrimos la conexion
                conexion.Open();
                //creamos el objeto para ejecutar el SP y le pasamos el nombre del SP a ejecutar    SP: Store Procedure
                SqlCommand comando = new SqlCommand("obtener_rfcs", conexion);
                //le indicamos al objeto 'comando' que va a ejecutar un sp
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    //creamos objeto de la clase rfc
                    E_RFC rfc = new E_RFC();
                    rfc.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    rfc.Nombre = reader["nombre"].ToString();
                    rfc.ApellidoPat = reader["apellidoPat"].ToString();
                    rfc.ApellidoMat = reader["apellidoMat"].ToString();
                    rfc.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                    rfc.RFC = reader["rfc"].ToString();

                    //se agrega el rfc a la lista
                    lista.Add(rfc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        public E_RFC ObtenerRFCporId(int idRFC)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                //Crear el objeto producto
                E_RFC objRFC = new E_RFC();
                conexion.Open();

                //Le pasamos el nombre del stored procedure a ejecutar
                SqlCommand comando = new SqlCommand("obtener_rfc_porId", conexion);
                //Le indicamos al objeto comando que va a ejecutar un stored procedure
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idRFC", idRFC);

                SqlDataReader reader = comando.ExecuteReader();

                //Leemos el resultado en el método Read
                if (reader.Read())
                {
                    //Le asignamos valores a las propiedades del producto
                    objRFC.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    objRFC.Nombre = reader["nombre"].ToString();
                    objRFC.ApellidoPat = reader["apellidoPat"].ToString();
                    objRFC.ApellidoMat = reader["apellidoMat"].ToString();
                    objRFC.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                    objRFC.RFC = reader["rfc"].ToString();
                }
                return objRFC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void GenerarRFC(E_RFC objRFC)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                //abrimos la conexion
                conexion.Open();
                //creamos el objeto para que ejecute el SP
                SqlCommand comando = new SqlCommand("generar_rfc", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@nombre", objRFC.Nombre);
                comando.Parameters.AddWithValue("@apellidoPat", objRFC.ApellidoPat);
                comando.Parameters.AddWithValue("@apellidoMat", objRFC.ApellidoMat);
                comando.Parameters.AddWithValue("@fechaNacimiento", objRFC.FechaNacimiento);


                comando.Parameters.AddWithValue("@rfc", objRFC.RFC);
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }


        public void EditarRFC(E_RFC objRFC)
        {

            SqlConnection conexion = new SqlConnection(CadenaConexion);

            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("editar_rfc", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@nombre", objRFC.Nombre);
                comando.Parameters.AddWithValue("@apellidoPat", objRFC.ApellidoPat);
                comando.Parameters.AddWithValue("@apellidoMat", objRFC.ApellidoMat);
                comando.Parameters.AddWithValue("@fechaNacimiento", objRFC.FechaNacimiento);


                comando.Parameters.AddWithValue("@rfc", objRFC.RFC);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                conexion.Close();
            }
        }

        public void Eliminar(int idRFC)
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                conexion.Open();
                //Crear el objeto para ejecutar el stored procedure
                //Le pasamos el nombre del store procedure a ejecutar
                SqlCommand comando = new SqlCommand("borrar_rfc", conexion);
                //Le indicamos al objeto comando que va a ejecutar un stored procedure
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("idRFC", idRFC);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public List<E_RFC> BuscarRFC(string texto)
        {
            //creamos la lista para almacenar los rfcs
            List<E_RFC> lista = new List<E_RFC>();
            //creamos el objeto conexion
            SqlConnection conexion = new SqlConnection(CadenaConexion);
            try
            {
                //abrimos la conexion
                conexion.Open();
                //creamos el objeto para ejecutar el SP y le pasamos el nombre del SP a ejecutar    SP: Store Procedure
                SqlCommand comando = new SqlCommand("buscar_datos_rfc", conexion);
                //le indicamos al objeto 'comando' que va a ejecutar un sp
                comando.CommandType = CommandType.StoredProcedure;

                //Pasamos valor al parámetro
                comando.Parameters.AddWithValue("texto", texto);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    //creamos objeto de la clase rfc
                    E_RFC rfc = new E_RFC();
                    rfc.IdRFC = Convert.ToInt32(reader["idRFC"]);
                    rfc.Nombre = reader["nombre"].ToString();
                    rfc.ApellidoPat = reader["apellidoPat"].ToString();
                    rfc.ApellidoMat = reader["apellidoMat"].ToString();
                    rfc.FechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]);
                    rfc.RFC = reader["rfc"].ToString();

                    //se agrega el rfc a la lista
                    lista.Add(rfc);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }   
    }
}