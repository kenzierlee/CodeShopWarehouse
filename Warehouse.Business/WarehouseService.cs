using System;
using System.Collections.Generic;
using Warehouse.Data;
using Warehouse.Entities;

namespace Warehouse.Business
{
	public class WarehouseService
	{
		private readonly IWarehouseRepo _warehouseRepo;

		public WarehouseService(IWarehouseRepo warehouseRepo)
		{
			_warehouseRepo = warehouseRepo;
		}

		public Order GetOrderById(int orderId)
		{
			try
			{
				return _warehouseRepo.GetOrderById(orderId);
			}
			catch
			{
				return null;
			}
		}

		public IEnumerable<Order> GetUnProcessedOrders()
		{
			try
			{
				return _warehouseRepo.GetUnProcessedOrders();
			}
			catch
			{
				return null;
			}
		}

		public IEnumerable<Order> GetOrdersByProductId(int productId)
		{
			try
			{
				return _warehouseRepo.GetOrdersByProductId(productId);
			}
			catch
			{
				return null;
			}
		}

		public Order CreateOrder(Order order)
		{
			try
			{
				return _warehouseRepo.CreateOrder(order);
			}
			catch
			{
				return null;
			}
		}

		public Order UpdateOrder(Order order)
		{
			try
			{
				Order currentOrder = GetOrderById(order.Id);
				if (currentOrder == null)
				{
					throw new Exception("Order not found");
				}
				else if (currentOrder.Processed == true)
				{
					throw new Exception("Order has already been processed");
				}
				else
				{
					return _warehouseRepo.UpdateOrder(currentOrder, order);
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
