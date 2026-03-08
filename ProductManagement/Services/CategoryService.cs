using System;
using System.Collections.Generic;
using ProductManagement.Entities;
using ProductManagement.Repositories;

namespace ProductManagement.Services;

public class CategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<Category> GetAll() => _categoryRepository.GetAll();

    public Category? GetById(int id) => _categoryRepository.GetById(id);

    public void Add(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty.");

        _categoryRepository.Add(category);
    }

    public void Update(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            throw new ArgumentException("Category name cannot be empty.");

        _categoryRepository.Update(category);
    }

    public void Delete(int id) => _categoryRepository.Delete(id);
}
