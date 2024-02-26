using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using frontend.Models;
using Npgsql;

namespace frontend.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _con;

        public EmployeeRepository(IConfiguration configuration)
        {
            _con = configuration.GetConnectionString("DefaultConnection");
        }
        public bool AddData(EmployeeModel employee)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_con))
            {
                try
                {
                    string hobbyString = string.Join(",", employee.c_hobby);

                    con.Open();
                    string query = "INSERT INTO t_employee VALUES(DEFAULT,@c_name,@c_email,@c_password,@c_gender,@c_dob,@c_hobby,@c_profile,@c_role,@c_cityid,@c_departmentid)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@c_name", employee.c_name);
                        cmd.Parameters.AddWithValue("@c_email", employee.c_email);
                        cmd.Parameters.AddWithValue("@c_password", employee.c_password);
                        cmd.Parameters.AddWithValue("@c_gender", employee.c_gender);
                        cmd.Parameters.AddWithValue("@c_dob", employee.c_dob);
                        cmd.Parameters.AddWithValue("@c_hobby", hobbyString);
                        cmd.Parameters.AddWithValue("@c_profile", employee.c_profile);
                        cmd.Parameters.AddWithValue("@c_role", employee.c_role);
                        cmd.Parameters.AddWithValue("@c_cityid", employee.c_cityid);
                        cmd.Parameters.AddWithValue("@c_departmentid", employee.c_departmentid);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public List<EmployeeModel> Alldata()
        {
            List<EmployeeModel> data = new List<EmployeeModel>();
            using (NpgsqlConnection con = new NpgsqlConnection(_con))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM t_employee";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {

                                EmployeeModel employee = new EmployeeModel
                                {
                                    c_empid = Convert.ToInt32(dr["c_empid"]),
                                    c_name = dr["c_name"].ToString(),
                                    c_email = dr["c_email"].ToString(),
                                    c_password = dr["c_password"].ToString(),
                                    c_gender = dr["c_gender"].ToString(),
                                    c_dob = Convert.ToDateTime(dr["c_dob"]),
                                    c_hobby = dr["c_hobby"].ToString().Split(','),
                                    c_profile = dr["c_profile"].ToString(),
                                    c_role = dr["c_role"].ToString(),
                                    c_cityid = Convert.ToInt32(dr["c_cityid"]),
                                    c_departmentid = Convert.ToInt32(dr["c_departmentid"]),
                                };
                                data.Add(employee);
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return data;
            }
        }

        public bool DeleteData(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_con))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM t_employee WHERE c_empid = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public EmployeeModel GetById(int id)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(_con))
                {
                    con.Open();
                    string query = "SELECT * FROM t_employee WHERE c_empid = @id";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                EmployeeModel employee = new EmployeeModel
                                {
                                    c_empid = Convert.ToInt32(dr["c_empid"]),
                                    c_name = dr["c_name"].ToString(),
                                    c_email = dr["c_email"].ToString(),
                                    c_password = dr["c_password"].ToString(),
                                    c_gender = dr["c_gender"].ToString(),
                                    c_dob = Convert.ToDateTime(dr["c_dob"]),
                                    c_hobby = dr["c_hobby"].ToString().Split(','),
                                    c_profile = dr["c_profile"].ToString(),
                                    c_role = dr["c_role"].ToString(),
                                    c_cityid = Convert.ToInt32(dr["c_cityid"]),
                                    c_departmentid = Convert.ToInt32(dr["c_departmentid"]),
                                };
                                return employee;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex}");
                throw; // Rethrow the exception after logging
            }
            return null;
        }

        public bool UpdateData(EmployeeModel employee)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_con))
            {
                try
                {
                    con.Open();
                    string query = "UPDATE t_employee SET c_name = @c_name,c_email = @c_email,c_password = @c_password,c_gender = @c_gender,c_dob = @c_dob,c_hobby = @c_hobby,c_profile = @c_profile,c_role = @c_role,c_cityid = @c_cityid,c_departmentid = @c_departmentid WHERE c_empid = @c_empid";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@c_name", employee.c_name);
                        cmd.Parameters.AddWithValue("@c_email", employee.c_email);
                        cmd.Parameters.AddWithValue("@c_password", employee.c_password);
                        cmd.Parameters.AddWithValue("@c_gender", employee.c_gender);
                        cmd.Parameters.AddWithValue("@c_dob", employee.c_dob);
                        cmd.Parameters.AddWithValue("@c_hobby", employee.c_hobby);
                        cmd.Parameters.AddWithValue("@c_profile", employee.c_profile);
                        cmd.Parameters.AddWithValue("@c_role", employee.c_role);
                        cmd.Parameters.AddWithValue("@c_cityid", employee.c_cityid);
                        cmd.Parameters.AddWithValue("@c_departmentid", employee.c_departmentid);
                        cmd.Parameters.AddWithValue("@c_empid", employee.c_empid);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }


        public List<CityModel> FetchCityData()
        {
            List<CityModel> cities = new List<CityModel>();

            using (NpgsqlConnection conn = new NpgsqlConnection(_con))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM t_city";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CityModel city = new CityModel
                                {
                                    c_cityid = Convert.ToInt32(reader["c_cityid"]),
                                    c_cityname = reader["c_cityname"].ToString()

                                };
                                cities.Add(city);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return cities;
        }
        public List<DepartmentModel> FetchDeptartmentData()
        {
            List<DepartmentModel> depts = new List<DepartmentModel>();

            using (NpgsqlConnection conn = new NpgsqlConnection(_con))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM t_department";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartmentModel dept = new DepartmentModel
                                {
                                    c_departmentid = Convert.ToInt32(reader["c_departmentid"]),
                                    c_deptname = reader["c_deptname"].ToString()

                                };
                                depts.Add(dept);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return depts;
        }
    }
}