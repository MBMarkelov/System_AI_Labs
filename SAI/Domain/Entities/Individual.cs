using SAI.Domain.Interfaces;
using SAI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Entities
{
    internal class Individual : User
    {
        public Individual(string fullName, Email email, PhoneNumber phoneNumber, TelegramNickName telegramNickName)
                : base(fullName, email, phoneNumber, telegramNickName) { }

    }
}
