using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeorgianComputers.Models
{
    //Used to manage ROLES for authorization and inherits from IdentityRole
    public class ApplicationRole : IdentityRole
    {
    }
}
