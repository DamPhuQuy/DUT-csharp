using System;
using System.Collections.Generic;
using ProductManagement.Entities;
using ProductManagement.Repositories;

namespace ProductManagement.Services;

public class CategoryService(IRepository<Category> categoryRepository) : ServiceBase<Category>(categoryRepository)
{
    override
    public void Add(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty.");

        _repository.Add(category);
    }

    override
    public void Update(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty.");

        _repository.Update(category);
    }

    override
    public void Delete(int id) => _repository.Delete(id);
}
