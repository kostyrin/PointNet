﻿using System;
using System.Web.Mvc;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Data.Repositories;
using PointNet.Web.ViewModels;
using PointNet.Web.Helpers;
using PointNet.Model.Commands;
using PointNet.Web.Core.ActionFilters;
using PointNet.Web.Core.Models;
using PointNet.Model;
using AutoMapper;

using System.Security.Permissions;
using System.Threading;
using Microsoft.IdentityModel.Claims;
using System.Security;

namespace PointNet.Web.Controllers
{
    [CompressResponse]
    [PointNetAuthorize(Roles.User, Roles.Admin)]
    public class ExpenseController : Controller
    {
        private readonly IMappingEngine mapper;
        private readonly ICommandBus commandBus;
        private readonly ICategoryRepository categoryRepository;
        private readonly IExpenseRepository expenseRepository;

        public ExpenseController(ICommandBus commandBus, IMappingEngine mapper, ICategoryRepository categoryRepository, IExpenseRepository expenseRepository)
        {
            this.commandBus = commandBus;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.expenseRepository = expenseRepository;
        }

        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            //If date is not passed, take current month's first and last dte 
            DateTime dtNow;
            dtNow = DateTime.Today;
            if (!startDate.HasValue)
            {
                startDate = new DateTime(dtNow.Year, dtNow.Month, 1);
                endDate = startDate.Value.AddMonths(1).AddDays(-1);
            }
            //take last date of start date's month, if end date is not passed 
            if (startDate.HasValue && !endDate.HasValue)
            {
                endDate = (new DateTime(startDate.Value.Year, startDate.Value.Month, 1)).AddMonths(1).AddDays(-1);
            }
            var expenses = expenseRepository.GetMany(exp => exp.Date >= startDate && exp.Date <= endDate);
            //if request is Ajax will return partial view
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ExpenseList", expenses);
            }
            //set start date and end date to ViewBag dictionary
            ViewBag.StartDate = startDate.Value.ToShortDateString();
            ViewBag.EndDate = endDate.Value.ToShortDateString();
            //if request is not ajax
            return View(expenses);
        }

        public ActionResult Create()
        {
            var viewModel = new ExpenseFormModel();
            var categories = categoryRepository.GetAll();
            viewModel.Category = categories.ToSelectListItems(-1);
            viewModel.Date = DateTime.Today;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ExpenseFormModel form)
        {
            Category category = categoryRepository.GetById(form.CategoryId);

            if (ModelState.IsValid)
            {
                var command = new CreateOrUpdateExpenseCommand
                {
                    ExpenseId = form.ExpenseId,
                    Category = category,
                    Date = form.Date,
                    TransactionDesc = form.Transaction,
                    Amount = form.Amount
                };
                var result = commandBus.Submit(command);
                if (result.Success) return RedirectToAction("Index");
            }
            
            if (form.ExpenseId == 0)
                return View("Create", form);
            else
                return View("Edit", form);
        }

        // GET: /Expense/Edit
        public ActionResult Edit(int id)
        {
            var expense = expenseRepository.GetById(id);
            var viewModel = new ExpenseFormModel(expense);
            var categories = categoryRepository.GetAll();
            viewModel.Category = categories.ToSelectListItems(expense.Category.CategoryId);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var command = new DeleteExpenseCommand { ExpenseId = id };
            var result = commandBus.Submit(command);
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            var expenses = expenseRepository.GetMany(exp => exp.Date >= startDate && exp.Date <= endDate);
            return PartialView("_ExpenseList", expenses);
        }
    }
}
