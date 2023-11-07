using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SIGA.Models;

namespace SIGA.Services
{
    public interface IRepositorioMaterias
    {
        Task Actualizar(Materias materias);
        Task Borrar(int id);
        Task Crear(Materias materias);
        Task<bool> Existe(string nombre);
        Task<IEnumerable<Materias>> Obtener();
        Task<Materias> ObtenerPorId(int id);
    }
    public class RepositorioMaterias: IRepositorioMaterias
    {
        private readonly string connectionString;

        public RepositorioMaterias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Materias materias)
        {
            using var connection = new MySqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO materias (Id, Nombre, Estatus) 
                                                   VALUES (@Id, @Nombre, @Estatus);
                                                   SELECT LAST_INSERT_ID();", materias);
            materias.Id = id;
        }

        public async Task<bool> Existe(string nombre)
        {
            using var connection = new MySqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                            @"SELECT 1
                                            FROM materias
                                            WHERE Nombre = @nombre;",
                                            new { nombre });
            return existe == 1;
        }

        public async Task<IEnumerable<Materias>> Obtener()
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryAsync<Materias>(@"SELECT Id, Nombre, Estatus FROM materias");
        }

        public async Task<Materias> ObtenerPorId(int id)
        {
            using var connection = new MySqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Materias>(@"
                                                                     SELECT *
                                                                     FROM materias
                                                                     WHERE Id = @Id",
                                                                     new { id });

        }

        public async Task Actualizar(Materias materias)
        {
            using var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE materias
                                            SET Nombre = @Nombre, Estatus = @Estatus
                                            WHERE Id = @id;", materias);
        }


        public async Task Borrar(int id)
        {
            var connection = new MySqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM materias WHERE Id = @id;", new { id });
        }
    }
}
