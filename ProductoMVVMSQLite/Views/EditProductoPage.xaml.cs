using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.ViewModels;

namespace ProductoMVVMSQLite.Views;

public partial class EditProductoPage : ContentPage
{
	public EditProductoPage(Producto productoParaEditar)
	{
		InitializeComponent();
		BindingContext = new EditProductoViewModel(productoParaEditar);
	}

}