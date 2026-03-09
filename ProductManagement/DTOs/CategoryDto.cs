namespace ProductManagement.DTOs;

/// <summary>
/// Dùng để hiển thị danh mục trong ComboBox và DataGrid.
/// ToString() trả về Name để ComboBox tự hiển thị đúng.
/// </summary>
public record CategoryDto(int Id, string Name)
{
    public override string ToString() => Name;
}
