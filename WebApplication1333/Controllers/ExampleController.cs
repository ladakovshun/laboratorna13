namespace WebApplication133.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Serilog;

    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            Log.Debug("Debug level log"); // Це не буде відправлено на email
            Log.Information("Information level log"); // Це не буде відправлено на email
            Log.Warning("Warning level log"); // Це не буде відправлено на email
            Log.Error("Error level log"); // Це буде відправлено на email
            Log.Fatal("Fatal level log"); // Це буде відправлено на email

            string userName = "Tom";
            DateTime loginTime = DateTime.Now;
            Log.Information("User {Name} logged in at {Time}", userName, loginTime);

            return View();
        }
    }
}
