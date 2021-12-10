﻿using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService _expenseService)
        {
            expenseService = _expenseService;
        }
        public IActionResult Index(string searchString)
        {
            List<Expense> lstEmployee = new List<Expense>();
            lstEmployee = expenseService.GetAllExpenses().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                lstEmployee = expenseService.GetSearchResult(searchString).ToList();
            }
            return View(lstEmployee);
        }

        public ActionResult AddEditExpenses(int expenseID)
        {
            Expense model = new Expense();
            if (expenseID > 0)
            {
                model = expenseService.GetExpenseData(expenseID);
            }
            return PartialView("_expenseForm", model);
        }

        [HttpPost]
        public ActionResult Create(Expense newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ExpenseID > 0)
                {
                    expenseService.UpdateExpense(newExpense);
                }
                else
                {
                    expenseService.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            expenseService.DeleteExpense(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }

        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = expenseService.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }

        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = expenseService.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}
