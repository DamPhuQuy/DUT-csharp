using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ProductManagement.Entities;

namespace ProductManagement.Forms;

public partial class MainWindow : Window
{
    private readonly List<Product> _products =
    [
        new Product { Id = 1, Name = "Bóng đèn điện quang", Price = 50 },
        new Product { Id = 2, Name = "Thuốc diệt mối",      Price = 75 },
        new Product { Id = 3, Name = "Thuốc kiến",          Price = 35 },
        new Product { Id = 4, Name = "Đèn tròn",            Price = 23 },
        new Product { Id = 6, Name = "Kéo cắt sắt",         Price = 45 },
        new Product { Id = 7, Name = "Bóng đèn đường",      Price = 12 },
    ];

    public MainWindow()
    {
        InitializeComponent();

        BtnCount.Click        += BtnCount_Click;
        BtnViewDetail.Click   += BtnViewDetail_Click;
        BtnViewDetailC2.Click += BtnViewDetailC2_Click;
        BtnShowList.Click     += BtnShowList_Click;
    }

    private void BtnCount_Click(object? sender, RoutedEventArgs e)
    {
        TxtCountResult.Text = $"Có {_products.Count} sản phẩm!";
    }

    private void BtnViewDetail_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(TxtSearchId.Text, out int id)) return;

        var p = _products.Find(x => x.Id == id);
        if (p is not null)
        {
            TxtId.Text    = p.Id.ToString();
            TxtName.Text  = p.Name;
            TxtPrice.Text = p.Price.ToString();
        }
        else
        {
            TxtId.Text    = "";
            TxtName.Text  = "Không tìm thấy!";
            TxtPrice.Text = "";
        }
    }

    private void BtnViewDetailC2_Click(object? sender, RoutedEventArgs e)
    {
        // C2: Show in a message box style using window title
        if (!int.TryParse(TxtSearchId.Text, out int id)) return;
        var p = _products.Find(x => x.Id == id);
        if (p is not null)
            Title = $"SP #{p.Id}: {p.Name} – {p.Price:C0}";
        else
            Title = "Không tìm thấy sản phẩm!";
    }

    private void BtnShowList_Click(object? sender, RoutedEventArgs e)
        => ProductGrid.ItemsSource = _products;
}
