using Repository.Common;
using Repository.Entities;

namespace Repository.Repositories
{
    public interface IProjectRepository : IRepositoryBase<Project> { }
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(SibersContext context) : base(context)
        {
        }
    }
}
