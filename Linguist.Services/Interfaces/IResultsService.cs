using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface IResultsService
    {
        void AddTestResult(TestResult result);

        IEnumerable<TestResult> GetTestResultsByUserId(int userId);

        void AddTrainingResult(TrainingResult result);

        IEnumerable<TrainingResult> GetTrainingResultsByUserId(int userId);
    }
}
