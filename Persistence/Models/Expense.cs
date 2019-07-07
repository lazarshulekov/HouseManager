namespace Persistence.Models
{
    using System;
    using System.Collections.Generic;

    public class Expense
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<PropertiesExpenses> PropertiesExpenses { get; set; }
    }
}