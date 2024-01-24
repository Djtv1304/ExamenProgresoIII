
using ProductoMVVMSQLite.Models;
using ProductoMVVMSQLite.Utils;
using PropertyChanged;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NuevoProductoViewModel
    {
        public bool ModoCreacion { get; private set; }

        public bool isReadOnly { get; private set; }

        public string Nombre { get; set; }

        public string Cantidad { get; set; }

        public string Descripcion { get; set; }


        public NuevoProductoViewModel()
        {

            ModoCreacion = true;
            isReadOnly = false;

        }

        public NuevoProductoViewModel(Producto productoSeleccionado)
        {

            ModoCreacion = false;
            isReadOnly = true;
            Nombre = productoSeleccionado.Nombre;
            Cantidad = productoSeleccionado.Cantidad.ToString();
            Descripcion = productoSeleccionado.Descripcion;

        }

        public ICommand GuardarProducto =>
            new Command(async () =>
            {

                Producto producto = new Producto
                {
                    Nombre = Nombre,
                    Descripcion = Descripcion,
                    Cantidad = Int32.Parse(Cantidad)
                };

                App.productoRepository.Add(producto);
                Util.ListaProductos = App.productoRepository.GetAll();

                await App.Current.MainPage.Navigation.PopAsync();

            });

    }
}
