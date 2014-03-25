using NoChromeFix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoChromeFix.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Page1()
    {
      var model = new Page1ViewModel()
      {
        TitoloPagina = "Pagina 1",
        Testo = "<p>Testo di pagina 1</p>"
      };
      if (Request.IsAjaxRequest())
        return Json(model, JsonRequestBehavior.AllowGet);
      return View(model);
    }

    public ActionResult Page2()
    {
      var model = new Page2ViewModel()
      {
        TitoloPagina = "Pagina 2",
        Testo = "<p>Testo di pagina 2</p>"
      };
      if (Request.IsAjaxRequest())
        return Json(model, JsonRequestBehavior.AllowGet);
      return View(model);
    }
  }
}