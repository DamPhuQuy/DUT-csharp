using System.Collections.Generic;
using ProductManagement.Repositories;
namespace ProductManagement.Services;

public abstract class ServiceBase<T>(IRepository<T> repository)
{
    protected readonly IRepository<T> _repository = repository;

    public virtual List<T> GetAll()
    {
        return _repository.GetAll();
    }

    public virtual T? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public virtual void Add(T item)
    {
        _repository.Add(item);
    }

    public virtual void Update(T item)
    {
        _repository.Update(item);
    }

    public virtual void Delete(int id)
    {
        _repository.Delete(id);
    }
}
