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
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
            mongoDB = new MongoDB();
        }

        private MongoDB mongoDB;

        private void btn1_Click(object sender, EventArgs e)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("username", txtUser.Text);

            // Se detecta si el usuario está registrado en la base de datos o no.
            var find = mongoDB.getCollection("Usuarios").Find(filter).FirstOrDefault();
            if (find != null)
            {
                // Los datos insertados son incorrectos.
                MessageBox.Show("An user by that name is already registered.");
                return;
            }

            // Se crea el documento del usuario (BSON).
            var doc = new BsonDocument
            {
                {"username", txtUser.Text},
                {"password", txtPass.Text}
            };
            // Se inserta el documento a la base de datos.
            mongoDB.getCollection("Usuarios").InsertOne(doc);
            var documents = mongoDB.getCollection("Usuarios").Find(new BsonDocument()).ToList();

            // Una vez insertado, se actualiza la data grid.
            this.update(documents);
            MessageBox.Show("Successfully registered new user: " + txtUser.Text + ".");
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            mongoDB.initDatabase();
            var documents = mongoDB.getCollection("Usuarios").Find(new BsonDocument()).ToList();
            this.update(documents);
        }

        private void update(List<BsonDocument> documents)
        {
            // Se crean las columnas y se insertan las rows para cada documento/producto en la base de datos.
            DataTable dt = new DataTable();

            dt.Columns.Add("Usuario");
            dt.Columns.Add("Contraseña");

            foreach (var x in documents)
            {
                dt.Rows.Add(x["username"].AsString, x["password"].AsString);
                Application.DoEvents();
            }
            dataGridView1.DataSource = dt;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            DialogResult n = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Usuarios", MessageBoxButtons.YesNo);
            if (n == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
            }
        }
    }
}
