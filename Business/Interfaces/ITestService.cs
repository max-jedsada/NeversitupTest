using DAL.Entities.Entities;

namespace Business.Interfaces
{
    public interface ITestService
    {
        List<Person> GetPerson();
        Task<Person> GetPerson(int id);

        Task<List<string>> Test2(string inputText);
        Task<string> Test3(string inputNumber);
        Task<string> Test4(List<string> inputText);
    }
}
