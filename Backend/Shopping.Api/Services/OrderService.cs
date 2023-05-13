using AutoMapper;
using Shopping.Api.Data.Repositories;
using Shopping.Api.DTO.ArticleDTO;
using Shopping.Api.DTO.OrderDTO;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Interfaces.IServices;
using Shopping.Api.Models;

namespace Shopping.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<HistoryOrderDto>> History(int id)
        {
            if (!await _userRepository.DoesUserExist(id))
                return null; 
            var result = await _orderRepository.History(id);
            var history = _mapper.Map<List<HistoryOrderDto>>(result);
            return history;
        }

        public async Task<GetCreatedOrderDto> Create(CreateOrderDto newOrder)
        {
            if (!await _userRepository.DoesUserExist(newOrder.UserId))
                return null;
            if (!await _userRepository.DoesUserExist(newOrder.Seller))
                return null;
            var order = _mapper.Map<Order>(newOrder);
            var result = await _orderRepository.Create(order);
            var returnValue = _mapper.Map<GetCreatedOrderDto>(result);
            return returnValue;
        }

        public async Task<List<GetAllOrderDto>> AllOrders()
        {
            var result = await _orderRepository.AllOrders();
            var returnValue = _mapper.Map<List<GetAllOrderDto>>(result);
            return returnValue;
        }

    }
}
