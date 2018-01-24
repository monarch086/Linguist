using System.Web;

namespace Linguist.Services.Interfaces
{
    public interface ILogsService
    {
        void AddVisitor(HttpContext context);
    }
}
