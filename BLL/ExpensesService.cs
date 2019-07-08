namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLL.Models;

    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

    public class ExpensesService : IExpensesService
    {
        private readonly AppDbContext context;

        public ExpensesService(AppDbContext context)
        {
            this.context = context;
        }

        public List<PropertyExpense> GetExpensesPerUserName(string userName)
        {
            var user = context.AppUsers.Include(x => x.AppUsersRoles).ThenInclude(x => x.AppRole)
                .Single(au => au.Email == userName);
            var userRole = user.AppUsersRoles.Select(x => x.AppRole.Name).ToList();
            IQueryable<PropertyExpense> expenses;
            if (userRole.Contains("Administrator"))
            {
                expenses = from t in context.Properties.Include(x => x.AppUser)
                           join b in context.BuildingProperties on t.Id equals b.PropertyId
                           join o in context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in context.Expenses on o.ExpenseId equals e.Id
                           join k in context.PropertyTypes on t.PropertyTypeId equals k.Id
                           join bld in context.Buildings on b.BuildingId equals bld.Id
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k, Building = bld };

            }

            else if (userRole.Contains("HouseManager"))
            {
                expenses = from t in context.Properties
                           join b in context.BuildingProperties on t.Id equals b.PropertyId
                           join j in context.BuildingHousemanagers on b.BuildingId equals j.BuildingId
                           join o in context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in context.Expenses on o.ExpenseId equals e.Id
                           join k in context.PropertyTypes on t.PropertyTypeId equals k.Id
                           join bld in context.Buildings on b.BuildingId equals bld.Id
                           where j.HouseManagerId == user.Id
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k, Building = bld };
            }
            else
            {
                expenses = from c in context.AppUsers
                           join t in context.Properties on c.Id equals t.AppUserId
                           join b in context.BuildingProperties on t.Id equals b.PropertyId
                           join o in context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in context.Expenses on o.ExpenseId equals e.Id
                           join k in context.PropertyTypes on t.PropertyTypeId equals k.Id
                           join bld in context.Buildings on b.BuildingId equals bld.Id
                           where c.Email == userName
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k, Building = bld };
            }

            return expenses.OrderByDescending(x => x.Expense.CreationDate).ToList();
        }

        public async Task AddAsync(PropertyExpense expense)
        {
            context.PropertiesExpenses.Add(
                new PropertiesExpenses()
                    {
                        CreationDate = DateTime.UtcNow, Expense = expense.Expense, PropertyId = expense.Property.Id
                    });
            await context.SaveChangesAsync();
        }

        public async Task TogglepaymentAsync(int expenseId)
        {
            var expense = await context.Expenses.FindAsync(expenseId);
            expense.IsPaid = !expense.IsPaid;
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Building bld)
        {
            context.Buildings.Update(bld);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int expenseId)
        {
            var expense = await context.Expenses.FindAsync(expenseId);
            context.Remove(expense);
            await context.SaveChangesAsync();
        }

        public PropertyExpense GetExpenseById(int propertyExpenseId)
        {
            var expenses = from c in context.AppUsers
                           join t in context.Properties on c.Id equals t.AppUserId
                           join o in context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in context.Expenses on o.ExpenseId equals e.Id
                           join k in context.PropertyTypes on t.PropertyTypeId equals k.Id
                           where e.Id == propertyExpenseId
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k };
            return expenses.FirstOrDefault();
        }

        public async Task<List<PropertyExpense>> GetAllExpenses()
        {
            var expences = from c in context.AppUsers
                           join t in context.Properties on c.Id equals t.AppUserId
                           join o in context.PropertiesExpenses on t.Id equals o.PropertyId
                           join e in context.Expenses on o.ExpenseId equals e.Id
                           join k in context.PropertyTypes on t.PropertyTypeId equals k.Id
                           join p in context.BuildingProperties on t.Id equals p.PropertyId
                           join v in context.Buildings on p.BuildingId equals v.Id
                           select new PropertyExpense() { Expense = e, Property = t, PropertyType = k, Building = v };
            return await expences.ToListAsync();
        }
    }
}