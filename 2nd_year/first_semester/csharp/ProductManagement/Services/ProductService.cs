using System;
using ProductManagement.Entities;
using ProductManagement.Repositories;

namespace ProductManagement.Services;

public class ProductService(IRepository<Product> productRepository) : ServiceBase<Product>(productRepository)
{
    override
    public void Add(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.");
        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        _repository.Add(product);
    }

    override
    public void Update(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.");
        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        _repository.Update(product);
    }

    override
    public void Delete(int id) => _repository.Delete(id);
}
