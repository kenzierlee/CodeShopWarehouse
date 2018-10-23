using System;
using System.Data;
using System.Collections.Generic;
using Warehouse.Entities;
using Dapper;

namespace Warehouse.Data
{
	public interface IWarehouseRepo
	{
		Order GetOrderById(string orderId);
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

		public Order GetOrderById(string orderId)
		{
			try
			{
				Order order = _db.QueryFirstOrDefault<Order>(@" 
					SELECT * FROM orders WHERE id = @orderId
					", new { id = orderId });
				return order;
			}
			catch
			{
				return null;
			}
				
		}

		public IEnumerable<Order> GetOrdersByProductId(int productId)
		{
			return _db.Query<Order>(@"
				SELECT
					o.id,
					o.title,
					o.description,
					o.processed,
					o.productId
				FROM orders o
				WHERE id = @productId
				", new { id = productId });
		}

		public IEnumerable<Order> GetUnProcessedOrders()
		{
			return _db.Query<Order>("SELECT * FROM orders WHERE processed = true");
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
