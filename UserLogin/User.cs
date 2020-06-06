using System;

namespace UserLogin
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FakNum { get; set; }
        public int Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime? ActiveTo { get; set; }

        public override string ToString()
        {
            return Username + " " + ((UserRole)Role) + " " + ActiveTo;
        }
    }
}
