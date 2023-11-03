using SIGA.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace SIGA.Services
{
    //Creamos una interface para que al implementar este repositorio, se implemente lo que contenga en la interface
    public interface IRepositoryUsers
    {
        User FindUser(string email, string password);
    }
    public class RepositoryUsers: IRepositoryUsers
    {
        //Agregamos la cadena de conexión 
        private readonly string connectionString;

        public RepositoryUsers(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        //Creamos el método que devolvera un objeto de "user" para acceder a la tabla usuarios
        public User FindUser(string email, string password)
        {
            //Declaramos el objeto tipo usuario
            User us = new User();

            //Utilizamos la cadena de conexión 
            using var connection = new MySqlConnection(connectionString);
            //Creamos el query para acceder a la tabla usuarios asignando los parametros email y password
            string query = "select * from usuarios where correo = @email and contrasena = @password";

            //Creamos el comando de ejecución y le asignamos la conexión y el query 
            MySqlCommand cmd = new MySqlCommand (query, connection); 

            //Asignamos los parametros al comando que estamos ejecutando 
            cmd.Parameters.AddWithValue ("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            //Indicamos que el tipo de comando es Text, no es store procedure u otro
            cmd.CommandType = CommandType.Text;

            //Abrimos la conexión 
            connection.Open();

            //Utilizamos el DataReader para poder leer la ejecución que tenemos desde mysql
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                //A través del ciclo while leemos la ejecución 
                while (reader.Read())
                {
                    //Actualizamos las propiedades del objeto
                    us = new User()
                    {
                        Id = (int)reader["Id"],
                        email = reader["correo"].ToString(),
                        password = reader["contrasena"].ToString(),
                        IdRole = (Role)reader["NivelUsuario_Id"],

                    };
                }
            }
            //Cerramos la conexión 
            connection.Close();
            //Retornamos el objeto 
            return us;
        }
        
    }
    
}
