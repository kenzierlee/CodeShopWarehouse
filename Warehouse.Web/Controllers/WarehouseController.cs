using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Business;
using Warehouse.Entities;
using Warehouse.Web.Models;

namespace Warehouse.Web.Controllers
{
	[ApiController]
	[Route("api/[Controller]")]
	public class WarehouseController : Controller
	{
		private readonly WarehouseService _warehouseService;

		public WarehouseController(WarehouseService warehouseService)
		{
			_warehouseService = warehouseService;
		}

		[HttpGet("{orderId}")]
		public IActionResult GetOrderById(int orderId)
		{
			return Ok(_warehouseService.GetOrderById(orderId));
		}

		[HttpGet("orders/productId/{productId}")]
		public IActionResult GetOrdersByProductId(int productId)
		{
			return Ok(_warehouseService.GetOrdersByProductId(productId));
		}
		
		[HttpGet("orders/unProcessed")]
		public IActionResult GetUnProcessedOrders()
		{
			return Ok(_warehouseService.GetUnProcessedOrders());
		}

		[HttpPut("update")]
		public IActionResult UpdateOrder([FromBody]Order order)
		{
			_warehouseService.UpdateOrder(order);
			return Ok();
		}

		[HttpPost("create")]
		public IActionResult CreateOrder([FromBody]Order order)
		{
			return Ok(_warehouseService.CreateOrder(order));
		}
	}
}
