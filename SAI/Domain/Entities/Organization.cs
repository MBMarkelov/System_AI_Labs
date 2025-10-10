using SAI.Domain.Interfaces;
using SAI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Entities
{
    public class Organization
    {
        public Guid OwnerUserId { get; private set; }
        
        private readonly List<User> _members;
        public IReadOnlyList<User> Members => _members.AsReadOnly();
        
        public Organization(List<User> members) 
        {
            _members = members;    
        }
        public void AddMember(User member) => _members.Add(member);
        public void RemoveMember(int index) => _members.RemoveAt(index);

    }
}
