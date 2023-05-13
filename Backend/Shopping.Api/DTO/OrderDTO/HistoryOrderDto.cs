using Shopping.Api.DTO.ItemDTO;

namespace Shopping.Api.DTO.OrderDTO
{
    public class HistoryOrderDto
    {
        public HistoryOrderItemDto Item { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
    }
}
