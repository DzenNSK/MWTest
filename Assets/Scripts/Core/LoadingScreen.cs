using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MWTest
{
    public class LoadingScreen : MonoBehaviour
    {
        [Inject] IGameDataService gameDataService;
        private async void Start()
        {
            await gameDataService.UpdateContent();
            await SceneManager.LoadSceneAsync("GameScene").ToUniTask();
        }
    }
}
