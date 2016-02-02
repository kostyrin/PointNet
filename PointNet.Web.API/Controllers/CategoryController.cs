﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using PointNet.Data.Repositories;
using PointNet.Model;
using System.Net;
using AutoMapper;
using PointNet.Core.Common;
using PointNet.Model.Commands;
using PointNet.CommandProcessor.Command;
using PointNet.CommandProcessor.Dispatcher;

namespace PointNet.Web.API.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IMappingEngine mapper;
        private readonly ICommandBus commandBus;
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICommandBus commandBus, IMappingEngine mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.commandBus = commandBus;
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<DTOCategory> Get()
        {
            var categories = categoryRepository.GetAll();
            if (categories == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return mapper.Mapper.Map<List<DTOCategory>>(categories);
        }

        // GET /api/categoryservice/5
        public DTOCategory Get(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return mapper.Mapper.Map<DTOCategory>(category);
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Category value)
        {
            if (ModelState.IsValid)
            {
                //TODO: Create Automapper form => command
                var command = mapper.Mapper.Map<CreateOrUpdateCategoryCommand>(value);

                IEnumerable<ValidationResult> errors = commandBus.Validate(command);

                var result = commandBus.Submit(command);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/values/5
        public HttpResponseMessage Put([FromBody]Category value)
        {
            if (ModelState.IsValid)
            {
                //TODO: Create Automapper form => command
                var command = mapper.Mapper.Map<CreateOrUpdateCategoryCommand>(value);

                IEnumerable<ValidationResult> errors = commandBus.Validate(command);

                var result = commandBus.Submit(command);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE /api/category/5
        public HttpResponseMessage Delete(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}