namespace Game.Interfaces
{
    using Game.Data;
    using System.Collections.Generic;

    public interface IWeapon
    {
        //top left coordinates of weapons image
        Point Position { get; set; }

        string Name { get; set; }

        int Damage { get; set; }

        IList<MovingUnit> GetWeapon();
    }
}
