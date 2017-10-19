using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class SearchFlight
    {
        [Required(ErrorMessage = "Dato")]
        public String Date { get; set; }
        [Required(ErrorMessage = "Fra")]
        public String From { get; set; }
        [Required(ErrorMessage = "Til")]
        public String To { get; set; }
    }
}
