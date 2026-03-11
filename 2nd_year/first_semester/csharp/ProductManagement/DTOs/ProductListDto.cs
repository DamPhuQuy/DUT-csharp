namespace ProductManagement.DTOs;

/// <summary>
/// Dùng để hiển thị danh sách sản phẩm trong DataGrid (read-only, đã join tên danh mục).
/// </summary>
public record ProductListDto(int Id, string Name, double Price, string CategoryName);
