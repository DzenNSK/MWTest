using Cysharp.Threading.Tasks;
using System;

namespace MWTest
{
    public interface IGameDataService
    {
        public int GetCounterValue();
        public void IncrementCounter();
        public string GetGreetMessage();
        public UniTask UpdateContent();

        public event Action CounterChange;
        public event Action ContentUpdated;
    }
}