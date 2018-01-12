using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Web.Models
{
    public class WordViewModel
    {
        public Word Word { get; set; }

        public IEnumerable<Category> WordCategories { get; set; }
    }
}