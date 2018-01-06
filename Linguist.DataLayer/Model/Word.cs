using System;

namespace Linguist.DataLayer.Model
{
    public class Word
    {
        public int WordId { get; set; }

        public int UserId { get; set; }

        public string OriginalWord { get; set; }

        public string Translation { get; set; }

        public DateTime DateAdded { get; set; }

        public int RememberIndex { get; set; }
    }
}
