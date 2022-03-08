using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string Role { get; set; }
        public string DateOfRelease { get; set; }
        public string Photo { get; set; }
    }
}
