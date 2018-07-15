using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using app.Models;

namespace app.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult GetInvolved()
    {
      return View();
    }

    public IActionResult Login()
    {
      return View();
    }
    public IActionResult Tests()
    {
      return View();
    }


    public IActionResult Tests2()
    {
      return View();
    }

    public IActionResult Tests3()
    {
      return View();
    }

    public IActionResult Tests4()
    {
      return View();
    }
    public IActionResult Testsmap()
    {
      return View();
    }


    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult Condition()
    {
      return View();
    }

    public IActionResult Help()
    {
      ViewData["Message"] = "New page.";

      return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
