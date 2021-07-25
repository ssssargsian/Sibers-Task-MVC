using Repository.Common;
using Repository.Entities;

namespace Repository.Repositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee> { }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SibersContext context) : base(context)
        {
        }
    }
}
