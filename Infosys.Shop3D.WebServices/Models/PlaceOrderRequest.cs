namespace Infosys.Shop3D.WebServices.Models
{
    public class PlaceOrderRequest
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int AddressId { get; set; }
    }
}

