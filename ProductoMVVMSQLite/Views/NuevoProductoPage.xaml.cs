using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.ViewModels;

namespace ProductoMVVMSQLite.Views;

public partial class NuevoProductoPage : ContentPage
{
	public NuevoProductoPage()
	{
		InitializeComponent();
		BindingContext = new NuevoProductoViewModel();
	}

    public NuevoProductoPage(Producto productoSeleccionado)
    {
        InitializeComponent();
        BindingContext = new NuevoProductoViewModel(productoSeleccionado);
    }
}