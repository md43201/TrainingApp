using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Shared.DataModels
{
    public class PhoneNumber
    {
        public PhoneNumber(string phoneNumber,
                            PhoneNumberType phoneNumberType)
        {
            Number = phoneNumber;
            PhoneNumberType = phoneNumberType;
        }

        public string Number { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }
    }
}
