using Shopping.Api.DTO.ItemDTO;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Api.DTO.OrderDTO
{
    public class GetCreatedOrderDto
    {
        public int Id { get; set; }
        public HistoryOrderItemDto Item { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string Status { get; set; }
    }
}
