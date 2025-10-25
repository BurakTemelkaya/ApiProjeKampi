using ApiProjeKampi.WebUI.Constants.Area;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Areas.Admin.Controllers;

[Area(AreaNames.Admin)]
public class ChatController : Controller
{
    public IActionResult SendChatWithAI()
    {
        return View();
    }
}