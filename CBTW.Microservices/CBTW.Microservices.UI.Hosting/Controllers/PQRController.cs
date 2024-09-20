using AutoMapper;
using CBTW.Microservices.UI.Application.Providers;
using CBTW.Microservices.UI.Domain.CallCenter.ConsultarClientes;
using CBTW.Microservices.UI.Domain.Models;
using CBTW.Microservices.UI.Hosting.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CBTW.Microservices.UI.Hosting.Controllers;

public class PQRController : Controller
{
	private readonly ICallCenterProvider callCenterProvider;
	private readonly IMapper mapper;

	public PQRController(ICallCenterProvider callCenterProvider,
		IMapper mapper)
	{
		this.callCenterProvider = callCenterProvider ?? throw new ArgumentNullException(nameof(callCenterProvider));
		this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	[HttpGet]
	public async Task<IActionResult> Index(long id, string customer)
	{
		var result = await this.callCenterProvider.ConsultarPQRs(default(CancellationToken)).ConfigureAwait(false);

		ViewBag.Clientes = await this.callCenterProvider.ConsultarClientesRegistrados(default(CancellationToken)).ConfigureAwait(false);

		if (id != 0)
			result = result.Where(j => j.Id == id).ToList();

		if (customer != null)
		{
			var customerViewModel = await this.callCenterProvider.ConsultarCliente(
				new CustomerViewModel { IdTipoDocumento = int.Parse(customer.Split('@')[0]), Documento = customer.Split('@')[1] },
				default(CancellationToken))
				.ConfigureAwait(false);

			result = result.Where(j => j.IdCustomer == customerViewModel.IdCustomer).ToList();
		}

		return View(result);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		var clientes = await this.callCenterProvider.ConsultarClientes(default(CancellationToken)).ConfigureAwait(false);

		var result = new List<ConsultarClientesRespuestaValue>();
		foreach (var cliente in clientes)
		{
			result.Add(this.mapper.Map<CustomerViewModel, ConsultarClientesRespuestaValue>(cliente));
		}

		ViewBag.Clientes = result;

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(PQRViewModel pqrViewModel)
	{
		if (ModelState.IsValid)
		{
			var customerViewModel = await this.callCenterProvider.ConsultarCliente(
				new CustomerViewModel { IdTipoDocumento = int.Parse(pqrViewModel.Customer.Split('@')[0]), Documento = pqrViewModel.Customer.Split('@')[1] },
				default(CancellationToken))
				.ConfigureAwait(false);

			pqrViewModel.IdCustomer = customerViewModel.IdCustomer;

			await this.callCenterProvider.CrearPQR(pqrViewModel, default(CancellationToken)).ConfigureAwait(false);
			return RedirectToAction(nameof(Index));
		}

		return View(pqrViewModel);
	}

	[HttpGet]
	public async Task<IActionResult> Details(long id)
	{
		if (id == 0)
		{
			return NotFound();
		}

		var pqrViewModel = await this.callCenterProvider.ConsultarPQR(
			new PQRViewModel { Id = id },
			default(CancellationToken))
			.ConfigureAwait(false);

		return View(pqrViewModel);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}