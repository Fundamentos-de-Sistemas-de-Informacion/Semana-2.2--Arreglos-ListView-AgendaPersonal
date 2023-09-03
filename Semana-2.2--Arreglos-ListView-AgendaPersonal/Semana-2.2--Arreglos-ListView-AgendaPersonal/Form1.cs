using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semana_2._2__Arreglos_ListView_AgendaPersonal
{
    public partial class Form1 : Form
    {
        Usuario[] usuarios = new Usuario[100];

        int cont = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxFecha_Leave(object sender, EventArgs e)
        {
            // Verificar si la fecha ingresada es válida
            int fecha;
            if (!int.TryParse(textBoxFecha.Text, out fecha) || fecha < 1 || fecha > 31)
            {
                MessageBox.Show("Ingrese un número válido del 1 al 31 para la fecha");
                textBoxFecha.Focus();
            }
        }

        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            //Validación
            if (textBoxAsunto.Text == "" || textBoxDescripcion.Text == "" || comboBoxDia.Text == "" || textBoxFecha.Text == "")
            {
                MessageBox.Show("Ingrese todos los campos necesarios");
                return;
            }

            //Crear el objeto
            Usuario usuario = new Usuario();
            usuario.Asunto = textBoxAsunto.Text;
            usuario.Descripcion = textBoxDescripcion.Text;
            usuario.Dia = comboBoxDia.Text;
            usuario.Fecha = int.Parse(textBoxFecha.Text);

            //Agregar el objeto al arreglo
            usuarios[cont] = usuario;
            cont++;
        }

        private void mostrar(Usuario[] arreglo, int cantidad)
        {
            listViewUsuarios.Items.Clear();

            for (int i = 0; i < cantidad; i++)
            {
                ListViewItem fila = new ListViewItem(arreglo[i].Asunto);
                fila.SubItems.Add(arreglo[i].Descripcion);
                fila.SubItems.Add(arreglo[i].Dia);
                fila.SubItems.Add(arreglo[i].Fecha.ToString());
                listViewUsuarios.Items.Add(fila);
            }
        }

        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            mostrar(usuarios, cont);
        }

        private class Metodo_Comparacion : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                int fecha1 = ((Usuario)x).Fecha;
                int fecha2 = ((Usuario)y).Fecha;

                if (fecha1 > fecha2) return 1;
                else return -1;
            }
        }

        private void buttonOrdenar_Click(object sender, EventArgs e)
        {
            Array.Sort(usuarios, 0, cont, new Metodo_Comparacion());
            mostrar(usuarios, cont);
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            //Validación
            if (listViewUsuarios.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione registro a eliminar");
                return;
            }

            //Obtener la posición
            int pos = Array.FindIndex(usuarios, usuario => usuario.Asunto == listViewUsuarios.SelectedItems[0].Text);

            //Eliminación
            for (int i = 0; i < cont; i++)
            {
                if (i >= pos)
                {
                    usuarios[i] = usuarios[i + 1];
                }
            }
            cont--;
            mostrar(usuarios, cont);
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            //Validación
            if (textBoxBuscar.Text == "")
            {
                MessageBox.Show("Inserte asunto a buscar");
                return;
            }

            Usuario[] usuariosTmp = Array.FindAll(usuarios, usuario => usuario != null && textBoxBuscar.Text == usuario.Asunto);
            mostrar(usuariosTmp, usuariosTmp.Length);
        }
    }
}
