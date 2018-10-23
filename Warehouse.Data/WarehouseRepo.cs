using System;
using System.Data;
using System.Collections.Generic;
using Warehouse.Entities;
using Dapper;

namespace Warehouse.Data
{
	public interface IWarehouseRepo
	{
		Order GetOrderById(int orderId);
		IEnumerable<Order> GetOrdersByProductId(int productId);
		IEnumerable<Order> GetUnProcessedOrders();
		Order UpdateOrder(Order order, Order orderData);
		Order CreateOrder(Order order);
	}

	public class WarehouseRepo : IWarehouseRepo
	{
		private readonly IDbConnection _db;

		public WarehouseRepo(IDbConnection db)
		{
			_db = db;
		}

		public Order GetOrderById(int orderId)
		{
			return new Order
			{
				Id = orderId,
				Title = $"My Order {orderId}",
				Description = "Order Description",
				Processed = false,
				ProductId = orderId
			};
				
		}

		public List<Order> GetOrdersByProductId(int productId)
		{
			List<Order> orders = new List<Order>();
			orders.Add(new Order
			{
				Id = 1,
				Title = "My Order 1",
				Description = "Order Description",
				Processed = false,
				ProductId = productId
			});
			orders.Add(new Order
			{
				Id = 2,
				Title = "My Order 2",
				Description = "Order Description",
				Processed = true,
				ProductId = productId
			});
			orders.Add(new Order
			{
				Id = 3,
				Title = "My Order 3",
				Description = "Order Description",
				Processed = false,
				ProductId = productId
			});
			orders.Add(new Order
			{
				Id = 4,
				Title = "My Order 4",
				Description = "Order Description",
				Processed = false,
				ProductId = productId
			});
			orders.Add(new Order
			{
				Id = 5,
				Title = "My Order 5",
				Description = "Order Description",
				Processed = true,
				ProductId = productId
			});
			return orders;
		}

		public List<Order> GetUnProcessedOrders()
		{
			List<Order> orders = new List<Order>();
			orders.Add(new Order
			{
				Id = 1,
				Title = "My Order 1",
				Description = "Order Description",
				Processed = false,
				ProductId = 2345
			});
			orders.Add(new Order
			{
				Id = 2,
				Title = "My Order 2",
				Description = "Order Description",
				Processed = false,
				ProductId = 3525
			});
			orders.Add(new Order
			{
				Id = 3,
				Title = "My Order 3",
				Description = "Order Description",
				Processed = false,
				ProductId = 1325
			});
			orders.Add(new Order
			{
				Id = 4,
				Title = "My Order 4",
				Description = "Order Description",
				Processed = false,
				ProductId = 2356
			});
			orders.Add(new Order
			{
				Id = 5,
				Title = "My Order 5",
				Description = "Order Description",
				Processed = false,
				ProductId = 6342
			});
			return orders;
		}

		public Order UpdateOrder(Order order, Order orderData)
		{
			var updatedOrder = _db.Execute(@" 
				UPDATE orders SET
					title = @Title,
					description = @Description,
					processed = @Processed,
					productId = @ProductId
				WHERE id = @Id", orderData);

			if (updatedOrder > 0)
			{
				return order;
			}
			else
			{
				return null;
			}
		}

		public Order CreateOrder(Order order)
		{
			Order newOrder = new Order
			{
				Id = Guid.NewGuid().ToString(),
				Title = order.Title,
				Description = order.Description,
				Processed = order.Processed,
				ProductId = order.ProductId
			};
			var successful = _db.ExecuteAsync(@"
				INSERT INTO orders
				(id, title, description, processed, productId)
				VALUES (@id, @Title, @Description, @Processed, @ProductId);
			", newOrder);

			if (successful.Result == 1)
			{
				return newOrder;
			}

			return null;
		}
	}
}
