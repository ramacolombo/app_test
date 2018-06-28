using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test_Rama
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private static readonly HttpClient postero = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ALBINOOOOOOOOOOOOOOOOOOO");

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            switch (select.Value)
            {
                case "Mongo":
                    MessageBox.Text = connectMongoAsync(ip.Value, puerto.Value, usu.Value, pw.Value, database.Value);
                    break;
                case "SQL":
                    MessageBox.Text = conectSql(ip.Value, puerto.Value, usu.Value, pw.Value,database.Value);
                    break;
                case "OPT":
                    MessageBox.Text = await ConnectOptAsync(ip.Value);
                    break;
            }
        }

        private String conectSql(string ip, string puerto, string usu, string pw,string database)
        {
            SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = "Data Source = CPX - 4S0TJIN43RE; Initial Catalog = DatabaseName; User ID = gonzalo.a.martin; Password = Berserk1!";

            var conex = "user id=" + usu + ";" +
                        "password=" + pw + " ;server=" + ip + ";" +
                        "Trusted_Connection=yes;" +
                        "database=" + database + "; " +
                        "connection timeout=30";

            conn.ConnectionString = conex;

            System.Diagnostics.Debug.WriteLine(conex);

            //id:albeno pw:Berserk1!
            Dictionary<string, Dictionary<string, string>> data_returned = new Dictionary<string, Dictionary<string, string>>();

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("Select * from test", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    var x = 0;
                    while (reader.Read())
                    {
                        // write the data on to the screen

                        Dictionary<string, string> fila = new Dictionary<string, string>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var key = reader.GetName(i);
                            var val = reader[i];
                            fila.Add(key, val.ToString());

                        }

                        data_returned.Add(x.ToString(), fila);
                        x++;
                    }
                    conn.Close();
                    var data = data_returned.ToJson().ToString();
                    return data;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            return "";
        }

        private String connectMongoAsync(string ip, string puerto, string user, string pass, string database)
        {
            System.Diagnostics.Debug.WriteLine("MONGO");
            var client = new MongoClient("mongodb://" + user + ":" + pass + "@" + ip + ":" + puerto + "");

            var db = client.GetDatabase(database);
            var coll = db.GetCollection<Albino>("Test");

            /* var newAlbino = new Albino{nombre = "DarkAlbino",id = "1",nombre_clave="Albino Trucho",aka ="AntiAlbino " };
             coll.InsertOne(newAlbino);*/

            System.Diagnostics.Debug.WriteLine(coll.ToJson().ToString());
            var id = new ObjectId("5b22bb3cd11d30efa2239431");
            var albinos = coll.Find(b => b.Id == id).ToListAsync().Result;


            for (var i = 0; i < albinos.Count(); i++)
            {

                var nombre = albinos[i].nombre;
                System.Diagnostics.Debug.WriteLine(albinos[i].ToJson().ToString());
                System.Diagnostics.Debug.WriteLine(nombre);
            }


            var json = albinos.ToJson().ToString();

            return json;

            /* var list = await coll.Find(new BsonDocument()).ToListAsync();
             foreach (var dox in list)
             ´¿'
             0+)p8i5ke{´8/9+9
                 Console.WriteLine(dox);
                 System.Diagnostics.Debug.WriteLine(dox.ToString());
             }*/


        }

        private async Task<string> ConnectOptAsync(string ip)
        {
            return await postero.GetStringAsync(ip);
        }

        public class Albino
        {
            public ObjectId Id { get; set; }
            public string id { get; set; }
            public string nombre { get; set; }
            public string nombre_clave { get; set; }
            public string aka { get; set; }


        }


    }
}