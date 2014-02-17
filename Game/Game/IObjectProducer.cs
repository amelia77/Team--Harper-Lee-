using System.Collections.Generic;
namespace Game
{
    public interface IObjectProducer
    {
        IEnumerable<GameUnit> ProduceObjects();
    }
}
