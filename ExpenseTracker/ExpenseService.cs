using ExpenseTracker.IRepository;
using System.Text.Json;

namespace ExpenseTracker
{
    
    public class ExpenseService : IExpenseService
    {
        private readonly string _filePath = "expenses.json";
        private readonly List<Expense> _expenses ;
        private int _nextId=1;

        public ExpenseService()
        {
            // Initialize with some sample expenses
            _expenses = new List<Expense>();
            LoadExpensesFromFile();
        }
        public void AddExpense(Expense expense)
        {
            expense.Id = _nextId++;
            _expenses.Add(expense);
            SaveExpensesToFile();
        }

        public List<Expense> GetAllExpenses()
        {
            return _expenses.ToList();
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
           return _expenses
                .GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
        }

        public decimal GetTotalExpenses()
        {
            return _expenses.Sum(e => e.Amount);
        }

        private void LoadExpensesFromFile()
        {
            if (!File.Exists(_filePath))
                return;

            try
            {
                var json = File.ReadAllText(_filePath);
                var expensesFromFile = JsonSerializer.Deserialize<List<Expense>>(json);

                if (expensesFromFile != null)
                {
                    _expenses.Clear();
                    _expenses.AddRange(expensesFromFile);
                    _nextId = _expenses.Any() ? _expenses.Max(e => e.Id) + 1 : 1;
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("Data file is corrupted. Starting with an empty list.");
            }
        }

        public bool RemoveExpense(int id)
        {
         var expenseToRemove = _expenses.FirstOrDefault(e => e.Id == id);
            if (expenseToRemove != null)
            {
                _expenses.Remove(expenseToRemove);
                SaveExpensesToFile();
                return true;
            }
            return false;
        }

        private void SaveExpensesToFile()
        {
            if(!File.Exists(_filePath))
            {
                File.Create(_filePath).Dispose();
            }
            var json = JsonSerializer.Serialize(_expenses);
            File.WriteAllText(_filePath, json);
        }
    }
}
