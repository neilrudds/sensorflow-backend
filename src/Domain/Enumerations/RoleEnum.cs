using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorFlow.Domain.Enumerations
{
    public enum RoleEnum
    {
        [Description("Workspace User")]
        User = 2,
        [Description("Workspace Adminstrator")]
        Admin = 4,
        [Description("Instance Owner")]
        Owner = 8
    }
}