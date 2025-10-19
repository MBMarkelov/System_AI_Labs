using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Application.DTOs
{
    public class AddressDto
    {
        public string House {  get; set; }
        public string Street {  get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public string? PostalIndex { get; }
        public string? Country { get; }
    }
}
