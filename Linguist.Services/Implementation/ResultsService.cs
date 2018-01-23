using Linguist.DataLayer.Model;
using System.Collections.Generic;
using System.Linq;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class ResultsService : IResultsService
    {
        private readonly IRepository<TestResult> _testResultsRepository;

        private readonly IRepository<TrainingResult> _trainingResultsRepository;

        public ResultsService(IRepository<TestResult> testResultsRepository, IRepository<TrainingResult> trainingResultsRepository)
        {
            _testResultsRepository = testResultsRepository;
            _trainingResultsRepository = trainingResultsRepository;
        }

        public void AddTestResult(TestResult result)
        {
            _testResultsRepository.Add(result);
        }

        public IEnumerable<TestResult> GetTestResultsByUserId(int userId)
        {
            return _testResultsRepository.GetAll().Where(r => r.UserId == userId);
        }

        public void AddTrainingResult(TrainingResult result)
        {
            _trainingResultsRepository.Add(result);
        }

        public IEnumerable<TrainingResult> GetTrainingResultsByUserId(int userId)
        {
            return _trainingResultsRepository.GetAll().Where(r => r.UserId == userId);
        }
    }
}
