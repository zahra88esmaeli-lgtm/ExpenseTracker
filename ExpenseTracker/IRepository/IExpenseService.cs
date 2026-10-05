using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker.IRepository
{
    public interface IExpenseService
    {
        void AddExpense(Expense expense);
        bool RemoveExpense(int id);
        List<Expense> GetAllExpenses();

        decimal GetTotalExpenses();
        Dictionary<string, decimal> GetExpensesByCategory();
     

    }
}
