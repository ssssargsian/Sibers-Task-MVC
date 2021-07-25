using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Common;
using Repository.Entities;
using Services.Common;
using Services.Entities;

namespace Services.Services
{
    public interface IEmployeeService : IServiceBase<EmployeeServiceEntity>
    {
        
    }
    public class EmployeeService : ServiceBase<Employee,EmployeeServiceEntity>, IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Employee> _projectRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IRepositoryBase<Employee> projectRepository) : base(projectRepository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeServiceEntity>();
                cfg.CreateMap<EmployeeServiceEntity, Employee>();
            });
            _mapper = config.CreateMapper();
        }
    }
}
