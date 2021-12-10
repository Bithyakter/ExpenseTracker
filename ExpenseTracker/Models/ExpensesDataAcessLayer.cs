using ExpenseTracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class ExpensesDataAcessLayer: IExpenseService
    {
        private ExpenseDBContext db;

        public ExpensesDataAcessLayer(ExpenseDBContext _db)
        {
            db = _db;
        }
        public IEnumerable<Expense> GetAllExpenses()
        {
            try
            {
                return db.Expenses.ToList();
            }
            catch
            {
                throw;
            }
        }

        // To filter out the records based on the search string 
        public IEnumerable<Expense> GetSearchResult(string searchString)
        {
            List<Expense> exp = new List<Expense>();
            try
            {
                exp = GetAllExpenses().ToList();
                return exp.Where(x => x.ExpenseType.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }

        //To Add new Expense record       
        public void AddExpense(Expense expense)
        {
            try
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar expense  
        public int UpdateExpense(Expense expense)
        {
            try
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the data for a particular expense  
        public Expense GetExpenseData(int id)
        {
            try
            {
                Expense expense = db.Expenses.Find(id);
                return expense;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular expense  
        public void DeleteExpense(int id)
        {
            try
            {
                Expense emp = db.Expenses.Find(id);
                db.Expenses.Remove(emp);
                db.SaveChanges();

            }
            catch
            {
                throw;
            }
        }

        // To calculate last six months expense  
        public Dictionary<string, decimal> CalculateMonthlyExpense()
        {
            List<Expense> lstEmployee = new List<Expense>();

            Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();

            decimal houseSum = db.Expenses.Where
                (cat => cat.Category == "House-Rent" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
                .Select(cat => cat.Amount)
                .Sum();

            decimal waterSum = db.Expenses.Where
               (cat => cat.Category == "Water-Bill" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal electricSum = db.Expenses.Where
               (cat => cat.Category == "Electric-Bill" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal groceriesSum = db.Expenses.Where
               (cat => cat.Category == "Groceries" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal uberSum = db.Expenses.Where
               (cat => cat.Category == "Uber" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal medicationsSum = db.Expenses.Where
               (cat => cat.Category == "Medications" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            dictMonthlySum.Add("House-Rent", houseSum);
            dictMonthlySum.Add("Water-Bill", waterSum);
            dictMonthlySum.Add("Electric-Bill", electricSum);
            dictMonthlySum.Add("Groceries", groceriesSum);
            dictMonthlySum.Add("Uber", uberSum);
            dictMonthlySum.Add("Medications", medicationsSum);

            return dictMonthlySum;
        }

        // To calculate last four weeks expense  
        public Dictionary<string, decimal> CalculateWeeklyExpense()
        {
            List<Expense> lstEmployee = new List<Expense>();

            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();

            decimal houseSum = db.Expenses.Where
                (cat => cat.Category == "House-Rent" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
                .Select(cat => cat.Amount)
                .Sum();

            decimal waterSum = db.Expenses.Where
               (cat => cat.Category == "Water-Bill" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal electricSum = db.Expenses.Where
               (cat => cat.Category == "Electric-Bill" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal groceriesSum = db.Expenses.Where
               (cat => cat.Category == "Groceries" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal uberSum = db.Expenses.Where
               (cat => cat.Category == "Uber" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal medicationsSum = db.Expenses.Where
               (cat => cat.Category == "Medications" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            dictWeeklySum.Add("House-Rent", houseSum);
            dictWeeklySum.Add("Water-Bill", waterSum);
            dictWeeklySum.Add("Electric-Bill", electricSum);
            dictWeeklySum.Add("Groceries", groceriesSum);
            dictWeeklySum.Add("Uber", uberSum);
            dictWeeklySum.Add("Medications", medicationsSum);

            return dictWeeklySum;
        }
    }
}
