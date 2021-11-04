using shop.Models;

namespace shop.Repositories
{
    public static class UserRepository
    {
        public static User GetUser(string username,  string password)
        {
            var users = new List<User>();
            users.Add(new (1, "batman", "123", "manager"));
            users.Add(new (2, "robin", "321", "employee"));
            return users.Where( x => 
                x.Username.ToLower() == username.ToLower() && 
                x.Password == password)
                .FirstOrDefault();
        }
    }
}