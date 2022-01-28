using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWpf.Model
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime LastEnter { get; set; }
        public DateTime DateCreated { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }


        public string ClientFullName
        {
            get => $"{MiddleName} {FirstName} {LastName}";
        }
    }
}
