namespace Game.Interfaces
{
    using Game.Data;
    using System.Collections.Generic;

    public interface IWeapon
    {
        //get set to the position of the part of the player that fires the weapon
        Point Position { get; set; }

        string Name { get; set; }

        int Damage { get; set; }

        IList<MovingUnit> GetWeapon();
    }
}
