using System;
using System.ComponentModel.DataAnnotations;

namespace BudzetDomowyApp.Models
{
    public class PlanowanyWydatek
    {
        public int Id { get; set; }

        [Required]
        public decimal Kwota { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }


        [Required]
        public string Kategoria { get; set; }
    }
}
