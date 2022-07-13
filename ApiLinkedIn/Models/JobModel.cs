using System.Data.SqlClient;

namespace ApiLinkedIn.Models
{
    public class JobModel
    {
        #region Conexion
        string ConnectionString = "Server=tcp:xamarin-extra.database.windows.net,1433;Initial Catalog=sqlLinkedIn;Persist Security Info=False;User ID=jll72778;Password=Naruhina150;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion


        #region Propiedades

        public int ID { get; set; }

        public string? Cargo { get; set; }

        public string? Modalidad { get; set; }

        public string? Ciudad { get; set; }

        public string? Empresa { get; set; }

        public string? Jornada { get; set; }

        public string? Description { get; set; }

        public string? Imagen { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        #endregion


        #region Metodos

        //Obtener una lista de trabajos
        public ApiResponse GetAll()
        {
            List<JobModel> list = new List<JobModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Job ";

                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new JobModel
                                {
                                    ID = (int)reader["ID"],
                                    Cargo = reader["Cargo"].ToString(),
                                    Modalidad = reader["Modalidad"].ToString(),
                                    Ciudad = reader["Ciudad"].ToString(),
                                    Empresa = reader["Empresa"].ToString(),
                                    Jornada = reader["Jornada"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Imagen = reader["Imagen"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                });
                            }
                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Messege = "Los trabajos disponibles fueron obtenidos con exito",
                    Result = list
                };

            }
            catch (Exception ex)
            {

                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = $"Se Genero un errror al obtener los puestos de trabajo :{ex.Message}",
                    Result = null
                };
            }

        }

        //Obtner trabajo por medio de id
        
        public ApiResponse Get(int id)
        {
            JobModel model = new JobModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "SELECT * FROM Job WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                model = new JobModel()
                                {
                                    ID = (int)reader["ID"],
                                    Cargo = reader["Cargo"].ToString(),
                                    Modalidad = reader["Modalidad"].ToString(),
                                    Ciudad = reader["Ciudad"].ToString(),
                                    Empresa = reader["Empresa"].ToString(),
                                    Jornada = reader["Jornada"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Imagen = reader["Imagen"].ToString(),
                                    Latitude = (double)reader["Latitude"],
                                    Longitude = (double)reader["Longitude"]
                                };
                            }

                        }
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Messege = "Los trabajos disponibles fueron obtenidos con exito",
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = $"Se Genero un errror al obtener los puestos de trabajo :{ex.Message}",
                    Result = null
                };
            }
        }
        
        //Añadir un trabajo
        public ApiResponse Add(JobModel model)
        {

            try
            {
                object newID;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "INSERT INTO Job " +
                                   "(ID," +
                                   "Cargo, " +
                                    "Modalidad, " +
                                    "Ciudad, " +
                                     "Empresa," +
                                    "Jornada," +
                                    "Description, " +
                                    "Imagen, " +
                                    "Latitude, " +
                                    "Longitude) " +
                                     "VALUES " +
                                     "(@ID," +
                                     "@Cargo, " +
                                     "@Modalidad, " +
                                     "@Ciudad, " +
                                     "@Empresa,"+
                                     "@Jornada,"+
                                     "@Description, " +
                                     "@Imagen, " +
                                     "@Latitude, " +
                                     "@Longitude)";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@ID", model.ID);
                        cmd.Parameters.AddWithValue("@Cargo", model.Cargo);
                        cmd.Parameters.AddWithValue("@Modalidad", model.Modalidad);
                        cmd.Parameters.AddWithValue("@Ciudad", model.Ciudad);
                        cmd.Parameters.AddWithValue("@Empresa",model.Empresa);
                        cmd.Parameters.AddWithValue("@Jornada",model.Jornada);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Imagen", model.Imagen);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        newID = cmd.ExecuteScalar();
                    }

                    return new ApiResponse
                    {
                        IsSuccess = true,
                        Messege = "El Puesto de trabajo fue añadido con exito",
                        Result = model
                    };


                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = $"Se Genero un errror al tratar de ingresar el empleo :{ex.Message}",
                    Result = null
                };
            }
        }
        
        //Actualizar un trabajo
        public ApiResponse Update(JobModel model)
        {

            try
            {
                object newID;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "UPDATE Job SET  Cargo = @Cargo, Modalidad = @Modalidad, Ciudad = @Ciudad, Empresa = @Empresa, Jornada = @Jornada, Description = @Description,Imagen = @Imagen , Latitude = @Latitude , Longitude = @Longitude  " +
                    "WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        
                        cmd.Parameters.AddWithValue("@Cargo", model.Cargo);
                        cmd.Parameters.AddWithValue("@Modalidad", model.Modalidad);
                        cmd.Parameters.AddWithValue("@Ciudad", model.Ciudad);
                        cmd.Parameters.AddWithValue("@Empresa", model.Empresa);
                        cmd.Parameters.AddWithValue("@Jornada", model.Jornada);
                        cmd.Parameters.AddWithValue("@Description", model.Description);
                        cmd.Parameters.AddWithValue("@Imagen", model.Imagen);
                        cmd.Parameters.AddWithValue("@Latitude", model.Latitude);
                        cmd.Parameters.AddWithValue("@Longitude", model.Longitude);
                        cmd.Parameters.AddWithValue("@ID", model.ID);
                        newID = cmd.ExecuteScalar();
                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Messege = "El Puesto fue Editado con exito",
                    Result = model
                };

            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = $"Se Genero un errror al tratar de actualizar el Puesto :{ex.Message}",
                    Result = null
                };
            }
        }
        
        //Eliminar
        public ApiResponse Delete(int id)
        {

            try
            {
                object newID;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string tsql = "DELETE FROM Job WHERE ID = @ID"; ;
                    using (SqlCommand cmd = new SqlCommand(tsql, conn))
                    {

                        cmd.Parameters.AddWithValue("@ID", id);
                        newID = cmd.ExecuteScalar();

                    }
                }
                return new ApiResponse
                {
                    IsSuccess = true,
                    Messege = "El Puesto fue eliminado con exito",
                    Result = id
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Messege = $"Se Genero un errror al tratar de eliminar el Puesto :{ex.Message}",
                    Result = null
                };
            }
        }

        
        #endregion
    }
}
