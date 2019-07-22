namespace HouseManager.ViewModels
{
    using System;
    using System.ComponentModel;

    public class ExpenseViewModel
    {
        public int ExpenseId { get; set; }

        public string Expense { get; set; }
        
        public int BuildingId { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreationDate { get; set; }

        [DisplayName("Building")]
        public IdNameViewModel BuildingView { get; set; }

        public int PropertyId { get; set; }

        [DisplayName("Property")]
        public IdNameViewModel PropertyViewModel { get; set; }
    }
}