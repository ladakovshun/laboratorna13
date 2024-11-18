using Microsoft.AspNetCore.Mvc;
using Serilog;

public class TestController : Controller
{
    public IActionResult Index()
    {
        // Логи різних рівнів
        Log.Verbose("This is a Verbose log");
        Log.Debug("This is a Debug log");
        Log.Information("This is an Information log");
        Log.Warning("This is a Warning log");
        Log.Error("This is an Error log");
        Log.Fatal("This is a Fatal log");

        return Content("Logs generated. Check your email and logs folder.");
    }
}
