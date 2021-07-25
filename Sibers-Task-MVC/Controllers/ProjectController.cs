using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Entities;
using Services.Services;
using TestSibers.Models;

namespace TestSibers.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projectService, IEmployeeService employeeService)
        {
            _projectService = projectService;
            _employeeService = employeeService;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProjectServiceEntity, ProjectModel>();
                cfg.CreateMap<ProjectModel, ProjectServiceEntity>();
            });
            _mapper = config.CreateMapper();
        }

        public ActionResult Index(string searchFilter, DateTime? startDate, DateTime? endDate, int? fromPrior, int? toPrior, SortProject sort = 0)
        {
            var projects = _projectService.GetAllProject(searchFilter, sort);

            if (startDate != null && endDate != null)
                projects = projects.Where(c => c.StartDate >= startDate && c.EndDate <= endDate).ToList();

            if (fromPrior != null && toPrior != null)
                projects = projects.Where(c => c.Priority >= fromPrior && c.Priority <= toPrior).ToList();

            return View(_mapper.Map<ICollection<ProjectServiceEntity>, ICollection<ProjectModel>>(projects.ToList()));

        }

        public ActionResult NewProject(ProjectModel project)
        {
            return View(project);
        }

        public ActionResult Edit(int id, ProjectModel project)
        {

            return View(_mapper.Map<ProjectServiceEntity, ProjectModel>(_projectService.GetById(id)));
        }

        public ActionResult FullProject(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var project = _mapper.Map<ProjectServiceEntity, ProjectModel>(_projectService.GetById(id.Value));
            ViewBag.employees = _mapper.Map<ICollection<EmployeeServiceEntity>, ICollection<EmployeeModel>>(_projectService.GetAllEmployees(id.Value));
            if (project != null)
                return View(project);
            return null;
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
       

        public RedirectResult Create(ProjectModel project)
        {
            _projectService.Add(_mapper.Map<ProjectModel, ProjectServiceEntity>(project));
            return Redirect("~/Project");
        }

        public RedirectResult Delete(int id)
        {
            _projectService.Delete(id);
            return Redirect("~/Project");
        }

        public RedirectResult Bind(int? projectId, int? ProjectManagerId)
        {
            if (projectId != null && ProjectManagerId != null)
                _projectService.Bind(projectId.Value, ProjectManagerId.Value);

            return Redirect($"~/Project/FullProject?id={projectId}");
        }

        public RedirectResult UnBind(int projectId, int employeeId)
        {
            _projectService.UnBind(projectId, employeeId);
            return Redirect($"~/Project/FullProject?id={projectId}");
        }

        public RedirectResult Save(ProjectModel project)
        {
            _projectService.Update(_mapper.Map<ProjectModel, ProjectServiceEntity>(project));
            return Redirect("~/project");
        }
    }
}