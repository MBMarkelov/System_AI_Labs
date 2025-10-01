using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAI.Domain.ValueObjects;

namespace SAI.Domain.Interfaces
{
    public abstract class User
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string FullName { get; protected set; }
        public Email Email { get; protected set; }
        public PhoneNumber PhoneNumber { get; protected set; }
        public TelegramNickName TelegramNickName { get; protected set; }

        private List<Address> _addresses = new();
        public IReadOnlyList<Address> Addresses => _addresses.AsReadOnly(); 


        protected User(string fullName, Email email, PhoneNumber phoneNumber, TelegramNickName telegramNickName)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            TelegramNickName = telegramNickName ?? throw new ArgumentNullException(nameof(telegramNickName));
        }
        public  string ChangeFullName(string NewFullName) => FullName = NewFullName;
        public  Email ChangeEmail(Email NewEmail) => Email = NewEmail;
        public  void ChangeAddresses(int index, Address ChangedAddress)
        {
            if (index < 0 || index >= Addresses.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            _addresses[index] = ChangedAddress ?? throw new ArgumentOutOfRangeException(nameof(index));
        }
        public  TelegramNickName ChangeTelegramNickName(TelegramNickName NewTelegramNickName) => TelegramNickName = NewTelegramNickName;
        public  void AddAddress(Address NewAddress) => _addresses.Add(NewAddress);
        public  void RemoveAddress(int index) => _addresses.RemoveAt(index);


    }    
}
