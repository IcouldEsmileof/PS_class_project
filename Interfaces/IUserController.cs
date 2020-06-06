namespace Interfaces
{
    public interface IUserController
    {
        public delegate void ToExec();
        
        public void ShowUser(string output);
        public void AppendToShowUser(string output);
        public void ActivateMainMenu();
        public void ChangeUserRole();
        public void ChangeUserRole(string username, string role);
        public void ChangeUserActiveTo();
        public void ChangeUserActiveTo(string username, string activeTo);
        public void AlertUser(string message);
        public void ShowUsersList();
        public void ShowLogs();
        public void ShowCurrentActivity();
        public void ProcessMainInput(string input);
        public void Close();
        
    }
}