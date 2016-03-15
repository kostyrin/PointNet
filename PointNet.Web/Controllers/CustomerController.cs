using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Core.Common;
using PointNet.Data.Repositories;
using PointNet.Model;
using PointNet.Model.Commands;
using PointNet.Web.Core.ActionFilters;
using PointNet.Web.Core.Extensions;
using PointNet.Web.Core.Models;

namespace PointNet.Web.Controllers
{
    [CompressResponse]
    [PointNetAuthorize(Roles.User, Roles.Admin)]
    public class CustomerController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IMappingEngine _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerSettingRepository _customerSettingRepository;

        public CustomerController(ICommandBus command
            , IMappingEngine mapper
            , ICustomerRepository customerRepository
            , ICustomerSettingRepository customerSettingRepository)
        {
            _commandBus = command;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerSettingRepository = customerSettingRepository;
        }
        // GET: Customer
        public ActionResult Index()
        {
            var customers = _customerRepository.GetAll();
            var customersFormModel = _mapper.Mapper.Map<IEnumerable<CustomerFormModel>>(customers);
            return View(customersFormModel);
        }

        public ActionResult Create()
        {
            var model = new CustomerFormModel() { IsActive = true };
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Create(CustomerFormModel fm)
        //{
        //    var command = _mapper.Mapper.Map<CreateOrUpdateCustomerCommand>(fm);
        //    //var cust = _mapper.Mapper.Map<Customer>(fm);
        //    //_customerRepository.Add(cust);
        //    var result = _commandBus.Submit(command);

        //    return RedirectToAction("Index");
        //}

        public ActionResult Edit(int id)
        {
            var vm = _mapper.Mapper.Map<CustomerFormModel>(_customerRepository.GetById(id));
            return View(vm);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormModel fm)
        {
            if (ModelState.IsValid)
            {

                var command = _mapper.Mapper.Map<CreateOrUpdateCustomerCommand>(fm);
                //IEnumerable<ValidationResult> errors = _commandBus.Validate(command);
                //ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = _commandBus.Submit(command);
                    if (result.Success) return RedirectToAction("Index");
                }

            }

            return View(fm.CustomerId == 0 ? "Create" : "Edit", fm);
        }

    }
}