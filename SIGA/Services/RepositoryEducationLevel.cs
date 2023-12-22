using Dapper;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using SIGA.Models;

namespace SIGA.Services
{
    public interface IRepositoryEducationLevel
    {
        Task Actualizar(EducationLevel educationLevel);
        Task Borrar(int id);
        Task Crear(EducationLevel educationLevel);
        Task<IEnumerable<EducationLevel>> Obtener();
        Task<EducationLevel> ObtenerPorId(int id);
    }
    public class RepositoryEducationLevel: IRepositoryEducationLevel
    {
        private readonly string connectionString;

        public RepositoryEducationLevel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(EducationLevel educationLevel)
        {
            using var connection = new MySqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO niveleducativo (Nombre, CostoInscripcion,CostoColegiatura) 
                                                                VALUES (@Nombre, @CostoInscripcion, @CostoColegiatura);
                                                                SELECT LAST_INSERT_ID();", educationLevel);

            educationLevel.Id = id;
        }
/*
        public async Task<bool> Existe(string nombre)
        {
            using var connection = new MySqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                            @"SELECT 1
                                            FROM niveleducativo
                                            WHERE Nombre = @nombre;",
                                            new { nombre });
            return existe == 1;
        }
*/
        public async Task<IEnumerable<EducationLevel>> Obtener()
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryAsync<EducationLevel>(@"SELECT * 
                                                                 FROM niveleducativo WHERE Estatus = 0;");
        }

        public async Task<EducationLevel> ObtenerPorId(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<EducationLevel>(@"
                                                                     SELECT *
                                                                     FROM niveleducativo
                                                                     WHERE Id = @Id",
                                                                     new { id });

        }

        public async Task Actualizar(EducationLevel educationLevel)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE niveleducativo
                                            SET Nombre = @Nombre, CostoInscripcion = @CostoInscripcion, CostoColegiatura = @CostoColegiatura
                                            WHERE Id = @id;", educationLevel);
        }


        public async Task Borrar(int id)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE niveleducativo  SET  Estatus = '1' WHERE Id = @id;", new { id });
            
        }


    }
}
