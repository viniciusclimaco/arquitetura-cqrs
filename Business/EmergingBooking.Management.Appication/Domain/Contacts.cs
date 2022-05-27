using EmergingBooking.Infrastructure.CQRS.Domain;
using MonoidSharp;
using System.Text.Json.Serialization;

namespace EmergingBooking.Management.Appication.Domain
{
    internal class Contacts : ValueObject
    {
        [JsonConstructor]
        public Contacts(string email, string phone, string mobile)
        {
            Phone = phone;
            Email = email;
            Mobile = mobile;            
        }

        public string Email { get; }
        public string Phone { get; }
        public string Mobile { get; }        

        public static Outcome<Contacts> Create(PossibleBe<string> email,
                                              PossibleBe<string> mobile,
                                              PossibleBe<string> phone)
        {
            if (IsValidContact(email, phone, mobile))            
                return Outcome.Successfully(new Contacts(email.Value, phone.Value, mobile.Value));
            
            return Outcome.Failed<Contacts>("The Contacts is invalid. Please verify all contacts values!");
        }

        private static bool IsValidContact(PossibleBe<string> email, PossibleBe<string> phone, PossibleBe<string> mobile)
        {
            return phone.HasValue &&
                   email.HasValue &&
                   mobile.HasValue;
        }

        protected override IEnumerable<object> GetEqualityProperties()
        {
            yield return Phone;
            yield return Email;
            yield return Mobile;            
        }

        public override string ToString()
        {
            return $"{Email}{Environment.NewLine}," +
                   $"{Mobile}{Environment.NewLine}," +                   
                   $"{Phone}{Environment.NewLine},";
        }
    }
}
