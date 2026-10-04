using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
    public class Expense
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public decimal Amount { get; set; }
        public string Category { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
