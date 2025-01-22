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
        [SerializeField] private Button incButton;

        [Inject] IGameDataService gameDataService;

        private void Start ()
        {
            gameDataService.CounterChange += OnCounterChange;
            gameDataService.ContentUpdated += OnContentUpdate;
            incButton.onClick.AddListener(OnIncButton);
            OnCounterChange();
            OnContentUpdate();
        }

        private void OnDestroy()
        {
            gameDataService.CounterChange -= OnCounterChange;
            gameDataService.ContentUpdated -= OnContentUpdate;
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

        private void OnApplicationQuit()
        {
            gameDataService.Save();
        }
    }
}
