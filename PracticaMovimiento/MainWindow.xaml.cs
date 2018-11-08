using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;

        public MainWindow()
        {
            InitializeComponent();
            Canvas1.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            //1.Establecer instrucciones
            ThreadStart threadStart = new ThreadStart(moverObjetos);
            //2.Inicializar el Thread
            Thread threadMoverObjetos = new Thread(threadStart);
            //3.Ejecutar el Thread
            threadMoverObjetos.Start();
        }

        

        void moverObjetos()
        {
            while (true)
            {
                Dispatcher.Invoke(() =>
                {

                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime = tiempoActual - tiempoAnterior;

                    double leftISSActual = Canvas.GetLeft(iss);
                    Canvas.SetLeft(iss, leftISSActual - (90 * deltaTime.TotalSeconds));

                    if (Canvas.GetLeft(iss)<= - 251)
                    {
                        Canvas.SetLeft(iss, 800);
                    }


                    //Intersección en X
                    double xISS = Canvas.GetLeft(iss);
                    double xNave = Canvas.GetLeft(nave);
                    double yISS = Canvas.GetTop(iss);
                    double yNave = Canvas.GetTop(nave);
                    if (xNave >= xISS && xNave <= xISS + iss.Width && yNave >= yISS && yNave <= yISS + iss.Height)
                    {
                        lblColision.Text = "Colisión";
                    }
                    else
                    {
                        lblColision.Text = "No hay colisión";
                    }

                    tiempoAnterior = tiempoActual;
                });
            }
        }

        private void Canvas1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Up)
            {
                double topNaveActual = Canvas.GetTop(nave);
                Canvas.SetTop(nave, topNaveActual - 15); //Esta funcion lleva dos parametros uno es el elemento que s está moviendo y el segundo la cantidad
            }
            if (e.Key == Key.Down)
            {
                double topNaveActual = Canvas.GetTop(nave);
                Canvas.SetTop(nave, topNaveActual + 15); //Esta funcion lleva dos parametros uno es el elemento que s está moviendo y el segundo la cantidad
            }
            if (e.Key == Key.Left)
            {
                double horizontalNaveActual = Canvas.GetLeft(nave);
                Canvas.SetLeft(nave, horizontalNaveActual - 15); //Esta funcion lleva dos parametros uno es el elemento que s está moviendo y el segundo la cantidad
            }
            if (e.Key == Key.Right)
            {
                double horizontalNaveActual = Canvas.GetLeft(nave);
                Canvas.SetLeft(nave, horizontalNaveActual + 15); //Esta funcion lleva dos parametros uno es el elemento que s está moviendo y el segundo la cantidad
            }
        }
    }
}
