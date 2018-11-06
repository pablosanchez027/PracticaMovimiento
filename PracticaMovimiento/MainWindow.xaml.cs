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
        public MainWindow()
        {
            InitializeComponent();
            Canvas1.Focus();
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

                    double leftISSActual = Canvas.GetLeft(iss);
                    Canvas.SetLeft(iss, leftISSActual - 0.001);

                    if (Canvas.GetLeft(iss)<= - 251)
                    {
                        Canvas.SetLeft(iss, 800);
                    }

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
        }
    }
}
