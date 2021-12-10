using ExpenseTracker.DateValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; }

        public string ExpenseType { get; set; }

        [Required]
        [Column(TypeName = "Expense Category")]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required, Column(TypeName = "date"), FutureDateValidation(ErrorMessage = "You can't select Future Expense Date"), Display(Name = "Expense Date")]
       
        //[Column(TypeName = "Expense Date")]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        

        
    }
}
