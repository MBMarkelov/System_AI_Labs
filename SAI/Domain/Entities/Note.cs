using SAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Entities
{
    public class Note
    {
        public string NoteId { get; set; }
        public string Title {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
    }
}
