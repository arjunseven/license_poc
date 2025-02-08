using Microsoft.AspNetCore.Mvc;

public class AIController : Controller
{
    // For logged-in users
    public IActionResult Index()
    {
        return View();
    }

    // For guest users
    public IActionResult Guest()
    {
        return View("Index"); // Reuse the same view for simplicity
    }
}