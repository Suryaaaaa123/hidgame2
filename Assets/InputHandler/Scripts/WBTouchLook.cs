using UnityEngine;
using UnityEngine.EventSystems;

public class WBTouchLook : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public static Vector2 TouchDist;

    private Vector2 _pointerOld;
    private int _pointerId;
    private bool _pressed;

    private void Update()
    {
        if (_pressed)
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[_pointerId].position - _pointerOld;
                _pointerOld = Input.touches[_pointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                _pointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        _pointerId = eventData.pointerId;
        _pointerOld = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }
}
