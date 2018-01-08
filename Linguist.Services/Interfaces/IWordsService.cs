using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface IWordsService
    {
        bool AddWord(Word word, int categoryId);

        bool EditWord(Word word);

        bool RemoveWord(int wordId);

        IEnumerable<Word> GetWordsByCategory(int categoryId);

        Word GetWordById(int wordId);
    }
}
