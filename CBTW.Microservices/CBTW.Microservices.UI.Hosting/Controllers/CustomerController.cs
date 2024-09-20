using CBTW.Microservices.UI.Application.Providers;
using CBTW.Microservices.UI.Domain.Models;
using CBTW.Microservices.UI.Hosting.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CBTW.Microservices.UI.Hosting.Controllers;

public class CustomerController : Controller
{
    private readonly ICallCenterProvider callCenterProvider;

    public CustomerController(ICallCenterProvider callCenterProvider)
    {
        this.callCenterProvider = callCenterProvider ?? throw new ArgumentNullException(nameof(callCenterProvider));
    }

    [HttpGet]
    public async Task<IActionResult> Index(int idTipoDocumento, string documento, int idCiudad, int idTelefonoCC)
    {
        var result = await this.callCenterProvider.ConsultarClientes(default(CancellationToken)).ConfigureAwait(false);

        ViewBag.TiposDeDocumento = await this.callCenterProvider.ConsultarTiposDeDocumento(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.Ciudades = await this.callCenterProvider.ConsultarCiudades(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.TelefonosCC = await this.callCenterProvider.ConsultarTelefonosCC(default(CancellationToken)).ConfigureAwait(false);

        if (idTipoDocumento != 0)
            result = result.Where(j => j.IdTipoDocumento == idTipoDocumento).ToList();

        if (documento != null)
            result = result.Where(j => j.Documento == documento).ToList();

        if (idCiudad != 0)
            result = result.Where(j => j.IdCiudad == idCiudad).ToList();

        if (idTelefonoCC != 0)
            result = result.Where(j => j.IdTelefonoCC == idTelefonoCC).ToList();

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.TiposDeDocumento = await this.callCenterProvider.ConsultarTiposDeDocumento(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.Ciudades = await this.callCenterProvider.ConsultarCiudades(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.TelefonosCC = await this.callCenterProvider.ConsultarTelefonosCC(default(CancellationToken)).ConfigureAwait(false);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
    {
        if (ModelState.IsValid)
        {
            await this.callCenterProvider.CrearCliente(customerViewModel, default(CancellationToken)).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        return View(customerViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customerViewModel = await this.callCenterProvider.ConsultarCliente(
            new CustomerViewModel { IdTipoDocumento = int.Parse(id.Split('@')[0]), Documento = id.Split('@')[1] },
            default(CancellationToken))
            .ConfigureAwait(false);

        return View(customerViewModel);
    }

    [HttpGet()]
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customerViewModel = await this.callCenterProvider.ConsultarCliente(
            new CustomerViewModel { IdTipoDocumento = int.Parse(id.Split('@')[0]), Documento = id.Split('@')[1] },
            default(CancellationToken))
            .ConfigureAwait(false);

        ViewBag.TiposDeDocumento = await this.callCenterProvider.ConsultarTiposDeDocumento(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.Ciudades = await this.callCenterProvider.ConsultarCiudades(default(CancellationToken)).ConfigureAwait(false);
        ViewBag.TelefonosCC = await this.callCenterProvider.ConsultarTelefonosCC(default(CancellationToken)).ConfigureAwait(false);

        return View(customerViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, CustomerViewModel customerViewModel)
    {
        if (int.Parse(id.Split('@')[0]) != customerViewModel.IdTipoDocumento
            || id.Split('@')[1] != customerViewModel.Documento)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await this.callCenterProvider.ActualizarCliente(customerViewModel, default(CancellationToken)).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }

        return View(customerViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}