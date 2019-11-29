namespace Shop.Common.ViewModels.CartViewModel
{
    public class ShowCartViewModel
    {
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }

    public class ShowOrderViewModel
    {
        public string OrderDetailId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Sum { get; set; }

    }
}
