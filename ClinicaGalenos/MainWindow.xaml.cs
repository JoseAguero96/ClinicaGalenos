using Biblioteca;
using Newtonsoft.Json;
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

namespace ClinicaGalenos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            ConexionApi conexion = new ConexionApi();

            string rut = txtRut.Text;
            string pass = txtPass.Password.ToString();
            var json = new { rut = rut, password = pass };
            var result = conexion.ejecutarLlamada("POST", "logear", "", json);

            if (result != "false")
            {
                // Si el usuario existe se hace una llamada (GET) a la api para que 
                // devuelva los datos del usuario
                var userjson = conexion.ejecutarLlamada("GET", "users/" + result, "", "");

                // Variable que se enviará a la siguiente ventana la cual muestra el perfil del usuario
                string perfil = string.Empty; 

                // Creo un objeto usuario pasandole los datos enviados por la API
                Usuario user = JsonConvert.DeserializeObject<Usuario>(userjson);

                Principal wPrincipal = new Principal();

                if (user.profile_id == 4)
                {
                    // Si el usuario es un paciente no se permite el ingreso.
                    MessageBox.Show("No tienes permisos para ingresar a la aplicación.");
                }else
                {
                    if (user.profile_id == 2)
                    {
                        perfil = "Secretaria";
                    }
                    else if (user.profile_id == 3)
                    {
                        perfil = "Médico";
                        var jsonMeds = conexion.ejecutarLlamada("GET", "medicos", "", "");
                        List<Medico> medicos = JsonConvert.DeserializeObject<List<Medico>>(jsonMeds);
                        foreach (Medico item in medicos)
                        {
                            wPrincipal.txt_userid.Text = item.id.ToString();
                        }
                    }
                    else
                    {
                        perfil = "Administrador";
                    }
                    wPrincipal.DataContext = user;

                    wPrincipal.txt_Profile.Text = perfil;
                    
                    wPrincipal.lbl_Bienvenida.Content = string.Format("Hola {0} | {1}", user.name, perfil);
                    wPrincipal.Owner = this;
                    wPrincipal.Show();
                    this.Hide();
                }                
            }else
            {
                MessageBox.Show("Rut o constraseña incorrectos");
            }
        }
    }
}
