using System;
using System.Collections.Generic;
using ProductManagement.Entities;
using ProductManagement.Repositories;

namespace ProductManagement.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAll() => _productRepository.GetAll();

    public Product? GetById(int id) => _productRepository.GetById(id);

    public void Add(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.");
        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        _productRepository.Add(product);
    }

    public void Update(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name cannot be empty.");
        if (product.Price < 0)
            throw new ArgumentException("Product price cannot be negative.");

        _productRepository.Update(product);
    }

    public void Delete(int id) => _productRepository.Delete(id);
}
