using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using GroceryList.Models;

namespace GroceryList.Controllers
{
  public class ItemsController : Controller
  {
    [HttpGet("/stores/{storeId}/items/new")]
    public ActionResult New(int storeId)
    {
      Store store = Store.Find(storeId);
      return View(store);
    }

    [HttpGet("/stores/{storeId}/items/{itemId}")]
    public ActionResult Show(int storeId, int itemId)
    {
      Item item = Item.Find(itemId);
      Store store = Store.Find(storeId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("item", item);
      model.Add("store", store);
      return View(model);
    }

    [HttpPost("/items/delete")]
    public ActionResult DeleteAll()

    {
      Item.ClearAll();

      return View();
    }

  }
}
