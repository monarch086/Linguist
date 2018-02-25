using System;
using Linguist.DataLayer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Linguist.DataLayer.Repositories;
using Linguist.Services.Extensions;
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

        public void AddTrainingResult(HttpContext context, int[] wordsIds)
        {
            var login = _accountsService.GetUserName(context);
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            var wordsString = wordsIds != null ? String.Join(",", wordsIds) : "";

            var result = new TrainingResult
            {
                UserId = user.UserId,
                Date = DateTime.UtcNow,
                Words = wordsString
            };

            _trainingResultsRepository.Add(result);
        }

        public IEnumerable<TrainingResult> GetTrainingResultsByUserId(int userId)
        {
            return _trainingResultsRepository.GetAll().Where(r => r.UserId == userId);
        }

        public int[] GetTestsCountPerWeek(string login, int week)
        {
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));
            
            DateTime startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(week * -7);
            DateTime endOfWeek = DateTime.Now.EndOfWeek(DayOfWeek.Sunday).AddDays(week * -7);
            DateTime endDate = endOfWeek < DateTime.Now ? endOfWeek : DateTime.Now;

            var daysInWeek = (endDate - startDate).Days + 1;
            int[] testsCount = new int[daysInWeek];

            var tests = GetTestResultsByUserId(user.UserId).Where(tr => tr.Date >= startDate && tr.Date <= endDate)
                .GroupBy(tr => tr.Date.DayOfWeek.ShiftDaysOfWeekToStartFromMonday());

            foreach (var test in tests)
            {
                testsCount[test.Key] = test.Count();
            }

            return testsCount;
        }

        public int[] GetTrainingsCountPerWeek(string login, int week)
        {
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            DateTime startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(week * -7);
            DateTime endOfWeek = DateTime.Now.EndOfWeek(DayOfWeek.Sunday).AddDays(week * -7);
            DateTime endDate = endOfWeek < DateTime.Now ? endOfWeek : DateTime.Now;

            var daysInWeek = (endDate - startDate).Days + 1;
            int[] trainingsCount = new int[daysInWeek];

            var trainings = GetTrainingResultsByUserId(user.UserId).Where(tr => tr.Date >= startDate && tr.Date <= endDate)
                .GroupBy(tr => tr.Date.DayOfWeek.ShiftDaysOfWeekToStartFromMonday());

            foreach (var training in trainings)
            {
                trainingsCount[training.Key] = training.Count();
            }

            return trainingsCount;
        }

        public int[] GetWordsCountPerWeek(string login, bool isRightWords, int week = 0)
        {
            var user = _usersRepository.GetAll().FirstOrDefault(u => u.Login.Equals(login));

            DateTime startDate = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(week * -7);
            DateTime endOfWeek = DateTime.Now.EndOfWeek(DayOfWeek.Sunday).AddDays(week * -7);
            DateTime endDate = endOfWeek < DateTime.Now ? endOfWeek : DateTime.Now;

            var daysInWeek = (endDate - startDate).Days + 1;
            int[] wordsCount = new int[daysInWeek];

            var testsByDays = GetTestResultsByUserId(user.UserId).Where(tr => tr.Date >= startDate && tr.Date <= endDate)
                .GroupBy(tr => tr.Date.DayOfWeek.ShiftDaysOfWeekToStartFromMonday());

            foreach (var tests in testsByDays)
            {
                wordsCount[tests.Key] = isRightWords ? tests.Sum(t => !string.IsNullOrEmpty(t.RightWords) ? t.RightWords.Split(',').Length : 0) 
                    : tests.Sum(t => !string.IsNullOrEmpty(t.WrongWords) ? t.WrongWords.Split(',').Length : 0);
            }

            return wordsCount;
        }
    }
}
