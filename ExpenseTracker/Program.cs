using ExpenseTracker;
using ExpenseTracker.IRepository;

IExpenseService expenseService = new ExpenseService();
while (true)
{
    Console.WriteLine("Expense Tracker");
    Console.WriteLine("1. Add Expense");
    Console.WriteLine("2. View All Expenses");
    Console.WriteLine("3. View Total Expenses");
    Console.WriteLine("4. Remove Expense");
    Console.WriteLine("5. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            AddExpense(expenseService);
            break;
        case "2":
            ViewAllExpenses(expenseService);
            break;
        case "3":
            ViewTotalExpenses(expenseService);
            break;
        case "4":
            RemoveExpense(expenseService );
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}
static void AddExpense(IExpenseService expenseService)
{
    Console.Write("Enter title: ");
    var title = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Title cannot be empty.");
        return;
    }
    Console.Write("Enter amount: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Amount must be a number greater than 0.");
        return;
    }

    Console.Write("Enter category: ");
    var category = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(category))
    {
        Console.WriteLine("Category cannot be empty.");
        return;
    }
    var expense = new Expense
    {
        Title = title.Trim(),
        Amount = amount,
        Category = category.Trim(),
        Date = DateTime.Now
    };
    expenseService.AddExpense(expense);
    Console.WriteLine("Expense added successfully.");
}
static void ViewAllExpenses(IExpenseService expenseService)
{
    var expenses = expenseService.GetAllExpenses();
    if (expenses.Count == 0)
    {
        Console.WriteLine("No expenses found.");
        return;
    }
    Console.WriteLine("All Expenses:");
    foreach (var expense in expenses)
    {
        Console.WriteLine($"ID: {expense.Id}, Title: {expense.Title}, Amount: {expense.Amount}, Category: {expense.Category}, Date: {expense.Date}");
    }
}
static void ViewTotalExpenses(IExpenseService expenseService)
{
    var total = expenseService.GetTotalExpenses();
    Console.WriteLine($"Total Expenses: {total}");
}
static void RemoveExpense(IExpenseService expenseService)
{
    Console.Write("Enter the ID of the expense to remove: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid ID. Please enter a valid number.");
        return;
    }
 
    if (expenseService.RemoveExpense(id))
    {
        Console.WriteLine("Expense removed successfully.");
    }
    else
    {
        Console.WriteLine("Expense not found.");
    }
}