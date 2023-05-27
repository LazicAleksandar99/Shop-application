﻿using Microsoft.EntityFrameworkCore;
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
                .Where(o => (o.UserId == id || o.SellerId == id) && o.Status == "Delivered")
                .ToListAsync();
            return historys;
        }

        public async Task<Order> Create(Order newOrder)
        {
            var article = await _data.Articles.SingleOrDefaultAsync(x => x.Id == newOrder.Item.ArticleId);
            if (article.Quantity < newOrder.Item.Quantity)
                return null;
            Random random = new Random();
            int randomMinutes = random.Next(1, 59);
            newOrder.CreationTime = DateTime.Now;
            newOrder.DeliveryTime = DateTime.Now.AddHours(1).AddMinutes(randomMinutes);
            newOrder.Status = "Delivering";
            newOrder.Price = article.Price * newOrder.Item.Quantity;
            var result = await _data.Orders.AddAsync(newOrder);
            article.Quantity -= newOrder.Item.Quantity;
            await _data.SaveChangesAsync();
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

        public async Task UpdateStatus()
        {
            var orders = await _data.Orders.Where(o => o.Status == "Delivering").ToListAsync();

            foreach (var order in orders)
            {
                if (order.DeliveryTime < DateTime.Now)
                    order.Status = "Delivered";
            }

            await _data.SaveChangesAsync();
        }
    }
}
