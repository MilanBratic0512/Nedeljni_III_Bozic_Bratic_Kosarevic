using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Models;

namespace Zadatak_1.Service
{
    public class ServiceCode
    {
        public List<ReceptType> GettAllTypes()
        {
            List<ReceptType> receptTypesList = new List<ReceptType>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Get_AllTypes";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            ReceptType r = new ReceptType
                            {
                                TypeID = int.Parse(row[0].ToString()),
                                TypeName = row[1].ToString(),
                            };
                            receptTypesList.Add(r);
                        }
                     return receptTypesList;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Components> GettAllComponentsByReceptId(int receptId)
        {
            List<Components> componentsList = new List<Components>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Get_AllComponentsByReceptId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReceptID", receptId);
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        foreach (DataRow row in dt.Rows)
                        {
                            Components c = new Components
                            {
                                ComponentId = int.Parse(row[0].ToString()),
                                ReceptId = int.Parse(row[1].ToString()),
                                ComponentName = row[2].ToString(),
                                ComponentAmount = int.Parse(row[3].ToString()),
                            };
                            componentsList.Add(c);
                        }
                        return componentsList;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int AddRecept(Recept recept)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {                
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert_Recept";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", recept.UserId);
                        cmd.Parameters.AddWithValue("@TypeID", recept.TypeId);
                        cmd.Parameters.AddWithValue("@ReceptName", recept.ReceptName);
                        cmd.Parameters.AddWithValue("@PersonNumber", recept.PersonNumber);
                        cmd.Parameters.AddWithValue("@Author", recept.Author);
                        cmd.Parameters.AddWithValue("@ReceptText", recept.ReceptText);
                        cmd.Parameters.AddWithValue("@CreationDate", recept.CreationDate);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            recept.ReceptId = int.Parse(reader.GetValue(0).ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return 0;
                }
                finally
                {
                    conn.Close();
                }                
            }
            return recept.ReceptId;
        }

        public int AddComponent(Components component)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Insert_Components";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReceptID", component.ReceptId);
                        cmd.Parameters.AddWithValue("@ComponentName", component.ComponentName);
                        cmd.Parameters.AddWithValue("@ComponentAmount", component.ComponentAmount);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            component.ComponentId = int.Parse(reader.GetValue(0).ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            return component.ComponentId;
        }

        public bool UpdateRecept(Recept recept)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Update_Recept";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ReceptID", recept.ReceptId);
                        cmd.Parameters.AddWithValue("@UserID", recept.UserId);
                        cmd.Parameters.AddWithValue("@TypeID", recept.TypeId);
                        cmd.Parameters.AddWithValue("@ReceptName", recept.ReceptName);
                        cmd.Parameters.AddWithValue("@Author", recept.Author);
                        cmd.Parameters.AddWithValue("@ReceptText", recept.ReceptText);
                        cmd.Parameters.AddWithValue("@CreationDate", recept.CreationDate);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool UpdateComponent(Components component)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "Update_Component";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ComponentID", component.ComponentId);
                        cmd.Parameters.AddWithValue("@ReceptID", component.ReceptId);                        
                        cmd.Parameters.AddWithValue("@ComponentName", component.ComponentName);
                        cmd.Parameters.AddWithValue("@ComponentAmount", component.ComponentAmount);                       
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exeption" + ex.Message.ToString());
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
