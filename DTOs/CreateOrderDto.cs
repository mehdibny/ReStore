using e_commece_API_React.Entities.OrderAggregate;

namespace e_commece_API_React.DTOs
{
    public class CreateOrderDto
    {
        public bool SaveAddress { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}
