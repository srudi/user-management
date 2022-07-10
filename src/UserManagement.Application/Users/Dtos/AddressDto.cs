namespace UserManagement.Application.Users.Dtos
{
    public class AddressDto
    {
        public string Street { get; init; }
        public string Suite { get; init; }
        public string City { get; init; }
        public string Zipcode { get; init; }
        public GeoDto Geo { get; init; }
    }
}
