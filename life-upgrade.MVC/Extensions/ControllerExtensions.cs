using LifeUpgrade.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LifeUpgrade.MVC.Extensions;

public static class ControllerExtensions
{
    public static void SetNotification(this Controller controller, string type, string message)
    {
        var notification = new Notification(type, message);
        controller.TempData["Notification"] = JsonConvert.SerializeObject(notification);
    }
}