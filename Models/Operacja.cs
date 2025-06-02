using System;
using System.ComponentModel.DataAnnotations;

namespace BudzetDomowyApp.Models
{
    public class Operacja
    {
        public int Id { get; set; }

        [Required]
        public string Kategoria { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Kwota musi być większa niż 0.")]
        public decimal Kwota { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
