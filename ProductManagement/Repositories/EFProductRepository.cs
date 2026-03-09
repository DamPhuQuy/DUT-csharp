using System.Collections.Generic;
using System.Linq;
using ProductManagement.Data;
using ProductManagement.Entities;

namespace ProductManagement.Repositories;

public class EFProductRepository : IRepository<Product>
{
    private readonly AppDbContext _context;

    public EFProductRepository(AppDbContext context) => _context = context;

    public List<Product> GetAll()
        => _context.Products.ToList();

    public Product? GetById(int id)
        => _context.Products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
