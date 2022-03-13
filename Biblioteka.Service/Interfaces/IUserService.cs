namespace Biblioteka.Services.Interfaces
{
    public interface IUserService
    {
        public void Register(string username, string password);
        public string Login(string username, string password);
    }
}
