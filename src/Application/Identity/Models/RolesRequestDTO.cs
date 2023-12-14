using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorFlow.Application.Identity.Models
{
    public sealed class RolesRequestDTO
    {
        public string userName {  get; set; }
        public List<string> roles { get; set; }
    }
}
