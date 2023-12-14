using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Etlap
{
	public class FoodService
	{
		private MySqlConnection connect;
		public FoodService()
		{ 
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";
			this.connect = new MySqlConnection(builder.ConnectionString);
		}

		public List<Etel> GetAll()
		{
			List<Etel> etelLista = new List<Etel>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand command = connect.CreateCommand();
			command.CommandText = sql;
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					Etel item = new Etel();
					item.Id = reader.GetInt32("id");
					item.Name = reader.GetString("name");
					item.Description = reader.GetString("description");
					item.Price = reader.GetInt32("price");
					item.Category = reader.GetString("category");
					etelLista.Add(item);
				}
			}
			CloseConnection();

			return etelLista;
		}

		public bool Create(Etel etel)
		{
			OpenConnection();
			string sql = "INSERT INTO etlap (nev, leiras, ar, kategoria) VALUES (@name, @description, @price, @category)";
			MySqlCommand command = this.connect.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@name", etel.Name);
			command.Parameters.AddWithValue("@description", etel.Description);
			command.Parameters.AddWithValue("@price", etel.Price);
			command.Parameters.AddWithValue("@category", etel.Category);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();

			return affectedRows == 1;
		}

		public bool Delete(int id)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id = @id";
			MySqlCommand command = this.connect.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@id", id);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();

			return affectedRows == 1;
		}

		public void OpenConnection()
		{
			if (connect.State != System.Data.ConnectionState.Open)
			{ 
				connect.Open();
			}
		}

		public void CloseConnection()
		{ 
			if (connect.State != System.Data.ConnectionState.Closed)
			{
				connect.Close();
			}
		}
	}
}
