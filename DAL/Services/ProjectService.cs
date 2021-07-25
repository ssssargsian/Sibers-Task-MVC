using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Repository.Common;
using Repository.Entities;
using Services.Common;
using Services.Entities;

namespace Services.Services
{
    public interface IProjectService : IServiceBase<ProjectServiceEntity>
    {
        new ProjectServiceEntity GetById(int id);
        ICollection<ProjectServiceEntity> GetAllProject(string filter, SortProject sort);
        ICollection<EmployeeServiceEntity> GetAllEmployees(int projectId);
        void Bind(int projectId, int employeeId);
        void UnBind(int projectId, int employeeId);
    }
    public class ProjectService : ServiceBase<Project, ProjectServiceEntity>, IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Project> _projectRepository;
        private readonly IRepositoryBase<Employee> _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        public ProjectService(IRepositoryBase<Project> projectRepository, IRepositoryBase<Employee> employeeRepository, IUnitOfWork unitOfWork, IEmployeeService employeeService) : base(projectRepository, unitOfWork)
        {
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectServiceEntity>();
                cfg.CreateMap<ProjectServiceEntity, Project>();
                cfg.CreateMap<Employee, EmployeeServiceEntity>();
                cfg.CreateMap<EmployeeServiceEntity, Employee>();
            });
            _mapper = config.CreateMapper();
        }

        public new ProjectServiceEntity GetById(int id)
        {
            var project = _mapper.Map<Project, ProjectServiceEntity>(_projectRepository.Get(id));
            if (project.ProjectManagerId != null)
            {
                project.ProjectManager = _mapper.Map<Employee, EmployeeServiceEntity>(_employeeRepository.Get(project.ProjectManagerId.Value));
                project.FullNameProjectManager = $"{project.ProjectManager.Surname} {project.ProjectManager.FirstName} {project.ProjectManager.Patronymic}";
            }
            return project;
        }
        public ICollection<ProjectServiceEntity> GetAllProject(string filter, SortProject sort)
        {
            var projects = _mapper.Map<List<Project>, List<ProjectServiceEntity>>(_projectRepository.GetAll().ToList());

            //filter
            if (!string.IsNullOrEmpty(filter))
            {
                projects = projects.Where(c => c.Name.ToLower().Contains(filter.ToLower())
                                               || c.CompanyCustomer.ToLower().Contains(filter.ToLower())
                                               || c.CompanyExecutor.ToLower().Contains(filter.ToLower())
                                               || c.FullNameProjectManager.ToLower().Contains(filter.ToLower())).ToList();
            }

            //Creating fullname for PM
            foreach (var item in projects)
            {
                item.ProjectManager = _employeeService.GetById(item.ProjectManagerId ?? 0);
                if (item.ProjectManager != null)
                    item.FullNameProjectManager =
                        $"{item.ProjectManager.Surname} {item.ProjectManager.FirstName} {item.ProjectManager.Patronymic}";
                else
                    item.FullNameProjectManager = string.Empty;
            }

            //Sorting
            switch (sort)
            {
                case SortProject.Name:
                    return projects.OrderBy(c => c.Name).ToList();
                case SortProject.Company:
                    return projects.OrderBy(c => c.CompanyCustomer).ToList();
                case SortProject.Executor:
                    return projects.OrderBy(c => c.CompanyExecutor).ToList();
                case SortProject.FullNameProjectManager:
                    return projects.OrderBy(c => c.FullNameProjectManager).ToList();
                case SortProject.Priority:
                    return projects.OrderBy(c => c.Priority).ToList();
                case SortProject.StartDate:
                    return projects.OrderBy(c => c.StartDate).ToList();
                case SortProject.EndDate:
                    return projects.OrderBy(c => c.EndDate).ToList();
                default:
                    return projects;
            }
        }

        public ICollection<EmployeeServiceEntity> GetAllEmployees(int projectId)
        {
            return _mapper.Map<ICollection<Employee>, ICollection<EmployeeServiceEntity>>(_projectRepository.Get(projectId).Employees);
        }
        public void Bind(int projectId, int employeeId)
        {
            var project = _projectRepository.Get(projectId);
            var employee = _employeeRepository.Get(employeeId);
            project.Employees.Add(employee);
            employee.Projects.Add(project);
            _unitOfWork.Commit();
        }
        public void UnBind(int projectId, int employeeId)
        {
            var project = _projectRepository.Get(projectId);
            var employee = _employeeRepository.Get(employeeId);
            project.Employees.Remove(employee);
            employee.Projects.Remove(project);
            _unitOfWork.Commit();
        }
    }
}
