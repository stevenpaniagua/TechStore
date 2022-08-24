using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GrupoTec
{
    internal class MongoDB
    {
        public MongoClient client;

        public MongoDB()
        {
        }

        public void initDatabase()
        {
            // Se inicializa la base de datos.
            client = new MongoClient("uri");
        }

        // Método para encontrar la colección necesaria (para posteriormente realizar queries).
        public IMongoCollection<BsonDocument> getCollection(String collection)
        {
            return client.GetDatabase("grupotec").GetCollection<BsonDocument>(collection);
        }
    }
}
