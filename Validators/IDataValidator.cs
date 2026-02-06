namespace ViktorynaApp.Validators
{
    public interface IDataValidator
    {
        bool ChyParolValidnyi(string parol);
        bool ChyDataValidna(string data);
    }
}