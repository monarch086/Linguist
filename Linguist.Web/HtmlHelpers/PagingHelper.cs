using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Linguist.Web.Models;

namespace Linguist.Web.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            if (pagingInfo.TotalPages == 1)
            {
                return null;
            }

            StringBuilder result = new StringBuilder();

            int[] pagesToShow = {1, pagingInfo.CurrentPage - 1, pagingInfo.CurrentPage, pagingInfo.CurrentPage + 1, pagingInfo.TotalPages };

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                if (pagingInfo.TotalPages > 5)
                {
                    if (!pagesToShow.Contains(i))
                    {
                        if (i == pagingInfo.CurrentPage - 2 || i == pagingInfo.CurrentPage + 2)
                        {
                            TagBuilder tagP = new TagBuilder("div");
                            tagP.InnerHtml = "...";
                            tagP.MergeAttribute("style", "float:left; margin:0 10px;");
                            result.Append(tagP);
                        }
                        continue;
                    }
                }

                TagBuilder tag = new TagBuilder("a");

                if (pageUrl != null)
                    tag.MergeAttribute("href", pageUrl(i));
                else
                {
                    tag.MergeAttribute("href", "#");
                }

                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}