using SAI.Domain.Interfaces;
using SAI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Entities
{
    internal class Organization : User
    {
        private readonly List<Individual> _members;
        public IReadOnlyList<Individual> Members => _members.AsReadOnly();
        public Organization(string fullName, Email email, PhoneNumber phoneNumber, TelegramNickName telegramNickName)
                         : base(fullName, email, phoneNumber, telegramNickName) { }
        public void AddMember(Individual member) => _members.Add(member);
        public void RemoveMember(int index) => _members.RemoveAt(index);
    }
}
