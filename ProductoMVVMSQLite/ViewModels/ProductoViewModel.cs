using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
using ProductoMVVMSQLite.Views;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProductoViewModel
    {

        public ObservableCollection<Producto> ListaProductos { get; set; }

        private Producto _productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                ProductoSeleccionadoCommand.Execute(value);
                 
            }
        }

        public ProductoViewModel() 
        {

            Util.ListaProductos = App.productoRepository.GetAll();

            ListaProductos = new ObservableCollection<Producto>(Util.ListaProductos);

            // Me suscribo al evento ProductoAgregado
            App.productoRepository.ProductoAgregado += ProductoRepository_ProductoAgregado;

        }

        private void ProductoRepository_ProductoAgregado(object sender, EventArgs e)
        {
            // Actualizar la lista cuando se reciba el evento
            Util.ListaProductos = App.productoRepository.GetAll();
            ListaProductos = new ObservableCollection<Producto>(Util.ListaProductos);
        }

        public ICommand CrearProducto =>
            new Command(async () =>
            {
               await App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage());
            });

        public ICommand ProductoSeleccionadoCommand => 
            new Command<Producto>(producto =>
            {
                // Envio a la página NuevoProductoPage.
                App.Current.MainPage.Navigation.PushAsync(new NuevoProductoPage(producto));
            });

        public ICommand EditarProducto => 
            new Command<Producto>(async (producto) =>
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditProductoPage(producto));
            });

        public ICommand EliminarProducto =>
            new Command<Producto>(async (producto) =>
            {
                bool respuesta = await App.Current.MainPage.DisplayAlert(
                    "Confirmación",
                    "¿Estás seguro de que quieres eliminar este producto?",
                    "Sí",
                    "No"
                );
                if (respuesta)
                {
                    App.productoRepository.Delete(producto);
                }
            });

    }
}
