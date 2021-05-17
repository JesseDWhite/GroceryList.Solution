using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GroceryList.Models
{
  public class Store
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public List<Item> Items { get; set; }

    public Store(string storeName)
    {
      Name = storeName;
      Items = new List<Item> { };
    }

    public Store(string storeName, int id) : this(storeName)
    {
      Id = id;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stores (name) VALUES (@StoreName);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@StoreName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stores;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Store> GetAll()
    {
      List<Store> allStores = new List<Store> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stores;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int storeId = rdr.GetInt32(0);
        string storeName = rdr.GetString(1);
        Store newStore = new Store(storeName, storeId);
        allStores.Add(newStore);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStores;

    }

    public static Store Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stores WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int storeId = 0;
      string storeName = "";
      while (rdr.Read())
      {
        storeId = rdr.GetInt32(0);
        storeName = rdr.GetString(1);
      }
      Store foundStore = new Store(storeName, storeId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStore;
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }
  }
}