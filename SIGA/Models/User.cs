namespace SIGA.Models
{
    public class User
    {
        //Asignamos las propiedades de la clase user
        public int Id { get; set; } 
        public string email { get; set; }
        public string password { get; set; }

        public Role IdRole { get; set; }
    }
}
