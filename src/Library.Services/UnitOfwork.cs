using System.Threading.Tasks;

namespace Library.Services
{
    public interface UnitOfwork
    {
        Task Complete();
    }
}
