using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CodeDao.MOV.Models
{
   public class User
    {
        [JsonProperty("Email")]
        public string Email { get; set; }
        public string Password { get; set; }

        public string Id { get; set; }
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
        public int Gender { get; set; }
        public string UrlImage { get; set; }
        public string AboutMe { get; set; }
        public string IdFb { get; set; }
        public string IdGg { get; set; }
        public string IdTags { get; set; }
        public string Career { get; set; }
        public long Birthday { get; set; }
    }
}
