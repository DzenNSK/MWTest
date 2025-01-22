using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using UnityEngine;
using Zenject;

namespace MWTest
{
    public class GameDataService : MonoBehaviour, IGameDataService
    {
        private GameSettings gameSettings;
        private GameData gameData;

        private int cCounter;

        [Inject] private ILocalSaveService saveService;

        public event Action CounterChange;
        public event Action ContentUpdated;

        public int GetCounterValue()
        {
            return cCounter;
        }

        public string GetGreetMessage()
        {
            return gameData.GreetMessage;
        }

        public void IncrementCounter()
        {
            cCounter++;
            CounterChange?.Invoke();
        }

        public async UniTask InitialLoad()
        {
            await UniTask.WhenAll(UniTask.Delay(5000), //Loading screen delay
                LoadSettings(),
                LoadGameData());

            SaveData? saveData = await saveService.GetSavedData();
            if(saveData == null)
            {
                cCounter = gameSettings.StartingNumber;
            }
            else
            {
                cCounter = saveData.Value.CounterValue;
            }
        }

        public async UniTask UpdateContent()
        {
            await LoadGameData();
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

        public async UniTask Save()
        {
            SaveData saveData = new SaveData();
            saveData.CounterValue = cCounter;
            await saveService.SaveData(saveData);
        }
    }
}