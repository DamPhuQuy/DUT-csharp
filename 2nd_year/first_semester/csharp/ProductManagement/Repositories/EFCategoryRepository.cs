using System.Collections.Generic;
using System.Linq;
using ProductManagement.Data;
using ProductManagement.Entities;

namespace ProductManagement.Repositories;

public class EFCategoryRepository : IRepository<Category>
{
    private readonly AppDbContext _context;

    public EFCategoryRepository(AppDbContext context) => _context = context;

    public List<Category> GetAll()
        => _context.Categories.ToList();

    public Category? GetById(int id)
        => _context.Categories.FirstOrDefault(c => c.Id == id);

    public void Add(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}

/*
So sánh LINQ to SQL (EF Core) vs ADO.NET thô:

| Thao tác   | ADO.NET (raw)                              | LINQ to SQL (EF Core)                          |
| ---------- | ------------------------------------------ | ---------------------------------------------- |
| Lấy tất cả | ExecuteReader() + while(reader.Read())     | _context.Categories.ToList()                   |
| Lấy theo id| cmd.Parameters.AddWithValue + reader.Read()| .FirstOrDefault(c => c.Id == id)               |
| Thêm       | INSERT INTO ... VALUES + ExecuteNonQuery() | _context.Categories.Add(obj) + SaveChanges()   |
| Sửa        | UPDATE ... SET ... + ExecuteNonQuery()     | _context.Categories.Update(obj) + SaveChanges()|
| Xóa        | DELETE FROM ... + ExecuteNonQuery()        | _context.Categories.Remove(obj) + SaveChanges()|

*/
