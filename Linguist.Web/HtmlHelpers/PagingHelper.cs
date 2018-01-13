﻿using System;
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

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
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