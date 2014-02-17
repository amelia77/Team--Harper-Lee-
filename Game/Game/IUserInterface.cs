namespace Game
{
    using System;
    public interface IUserInterface
    {
        event EventHandler OnLeftPressed;

        event EventHandler OnRightPressed;

        event EventHandler OnUpPressed;

        event EventHandler OnDownPressed;

        event EventHandler OnActionPressed;
        void ProcessInput();
    }
}
