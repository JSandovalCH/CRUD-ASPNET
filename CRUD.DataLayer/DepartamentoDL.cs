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
    public class DepartamentoDL
    {
        public List<Departamento> Lista()
        {
            List<Departamento> lista = new List<Departamento>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                SqlCommand cmd = new SqlCommand("selct * from fn_departamentos()",oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(dr[ "idDepartamento" ].ToString()),
                                Nombre = dr["Nombre"].ToString()
                            });
                        }
                    }
                    return lista;
                }catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
