using System.Collections.Generic;
using System.Web;
using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface IResultsService
    {
        void AddTestResult(HttpContext context, int[] rightWords, int[] wrongWords);

        IEnumerable<TestResult> GetTestResultsByUserId(int userId);

        void AddTrainingResult(HttpContext context, int[] wordsIds);

        IEnumerable<TrainingResult> GetTrainingResultsByUserId(int userId);

        int[] GetTestsCountPerWeek(string login, int week = 0);

        int[] GetTrainingsCountPerWeek(string login, int week = 0);

        int[] GetWordsCountPerWeek(string login, bool isRightWords, int week = 0);
    }
}
