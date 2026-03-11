using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Forms;

public partial class CategoryWindow : Window
{
    private readonly CategoryService _categoryService = null!;

    // Parameterless constructor for XAML designer
    public CategoryWindow() : this(null!) { }

    public CategoryWindow(CategoryService categoryService)
    {
        InitializeComponent();
        _categoryService = categoryService;

        if (categoryService is null) return;

        BtnAdd.Click    += BtnAdd_Click;
        BtnUpdate.Click += BtnUpdate_Click;
        BtnDelete.Click += BtnDelete_Click;
        BtnClear.Click  += (_, _) => ClearForm();
        CategoryGrid.SelectionChanged += (_, _) => PopulateFormFromSelection();

        LoadCategories();
    }

    private void LoadCategories()
    {
        CategoryGrid.ItemsSource = _categoryService
            .GetAll()
            .Select(c => c.ToCategoryDto())
            .ToList();
    }

    private void PopulateFormFromSelection()
    {
        if (CategoryGrid.SelectedItem is not CategoryDto dto) return;
        TxtId.Text   = dto.Id.ToString();
        TxtName.Text = dto.Name;
        TxtStatus.Text = "";
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var dto = new CategoryDto(0, TxtName.Text?.Trim() ?? "");
            _categoryService.Add(dto.ToEntity());
            ClearForm();
            LoadCategories();
            TxtStatus.Text = "Thêm danh mục thành công!";
        }
        catch (Exception ex) { TxtStatus.Text = ex.Message; }
    }

    private void BtnUpdate_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(TxtId.Text, out int id) || id == 0)
            {
                TxtStatus.Text = "Chọn danh mục cần sửa từ danh sách.";
                return;
            }
            var dto = new CategoryDto(id, TxtName.Text?.Trim() ?? "");
            _categoryService.Update(dto.ToEntity());
            ClearForm();
            LoadCategories();
            TxtStatus.Text = "Cập nhật danh mục thành công!";
        }
        catch (Exception ex) { TxtStatus.Text = ex.Message; }
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TxtId.Text, out int id) || id == 0)
        {
            TxtStatus.Text = "Chọn danh mục cần xóa từ danh sách.";
            return;
        }
        _categoryService.Delete(id);
        ClearForm();
        LoadCategories();
        TxtStatus.Text = "Đã xóa danh mục!";
    }

    private void ClearForm()
    {
        TxtId.Text = TxtName.Text = TxtStatus.Text = "";
        CategoryGrid.SelectedItem = null;
    }
}
