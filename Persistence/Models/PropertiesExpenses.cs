namespace DAL.Models
{
    using System;

    public class PropertiesExpenses
    {
        public int ExpenseId { get; set; }

        public Expense Expense { get; set; }

        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public DateTime CreationDate { get; set; }
    }
}