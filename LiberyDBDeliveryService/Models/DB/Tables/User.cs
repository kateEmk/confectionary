namespace LibraryDatabaseCoffe.Models.DB.Tables
{
    public enum StatusUser 
    {
        Admin,
        Manager,
        User
    }
    public class User 
    {
        public int? UserId { get; set; }
        public string NameUser { get; set; }
        public string EmailUser { get; set; }
        public string Password { get; set; }
        public StatusUser Status { get; set; } = StatusUser.User;
        public float TotalSpent { get; set; } = 0;
        public User(int user_id, string user_name, string email, string password, short status, float total)
        {
            UserId = user_id;
            NameUser = user_name;
            EmailUser = email;
            Password = password;
            Status = (StatusUser)status;
            TotalSpent = total;
        }
        public int OrderId { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public User(string user_name, string email, string password)
        {
            NameUser = user_name;
            EmailUser = email;
            Password = password;
        }
    }
}
