//CREDIT
//modified from: https://forum.unity.com/threads/a-free-simple-smooth-mouselook.73117
using UnityEngine;

public class MouseTurn : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;
 
    public Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;
    
    public GameObject charObject;
    private bool canControl;

    private void OnEnable()
    {
        EventManager.StartListening(GameStates.GAME, SetEnabled);
        EventManager.StartListening(GameStates.PAUSE, SetDisabled);
        EventManager.StartListening(GameStates.GAME_OVER, SetDisabled);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(GameStates.GAME, SetEnabled);
        EventManager.StopListening(GameStates.PAUSE, SetDisabled);
        EventManager.StopListening(GameStates.GAME_OVER, SetDisabled);
    }

    private void SetEnabled()
    {
        canControl = true;
    }
    
    private void SetDisabled()
    {
        canControl = false;
    }
 
    private void Start()
    {
        targetDirection = transform.localRotation.eulerAngles;
        targetCharacterDirection = charObject.transform.localRotation.eulerAngles;
    }
 
    private void Update()
    {
        if (!canControl)
        {
            return;
        }
        
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);
        
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
        
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _mouseAbsolute += _smoothMouse;
        
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);
        
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);
 
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
 
        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
        charObject.transform.localRotation = yRotation * targetCharacterOrientation;
    }
}
