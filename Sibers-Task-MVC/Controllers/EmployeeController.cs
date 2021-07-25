using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Entities;
using Services.Services;
using TestSibers.Models;

namespace TestSibers.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeServiceEntity, EmployeeModel>();
                cfg.CreateMap<EmployeeModel, EmployeeServiceEntity>();
            });
            _mapper = config.CreateMapper();
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View(_mapper.Map<ICollection<EmployeeServiceEntity>, ICollection<EmployeeModel>>(_employeeService.GetAll()).ToList());
        }

        public ActionResult NewEmployee(EmployeeModel employee)
        {
            return View(employee);
        }

        public RedirectResult Delete(int id)
        {
            _employeeService.Delete(id);
            return Redirect("~/Employee");
        }

        public RedirectResult Create(EmployeeModel employee)
        {
            _employeeService.Add(_mapper.Map<EmployeeModel,EmployeeServiceEntity>(employee));
            return Redirect("~/Employee");
        }

        public ActionResult Edit(int id, EmployeeModel employee)
        {
            return View(_mapper.Map<EmployeeServiceEntity, EmployeeModel>(_employeeService.GetById(id)));
        }

        public RedirectResult Save(EmployeeModel employee)
        {
            _employeeService.Update(_mapper.Map<EmployeeModel,EmployeeServiceEntity>(employee));
            return Redirect("~/Employee");
        }
    }
}