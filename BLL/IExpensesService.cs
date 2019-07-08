using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    using BLL.Models;

    using DAL.Models;

    public interface IExpensesService
    {
        Task AddAsync(PropertyExpense expense);

        Task TogglepaymentAsync(int expenseId);

        Task DeleteByIdAsync(int expenseId);

        List<PropertyExpense> GetExpensesPerUserName(string userName);

        Task UpdateAsync(Building bld);

        PropertyExpense GetExpenseById(int propertyExpenseId);

        Task<List<PropertyExpense>> GetAllExpenses();
    }
}