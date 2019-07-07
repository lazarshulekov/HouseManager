namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Persistence.Models;

    public class ExpencesService : IExpensesService
    {
        private readonly AppDbContext context;

        public ExpencesService(AppDbContext context)
        {
            this.context = context;
        }

        public List<PropertyExpense> GetExpencesPerUserName(string userName)
        {
            var expences = from c in this.context.AppUsers
                           join t in this.context.Properties on c.Id equals t.AppUserId
                           join o in this.context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in this.context.Expenses on o.ExpenseId equals e.Id
                           join k in this.context.PropertyTypes on t.PropertyTypeId equals k.Id
                           where c.Email == userName
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k };

            return expences.ToList();
        }

        public async Task AddAsync(PropertyExpense expense)
        {
            this.context.PropertiesExpenses.Add(
                new PropertiesExpenses()
                    {
                        DueDate = DateTime.UtcNow,
                        Expense = expense.Expense,
                        ExpenseId = expense.Expense.Id,
                        Property = expense.Property,
                        PropertyId = expense.Property.Id
                    });
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Building bld)
        {
            this.context.Buildings.Update(bld);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Building bld)
        {
            this.context.Remove(bld);
            await this.context.SaveChangesAsync();
        }
    }
}