using System.Web;

namespace Linguist.Web.Extensions
{
    internal static class UrlStringExtensions
    {
        public static string AddMessageToReturnUrl(this string returnUrl, string message)
        {
            string encodedMessage = HttpUtility.UrlEncode(message);
            return returnUrl.Contains("?") ? returnUrl + "&message=" + encodedMessage : returnUrl + "?message=" + encodedMessage;
        }

    }
}