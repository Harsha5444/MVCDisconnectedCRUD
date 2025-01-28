using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace MVCDisconnected.Models
{
    public class EmployeeDBContext
    {
        DataTable dt;
        public EmployeeDBContext()
        {
            dt = GetEmployees();
        }
        private string _connectionString = ConfigurationManager.ConnectionStrings["CompanyDB"].ConnectionString;
        public DataTable GetEmployees()
        {
            DataTable datatable = new DataTable();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using(SqlDataAdapter da = new SqlDataAdapter("select * from employee", con))
                {
                    try
                    {
                        da.Fill(datatable);
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return datatable;
        }
        public List<Employee> GetEmployeesList()
        {
            List<Employee> emps = new List<Employee>();
            foreach(DataRow row in dt.Rows)
            {
                emps.Add(new Employee
                {
                    Eno = Convert.ToInt32(row["Eno"]),
                    Ename = Convert.ToString(row["Ename"]),
                    Job = Convert.ToString(row["Job"]),
                    Salary = Convert.ToDecimal(row["Salary"]),
                    Dname = Convert.ToString(row["Dname"])                    
                });
            }
            return emps;
        }
        public bool insertEmployee(Employee emp)
        {
            DataRow row = dt.NewRow();
            row[0] = emp.Eno;
            row[1] = emp.Ename;
            row[2] = emp.Job;
            row[3] = emp.Salary;
            row[4] = emp.Dname;
            dt.Rows.Add(row);
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("select * from employee", con))
                {
                    try
                    {
                        SqlCommandBuilder cb = new SqlCommandBuilder(da);
                        da.Update(dt);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool updateEmployee(Employee emp)
        {
            int eno = emp.Eno;
            DataRow row = dt.Select($"Eno = {eno}").FirstOrDefault();
            row["Ename"] = emp.Ename;
            row["Job"] = emp.Job;
            row["Salary"] = emp.Salary;
            row["Dname"] = emp.Dname;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("select * from employee", con))
                {
                    try
                    {
                        SqlCommandBuilder cb = new SqlCommandBuilder(da);
                        da.Update(dt);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool deleteEmployee(int id)
        {
            DataRow row = dt.Select($"Eno = {id}").FirstOrDefault();
            row.Delete();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("select * from employee", con))
                {
                    try
                    {
                        SqlCommandBuilder cb = new SqlCommandBuilder(da);
                        da.Update(dt);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}