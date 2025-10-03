using SAI.Domain.Enums;
using SAI.Domain.Interfaces;
using SAI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Entities
{
    public  class Invitation 
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid InviterId {  get; private set; }
        public Guid InviteeId { get; private set; }
        public Guid? OrganizationId { get; private set; }
        public Guid? EventId { get; private set; }
        public InviteStatus Status { get; private set; } = InviteStatus.Waiting;

        public Invitation(Guid inviterId, Guid inviteeId, Guid? organizationId = null, Guid? eventId = null) 
        {
            InviterId = inviterId;
            InviteeId = inviteeId;
            OrganizationId = organizationId;
            EventId = eventId;
        }

        public static Invitation CreateForOrganization(Guid inviterId, Guid inviteeId, Guid organizationId) => new(inviterId, inviteeId, organizationId, null);
        public static Invitation CreateForEvent(Guid inviterId, Guid inviteeId, Guid eventId) => new(inviterId, inviteeId, null, eventId);
        public void AcceptInvite() => Status = InviteStatus.Accepted;
        public void DeclineInvite() => Status = InviteStatus.Declined;
    }
}
