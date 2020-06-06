namespace Interfaces
{
    public interface ILoginController
    {
        public void Login(string username, string password);
        public void TellUser(string message);
        
    }
}