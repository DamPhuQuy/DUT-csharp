using ProductManagement.Entities;

namespace ProductManagement.DTOs;

/// <summary>
/// Extension methods để chuyển đổi giữa Entity và DTO.
///
/// Quy tắc:
///   Entity  → DTO   : dùng khi trả về data cho UI (đọc từ DB xong map ra)
///   DTO     → Entity: dùng khi ghi xuống DB (lấy dữ liệu từ form rồi map vào)
/// </summary>
public static class Mappers
{
    // ── Product ──────────────────────────────────────────────────

    /// <summary>Map Product entity → ProductListDto (cần truyền categoryName đã join sẵn).</summary>
    public static ProductListDto ToListDto(this Product p, string categoryName)
        => new(p.Id, p.Name, p.Price, categoryName);

    /// <summary>Map Product entity → ProductFormDto (để điền sẵn vào form sửa).</summary>
    public static ProductFormDto ToFormDto(this Product p)
        => new() { Id = p.Id, Name = p.Name, Price = p.Price, CategoryId = p.CategoryId };

    /// <summary>Map ProductFormDto → Product entity (để ghi xuống DB).</summary>
    public static Product ToEntity(this ProductFormDto dto)
        => new() { Id = dto.Id, Name = dto.Name, Price = dto.Price, CategoryId = dto.CategoryId };

    // ── Category ─────────────────────────────────────────────────

    /// <summary>Map Category entity → CategoryDto.</summary>
    public static CategoryDto ToCategoryDto(this Category c)
        => new(c.Id, c.Name);

    /// <summary>Map CategoryDto → Category entity.</summary>
    public static Category ToEntity(this CategoryDto dto)
        => new() { Id = dto.Id, Name = dto.Name };
}
