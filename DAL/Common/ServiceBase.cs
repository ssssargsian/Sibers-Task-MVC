using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Repository.Common;

namespace Services.Common
{
    public interface IServiceBase<T> where T : class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
    }
    public abstract class ServiceBase<T, E> : IServiceBase<E> where T : class where E : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<T> _projectRepository;
        private readonly IMapper _mapper;
        public ServiceBase(IRepositoryBase<T> projectRepository, IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, E>();
                cfg.CreateMap<E, T>();
            });
            _mapper = config.CreateMapper();
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }
        public ICollection<E> GetAll()
        {
            return _mapper.Map<ICollection<T>, ICollection<E>>(
                _projectRepository.GetAll().ToArray());
        }
        public void Add(E entity)
        {
            _projectRepository.Create(_mapper.Map<E, T>(entity));
            _unitOfWork.Commit();
        }
        public void Delete(int id)
        {
            _projectRepository.Delete(id);
            _unitOfWork.Commit();
        }
        public void Update(E entity)
        {
            _projectRepository.Update(_mapper.Map<E, T>(entity));
            _unitOfWork.Commit();
        }

        public E GetById(int id)
        {
            return _mapper.Map<T, E>(_projectRepository.Get(id));
        }
    }
}
