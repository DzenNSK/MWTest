using Cysharp.Threading.Tasks;

namespace MWTest
{
    public interface ILocalSaveService
    {
        public UniTask<SaveData?> GetSavedData();
        public UniTask SaveData(SaveData data);
    }

    public struct SaveData
    {
        public int CounterValue;
    }
}
