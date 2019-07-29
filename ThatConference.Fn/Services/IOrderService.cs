using System.Threading.Tasks;
using ThatConference.Fn.Models.Request;
using ThatConference.Fn.Models.Response;

namespace ThatConference.Fn.Services
{
    public interface IOrderService
    {
        Task SubmitOrderAsync(SubmitOrderRequest req);
    }
}