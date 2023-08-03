namespace CourseMicroservices.Services.Basket.DTOs
{
    public class BasketDto
    {
        private decimal _totalPrice;
        public string? UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }

        public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);
    }
}

