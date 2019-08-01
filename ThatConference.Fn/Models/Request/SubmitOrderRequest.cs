namespace ThatConference.Fn.Models.Request
{
    public class SubmitOrderRequest
    {
        public int OrderId { get; set; }
    }

    public static class SubmitOrderRequestExtensions
    {
        public static Order ToOrder(this SubmitOrderRequest @this)
        {
            return new Order
            {
                OrderId = @this.OrderId
            };
        }
    }
}