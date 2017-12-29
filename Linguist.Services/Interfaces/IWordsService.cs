using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface IWordsService
    {
        bool AddWord(Word word);

        bool EditWord(Word word);

        bool RemoveWord(Word word);
    }
}
