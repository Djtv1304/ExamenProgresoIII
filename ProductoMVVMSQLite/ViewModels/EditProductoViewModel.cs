using ProductoMVVMSQLite.Models;
using PropertyChanged;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class EditProductoViewModel
    {

        public Producto Producto { get; set; }

        public EditProductoViewModel(Producto producto)
        {

            Producto = producto;

        }

        public ICommand GuardarModificaciones =>
            new Command( async () => 
            {

                Producto producto = new Producto
                {
                    Nombre = Producto.Nombre,
                    Descripcion = Producto.Descripcion,
                    Cantidad = Producto.Cantidad
                };

                App.productoRepository.Update(producto);
                //Util.ListaProductos = App.productoRepository.GetAll();

                await App.Current.MainPage.Navigation.PopAsync();

            });

    }
}
