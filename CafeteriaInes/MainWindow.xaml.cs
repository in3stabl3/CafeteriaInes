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

namespace CafeteriaInes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float precio = 0;

        ProyectoEntities DbEntityes = new ProyectoEntities();
        public static DataGrid ControlDatagrid; 
       

        public MainWindow()
        {
            InitializeComponent();
            MyDG.ItemsSource = DbEntityes.PROVEEDORES.ToList();
            ControlDatagrid = MyDG;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            PROVEEDORES MyProducto = new PROVEEDORES()
            {
                Nombre = txtNombre.Text,
                Stock = int.Parse(txtStock.Text),
                Precio = int.Parse(txtPrecio.Text),
                Pedido= int.Parse(txtPedido.Text)
            };

            DbEntityes.PROVEEDORES.Add(MyProducto);
            DbEntityes.SaveChanges();
            MainWindow.ControlDatagrid.ItemsSource = DbEntityes.PROVEEDORES.ToList();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            PROVEEDORES MyProducto = DbEntityes.PROVEEDORES.Single(z => z.Nombre == txtNombre.Text);
            MyProducto.Nombre = txtNombre.Text;
            MyProducto.Stock = Convert.ToInt32(txtStock.Text);
            MyProducto.Precio = Convert.ToDecimal(txtPrecio.Text);
            MyProducto.Pedido = Convert.ToInt32(txtPedido.Text);
            DbEntityes.SaveChanges();
            MainWindow.ControlDatagrid.ItemsSource = DbEntityes.PROVEEDORES.ToList();

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string name = (MyDG.SelectedItem as PROVEEDORES).Nombre;
            var deleteProduct = DbEntityes.PROVEEDORES.Where(m => m.Nombre == name).Single();
            DbEntityes.PROVEEDORES.Remove(deleteProduct);
            DbEntityes.SaveChanges();
            ControlDatagrid.ItemsSource = DbEntityes.PROVEEDORES.ToList();

        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float devuelto = 0;
                float precioTotal = 0;
                
                devuelto = (Convert.ToSingle(this.txtEfectivo.Text));
                precioTotal = (Convert.ToSingle(this.txtTotal.Text));

                
                if (precioTotal < Convert.ToInt32(txtEfectivo.Text))
                {
                    devuelto = ((Convert.ToSingle(this.txtEfectivo.Text)) - precioTotal);
                    MessageBox.Show("El precio total es: " + precioTotal + " Y la cantidad devuelta es: " + devuelto);
                }
                else
                {
                    MessageBox.Show("No tienes dinero suficiente.");
                }
            }
            catch (Exception err)

            {
                MessageBox.Show(err.Message);
            }
        }

        private void MyDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (MyDG.SelectedItem != null)
                {
                    PROVEEDORES proveedor = (PROVEEDORES)MyDG.SelectedItem;
                    txtNombre.Text = proveedor.Nombre;
                    txtStock.Text = proveedor.Stock.ToString();
                    txtPrecio.Text = proveedor.Precio.ToString();
                    txtPedido.Text = proveedor.Precio.ToString();
                }
            }
            catch (Exception)

            {
                MessageBox.Show("Seleccione una fila");
            }
        }

        private void calcularPrecio(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            precio += float.Parse(boton.Tag.ToString());
            txtTotal.Text = precio.ToString();
        }
    }
}
