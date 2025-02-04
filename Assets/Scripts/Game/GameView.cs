using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MWTest
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text counterLabel;
        [SerializeField] private TMP_Text greetLabel;
        [SerializeField] private Button updateButton;
        [SerializeField] private Button incButton;
        [SerializeField] private Image buttonBack;

        [Inject] IGameDataService gameDataService;
        [Inject] IResourceProvider resourceProvider;

        private void Start ()
        {
            gameDataService.CounterChange += OnCounterChange;
            gameDataService.ContentUpdated += OnContentUpdate;
            resourceProvider.ResourcesLoaded += OnResourcesReload;
            updateButton.onClick.AddListener(OnUpdateButton);
            incButton.onClick.AddListener(OnIncButton);
            OnCounterChange();
            OnContentUpdate();
            OnResourcesReload();
        }

        private void OnDestroy()
        {
            gameDataService.CounterChange -= OnCounterChange;
            gameDataService.ContentUpdated -= OnContentUpdate;
            resourceProvider.ResourcesLoaded -= OnResourcesReload;
            incButton.onClick.RemoveListener(OnIncButton);
        }

        private void OnCounterChange()
        {
            counterLabel.text = gameDataService.GetCounterValue().ToString();
        }

        private void OnContentUpdate()
        {
            greetLabel.text = gameDataService.GetGreetMessage();
        }

        private void OnIncButton()
        {
            gameDataService.IncrementCounter();
        }

        private async void OnUpdateButton()
        {
            updateButton.interactable = false;
            await UniTask.WhenAll(resourceProvider.LoadResources(),
                gameDataService.UpdateContent());
            updateButton.interactable = true;
        }

        private void OnResourcesReload()
        {
            var spr = resourceProvider.GetSprite("button");
            buttonBack.sprite = spr;
        }

        private void OnApplicationQuit()
        {
            gameDataService.Save();
        }
    }
}
