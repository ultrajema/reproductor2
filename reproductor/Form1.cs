using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using reproductor.ListCircurlar;
using reproductor.listadobleenla;

namespace reproductor
{
    public partial class Form1 : Form
    {
        Listadoble ListaDoble = new Listadoble();
        listacircul circular = new listacircul();
        NodoC nuevo;
        OpenFileDialog CajaDeBusquedaDeArchivos = new OpenFileDialog(); //Para seleccionar las canciones que quiera
        String[] ArchivosMP3; //para almacenar todos los archivos seleccionados de forma temporal
        String[] rutasArchivoMP3;
        public Form1()
        {
            InitializeComponent();
            track_volume.Value = 50;
            lbl_volume.Text = "50%";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string[] paths, files;

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_addf.Enabled = true;
            player.URL = paths[track_list.SelectedIndex];
            player.Ctlcontrols.play();

            try
            {
                var file = TagLib.File.Create(paths[track_list.SelectedIndex]);
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                pic_art.Image = Image.FromStream(new MemoryStream(bin));

            }
            catch { }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            p_bar.Value = 0;
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();

        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            cambiar();
        }
        public void cambiar()
        {
            if (track_list.SelectedIndex < track_list.Items.Count - 1)
            {
                track_list.SelectedIndex = track_list.SelectedIndex + 1;
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex > 0)
            {
                track_list.SelectedIndex = track_list.SelectedIndex - 1;
            }
        }

        private void p_bar_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                p_bar.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                p_bar.Value = (int)player.Ctlcontrols.currentPosition;
                lbl_track_end.Text = player.Ctlcontrols.currentItem.durationString.ToString();
                lbl_track_start.Text = player.Ctlcontrols.currentPositionString;
                if (p_bar.Maximum == p_bar.Value)
                {
                    cambiar();
                }


            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = track_volume.Value;
            lbl_volume.Text = track_volume.Value.ToString() + "%";
        }

        private void p_bar_MouseDown(object sender, MouseEventArgs e)//controlador de la barra de progreso segun donde se de click
        {
            player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / p_bar.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn_addf.Enabled = false;
            favori_list.Items.Add(player.URL);
        }

        private void favori_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = favori_list.Text;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            StreamWriter Save = new StreamWriter(@"C:\Users\emanu\source\repos\reproductor\reproductor\Storage\Favorite.txt");
            foreach (var item in favori_list.Items)
            {
                Save.WriteLine(item.ToString());
                this.Refresh();


            }
            MessageBox.Show("Guardado en favoritos");
            Save.Close();
            favori_list.Items.Clear();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            using (StreamReader read = new StreamReader(@"C:\Users\emanu\source\repos\reproductor\reproductor\Storage\Favorite.txt"))
            {
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    favori_list.Items.Add(line);
                }
            }

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (favori_list.SelectedItems.Count > 0)
            {
                favori_list.Items.Remove(favori_list.SelectedItems[0]);
                MessageBox.Show("Se elimino de favoritos");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (nuevo != null)
            {
                nuevo = circular.lc.enlace;

                while (nuevo == circular.lc.enlace)
                {
                    if (track_list.SelectedIndex < track_list.Items.Count - 1)
                    {
                        MessageBox.Show("AUN NO HA LLEGADO A LA ULTIMA CANCION");
                        track_list.SelectedIndex += 1;
                        nuevo = nuevo.enlace;
                    }
                    else
                    {
                        MessageBox.Show("¿QUIERES VOLVER A LA PRIMERA CANCION DE LA LISTA?");
                        player.URL = CajaDeBusquedaDeArchivos.FileNames[0];
                        track_list.SelectedIndex = 0;
                        nuevo = nuevo.enlace;
                    }

                    nuevo = nuevo.enlace;
                }
            }
            else
            {
                MessageBox.Show("\t LA LISTA SE ENCUENTRA VACIA.");
            }

        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            CajaDeBusquedaDeArchivos.Multiselect = true; 

            
            if (CajaDeBusquedaDeArchivos.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                rutasArchivoMP3 = CajaDeBusquedaDeArchivos.SafeFileNames; 
                ArchivosMP3 = CajaDeBusquedaDeArchivos.FileNames; 

                
                for (int i = 0; i < CajaDeBusquedaDeArchivos.FileNames.Length; i++)
                {
                    string rutas;
                    rutas = rutasArchivoMP3[i]; 
                   
                   ListaDoble.insertarCabezaLista(rutas); 
                    circular.insertar(rutas); 
                }

                string[] datos = ListaDoble.vizualizarTam(); 

                foreach (string dato in datos) 
                {
                    track_list.Items.Add(dato);   
                }
                

                int pausa;
                pausa = 0;


            }

        }
    }

    
}
