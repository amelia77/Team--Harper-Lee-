namespace Game.Interfaces
{
    using System;
    public interface IUserInterface
    {
        event EventHandler OnLeftPressed;

        event EventHandler OnRightPressed;

        event EventHandler OnUpPressed;

        event EventHandler OnDownPressed;

        event EventHandler OnActionPressed;

        event EventHandler OnEscapePressed;
        
        void ProcessInput();

        string EnterText(int x, int y); 
    }
}
