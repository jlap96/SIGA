using Dapper;
using MySql.Data.MySqlClient;
using SIGA.Models;

namespace SIGA.Services
{
    public interface IRepositorioGrupos
    {
        Task Actualizar(Grupos grupos);
        Task Borrar(int id);
        Task Crear(Grupos grupos);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Grupos>> Obtener();
        Task<Grupos> ObtenerPorId(int id);
    }
    public class RepositorioGrupos: IRepositorioGrupos
    {
        private readonly string connectionString;

        public RepositorioGrupos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Grupos grupos)
        {
            using var connection = new MySqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO grupos (Id, Nombre, Titular) VALUES (@Id, @Nombre, @Titular);
                                                   SELECT LAST_INSERT_ID();", grupos);

            grupos.Id = id;

        }

        public async Task<bool> Existe(string nombre)
        {
            using var connection = new MySqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                            @"SELECT 1
                                            FROM grupos
                                            WHERE Nombre = @nombre;",
                                            new { nombre });
            return existe == 1;
        }

        public async Task<IEnumerable<Grupos>> Obtener()
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryAsync<Grupos>(@"SELECT Id, Nombre, Titular FROM grupos");
        }

        public async Task<Grupos> ObtenerPorId(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Grupos>(@"
                                                                     SELECT *
                                                                     FROM grupos
                                                                     WHERE Id = @Id",
                                                                     new { id });

        }

        public async Task Actualizar(Grupos grupos)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE grupos
                                            SET Nombre = @Nombre, Titular = @Titular
                                            WHERE Id = @id;", grupos);
        }


        public async Task Borrar(int id)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM grupos WHERE Id = @id;", new { id });
        }
    }
}
