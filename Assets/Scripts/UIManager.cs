using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject instructionObject;
    [SerializeField] private GameObject countdownObject;
    [SerializeField] private GameObject gameUIObject;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject gameOverObject;

    //countdown items
    [SerializeField] private TextMeshProUGUI countdownText;
    private int counter = 3;

    public void AppExit()
    {
        Application.Quit();
    }

    private void OnMainMenu()
    {
        mainMenuObject.SetActive(true);
        instructionObject.SetActive(false);
        countdownObject.SetActive(false);
        gameUIObject.SetActive(false);
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
    }
    
    private void OnInstructions()
    {
        mainMenuObject.SetActive(false);
        instructionObject.SetActive(true);
        countdownObject.SetActive(false);
        gameUIObject.SetActive(false);
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
    }

    private void OnCountdown()
    {
        mainMenuObject.SetActive(false);
        instructionObject.SetActive(false);
        countdownObject.SetActive(true);
        gameUIObject.SetActive(false);
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);

        StartCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        var currentCount = counter;
        countdownText.text = currentCount + "";
        
        while (currentCount > 0)
        {
            yield return new WaitForSeconds(1);
            currentCount -= 1;
            countdownText.text = currentCount + "";
        }
        
        EventManager.TriggerEvent(GameStates.GAME);
        StopCoroutine("Timer");
    }

    private void OnGame()
    {
        mainMenuObject.SetActive(false);
        instructionObject.SetActive(false);
        countdownObject.SetActive(false);
        gameUIObject.SetActive(true);
        pauseObject.SetActive(false);
        gameOverObject.SetActive(false);
    }
    
    private void OnPause()
    {
        mainMenuObject.SetActive(false);
        instructionObject.SetActive(false);
        countdownObject.SetActive(false);
        gameUIObject.SetActive(false);
        pauseObject.SetActive(true);
        gameOverObject.SetActive(false);
    }
    
    private void OnGameOver()
    {
        mainMenuObject.SetActive(false);
        instructionObject.SetActive(false);
        countdownObject.SetActive(false);
        gameUIObject.SetActive(false);
        pauseObject.SetActive(false);
        gameOverObject.SetActive(true);
    }
    
    private void OnEnable()
    {
        EventManager.StartListening(GameStates.MAIN_MENU, OnMainMenu);
        EventManager.StartListening(GameStates.INSTRUCTIONS, OnInstructions);
        EventManager.StartListening(GameStates.COUNTDOWN, OnCountdown);
        EventManager.StartListening(GameStates.GAME, OnGame);
        EventManager.StartListening(GameStates.PAUSE, OnPause);
        EventManager.StartListening(GameStates.GAME_OVER, OnGameOver);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(GameStates.MAIN_MENU, OnMainMenu);
        EventManager.StopListening(GameStates.INSTRUCTIONS, OnInstructions);
        EventManager.StopListening(GameStates.COUNTDOWN, OnCountdown);
        EventManager.StopListening(GameStates.GAME, OnGame);
        EventManager.StopListening(GameStates.PAUSE, OnPause);
        EventManager.StopListening(GameStates.GAME_OVER, OnGameOver);
    }

    private void Start()
    {
        EventManager.TriggerEvent(GameStates.MAIN_MENU);
    }
}
