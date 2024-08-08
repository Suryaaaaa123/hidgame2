using UnityEngine;
using Cinemachine;
using UnityStandardAssets.CrossPlatformInput;

public class WBInputHandler : MonoBehaviour
{
    private enum InputType
    {
        PC,
        Mobile
    }

    [SerializeField] private InputType _inputType;

    [Space]
    [SerializeField] private GameObject _mobileInput;
    [SerializeField] private float _touchSensitivity;

    private FixedJoystick _joystick;

    private void Start()
    {
        if (_inputType == InputType.Mobile)
        {
            _mobileInput?.SetActive(true);
            CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
            _joystick = FindObjectOfType<FixedJoystick>();
        }
        else
        {
            _mobileInput?.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public float Horizontal
    {
        get
        {
            if (_inputType == InputType.PC)
                return Input.GetAxis("Horizontal");
            else if (_inputType == InputType.Mobile)
                return _joystick.Horizontal;
            return 0;
        }
    }

    public float Vertical
    {
        get
        {
            if (_inputType == InputType.PC)
                return Input.GetAxis("Vertical");
            else if (_inputType == InputType.Mobile)
                return _joystick.Vertical;
            return 0;
        }
    }

    public bool GetButton(string buttonName)
    {
        if (_inputType == InputType.PC)
        {
            return Input.GetButton(buttonName);
        }
        else if (_inputType == InputType.Mobile)
        {
            return CrossPlatformInputManager.GetButton(buttonName);
        }
        return false;
    }

    public bool GetButtonDown(string buttonName)
    {
        if (_inputType == InputType.PC)
        {
            return Input.GetButtonDown(buttonName);
        }
        else if (_inputType == InputType.Mobile)
        {
            return CrossPlatformInputManager.GetButtonDown(buttonName);
        }
        return false;
    }

    public bool GetButtonUp(string buttonName)
    {
        if (_inputType == InputType.PC)
        {
            return Input.GetButtonUp(buttonName);
        }
        else if (_inputType == InputType.Mobile)
        {
            return CrossPlatformInputManager.GetButtonUp(buttonName);
        }
        return false;
    }

    private float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {

            case "Mouse X":

                if (Input.touchCount > 0)
                {
                    return WBTouchLook.TouchDist.x * _touchSensitivity;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    return WBTouchLook.TouchDist.y * _touchSensitivity;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}