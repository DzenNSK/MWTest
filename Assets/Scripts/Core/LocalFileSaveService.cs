using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace MWTest
{
    public class LocalFileSaveService : MonoBehaviour, ILocalSaveService
    {
        private const string saveFileName = "SaveGame.json";

        public async UniTask<SaveData?> GetSavedData()
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            if (!File.Exists(filePath)) return null;

            var reader = new StreamReader(filePath);
            var data = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<SaveData>(data);
        }

        public async UniTask SaveData(SaveData data)
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            Debug.Log(filePath);
            var writer = new StreamWriter(filePath);
            await writer.WriteAsync(JsonConvert.SerializeObject(data));
            writer.Flush();
            writer.Close();
        }
    }
}
