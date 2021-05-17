using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using GroceryList.Models;

namespace GroceryList.Controllers
{
  public class StoresController : Controller
  {
    [HttpGet("/stores")]
    public ActionResult Index()
    {
      List<Store> allStores = Store.GetAll();
      return View(allStores);
    }

    [HttpGet("/stores/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stores")]
    public ActionResult Create(string storeName)
    {
      Store newStore = new Store(storeName);
      return RedirectToAction("Index");
    }

    [HttpGet("/stores/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Store selectedStore = Store.Find(id);
      List<Item> storeItems = selectedStore.Items;
      model.Add("store", selectedStore);
      model.Add("items", storeItems);
      return View(model);
    }
    [HttpPost("/stores/{storeId}/items")]
    public ActionResult Create(int storeId, string itemName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Store foundStore = Store.Find(storeId);
      Item newItem = new Item(itemName);
      newItem.Save();
      foundStore.AddItem(newItem);
      List<Item> storeItems = foundStore.Items;
      model.Add("items", storeItems);
      model.Add("store", foundStore);
      return View("Show", model);
    }
  }
}