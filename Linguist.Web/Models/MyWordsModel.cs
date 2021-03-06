﻿using System.Collections.Generic;

namespace Linguist.Web.Models
{
    public class MyWordsModel
    {
        public IEnumerable<WordViewModel> WordsWithCategories { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public int CurrentCategoryId { get; set; }

        public string Message { get; set; }
    }
}