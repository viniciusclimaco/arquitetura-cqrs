using EmergingBooking.Infrastructure.CQRS.Command;

namespace EmergingBooking.Management.Appication.Commands
{
    public class CreateHotel : ICommand
    {           
        public CreateHotel(string name, int starsOfCategory, string street, string district, string city, string country, int zipCode, string email, string phone, string mobile)
        {
            Name = name;
            StarsOfCategory = starsOfCategory;
            Street = street;
            District = district;
            City = city;
            Country = country;
            ZipCode = zipCode;
            Email = email;
            Phone = phone;
            Mobile = mobile;
        }

        public string Name { get; }
        public int StarsOfCategory { get; }
        public string Street { get; }
        public string District { get; }
        public string City { get; }
        public string Country { get; }
        public int ZipCode { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Mobile { get; }


    }
}
