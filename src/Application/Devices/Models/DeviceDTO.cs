using SensorFlow.Application.Common.Models;
using SensorFlow.Application.Dashboards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorFlow.Application.Devices.Models
{
    public sealed class DeviceDTO : EntityDTO
    {
        public string Id { get; set; }
        public string? name { get; set; }
    }
}
