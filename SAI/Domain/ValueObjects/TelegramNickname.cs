using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.ValueObjects
{
    public sealed class TelegramNickName
    {
        public string NickName { get; set; }
        public TelegramNickName(string nick) { NickName = nick; }
    }
}
