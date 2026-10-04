using ExpenseTracker.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
    
    public class ExpenseService : IExpenseService
    {
        private readonly List<Expense> _expenses ;
        private int _nextId=1;

        public ExpenseService()
        {
            // Initialize with some sample expenses
            _expenses = new List<Expense>();
        }
        public void AddExpense(Expense expense)
        {
            expense.Id = _nextId++;
            _expenses.Add(expense);
        }

        public List<Expense> GetAllExpenses()
        {
            return _expenses.ToList();
        }

        public decimal GetTotalExpenses()
        {
            return _expenses.Sum(e => e.Amount);
        }

        public bool RemoveExpense(int id)
        {
         var expenseToRemove = _expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToRemove != null)
            {
                _expenses.Remove(expenseToRemove);
                return true;
            }
            return false;
        }
    }
}
