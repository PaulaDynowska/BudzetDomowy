using System;
using System.ComponentModel.DataAnnotations;

namespace BudzetDomowyApp.Models
{
    public class Operacja
    {
        public int Id { get; set; }
        public bool CzyUsunieta { get; set; } = false;


        [Required]
        public string Kategoria { get; set; }

        [Required]
        [Range(typeof(decimal), "-1000000", "1000000", ErrorMessage = "Kwota musi mieścić się w zakresie -1 000 000 do 1 000 000.")]
        public decimal Kwota { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}
