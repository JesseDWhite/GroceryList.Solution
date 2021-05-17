using Microsoft.AspNetCore.Mvc;

namespace GroceryList.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

  }
}