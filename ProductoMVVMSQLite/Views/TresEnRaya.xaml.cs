using ProductoMVVMSQLite.ViewModels;

namespace ProductoMVVMSQLite.Views;

public partial class TresEnRaya : ContentPage
{

    private JuegoTresEnRaya juego;

    public TresEnRaya()
	{
		InitializeComponent();
        juego = new JuegoTresEnRaya();
        BindingContext = juego;
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var fila = Grid.GetRow(button);
        var columna = Grid.GetColumn(button);

        if (juego.RealizarMovimiento(fila, columna))
        {
            button.Text = juego.Tablero[fila, columna];
            var ganador = juego.VerificarGanador();


            if(ganador != null && ganador.Equals("Empate"))
            {

                bool respuesta = await App.Current.MainPage.DisplayAlert(
                "Empate",
                "Empate",
                "Sí",
                "No"
                );
                return;

            }
            if (ganador != null)
            {
                bool respuesta = await App.Current.MainPage.DisplayAlert(
                "Win",
                "Ganaste, felicidades",
                "Sí",
                "No"
                );

            }
        }
        else
        {
            // Aquí puedes mostrar un mensaje al usuario indicando que el movimiento es inválido
        }
    }

}