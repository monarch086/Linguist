using System;
using Linguist.DataLayer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class ResultsService : IResultsService
    {
        private readonly IRepository<User> _usersRepository;

        private readonly IRepository<TestResult> _testResultsRepository;

        private readonly IRepository<TrainingResult> _trainingResultsRepository;

        private readonly IAccountsService _accountsService;

        public ResultsService(IRepository<User> usersRepository, IRepository<TestResult> testResultsRepository, IRepository<TrainingResult> trainingResultsRepository, IAccountsService accountsService)
        {
            _usersRepository = usersRepository;
            _testResultsRepository = testResultsRepository;
            _trainingResultsRepository = trainingResultsRepository;
            _accountsService = accountsService;
        }

        public void AddTestResult(HttpContext context, int[] rightWords, int[] wrongWords)
        {
            var login = _accountsService.GetUserName(context);
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            var rightWordsString = rightWords != null ? String.Join(",", rightWords) : "";
            var wrongWordsString = wrongWords != null ? String.Join(",", wrongWords) : "";

            var result = new TestResult
            {
                UserId = user.UserId,
                Date = DateTime.UtcNow,
                RightWords = rightWordsString,
                WrongWords = wrongWordsString
            };

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
