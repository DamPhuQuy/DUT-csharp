using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using ProductManagement.Entities;

namespace ProductManagement.Repositories;

public class SqlServerCategoryRepository : IRepository<Category>
{
    private readonly string _connectionString;

    public SqlServerCategoryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Category> GetAll()
    {
        var categories = new List<Category>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT id, name FROM categories", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            categories.Add(new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }
        return categories;
    }

    public Category? GetById(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT id, name FROM categories WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", id);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Category
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
        }
        return null;
    }

    public void Add(Category category)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("INSERT INTO categories (name) VALUES (@name)", conn);
        cmd.Parameters.AddWithValue("name", category.Name);
        cmd.ExecuteNonQuery();
    }

    public void Update(Category category)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("UPDATE categories SET name = @name WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("name", category.Name);
        cmd.Parameters.AddWithValue("id", category.Id);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM categories WHERE id = @id", conn);
        cmd.Parameters.AddWithValue("id", id);
        cmd.ExecuteNonQuery();
    }
}

/*
Học ADO.NET với kiến thức Java JDBC:

| Concept        | C# (ADO.NET)      | Java (JDBC)       |
| -------------- | ----------------- | ----------------- |
| Connection     | NpgsqlConnection  | Connection        |
| Command        | NpgsqlCommand     | PreparedStatement |
| Result         | NpgsqlDataReader  | ResultSet         |
| Execute Query  | ExecuteReader()   | executeQuery()    |
| Execute Update | ExecuteNonQuery() | executeUpdate()   |

C#:
using var conn = new NpgsqlConnection(connectionString);
conn.Open();

using var cmd = new NpgsqlCommand(
    "SELECT id, name FROM categories WHERE id = @id",
    conn
);

cmd.Parameters.AddWithValue("id", id);

using var reader = cmd.ExecuteReader();

if (reader.Read())
{
    return new Category
    {
        Id = reader.GetInt32(0),
        Name = reader.GetString(1)
    };
}


Java:
Connection conn = DriverManager.getConnection(url, user, password);

PreparedStatement stmt = conn.prepareStatement(
    "SELECT id, name FROM categories WHERE id = ?"
);

stmt.setInt(1, id);

ResultSet rs = stmt.executeQuery();

if (rs.next()) {
    Category category = new Category();
    category.setId(rs.getInt(1));
    category.setName(rs.getString(2));
}

*/
