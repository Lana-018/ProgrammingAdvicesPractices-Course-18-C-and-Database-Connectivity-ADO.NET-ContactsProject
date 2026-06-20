using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDataAccessLayer
{
    public class clsCountriesDataAccess
    {
        static public bool GetCountryByID(int ID, ref string CountryName,ref string Code,ref string PhoneCode)
            {
                bool IsFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = "select * from Countries where CountryID=@ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            IsFound = true;
                            CountryName = (string)reader["CountryName"];
                        if (reader["Code"] != DBNull.Value)
                            Code = (string)reader["Code"];
                        else Code = "";
                        if (reader["PhoneCode"] != DBNull.Value)
                            PhoneCode = (string)reader["PhoneCode"];
                        else PhoneCode = "";
                        }
                }
                }
                catch (Exception ex)
                {
                IsFound = false;
                    Console.WriteLine(ex.Message);
                }
                return IsFound;
            }
            static public bool GetCountryByName(ref int ID, string CountryName,ref string Code,ref string PhoneCode)
            {
                bool IsFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = "select * from Countries where CountryName=@CountryName";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            IsFound = true;
                            ID = (int)reader["CountryID"];
                        if (reader["Code"] != DBNull.Value)
                            Code = (string)reader["Code"];
                        else Code = "";
                        if (reader["PhoneCode"] != DBNull.Value)
                            PhoneCode = (string)reader["PhoneCode"];
                        else PhoneCode = "";
                        
                        }
                }
                }
                catch (Exception ex)
                {
                IsFound = false;
                    Console.WriteLine(ex.Message);
                }
                return IsFound;
            }
            static public bool IsCountryExist(int ID)
            {
                bool IsFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = "select result=1 from Countries where CountryID=@CountryID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryID", ID);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                IsFound = false;
                    Console.WriteLine(ex.Message);
                }
                return IsFound;
            }
            static public bool IsCountryExist(string CountryName)
            {
                bool IsFound = false;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = "select result=1 from Countries where CountryName=@CountryName";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                IsFound = false;
                    Console.WriteLine(ex.Message);
                }
                return IsFound;
            }
            static public int AddNewCountry(string CountryName, string Code, string PhoneCode)
            {
                try
                {
                    int insertedID = -1;
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = @"INSERT INTO Countries (CountryName,Code,PhoneCode)
                       VALUES (@CountryName,@Code,@PhoneCode);
                       SELECT SCOPE_IDENTITY(); ";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                    if (string.IsNullOrEmpty(Code))
                        command.Parameters.AddWithValue("@Code", DBNull.Value);
                    else command.Parameters.AddWithValue("@Code", Code);
                    if(string.IsNullOrEmpty(PhoneCode))
                        command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
                   else  command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out insertedID))
                        {
                            return insertedID;
                        }
                        else return -1;
                    }
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
                return -1;
            }
            static public int UpdateCountryName(int ID, string CountryName, string Code, string PhoneCode)
            {
                int RowsAffected = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = @"UPDATE Countries SET CountryName = @CountryName,
                                        Code=@Code,PhoneCode=@PhoneCode where CountryID=@ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        command.Parameters.AddWithValue("@ID", ID);
                    if (string.IsNullOrEmpty(Code))
                        command.Parameters.AddWithValue("@Code", DBNull.Value);
                    else command.Parameters.AddWithValue("@Code", Code);
                    if (string.IsNullOrEmpty(PhoneCode))
                        command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);
                    else command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
                    connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
                return RowsAffected;
            }
            static public int DeleteCountry(int ID)
            {
                int RowsAffected = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = @"Delete from Countries where CountryID=@ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", ID);
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
                return RowsAffected;
            }
            static public int DeleteCountry(string CountryName)
            {
                int RowsAffected = 0;
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = @"Delete from Countries where CountryName=@CountryName";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                        connection.Open();
                        RowsAffected = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
                return RowsAffected;
            }
            static public DataTable GetAllCountries()
            {
                DataTable Table = new DataTable();
                try
                {
                    using (SqlConnection connection = new SqlConnection(clsContactsConnection.ConnectionString))
                    {
                        string query = "Select * from Countries";
                        SqlCommand command = new SqlCommand(query, connection);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            Table.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return Table;
            }
           
        }
    }

