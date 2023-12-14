using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorFlow.Application.Identity.Models
{
    public class LoginResponseDTO
    {
        public string userName {  get; set; }
        public bool success { get; set; }
        public bool requires2FA { get; set; }
        public bool isLockedOut { get; set; }
        public string jwtToken { get; set; }
    }
}
