using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Interfaces
{
    public interface IExpenseService
    {
        IEnumerable<Expense> GetAllExpenses();
        IEnumerable<Expense> GetSearchResult(string searchString);
        void AddExpense(Expense expense);
        int UpdateExpense(Expense expense);
        Expense GetExpenseData(int id);
        void DeleteExpense(int id);
        Dictionary<string, decimal> CalculateMonthlyExpense();
        Dictionary<string, decimal> CalculateWeeklyExpense();
    }
}
