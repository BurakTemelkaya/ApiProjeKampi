using ApiProjeKampi.WebUI.Constants.Area;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]

public class AdminLayoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
