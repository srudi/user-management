using UserManagement.Domain.Entities;

namespace UserManagement.Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; init; }
        public string Suite { get; init; }
        public string City { get; init; }
        public string Zipcode { get; init; }
        public Geo Geo { get; init; }
    }
}
