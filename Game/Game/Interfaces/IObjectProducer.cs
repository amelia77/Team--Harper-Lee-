namespace Game.Interfaces
{
    using Game.Data;
    using System.Collections.Generic;
    public interface IObjectProducer
    {
        IEnumerable<GameUnit> ProduceObjects();
    }
}
