using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler, IPointerClickHandler, IEndDragHandler 
{

    public PlayerController playerController;
    public float swipeRange;

    private enum PlayerInput
    {
        Up,
        Down,
        Right,
        Left,
        Tap
    }

    private bool dragged = false;
   

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!dragged)
        {
            PlayerInput playerInput = PlayerInput.Tap;
            playerController.UpdateInput(playerInput.ToString());
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition);
        PlayerInput playerInput = GetDragDirection(dragVectorDirection);
        playerController.UpdateInput(playerInput.ToString());
        dragged = true;       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragged = false;
    }

    private PlayerInput GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        PlayerInput dragDirection;
        if (positiveX > positiveY)
        {
            dragDirection = (dragVector.x > 0) ? PlayerInput.Right : PlayerInput.Left;
        }
        else
        {
            dragDirection = (dragVector.y > 0) ? PlayerInput.Up : PlayerInput.Down;
        }
        Debug.Log(dragDirection);
        return dragDirection;
    }

    
}
