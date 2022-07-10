namespace UserManagement.Application.Users.Dtos
{
    public class UserDto
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public AddressDto Address { get; init; }
        public string Phone { get; init; }
        public string Website { get; init; }
        public CompanyDto Company { get; init; }
    }
}
