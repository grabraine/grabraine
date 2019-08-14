namespace WebApi.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string UserRole {get;set;}
        public string DatabaseAccess {get; set;}
        public string Password { get; set; }
        public string Longitude {get; set;}
        public string Lattitude {get; set;}
    
    }
}