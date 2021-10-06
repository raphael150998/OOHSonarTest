using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Models.Site
{
    /// <summary>
    /// DTO de entrada para la libreria select2.js
    /// </summary>
    public class SiteSelect2InputDto
    {
        public string term { get; set; } = "";
        public string q { get; set; }
        public string _type { get; set; }
        public int page { get; set; } = 1;
    }
}
