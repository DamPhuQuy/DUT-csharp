namespace ProductManagement.DTOs;

/// <summary>
/// Dùng để nhận dữ liệu từ form thêm / sửa sản phẩm.
/// Chứa CategoryId thay vì CategoryName để ghi xuống DB.
/// </summary>
public class ProductFormDto
{
    public int    Id         { get; set; }
    public string Name       { get; set; } = "";
    public double Price      { get; set; }
    public int    CategoryId { get; set; }
}
