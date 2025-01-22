using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MWTest
{
    public class GameDataService : MonoBehaviour, IGameDataService
    {
        private GameSettings gameSettings;
        private GameData gameData;

        public event Action CounterChange;
        public event Action ContentUpdated;

        public int GetCounterValue()
        {
            return gameSettings.StartingNumber;
        }

        public string GetGreetMessage()
        {
            return gameData.GreetMessage;
        }

        public void IncrementCounter()
        {
            gameSettings.StartingNumber++;
            CounterChange?.Invoke();
        }

        public async UniTask UpdateContent()
        {
            await UniTask.WhenAll(UniTask.Delay(5000), //Loading screen delay
                LoadSettings(),
                LoadGameData());

            ContentUpdated?.Invoke();
        }

        private async UniTask LoadSettings()
        {
            TextAsset settingsJSON = (TextAsset) await Resources.LoadAsync<TextAsset>("GameSettings"); //Request to backend here

            gameSettings = JsonConvert.DeserializeObject<GameSettings>(settingsJSON.text);
        }

        private async UniTask LoadGameData()
        {
            TextAsset settingsJSON = (TextAsset)await Resources.LoadAsync<TextAsset>("GameData"); //Request to backend here

            gameData = JsonConvert.DeserializeObject<GameData>(settingsJSON.text);
        }
    }
}