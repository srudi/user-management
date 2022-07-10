using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Entities
{
    public class User
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public Address Address { get; init; }
        public string Phone { get; init; }
        public string Website { get; init; }
        public Company Company { get; init; }
    }
}
