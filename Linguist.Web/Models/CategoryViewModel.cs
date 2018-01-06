using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Web.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public int CurrentCategoryId { get; set; }
    }
}