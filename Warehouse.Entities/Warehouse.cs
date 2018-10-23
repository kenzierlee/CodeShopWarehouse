using System;

namespace Warehouse.Entities
{
	public class Order
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool Processed { get; set; }
		public int ProductId { get; set; }
	}
}
