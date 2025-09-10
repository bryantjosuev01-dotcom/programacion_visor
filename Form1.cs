using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Visor_imagenes
{
    public partial class Form1 : Form
    {
        string[] archivos;
        int indice = 0;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string carpeta = @"C:\Users\ELITE X2\Documents\Imagenes";
            archivos = Directory.GetFiles(carpeta, "*.jpeg");

            if (archivos.Length > 0)
            {
                foreach (string archivo in archivos)
                {
                    comboBox.Items.Add(Path.GetFileName(archivo)); // nombre correcto
                }

                comboBox.SelectedIndex = 0;
                MostrarImagen(0);
            }
            else
            {
                MessageBox.Show("No se encontraron imágenes JPG en la carpeta.");
            }
        }


        private void MostrarImagen(int i)
        {
            if (i >= 0 && i < archivos.Length)
            {
                Image img = Image.FromFile(archivos[i]);
                pictureBox1.Image = img;
                toolStripStatusLabel1.Text = archivos[i];
                comboBox.SelectedIndex = i;
                indice = i;
            }
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (archivos != null && indice >= 0)
            {
                MostrarImagen(indice); // vuelve a cargar la imagen original
                checkNormal.Checked = true;
                checkGrises.Checked = false;
            }
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
            
        {
           
            if (pictureBox1.Image != null)
            {
                Image img = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = img; // Actualiza el control para mostrar la imagen rotada
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (archivos != null && indice < archivos.Length - 1)
                MostrarImagen(++indice);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (archivos != null && indice > 0)
                MostrarImagen(--indice);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (archivos != null)
                MostrarImagen(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MostrarImagen(archivos.Length - 1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkGrises.Checked)
            {
                checkNormal.Checked = false;
                MostrarImagen(indice); // vuelve a la imagen original
            }
            if (pictureBox1.Image != null && checkGrises.Checked)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color c = bmp.GetPixel(x, y);
                        int gris = (c.R + c.G + c.B) / 3;
                        bmp.SetPixel(x, y, Color.FromArgb(gris, gris, gris));
                    }
                }
                pictureBox1.Image = bmp;
            }
            else if (comboBox.SelectedIndex >= 0)
            {
                MostrarImagen(comboBox.SelectedIndex);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNormal.Checked)
            {
                checkGrises.Checked = false;
                MostrarImagen(indice); // vuelve a la imagen original
            }
        }

        private void centradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            rtCentrar.Checked = true;
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            rtajustar.Checked= true;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            rtzoom.Checked= true;
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarImagen(indice);
            checkNormal.Checked = true;
        }

        private void escalaDeGrisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color c = bmp.GetPixel(x, y);
                        int gris = (c.R + c.G + c.B) / 3;
                        bmp.SetPixel(x, y, Color.FromArgb(gris, gris, gris));
                    }
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                MessageBox.Show("No hay imagen cargada para convertir a escala de grises.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            checkNormal.Checked = false;
            checkGrises.Checked = true;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox.SelectedIndex;
            if (i >= 0 && archivos != null)
            {
                MostrarImagen(i);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (archivos != null && indice >= 0)
            {
                checkGrises.Checked = true;
                checkNormal.Checked = false;
                if (pictureBox1.Image != null && checkGrises.Checked)
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Image);
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            Color c = bmp.GetPixel(x, y);
                            int gris = (c.R + c.G + c.B) / 3;
                            bmp.SetPixel(x, y, Color.FromArgb(gris, gris, gris));
                        }
                    }
                    pictureBox1.Image = bmp;
                }
                else if (comboBox.SelectedIndex >= 0)
                {
                    MostrarImagen(comboBox.SelectedIndex);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            rtCentrar.Checked = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            rtajustar.Checked = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            rtzoom.Checked = true;
        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // Si hay imagen cargada
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos de imagen|*.png;*.jpg;*.bmp";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Imagen guardada correctamente.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No hay imagen para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea salir sin guardar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void girar90ºALaIzquierdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;

            // Verifica si hay una imagen cargada
            if (img != null)
            {
                Image imge = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = img;

            }
            }

        private void girar90ºALaDerechaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Image img = pictureBox1.Image;

            // Verifica si hay una imagen cargada
            if (img != null)
            {
                Image imge = pictureBox1.Image;
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Image = img;

            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                try
                {
                    Clipboard.SetImage(pictureBox1.Image);
                    MessageBox.Show("Imagen copiada");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la imagen: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna imagen para guardar.");
            }
        }
    }
}
