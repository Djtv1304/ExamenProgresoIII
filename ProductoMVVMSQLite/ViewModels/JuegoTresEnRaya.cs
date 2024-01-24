using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductoMVVMSQLite.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class JuegoTresEnRaya : INotifyPropertyChanged
    {
        private string[,] tablero { get; set; } // Tablero del juego
        private string jugadorActual { get; set; } // Jugador actual
        public ICommand IniciarResetearJuegoCommand { get; private set; }

        public JuegoTresEnRaya()
        {
            tablero = new string[3, 3];
            jugadorActual = "X"; // El jugador X siempre comienza primero
            IniciarResetearJuegoCommand = new Command(IniciarResetearJuego);
        }

        public bool RealizarMovimiento(int fila, int columna)
        {
            // Verificar si la celda está vacía
            if (string.IsNullOrEmpty(tablero[fila, columna]))
            {
                tablero[fila, columna] = jugadorActual;
                // Cambiar al otro jugador
                jugadorActual = jugadorActual == "X" ? "O" : "X";
                return true;
            }
            return false; // Movimiento inválido
        }

        public string VerificarGanador()
        {
            // Verificar filas
            for (int i = 0; i < 3; ++i)
            {
                if (tablero[i, 0] == tablero[i, 1] && tablero[i, 0] == tablero[i, 2] && !string.IsNullOrEmpty(tablero[i, 0]))
                    return tablero[i, 0];
            }

            // Verificar columnas
            for (int i = 0; i < 3; ++i)
            {
                if (tablero[0, i] == tablero[1, i] && tablero[0, i] == tablero[2, i] && !string.IsNullOrEmpty(tablero[0, i]))
                    return tablero[0, i];
            }

            // Verificar diagonales
            if (tablero[0, 0] == tablero[1, 1] && tablero[0, 0] == tablero[2, 2] && !string.IsNullOrEmpty(tablero[0, 0]))
                return tablero[0, 0];
            if (tablero[0, 2] == tablero[1, 1] && tablero[0, 2] == tablero[2, 0] && !string.IsNullOrEmpty(tablero[0, 2]))
                return tablero[0, 2];

            // Verificar si todas las celdas están llenas
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (string.IsNullOrEmpty(tablero[i, j]))
                        return null; // El juego aún no ha terminado
                }
            }

            return "Empate"; // Todas las celdas están llenas y no hay ganador
        }
        // ... Resto de tu código ...

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Luego, cada vez que cambies una propiedad que esté enlazada a tu vista, llama a OnPropertyChanged. Por ejemplo:

        public string[,] Tablero
        {
            get { return tablero; }
            set
            {
                if (tablero != value)
                {
                    tablero = value;
                    OnPropertyChanged();
                }
            }
        }

        private void IniciarResetearJuego()
        {
            // Reinicia el tablero
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Tablero[i, j] = null;
                }
            }

            // Reinicia el jugador actual
            jugadorActual = "X";

            // Notifica a la vista que el tablero y el jugador actual han cambiado
            OnPropertyChanged(nameof(Tablero));
            OnPropertyChanged(nameof(jugadorActual));
        }
    }

}
