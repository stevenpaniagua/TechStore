using MongoDB.Bson;
using MongoDB.Driver;

namespace GrupoTec
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // Implementar MongoDB
            try
            {
                MongoDB mongoDB = new MongoDB();
                mongoDB.initDatabase();

                var filter = Builders<BsonDocument>.Filter.Eq("username", txtUser.Text) & Builders<BsonDocument>.Filter.Eq("password", txtPass.Text);

                // Se detecta si el usuario está registrado en la base de datos o no.
                var doc = mongoDB.getCollection("Usuarios").Find(filter).FirstOrDefault();
                if (doc == null)
                {
                    // Los datos insertados son incorrectos.
                    MessageBox.Show("Couldn't connect to database.");
                    return;
                }
                this.Hide();
                // Abrir menú principal (entrega semana 10)
                MessageBox.Show("Connected to database!");
                new PanelAdministrador().Show();

            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't connect to database.");
            }
        }
    }
}