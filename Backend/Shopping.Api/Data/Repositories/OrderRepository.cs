using Microsoft.EntityFrameworkCore;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Models;

namespace Shopping.Api.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _data;

        public OrderRepository(DataContext data)
        {
            _data = data;
        }

        public async Task<List<Order>> History(int id)
        {
            var historys = await _data.Orders
                .Include(o => o.Item)
                    .ThenInclude(i => i.Article)
                .Where(o => o.Id == id)
                .ToListAsync();
            return historys;
        }

        public async Task<Order> Create(Order newOrder)
        {
            var result = await _data.Orders.AddAsync(newOrder);
            return result.Entity;
        }

        public async Task<List<Order>> AllOrders()
        {
            var orders = await _data.Orders
                .Include(o => o.Item)
                    .ThenInclude(i => i.Article)
                .ToListAsync();
            return orders;
        }
    }
}
