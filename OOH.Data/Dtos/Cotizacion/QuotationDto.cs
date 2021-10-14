using OOH.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Data.Dtos.Cotizacion
{
    public class QuotationDto: Cotizaciones
    {

        public List<QuotationDetailDto> LstCaras { get; set; }

    }
}
