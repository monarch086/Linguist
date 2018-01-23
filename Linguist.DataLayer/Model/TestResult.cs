using System;

namespace Linguist.DataLayer.Model
{
    public class TestResult
    {
        public int TestResultId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string RightWords { get; set; }

        public string WrongWords { get; set; }
    }
}
