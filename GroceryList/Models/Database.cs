using System;
using MySql.Data.MySqlClient;
using GroceryList;

namespace GroceryList.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}