using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.EntityLayer;
using System.Data.SqlClient;
using System.Data;

namespace CRUD.DataLayer
{
    public class EmpleadoDL
    {
        public Empleado Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("selct * from fn_empleados()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"].ToString()),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Departamento = new Departamento()
                                {
                                    IdDepartamento = Convert.ToInt32(dr["idDepartamento"].ToString()),
                                    Nombre = dr["Nombre"].ToString()
                                },
                                Sueldo = (decimal)dr["Sueldo"],
                                FechaContrato = dr["FechaContrato"].ToString()
                            });
                        }
                    }
                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<Empleado> Obtener(int IdEmpleado)
        {
            Empleado entidad = new Empleado();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("selct * from fn_empleado(@idEmpleado)", oConexion);
                cmd.Parameters.AddWithValue("@idEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            entidad.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"].ToString());
                            entidad.NombreCompleto = dr["NombreCompleto"].ToString();
                            entidad.Departamento = new Departamento()
                            {
                                IdDepartamento = Convert.ToInt32(dr["idDepartamento"].ToString()),
                                Nombre = dr["Nombre"].ToString()
                            };
                            entidad.Sueldo = (decimal)dr["Sueldo"];
                            entidad.FechaContrato = dr["FechaContrato"].ToString();
                        }                       
                    }
                    return entidad;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Crear(Empleado entidad)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@nombreCompleto", entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@idDepartamento", entidad.Departamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@sueldo", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@fechaContrato", entidad.FechaContrato);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) { respuesta = true; }
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Editar(Empleado entidad)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@idEmpleado", entidad.IdEmpleado);
                cmd.Parameters.AddWithValue("@nombreCompleto", entidad.NombreCompleto);
                cmd.Parameters.AddWithValue("@idDepartamento", entidad.Departamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@sueldo", entidad.Sueldo);
                cmd.Parameters.AddWithValue("@fechaContrato", entidad.FechaContrato);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) { respuesta = true; }
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public bool Eliminar(int IdEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", oConexion);
                cmd.Parameters.AddWithValue("@idEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0) { respuesta = true; }
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
