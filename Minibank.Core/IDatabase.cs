namespace Minibank.Core
{
    public interface IDatabase
    {
        int GetRubleCourse(string currency);
    }
}