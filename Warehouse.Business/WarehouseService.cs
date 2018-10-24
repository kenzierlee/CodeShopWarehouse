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
			return _warehouseRepo.GetOrderById(orderId);
			
		}

		public List<Order> GetUnProcessedOrders()
		{
			return _warehouseRepo.GetUnProcessedOrders();
		}

		public List<Order> GetOrdersByProductId(int productId)
		{
			return _warehouseRepo.GetOrdersByProductId(productId);
		}

		public Order CreateOrder(Order order)
		{
			return _warehouseRepo.CreateOrder(order);
		}

		public Order UpdateOrder(Order order)
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
				return _warehouseRepo.UpdateOrder(currentOrder);
			}
		}

		public Order ProcessOrder(Order order)
		{
			Order databaseOrder = _warehouseRepo.GetOrderById(order.Id);

			if (order.Processed == true && databaseOrder.Processed == false)
			{
				return _warehouseRepo.UpdateOrder(order);
			}

			return null;
		}
	}
}
