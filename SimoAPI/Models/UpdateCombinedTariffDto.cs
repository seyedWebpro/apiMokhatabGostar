using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Models
{
    public class UpdateCombinedTariffDto
    {
        public string? Title { get; set; } // nullable

        public string? TariffNames { get; set; } // nullable

        public decimal? Price { get; set; } // nullable
    }
}