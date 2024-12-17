namespace My_WebAPI.Models
{
    public class UserDbExample
    {
        public static List<User> Users = new ()
        {
            new User { Id = 1, Username = "admin", Password = "password", Email="admin@Example.com", Roles = new() { "Admin" } },
            new User { Id = 2, Username = "user", Password = "password", Email="user@Example.com", Roles = new() { "User" } }
        };
    }
}
