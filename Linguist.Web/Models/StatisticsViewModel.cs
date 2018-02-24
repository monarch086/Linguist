namespace Linguist.Web.Models
{
    public class StatisticsViewModel
    {
        public int AllWords { get; set; }

        public int WordsFrom0To3 { get; set; } //RememberIndex <= 3

        public int WordsFrom4To7 { get; set; } //RememberIndex >= 4 and <= 7

        public int WordsFrom8To9 { get; set; } //RememberIndex == 8 and 9

        public int WordsAddedThisMonth { get; set; }

        public int TrainingsThisWeek { get; set; }

        public int[] TrainingsTakenLastWeek { get; set; }

        public int[] TestsTakenLastWeek { get; set; }
    }
}