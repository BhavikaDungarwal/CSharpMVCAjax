using System.Data;
using frontend.Models;
using Npgsql;

namespace frontend.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _conn;

        public AuthRepository(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }

        public authModel login(authModel auth)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_conn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM public.t_auth WHERE c_email = @email AND c_password = @password";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@email", auth.c_email);
                        cmd.Parameters.AddWithValue("@password", auth.c_password);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                authModel user = new authModel
                                {
                                    c_userid = Convert.ToInt32(reader["c_userid"]),
                                    c_name = reader["c_name"].ToString(),
                                    c_email = reader["c_email"].ToString(),
                                    c_password = reader["c_password"].ToString(),
                                    c_role = Convert.ToInt32(reader["c_role"])
                                };
                                return user;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public bool signup(authModel auth)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_conn))
            {
                try
                {
                    conn.Open();
                    string squery = "INSERT INTO public.t_auth(c_name, c_email, c_password, c_role) VALUES(@c_name, @c_email, @c_password, 0)";
                    using (NpgsqlCommand com = new NpgsqlCommand(squery, conn))
                    {
                        com.CommandType = CommandType.Text;
                        com.Parameters.AddWithValue("@c_name", auth.c_name);
                        com.Parameters.AddWithValue("@c_email", auth.c_email);
                        com.Parameters.AddWithValue("@c_password", auth.c_password);
                        com.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
