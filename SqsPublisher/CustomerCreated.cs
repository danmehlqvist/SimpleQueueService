using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqsPublisher
{
    public class CustomerCreated
    {


        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GitHubUsername { get; set; }


        private CustomerCreated() { }

        public CustomerCreated(string email, string fullName, DateTime dateOfBirth, string gitHubUsername)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            GitHubUsername = gitHubUsername;
        }
    }
}
