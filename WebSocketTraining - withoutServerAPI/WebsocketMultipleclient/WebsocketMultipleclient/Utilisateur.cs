using System.ComponentModel.DataAnnotations;

namespace WebSocketServerProject.Modeles.Domaine
{
    public class Utilisateur
    {
        public Utilisateur(string name, string login, string password)
        {
            Name = name;
            Login = login;
            Password = password;
        }
        public Utilisateur( string login, string password)
        {
      
            Login = login;
            Password = password;
        }


        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
