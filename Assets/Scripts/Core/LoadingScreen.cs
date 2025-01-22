using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MWTest
{
    public class LoadingScreen : MonoBehaviour
    {
        [Inject] IGameDataService gameDataService;
        [Inject] IResourceProvider resourceProvider;
        private async void Start()
        {
            await UniTask.WhenAll(gameDataService.InitialLoad(),
                resourceProvider.LoadResources());

            await SceneManager.LoadSceneAsync("GameScene").ToUniTask();
        }
    }
}
