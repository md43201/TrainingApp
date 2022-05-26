using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Shared.DataModels
{
    public class Person
    {
        public Person(string accountNumber,
                        string lastName,
                        string firstName,
                        string emailAddress)
        {
            AccountNumber = accountNumber;
            LastName = lastName;
            FirstName = firstName;
            EmailAddress = emailAddress;

            PhoneNumbers = new List<PhoneNumber>();
        }

        public int PersonId { get; set; }
        public string AccountNumber { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string EmailAddress { get; }
        public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }  

        public string FullName
        {
            get
            {
                string firstName = !string.IsNullOrEmpty(FirstName) ? $"{FirstName} " : string.Empty;
                return $"{firstName}{LastName}";
            }
        }

        public void AddPhoneNumber(PhoneNumber phoneNumber)
        {
            var phoneNumbers = PhoneNumbers.ToList();
            phoneNumbers.Add(phoneNumber);
            PhoneNumbers = phoneNumbers.ToArray();
        }
    }
}
