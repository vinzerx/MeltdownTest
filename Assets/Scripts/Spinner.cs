using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    private bool isSpinning;
    private Vector3 initEulerAngles;

    private void Awake()
    {
        initEulerAngles = transform.eulerAngles;
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameStates.MAIN_MENU, Reset);
        EventManager.StartListening(GameStates.GAME, SetEnabled);
        EventManager.StartListening(GameStates.PAUSE, SetDisabled);
        EventManager.StartListening(GameStates.GAME_OVER, SetDisabled);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(GameStates.MAIN_MENU, Reset);
        EventManager.StopListening(GameStates.GAME, SetEnabled);
        EventManager.StopListening(GameStates.PAUSE, SetDisabled);
        EventManager.StopListening(GameStates.GAME_OVER, SetDisabled);
    }

    private void Reset()
    {
        transform.eulerAngles = initEulerAngles;
    }

    private void SetEnabled()
    {
        isSpinning = true;
    }
    
    private void SetDisabled()
    {
        isSpinning = false;
    }
    
    void Update()
    {
        if (!isSpinning)
        {
            return;
        }
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 
            transform.eulerAngles.y + spinSpeed,
            transform.eulerAngles.z);
    }
}
