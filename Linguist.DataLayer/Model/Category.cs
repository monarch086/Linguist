using System;

namespace Linguist.DataLayer.Model
{
    public class Category
    {
        public int CategoryId { get; set; }

        public int ParentCategoryId { get; set; }

        public int UserId { get; set; }

        public string CategoryName { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
