
using System;
using NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warehouse.Data;
using Warehouse.Entities;
using System.Collections.Generic;
using Warehouse.Business;

namespace WarehouseServiceTests
{
	[TestClass]
	public class WarehouseServiceTests
	{
		[TestMethod]
		public void UnresolvedFillOrdersCanBeRetrieved()
		{
			IWarehouseRepo mockWarehouseRepo = Substitute.For<IWarehouseRepo>();
			List<Order> unprocessedOrders = mockWarehouseRepo.GetUnProcessedOrders();
			bool unprocessed = true;
			for (var index = 0; 0 < unprocessedOrders.Count; index++)
			{
				if (unprocessedOrders[index].Processed == true)
				{
					unprocessed = false;
				}
			}

			Assert.IsTrue(unprocessed);
		}

		[TestMethod]
		public void UnresolvedFillOrderCanBeProcessed()
		{
			IWarehouseRepo mockWarehouseRepo = Substitute.For<IWarehouseRepo>();
			Order order = new Order
			{
				Id = 1,
				Title = "Test Order",
				Description = "Will this resolve an order",
				Processed = true,
				ProductId = 2
			};

			Order processedOrder = mockWarehouseRepo.UpdateOrder(order);

			Assert.AreEqual(processedOrder, order);
		}

		[TestMethod]
		public void ProcessedFillOrderCannotBeModified()
		{
			IWarehouseRepo mockWarehouseRepo = Substitute.For<IWarehouseRepo>();
			var warehouseService = new WarehouseService(mockWarehouseRepo);
			Order order = new Order
			{
				Id = 1,
				Title = "Test Order",
				Description = "Will this resolve an order",
				Processed = true,
				ProductId = 2
			};

			Order expectedOrder = warehouseService.ProcessOrder(order);

			Assert.AreEqual(order, expectedOrder);
		}
	}
}
