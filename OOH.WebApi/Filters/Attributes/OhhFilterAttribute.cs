using Microsoft.AspNetCore.Mvc;
using OOH.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Filters.Attributes
{
    public class OhhFilterAttribute : TypeFilterAttribute
    {
        public OhhFilterAttribute(string permission, ActionPermission action) : base(typeof(VivaFilter))
        {
            Arguments = new object[]
            {
                action,
                permission
            };
        }

        public OhhFilterAttribute() : base(typeof(VivaFilter))
        {

        }
    }
}
