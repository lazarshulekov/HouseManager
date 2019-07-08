namespace BLL.Models
{
    using DAL.Models;

    public class PropertyExpense
    {
        public PropertyType PropertyType { get; set; }

        public Property Property { get; set; }

        public Expense Expense { get; set; }

        public Building Building { get; set; }
    }
}