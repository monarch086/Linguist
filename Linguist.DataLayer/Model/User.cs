using System;

namespace Linguist.DataLayer.Model
{
    public class User
    {
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
