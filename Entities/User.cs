namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserRole {get;set;}
        public string DatabaseAccess {get; set;}
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Lattitude {get; set;}
        public string Longitude {get; set;}
     
    }
}