namespace Shared.Contracts
{
    public class CustomerCreated
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GitHubUsername { get; set; }

        public CustomerCreated(string email, string fullName, DateTime dateOfBirth, string gitHubUsername)
        {
            Id = Guid.NewGuid();
            Email = email ?? throw new ArgumentNullException(nameof(email));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            DateOfBirth = dateOfBirth;
            GitHubUsername = gitHubUsername ?? throw new ArgumentNullException(nameof(gitHubUsername));
        }
    }
}
