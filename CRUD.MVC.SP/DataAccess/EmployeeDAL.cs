using CRUD.MVC.SP.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CRUD.MVC.SP.DataAccess
{
    public class EmployeeDAL
    {
        private readonly string connectionString;

        public EmployeeDAL(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public PagedResult<Employee> GetAllPagination(string searchTerm, int pageNumber, int pageSize)
        {
            var result = new PagedResult<Employee>();
            result.Items = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployees_06082025", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Items.Add(new Employee
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString(),
                            Age = Convert.ToInt32(dr["Age"]),
                            Department = dr["Department"].ToString()
                        });
                    }

                    if (dr.NextResult() && dr.Read())
                    {
                        result.TotalRecords = Convert.ToInt32(dr["TotalCount"]);
                    }
                }
            }

            result.PageNumber = pageNumber;
            result.PageSize = pageSize;
            return result;
        }

        public List<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Department = dr["Department"].ToString()
                    });
                }
            }
            return list;
        }

        public Employee GetById(int id)
        {
            Employee emp = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.Name = dr["Name"].ToString();
                    emp.Age = Convert.ToInt32(dr["Age"]);
                    emp.Department = dr["Department"].ToString();
                }
            }
            return emp;
        }

        public void Insert(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Age", emp.Age);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
