using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW
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
