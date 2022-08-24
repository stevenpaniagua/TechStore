using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GrupoTec
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
            mongoDB = new MongoDB();
        }

        private MongoDB mongoDB;

        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                // Declaración de objetos desde la data grid.
                int cod, cant;
                float precio, pago, descuento;
                string nombre;

                cod = int.Parse(txtCod.Text);
                nombre = (txtNom.Text);
                cant = int.Parse(txtCant.Text);
                precio = float.Parse(txtPrc.Text);
                descuento = float.Parse(txtDesc.Text);

                var filter = Builders<BsonDocument>.Filter.Eq("cod", txtCod.Text);

                // Se detecta si el producto está registrado en la base de datos o no.
                var find = mongoDB.getCollection("Productos").Find(filter).FirstOrDefault();
                if (find != null)
                {
                    // El código del producto ya existe en la base de datos.
                    MessageBox.Show("A product by that code is already registered.");
                    return;
                }

                // Se crea el documento del producto (BSON).
                var doc = new BsonDocument
                {
                    {"cod", cod},
                    {"nom", nombre},
                    {"cant", cant},
                    {"prc", precio},
                    {"desc", descuento}
                };
                // Se inserta el documento a la base de datos.
                mongoDB.getCollection("Productos").InsertOne(doc);
                var documents = mongoDB.getCollection("Productos").Find(new BsonDocument()).ToList();

                // Una vez insertado, se actualiza la data grid.
                this.update(documents);
                MessageBox.Show("Successfully registered new product: " + cod + ".");

            } catch (FormatException)
            {
                // Algún valor insertado no fue numérico.
                MessageBox.Show("All values except the product name must be numerical.");
                return;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCod.Text = "";
            txtNom.Text = "";
            txtCant.Text = "";
            txtPrc.Text = "";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            DialogResult n = MessageBox.Show("¿Desea Salir?", "Registro", MessageBoxButtons.YesNo);
            if (n == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            mongoDB.initDatabase();
            var documents = mongoDB.getCollection("Productos").Find(new BsonDocument()).ToList();
            this.update(documents);
        }

        private void update(List<BsonDocument> documents)
        {
            // Se crean las columnas y se insertan las rows para cada documento/producto en la base de datos.
            DataTable dt = new DataTable();

            dt.Columns.Add("Código");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Descuento");

            foreach (var x in documents)
            {
                dt.Rows.Add(x["cod"].AsInt32, x["nom"].AsString, x["cant"].AsInt32, x["prc"].AsDouble, x["desc"].AsDouble);
                Application.DoEvents();
            }
            dataGridView1.DataSource = dt;
        }
    }
}
