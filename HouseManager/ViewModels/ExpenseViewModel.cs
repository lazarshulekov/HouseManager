namespace HouseManager.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseViewModel
    {
        public int ExpenseId { get; set; }

        [Required]
        public string Expense { get; set; }
        
        [Required]
        public int BuildingId { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreationDate { get; set; }

        [DisplayName("Building")]
        public IdNameViewModel BuildingView { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [DisplayName("Property")]
        public IdNameViewModel PropertyViewModel { get; set; }
    }
}