using Biblioteca;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace ClinicaGalenos
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        ConexionApi conexion = new ConexionApi();
        public Principal()
        {
            
            InitializeComponent();
            
            txt_Profile.Visibility = Visibility.Collapsed;
            txtIdPaciente.Visibility = Visibility.Collapsed;
            txtIdUserEdit.Visibility = Visibility.Collapsed;
            txt_userid.Visibility = Visibility.Collapsed;

            gridManejoUsuarios.Visibility = Visibility.Hidden;
            gridAgendarHora.Visibility = Visibility.Hidden;
            gridNewEdit.Visibility = Visibility.Hidden;

            // Buttons
            btnAddNew.Visibility = Visibility.Hidden;
            btnEdit.Visibility = Visibility.Hidden;

            // Grids Menú
            gridMenuSecretaria.Visibility = Visibility.Hidden;
            gridAdministrador.Visibility = Visibility.Hidden;
            gridRegistrarAtencion.Visibility = Visibility.Hidden;
            gridAgendaMedicos.Visibility = Visibility.Hidden;
            gridAgendas.Visibility = Visibility.Hidden;
            gridMenuMedicos.Visibility = Visibility.Hidden;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    
                    break;
                case 1:
                    // Botón médicos
                    var jsonMedicos = conexion.ejecutarLlamada("GET", "users" + "", "", null);
                    List<Usuario> medicos = new List<Usuario>();

                    List<Usuario> medicos_list = new List<Usuario>();

                    medicos = JsonConvert.DeserializeObject<List<Usuario>>(jsonMedicos);

                    foreach (Usuario item in medicos)
                    {
                        if (item.profile_id == 3)
                        {
                            Usuario medico = new Usuario();

                            medico.id = item.id;
                            medico.username = item.username;
                            medico.email = item.email;
                            medico.name = item.name;
                            medico.last_name = item.last_name;
                            medico.birth_day = item.birth_day;
                            medico.phone_number = item.phone_number;
                            medico.rut = item.rut;
                            medico.profile_id = item.profile_id;

                            medicos_list.Add(medico);
                        }
                    }
                    gvMedicos.ItemsSource = medicos_list;

                    gridRegistrarAtencion.Visibility = Visibility.Hidden;
                    gridAgendarHora.Visibility = Visibility.Hidden;
                    gridAgendas.Visibility = Visibility.Hidden;
                    gridAgendaMedicos.Visibility = Visibility.Visible;
                    break;
                case 2:
                    // botón agendar hora
                    gridAgendas.Visibility = Visibility.Hidden;
                    gridAgendaMedicos.Visibility = Visibility.Hidden;
                    gridRegistrarAtencion.Visibility = Visibility.Hidden;
                    gridAgendarHora.Visibility = Visibility.Visible;
                    #region Combobox Areas (Llenado)
                    // Llenamos el combobox de areas
                    var areasjson = conexion.ejecutarLlamada("GET", "areas", "", "");

                    JArray arrJson = JArray.Parse(areasjson);

                    List<Area> areas = new List<Area>();

                    foreach (JObject jsonOperaciones in arrJson.Children<JObject>())
                    {
                        Area area = new Area();
                        area.id = Convert.ToInt32(jsonOperaciones["id"]);
                        area.nombre = jsonOperaciones["nombre"].ToString();

                        areas.Add(area);
                    }

                    cbxArea.ItemsSource = areas;
                    cbxArea.DisplayMemberPath = "nombre";
                    cbxArea.SelectedValuePath = "id";
                    #endregion
                    break;
                case 3:
                    var jsonMeds = conexion.ejecutarLlamada("GET", "medicos", "", null);
                    lblHorasAgregadas.Visibility = Visibility.Hidden;
                    cbxHorasPaciente.Visibility = Visibility.Hidden;

                    List<MetodoPago> mp_list = new List<MetodoPago>();

                    mp_list.Add(new MetodoPago(1, "Efectivo"));
                    mp_list.Add(new MetodoPago(2, "Documento"));
                    mp_list.Add(new MetodoPago(3, "Tarjeta"));
                    mp_list.Add(new MetodoPago(4, "Bono"));

                    cbxMetodo.ItemsSource = mp_list;
                    cbxMetodo.DisplayMemberPath = "metodo";
                    cbxMetodo.SelectedValuePath = "id";

                    gridAgendas.Visibility = Visibility.Hidden;
                    gridAgendaMedicos.Visibility = Visibility.Hidden;
                    gridAgendarHora.Visibility = Visibility.Hidden;
                    gridRegistrarAtencion.Visibility = Visibility.Visible;
                    break;
                case 4:
                    var json = conexion.ejecutarLlamada("GET", "users" + "", "", null);
                    List<Usuario> users = new List<Usuario>();

                    List<Usuario> nueva_lista = new List<Usuario>();

                    users = JsonConvert.DeserializeObject<List<Usuario>>(json);

                    foreach (Usuario item in users)
                    {
                        if (item.profile_id != 4 && item.profile_id != 1)
                        {
                            Usuario user = new Usuario();

                            user.id = item.id;
                            user.username = item.username;
                            user.email = item.email;
                            user.name = item.name;
                            user.last_name = item.last_name;
                            user.birth_day = item.birth_day;
                            user.phone_number = item.phone_number;
                            user.rut = item.rut;
                            user.profile_id = item.profile_id;

                            nueva_lista.Add(user);
                        }
                    }
                    gvUsers.ItemsSource = nueva_lista;

                    gridManejoUsuarios.Visibility = Visibility.Visible;
                    break;
                case 5:
                    break;
                case 6:
                    break;                    
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (txt_Profile.Text == "Secretaria")
            {
                gridMenuSecretaria.Visibility = Visibility.Visible;
            }
            else if (txt_Profile.Text == "Administrador")
            {
                gridAdministrador.Visibility = Visibility.Visible;
            }
            else if (txt_Profile.Text == "Médico")
            {
                int id_medico = int.Parse(txt_userid.Text);
                var jsonAgendas = conexion.ejecutarLlamada("GET", "agendas", "", null);
                List<Agenda> agenda = new List<Agenda>();

                List<Usuario> nueva_lista = new List<Usuario>();

                agenda = JsonConvert.DeserializeObject<List<Agenda>>(jsonAgendas);

                foreach (Agenda item in agenda)
                {
                    if (item.medico_id == id_medico)
                    {
                        var pacientes = conexion.ejecutarLlamada("GET", "users/"+item.user_id, "", null);
                        Usuario users = JsonConvert.DeserializeObject<Usuario>(pacientes);

                        nueva_lista.Add(users);           
                    }
                }
                gvPacientes.ItemsSource = nueva_lista;
                gridMenuMedicos.Visibility = Visibility.Visible;
            }
        }

        
        private void btnBuscarRut_Click(object sender, RoutedEventArgs e)
        {
            var userjson = conexion.ejecutarLlamada("GET", "buscar_por_rut?rut=" + txtBuscarRut.Text, "", "");

            if (userjson != "null")
            {
                Usuario user = JsonConvert.DeserializeObject<Usuario>(userjson);
                if (user.profile_id == 4)
                {
                    txtNombrePaciente.Text = string.Format("{0} {1}", user.name, user.last_name);
                    txtIdPaciente.Text = user.id.ToString();
                }else
                {
                    txtNombrePaciente.Text = "";
                }     
                
            }else
            {
                txtNombrePaciente.Text = "";
                MessageBox.Show("Rut paciente no encontrado");
            }
        }

        private void cbxArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id_area = cbxArea.SelectedValue;

            var medicosjson = conexion.ejecutarLlamada("GET", "buscar_por_area?area_id=" + id_area, "", "");

            JArray arrJson = JArray.Parse(medicosjson);

            List<Medico> medicos = new List<Medico>();

            foreach (JObject jsonOperaciones in arrJson.Children<JObject>())
            {
                Medico medico = new Medico();
                medico.id = Convert.ToInt32(jsonOperaciones["id"]);
                medico.nombre = jsonOperaciones["nombre"].ToString();
                medico.apellido = jsonOperaciones["apellido"].ToString();
                medico.nombreCompleto = jsonOperaciones["nombre"].ToString() + " " + jsonOperaciones["apellido"].ToString();

                medicos.Add(medico);
            }

            cbxMedico.ItemsSource = medicos;
            cbxMedico.DisplayMemberPath = "nombreCompleto";
            cbxMedico.SelectedValuePath = "id";
        }

        private void dpFechaHora_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFechaHora.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("La fecha no puede ser menor o igual a la de hoy");
            }else
            {
                DateTime fecha = Convert.ToDateTime(dpFechaHora.SelectedDate);
                var id_medico = cbxMedico.SelectedValue;
                var modulosjson = conexion.ejecutarLlamada("POST", "disponibles_por_medico?medico_id=" + id_medico + "&fechaReserva=" + fecha, "", null);

                JArray arrJson = JArray.Parse(modulosjson);

                List<string> modulos_hrs = new List<string>();

                modulos_hrs.Add("Seleccione");
                modulos_hrs.AddRange(JsonConvert.DeserializeObject<List<string>>(modulosjson));

                cbxModulo.ItemsSource = modulos_hrs;
            }
        }

        private void btnAgendarHora_Click(object sender, RoutedEventArgs e) {

            var area_id = cbxArea.SelectedValue;
            var id_medico = cbxMedico.SelectedValue;
            var id_usuario = txtIdPaciente.Text;
            var fecha = dpFechaHora.SelectedDate;
            var hora = cbxModulo.SelectedValue;

            string errores = string.Empty;

            if (txtBuscarRut.Text == string.Empty){errores += "- Rut no puede estar vacío.";}
            if (txtNombrePaciente.Text == string.Empty){errores += "\n- Nombre del paciente no puede estar vacío.";}
            if (area_id == null) { errores += "\n- Debe seleccionar un área"; }
            if (id_medico == null){errores += "\n- Debe seleccionar un médico.";}
            if (fecha <= DateTime.Now){errores += "\n- La fecha no puede ser menor o igual a la de hoy.";}
            if (hora == null){ errores += "\n- Debe seleccionar un módulo de atención."; }

            if (errores == string.Empty)
            {
                var json = new { idMedico = id_medico, idUsuario = id_usuario, fechaReserva = fecha, horaReserva = hora };
                var result = conexion.ejecutarLlamada("POST", "registrar_hora", "", json);

                if (result != null)
                {
                    MessageBox.Show("Hora registrada correctamente.");
                    txtBuscarRut.Text = string.Empty;
                    txtNombrePaciente.Text = string.Empty;
                    txtIdPaciente.Text = string.Empty;
                    cbxMedico.ItemsSource = null;
                    cbxModulo.ItemsSource = null;
                }
                else
                {
                    MessageBox.Show("Error al registrar la hora", "Oops!", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }else
            {
                MessageBox.Show(errores, "Oops!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }          
            
        }

        private void btnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            cbxPerfilesEdit.ItemsSource = null;
            cbxPerfilesEdit.DisplayMemberPath = null;
            cbxPerfilesEdit.SelectedValuePath = null;

            lblTitle.Content = "Agregar nuevo usuario:";

            gvUsers.SelectedIndex = -1;

            // Reseteo el formulario
            txtIdUserEdit.Text = string.Empty;
            txtNombreEdit.Text = string.Empty;
            txtApellidoEdit.Text = string.Empty;
            txtEmailEdit.Text = string.Empty;
            txtRutEdit.Text = string.Empty;
            txtTelefonoEdit.Text = string.Empty;

            // Oculto el botón de áreas y lo muestro solo si el usuario a agregar será médico.
            cbxAreaEdit.Visibility = Visibility.Hidden;
            lblAreaEdit.Visibility = Visibility.Hidden;

            List<Perfil> perfiles = new List<Perfil>();

            Perfil perfilSecretaria = new Perfil();
            perfilSecretaria.id = 2;
            perfilSecretaria.nombre = "Secretaria";

            Perfil perfilMedico = new Perfil();
            perfilMedico.id = 3;
            perfilMedico.nombre = "Médico";

            perfiles.Add(perfilSecretaria);
            perfiles.Add(perfilMedico);

            cbxPerfilesEdit.ItemsSource = perfiles;
            cbxPerfilesEdit.DisplayMemberPath = "nombre";
            cbxPerfilesEdit.SelectedValuePath = "id";

            lblPerfilEdit.Visibility = Visibility.Visible;
            cbxPerfilesEdit.Visibility = Visibility.Visible;

            gridNewEdit.Visibility = Visibility.Visible;
            btnAddNew.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;
        }

        private void cbxPerfilesEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPerfilesEdit.SelectedValue != null)
            {
                if (cbxPerfilesEdit.SelectedValue.ToString() == "3")
                {
                    // Llenamos el combobox de areas
                    var areasjson = conexion.ejecutarLlamada("GET", "areas", "", "");

                    JArray arrJson = JArray.Parse(areasjson);

                    List<Area> areas = new List<Area>();

                    foreach (JObject jsonOperaciones in arrJson.Children<JObject>())
                    {
                        Area area = new Area();
                        area.id = Convert.ToInt32(jsonOperaciones["id"]);
                        area.nombre = jsonOperaciones["nombre"].ToString();

                        areas.Add(area);
                    }

                    cbxAreaEdit.ItemsSource = areas;
                    cbxAreaEdit.DisplayMemberPath = "nombre";
                    cbxAreaEdit.SelectedValuePath = "id";

                    cbxAreaEdit.Visibility = Visibility.Visible;
                    lblAreaEdit.Visibility = Visibility.Visible;
                }
                else
                {
                    cbxAreaEdit.Visibility = Visibility.Hidden;
                    lblAreaEdit.Visibility = Visibility.Hidden;
                }
            }          
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombreEdit.Text;
            string apellido = txtApellidoEdit.Text;
            string email = txtEmailEdit.Text;
            string rut = txtRutEdit.Text;
            string pass1 = txtPassword1.Password.ToString();
            string pass2 = txtPassword2.Password.ToString();
            int profile_id = Convert.ToInt32(cbxPerfilesEdit.SelectedValue);
            string telefono = txtTelefonoEdit.Text;

            if (nombre == string.Empty || apellido == string.Empty || email == string.Empty || rut == string.Empty || pass1 == string.Empty || pass2 == string.Empty)
            {
                MessageBox.Show("Debes llenar todos los datos.");
            }else
            {
                if (pass1 == pass2)
                {
                    int area_id;
                    if (cbxPerfilesEdit.SelectedValue.ToString() == "3")
                    {
                        area_id = Convert.ToInt32(cbxAreaEdit.SelectedValue);
                    }else
                    {
                        area_id = 0;
                    }
                    var jsonObject = new { name = nombre, last_name = apellido, password = pass1, password_confirmation = pass2, rut = rut, email = email, profile_id = profile_id, area_id = area_id, phone_number = telefono };

                    string result = conexion.ejecutarLlamada("POST", "users", "", jsonObject);
                    if (result != "false")
                    {
                        resetGV();

                        // Reseteo el formulario
                        txtIdUserEdit.Text = string.Empty;
                        txtNombreEdit.Text = string.Empty;
                        txtApellidoEdit.Text = string.Empty;
                        txtEmailEdit.Text = string.Empty;
                        txtRutEdit.Text = string.Empty;
                        txtTelefonoEdit.Text = string.Empty;
                        MessageBox.Show("Registro exitoso");
                    }
                }
                else
                {
                    MessageBox.Show("Contraseñas no coinciden");
                }
            }

        }

        private void gvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvUsers.SelectedIndex != -1)
            {
                cbxPerfilesEdit.ItemsSource = null;
                cbxPerfilesEdit.DisplayMemberPath = null;
                cbxPerfilesEdit.SelectedValuePath = null;

                lblTitle.Content = "Editar Usuario:";

                Usuario user = this.gvUsers.SelectedItem as Usuario;
                int id_usuario = user.id;

                txtIdUserEdit.Text = user.id.ToString();
                txtNombreEdit.Text = user.name;
                txtApellidoEdit.Text = user.last_name;
                txtEmailEdit.Text = user.email;
                txtRutEdit.Text = user.rut;
                txtTelefonoEdit.Text = user.phone_number;

                lblAreaEdit.Visibility = Visibility.Hidden;
                lblPerfilEdit.Visibility = Visibility.Hidden;
                cbxAreaEdit.Visibility = Visibility.Hidden;
                cbxPerfilesEdit.Visibility = Visibility.Hidden;

                gridNewEdit.Visibility = Visibility.Visible;
                btnAddNew.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Visible;
            }else
            {
                cbxPerfilesEdit.ItemsSource = null;
                cbxPerfilesEdit.DisplayMemberPath = null;
                cbxPerfilesEdit.SelectedValuePath = null;
            }
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wMain = new MainWindow();
            wMain.Owner = this;
            wMain.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            string nombre = txtNombreEdit.Text;
            string apellido = txtApellidoEdit.Text;
            string email = txtEmailEdit.Text;
            string rut = txtRutEdit.Text;
            string pass1 = txtPassword1.Password.ToString();
            string pass2 = txtPassword2.Password.ToString();
            string telefono = txtTelefonoEdit.Text;
            string id_user = txtIdUserEdit.Text;

            string result = string.Empty;
            if (pass1 == "" || pass2 == "")
            {
                var jsonObject = new { name = nombre, last_name = apellido, rut = rut, email = email, phone_number = telefono };
                result = conexion.ejecutarLlamada("PUT", "users/" + id_user, "", jsonObject);
            }
            else
            {
                var jsonObject = new { name = nombre, last_name = apellido, password = pass1, rut = rut, email = email, phone_number = telefono };
                result = conexion.ejecutarLlamada("PUT", "users/" + id_user, "", jsonObject);
            }
            if (pass1 == pass2)
            {
                if (result != "false")
                {
                    resetGV();
                    MessageBox.Show("Usuario editado correctamente");
                }
                else
                {
                    MessageBox.Show("Error al editar el usuario");
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden");
            }
        }

        private void btnEliminarUser_Click(object sender, RoutedEventArgs e)
        {
            string id_user = txtIdUserEdit.Text;

            var result = conexion.ejecutarLlamada("DELETE", "eliminar_medico/"+ id_user, "", null);
            if (result != "false")
            {
                resetGV();
                txtIdUserEdit.Text = string.Empty;
                txtNombreEdit.Text = string.Empty;
                txtApellidoEdit.Text = string.Empty;
                txtEmailEdit.Text = string.Empty;
                txtRutEdit.Text = string.Empty;
                txtTelefonoEdit.Text = string.Empty;
                MessageBox.Show("Usuario Eliminado correctamente");
            }else
            {
                MessageBox.Show("Error al eliminar el usuario");
            }

        }

        public void resetGV()
        {
            var json = conexion.ejecutarLlamada("GET", "users" + "", "", null);
            List<Usuario> users = new List<Usuario>();

            List<Usuario> nueva_lista = new List<Usuario>();

            users = JsonConvert.DeserializeObject<List<Usuario>>(json);

            foreach (Usuario item in users)
            {
                if (item.profile_id != 4 && item.profile_id != 1)
                {
                    Usuario user = new Usuario();

                    user.id = item.id;
                    user.email = item.email;
                    user.name = item.name;
                    user.last_name = item.last_name;
                    user.phone_number = item.phone_number;
                    user.rut = item.rut;
                    user.profile_id = item.profile_id;

                    nueva_lista.Add(user);
                }


            }
            gvUsers.ItemsSource = nueva_lista;
        }

        private void gvMedicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(gvMedicos.SelectedIndex != -1)
            {
                Usuario user = this.gvMedicos.SelectedItem as Usuario;
                string nombre = user.fullName;
                lblTitleAgenda.Content = "Agenda de " + nombre;
                var jsonModulos = conexion.ejecutarLlamada("GET", "atention_modules", "", null);
                List<Modulo> modulos = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);
                List<Modulo> modulosMartes = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);
                List<Modulo> modulosMiercoles = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);
                List<Modulo> modulosJueves = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);
                List<Modulo> modulosViernes = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);
                List<Modulo> modulosSabado = JsonConvert.DeserializeObject<List<Modulo>>(jsonModulos);

                gvLunes.ItemsSource = modulos;
                gvMartes.ItemsSource = modulosMartes;
                gvMiercoles.ItemsSource = modulosMiercoles;
                gvJueves.ItemsSource = modulosJueves;
                gvViernes.ItemsSource = modulosViernes;
                gvSabado.ItemsSource = modulosSabado;

                gridAgendas.Visibility = Visibility.Visible;
            }else
            {
                gvMedicos.SelectedIndex = -1;
            }
        }

        private void btnBuscarHora_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRutReg.Text;
            var jsonBusqueda = conexion.ejecutarLlamada("GET", "horas_por_rut?rut=" + rut, "", null);
            int count = 0;
            if (jsonBusqueda != "null")
            {
                List<Agenda> agendas = JsonConvert.DeserializeObject<List<Agenda>>(jsonBusqueda);

                List<HoraAtencion> agenda_list = new List<HoraAtencion>();

                foreach (Agenda item in agendas)
                {
                    if (item.estado_agenda == null)
                    {
                        HoraAtencion ht = new HoraAtencion();

                        var jsonModulo = conexion.ejecutarLlamada("GET", "atention_modules/" + item.atention_module_id, "", null);
                        Modulo modulo = JsonConvert.DeserializeObject<Modulo>(jsonModulo);

                        ht.id = item.id;
                        ht.hora_atencion = modulo.start_time;

                        agenda_list.Add(ht);
                        count++;
                    }
                    
                }

                if (count > 0)
                {
                    cbxHorasPaciente.Visibility = Visibility.Visible;
                    lblHorasAgregadas.Visibility = Visibility.Visible;
                    cbxHorasPaciente.ItemsSource = agenda_list;
                    cbxHorasPaciente.DisplayMemberPath = "hora_atencion";
                    cbxHorasPaciente.SelectedValuePath = "id";
                }
                else
                {
                    MessageBox.Show("No se han encontrado horas para este rut.");
                }

                
            }else
            {
                MessageBox.Show("No se han encontrado horas para este rut.");
            }
            
        }

        private void btnClickNewReg_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRutReg.Text;
            var jsonPaciente = conexion.ejecutarLlamada("GET", "buscar_por_rut?rut="+rut, "", null);
            Usuario user = JsonConvert.DeserializeObject<Usuario>(jsonPaciente);

            int user_id = user.id;
            int agenda_id = Convert.ToInt32(cbxHorasPaciente.SelectedValue);
            int payment_type_id = Convert.ToInt32(cbxMetodo.SelectedValue);
            int monto = int.Parse(txtValorReg.Text);

            var objJson = new { user_id = user_id, agenda_id = agenda_id, payment_type_id = payment_type_id, monto = monto };
            string result = conexion.ejecutarLlamada("POST", "payments", "", objJson);

            if (result != "false")
            {
                Payment payment = JsonConvert.DeserializeObject<Payment>(result);
                int agen_id = payment.agenda_id;
                var jsonEdit = new { estado_agenda = "En espera" };
                string edit_agenda = conexion.ejecutarLlamada("PUT", "agendas/" + agen_id, "", jsonEdit);

                MessageBox.Show("Atención registrada correctamente");
            }else
            {
                MessageBox.Show("Error al registrar la atención");
            }
        }
    }
}
