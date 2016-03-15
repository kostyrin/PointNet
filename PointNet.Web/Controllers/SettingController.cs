using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PointNet.CommandProcessor.Dispatcher;
using PointNet.Core.Common;
using PointNet.Data.Repositories;
using PointNet.Model.Commands;
using PointNet.Web.Core.Extensions;
using PointNet.Web.Core.Models;

namespace PointNet.Web.Controllers
{
    public class SettingController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IMappingEngine _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerSettingRepository _customerSettingRepository;

        public SettingController(ICommandBus command
            , IMappingEngine mapper
            , ICustomerRepository customerRepository
            , ICustomerSettingRepository customerSettingRepository)
        {
            _commandBus = command;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _customerSettingRepository = customerSettingRepository;
        }

        //// GET: Setting
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(int id)
        {
            var setting = _customerSettingRepository.Get(s => s.Customer.CustomerId == id);
            var vm = _mapper.Mapper.Map<CustomerSettingFormModel>(setting);
            if (vm == null) vm = new CustomerSettingFormModel() { CustomerId = id };
            return View(vm);
        }

        public ActionResult Create(int id)
        {
            var customer = _customerRepository.GetById(id);
            var vm = new CustomerSettingFormModel { CustomerId = customer.CustomerId, CustomerName = customer.Name };
            return View(vm);
        }

        public ActionResult Edit(int id)
        {
            var setting = _customerSettingRepository.Get(s => s.Customer.CustomerId == id);
            var vm = _mapper.Mapper.Map<CustomerSettingFormModel>(setting);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Save(CustomerSettingFormModel fm)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Mapper.Map<CreateOrUpdateCustomerSettingCommand>(fm);
                command.Customer = _customerRepository.GetById(fm.CustomerId);
                //IEnumerable<ValidationResult> errors = _commandBus.Validate(command);
                //ModelState.AddModelErrors(errors);
                if (ModelState.IsValid)
                {
                    var result = _commandBus.Submit(command);
                    if (result.Success) return RedirectToAction("Index", new { id = fm.CustomerId });
                }
            }

            return View(fm.CustomerSettingId == 0 ? "Create" : "Edit", fm);
        }
    }
}