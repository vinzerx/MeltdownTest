using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private float gravityValue;
    [SerializeField] private float moveFactor = 1f;
    [SerializeField] private float jumpValue = 10f;
    private float currentYValue;
    private float wantedRotation;
    private bool canControl;
    private Vector3 initEulerAngles;
    private Vector3 initPosition;

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
        transform.position = initPosition;
        transform.eulerAngles = initEulerAngles;
    }

    private void SetEnabled()
    {
        canControl = true;
    }
    
    private void SetDisabled()
    {
        canControl = false;
    }

    private void Awake()
    {
        initPosition = transform.position;
        initEulerAngles = transform.eulerAngles;
    }

    private void Update()
    {
        if (!canControl)
        {
            return;
        }
        
        if (charController.isGrounded && Input.GetButtonDown("Jump"))
        {
            currentYValue = jumpValue;
        }
    }

    private void FixedUpdate()
    {
        if (!canControl)
        {
            return;
        }
        
        currentYValue += gravityValue;

        if (currentYValue < gravityValue)
        {
            currentYValue = gravityValue;
        }

        var xValue = 0f;
        var yValue = currentYValue;
        var zValue = 0f;

        xValue = Input.GetAxis("Horizontal") * moveFactor;
        zValue = Input.GetAxis("Vertical") * moveFactor;

        var finalMoveVector = new Vector3(xValue, yValue, zValue);

        var moveVector = charController.transform.TransformDirection(finalMoveVector);
        charController.Move(moveVector);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadZone")
        {
            EventManager.TriggerEvent(GameStates.GAME_OVER);
        }
    }
}
