using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.HttpClients;
using TestTask.UI.Models;

namespace TestTask.UI.Controllers;

public class HomeController(
    ICalculationsHttpClient calculationsHttpClient
    ,ILogger<HomeController> logger) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Calculate()
    {
        return View(calculationsHttpClient);
    }

    public IActionResult CalculateHistory(CancellationToken cancellationToken)
    {
        return View(calculationsHttpClient);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateCalculation(CalculationModel model, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            // Call the HTTP client to create the calculation
            var createCalculationRequest = new CreateCalculationRequest(model.InputValue);
            var response = await calculationsHttpClient.CalculateAsync(createCalculationRequest, cancellationToken);
            // Handle the result as needed
            var result = new CalculationViewModel
            {
                GrossAnnualSalary = response.GrossAnnualSalary,
                GrossMonthlySalary = response.GrossMonthlySalary,
                NetAnnualSalary = response.NetAnnualSalary,
                NetMonthlySalary = response.NetMonthlySalary,
                AnnualTaxPaid = response.AnnualTaxPaid,
                MonthlyTaxPaid = response.MonthlyTaxPaid
            };
            return View("Index", result);
        }

        throw new Exception("Invalid model state");
    }
}
