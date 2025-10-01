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
        private int _id;
        private string _name;
        private Email _email;
        private PhoneNumber _phoneNumber;
        private List<Address> _Addresses;
        private TelegramNickName _telegramNickName;

    }    
}
