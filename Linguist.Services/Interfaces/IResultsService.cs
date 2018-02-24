using System.Collections.Generic;
using System.Web;
using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface IResultsService
    {
        void AddTestResult(HttpContext context, int[] rightWords, int[] wrongWords);

        IEnumerable<TestResult> GetTestResultsByUserId(int userId);

        void AddTrainingResult(TrainingResult result);

        IEnumerable<TrainingResult> GetTrainingResultsByUserId(int userId);

        int[] GetTestsCountPerWeek(string login, int week = 0);
    }
}
