using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.DTOs;
using ProductManagement.Entities;
using ProductManagement.Repositories;
using ProductManagement.Services;

namespace ProductManagement.Forms;

public partial class MainWindow : Window
{
    private readonly ProductService _productService = null!;
    private readonly CategoryService _categoryService = null!;
    private List<CategoryDto> _categories = [];

    // Parameterless constructor for XAML designer
    public MainWindow() : this("") { }

    public MainWindow(string connectionString)
    {
        InitializeComponent();

        if (string.IsNullOrEmpty(connectionString)) return;

        var dbContext = new AppDbContext(
            new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString)
                .Options);

        _productService  = new ProductService(new EFProductRepository(dbContext));
        _categoryService = new CategoryService(new EFCategoryRepository(dbContext));

        BtnAdd.Click    += BtnAdd_Click;
        BtnUpdate.Click += BtnUpdate_Click;
        BtnDelete.Click += BtnDelete_Click;
        BtnClear.Click  += BtnClear_Click;
        BtnSearch.Click += BtnSearch_Click;
        BtnRefresh.Click += BtnRefresh_Click;
        BtnCategories.Click += BtnCategories_Click;
        ProductGrid.SelectionChanged += (_, _) => PopulateFormFromSelection();

        LoadCategories();
        CmbFilterCategory.SelectionChanged += (_, _) => LoadProducts();
        LoadProducts();
    }

    private void LoadCategories()
    {
        _categories = _categoryService
            .GetAll()
            .Select(c => c.ToCategoryDto())
            .ToList();

        var filterItems = new List<CategoryDto> { new(0, "-- Tất cả --") };
        filterItems.AddRange(_categories);
        CmbFilterCategory.ItemsSource = filterItems;
        CmbFilterCategory.SelectedIndex = 0;

        CmbCategory.ItemsSource = _categories;
        if (_categories.Count > 0) CmbCategory.SelectedIndex = 0;
    }

    private void LoadProducts()
    {
        var products   = _productService.GetAll();
        var search     = TxtSearch.Text?.Trim() ?? "";
        var filterCat  = CmbFilterCategory.SelectedItem as CategoryDto;

        var rows = products
            .Where(p => filterCat is null || filterCat.Id == 0 || p.CategoryId == filterCat.Id)
            .Where(p => string.IsNullOrEmpty(search)
                        || p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
            .Select(p => p.ToListDto(
                _categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name ?? ""))
            .ToList();

        ProductGrid.ItemsSource = rows;
    }

    private void PopulateFormFromSelection()
    {
        if (ProductGrid.SelectedItem is not ProductListDto row) return;
        TxtId.Text    = row.Id.ToString();
        TxtName.Text  = row.Name;
        TxtPrice.Text = row.Price.ToString();
        CmbCategory.SelectedItem = _categories.FirstOrDefault(c => c.Name == row.CategoryName);
        TxtStatus.Text = "";
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            var product = BuildProductFromForm();
            _productService.Add(product);
            ClearForm();
            LoadProducts();
            TxtStatus.Text = "Thêm sản phẩm thành công!";
        }
        catch (Exception ex) { TxtStatus.Text = ex.Message; }
    }

    private void BtnUpdate_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(TxtId.Text, out int id) || id == 0)
            {
                TxtStatus.Text = "Chọn sản phẩm cần sửa từ danh sách.";
                return;
            }
            var product = BuildProductFromForm();
            product.Id = id;
            _productService.Update(product);
            ClearForm();
            LoadProducts();
            TxtStatus.Text = "Cập nhật thành công!";
        }
        catch (Exception ex) { TxtStatus.Text = ex.Message; }
    }

    private void BtnDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TxtId.Text, out int id) || id == 0)
        {
            TxtStatus.Text = "Chọn sản phẩm cần xóa từ danh sách.";
            return;
        }
        _productService.Delete(id);
        ClearForm();
        LoadProducts();
        TxtStatus.Text = "Đã xóa sản phẩm!";
    }

    private void BtnClear_Click(object? sender, RoutedEventArgs e)   => ClearForm();
    private void BtnSearch_Click(object? sender, RoutedEventArgs e)  => LoadProducts();
    private void BtnCategories_Click(object? sender, RoutedEventArgs e)
    {
        var win = new CategoryWindow(_categoryService) { ShowInTaskbar = false };
        win.ShowDialog(this);
        // Reload categories and products in case categories changed
        LoadCategories();
        LoadProducts();
    }
    private void BtnRefresh_Click(object? sender, RoutedEventArgs e)
    {
        TxtSearch.Text = "";
        CmbFilterCategory.SelectedIndex = 0;
        LoadProducts();
    }

    private Product BuildProductFromForm()
    {
        if (!double.TryParse(TxtPrice.Text, out double price))
            throw new ArgumentException("Giá không hợp lệ.");
        var category = CmbCategory.SelectedItem as CategoryDto
            ?? throw new ArgumentException("Chọn danh mục.");
        return new ProductFormDto
        {
            Name = TxtName.Text ?? "",
            Price = price,
            CategoryId = category.Id
        }.ToEntity();
    }

    private void ClearForm()
    {
        TxtId.Text = TxtName.Text = TxtPrice.Text = TxtStatus.Text = "";
        if (_categories.Count > 0) CmbCategory.SelectedIndex = 0;
        ProductGrid.SelectedItem = null;
    }
}
