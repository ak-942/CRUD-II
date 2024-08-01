using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EmployeeDetails.Models;

namespace EmployeeDetails.DAL
{
    public class Employee_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["EmpDetailsConnectionString"].ToString();
        //Get All Employee
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SPAK_GetAllEmployees";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployeeDet = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployeeDet);
                connection.Close();

                foreach (DataRow dr in dtEmployeeDet.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Empid = Convert.ToInt32(dr["Empid"]),
                        Empname = dr["Empname"].ToString(),
                        Empcode = dr["Empcode"].ToString(),
                        Joiningdate = Convert.ToDateTime(dr["Joiningdate"]).ToString("yyyy-MM-dd"), 
                        DoB = Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd"),
                        Mobile = Convert.ToInt32(dr["Mobile"]),
                        Aadhaar = Convert.ToInt32(dr["Aadhaar"]),
                        Presentaddress = dr["Presentaddress"].ToString(),
                        School = dr["School"].ToString(),
                        College = dr["College"].ToString(),
                        Highereducation = dr["Highereducation"].ToString()
                    });

                }
            }

            return employeeList;
        }

        //Insert Employee Detail
        public bool InsertDetail(Employee emp)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spak_InsertDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@Empname", emp.Empname);
                command.Parameters.AddWithValue("@Empcode", emp.Empcode);
                command.Parameters.AddWithValue("@Joiningdate", DateTime.Parse(emp.Joiningdate));
                command.Parameters.AddWithValue("@DoB", DateTime.Parse(emp.DoB));
                command.Parameters.AddWithValue("@Mobile", emp.Mobile);
                command.Parameters.AddWithValue("@Aadhaar", emp.Aadhaar);
                command.Parameters.AddWithValue("@Presentaddress", emp.Presentaddress);
                command.Parameters.AddWithValue("@School", emp.School);
                command.Parameters.AddWithValue("@College", emp.College);
                command.Parameters.AddWithValue("@Highereducation", emp.Highereducation);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
                return true;
            else
                return false;

        }

        //Get Employee by ID
        public List<Employee> GetEmployeesbyID(int Empid)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spak_GetEmployeeDetails";
                command.Parameters.AddWithValue("@Empid", Empid);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Empid = Convert.ToInt32(dr["Empid"]),
                        Empname = dr["Empname"].ToString(),
                        Empcode = dr["Empcode"].ToString(),
                        Joiningdate = Convert.ToDateTime(dr["Joiningdate"]).ToString("yyyy-MM-dd"),
                        DoB = Convert.ToDateTime(dr["DOB"]).ToString("yyyy-MM-dd"),
                        Mobile = Convert.ToInt32(dr["Mobile"]),
                        Aadhaar = Convert.ToInt32(dr["Aadhaar"]),
                        Presentaddress = dr["Presentaddress"].ToString(),
                        School = dr["School"].ToString(),
                        College = dr["College"].ToString(),
                        Highereducation = dr["Highereducation"].ToString()
                    });

                }
            }

            return employeeList;
        }

        //Update Employee Detail
        public bool UpdateDetail(Employee emp)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPAK_UpdateDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Empid", emp.Empid);
                command.Parameters.AddWithValue("@Empname", emp.Empname);
                command.Parameters.AddWithValue("@Empcode", emp.Empcode);
                command.Parameters.AddWithValue("@Joiningdate", DateTime.Parse(emp.Joiningdate));
                command.Parameters.AddWithValue("@DoB", DateTime.Parse(emp.DoB));
                command.Parameters.AddWithValue("@Mobile", emp.Mobile);
                command.Parameters.AddWithValue("@Aadhaar", emp.Aadhaar);
                command.Parameters.AddWithValue("@Presentaddress", emp.Presentaddress);
                command.Parameters.AddWithValue("@School", emp.School);
                command.Parameters.AddWithValue("@College", emp.College);
                command.Parameters.AddWithValue("@Highereducation", emp.Highereducation);

                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
                return true;
            else
                return false;

        }

        //Delete Employee Detail
        public string DeleteDetail(int empid)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("SPAK_DeleteEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", empid);
                command.Parameters.Add("@RETURNMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@RETURNMESSAGE"].Value.ToString();
                connection.Close();
            }
            return result;
        }

    }
}