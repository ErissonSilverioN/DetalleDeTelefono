using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TelefonoDetalle.BLL;
using TelefonoDetalle.Entidades;


namespace TelefonoDetalle.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPersonas.xaml
    /// </summary>
    public partial class rPersonas : Window
    {
        public List<TelefonosDetalle> Detalles { get; set; }
        public rPersonas()
        {
            InitializeComponent();
            this.Detalles = new List<TelefonosDetalle>();
            IdTextBox.Text = "0";


        }


        private void LimpiarCampos()
        {
            nombreTextBox.Text = string.Empty;
            direccionTextBox.Text = string.Empty;
            cedulaTextBox.Text = string.Empty;
            fechanacDatePicker.SelectedDate = DateTime.Now;
            IdTextBox.Text = "0";

            this.Detalles = new List<TelefonosDetalle>();
            CargarGrid();

                
        }

        private Personas LlenaClase()
        {
            Personas personas = new Personas();

            personas.PersonaId = Convert.ToInt32(IdTextBox.Text);
            personas.Nombre = nombreTextBox.Text;
            personas.Direccion = direccionTextBox.Text;
            personas.Cedula = cedulaTextBox.Text;
            personas.FechaNacimiento = fechanacDatePicker.DisplayDate;

            personas.Telefonos = this.Detalles; //Agregando el detalle al LlenaClase

            return personas;
        }

        private void LlenaCampo(Personas personas)
        {
            IdTextBox.Text = Convert.ToString(personas.PersonaId);
            nombreTextBox.Text = personas.Nombre;
            direccionTextBox.Text = personas.Direccion;
            cedulaTextBox.Text = personas.Cedula;
            fechanacDatePicker.SelectedDate = personas.FechaNacimiento;

            this.Detalles = personas.Telefonos;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(nombreTextBox.Text))
            {
                MessageBox.Show("Debe llenar el Campo Nombre!!");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(direccionTextBox.Text))
            {
                MessageBox.Show("Debe llenar el Campo Direccion!!");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(cedulaTextBox.Text))
            {
                MessageBox.Show("Debe llenar el Campo Cedula!!");
                paso = false;
            }

            if (this.Detalles.Count == 0)
            {
                MessageBox.Show("Debe Agregar un Telefono!!");
                paso = false;
            }

            return paso;
        }

        private bool ExisteEnLaBaseDatos()
        {
            Personas personas = PersonaBLL.Buscar((int)Convert.ToInt32(IdTextBox.Text));
            return (personas != null);
        }

        private void CargarGrid()
        {
            telefonodetalleDataGrid.ItemsSource = null;
            telefonodetalleDataGrid.ItemsSource = this.Detalles;
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas personas;
            bool paso = false;

            if (!Validar())
                return;

            personas = LlenaClase();


            if (IdTextBox.Text == "0")
                paso = PersonaBLL.Guardar(personas);

            else
            {
                if (!ExisteEnLaBaseDatos())
                {
                    MessageBox.Show("Personas No Existe!!");
                }
                MessageBox.Show("Persona Modificada!!");
                paso = PersonaBLL.Modificar(personas);
            }

            if (paso)
            {
                MessageBox.Show("¡¡Guardado!!");
            }
            else
            {
                MessageBox.Show("No se Guardo!!");
            }

        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);
            Personas personas = new Personas();

            personas = PersonaBLL.Buscar(id);

            if (personas != null)
            {
               
                LlenaCampo(personas);
            }
            else
            {
                MessageBox.Show("Persona No Encontrada!!");
            }


        }

        private void eliminarButton__Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(IdTextBox.Text, out id);


            if (PersonaBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado!!");
            }
            else
            {
                MessageBox.Show("No se pudo Eliminar!!");
            }
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (telefonodetalleDataGrid.SelectedItem != null)
                this.Detalles = (List<TelefonosDetalle>)telefonodetalleDataGrid.ItemsSource;


            this.Detalles.Add(
                new TelefonosDetalle(
                    id: 0,
                    personaId: Convert.ToInt32(IdTextBox.Text),
                    tipoTelefono: tipotelefonoTextBox.Text,
                    telefono: telefonoTextBox.Text

                    ));

            CargarGrid();
            telefonoTextBox.Clear();
            tipotelefonoTextBox.Clear();
        }

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (telefonodetalleDataGrid.Columns.Count > 0 && telefonodetalleDataGrid.CurrentColumn != null)

                Detalles.RemoveAt(telefonodetalleDataGrid.CurrentColumn.DisplayIndex);

            CargarGrid();
        }
    }
}
