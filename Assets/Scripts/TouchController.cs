using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TouchController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    //private InputAction touchMoveAction;

    [SerializeField] GameObject[] objeto;
    [SerializeField] GameObject almacenObjetos;
    [SerializeField] GameObject trailBehaviour;
    private int index = 0;

    public int tapCount = 0;

    public Vector2 touchposition;
    //public Vector2 swipePosition;

    public float swipeThreshold = 3f; // Umbral del swipe

    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("Touch");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");
        //touchMoveAction = playerInput.actions.FindAction("MoveFigure");
    }
    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        //touchMoveAction.performed += OnMoveFigure;
    }
    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        //touchMoveAction.performed -= OnMoveFigure;

    }
    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touchPositionAction.
            ReadValue<Vector2>());
        pos.z = objeto[index].transform.position.z;
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider == null)
        {
            Instantiate(objeto[index], pos, Quaternion.identity, almacenObjetos.transform);
        }
    }
    public void OnMoveFigure(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 position = context.ReadValue<Vector2>();
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(position);
            RaycastHit2D hit = Physics2D.Raycast(cameraPosition, Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.transform.position = cameraPosition;
            }
        }
    }
    public void OnSwipe(InputAction.CallbackContext context)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(touchPositionAction.
            ReadValue<Vector2>());
        if (context.phase == InputActionPhase.Started)
        {
            startTouchPosition = pos;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            endTouchPosition = touchposition;
        }
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider == null)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (Mathf.Abs(endTouchPosition.x - startTouchPosition.x) >= swipeThreshold)
                {

                    trailBehaviour.transform.position = endTouchPosition;
                    RaycastDestroy();
                }
            }
        }
    }

    void Update()
    {

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        RaycastDetected(touch);
        //    }
        //    Vector2 pos2=Camera.main.ScreenToWorldPoint(touch.position);
        //    //Para mover la figura
        //    if(touch.phase == TouchPhase.Moved)
        //    { 
        //        MoveFigure(pos2);
        //    }
        //}
        //DetectSwipe();
    }
    void DetectSwipe()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        startTouchPosition = touch.position;
        //    }

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        endTouchPosition = touch.position;


        //    }

        //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        //    if(hit.collider == null)
        //    {
        //        if (touch.phase == TouchPhase.Moved)
        //        {
        //            float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
        //            Mathf.Abs(swipeMagnitude);
        //            Debug.Log(swipeMagnitude);
        //            Debug.Log("StartPosition:" + startTouchPosition);
        //            Debug.Log("EndPosition:" + endTouchPosition);

        //            if (swipeMagnitude > swipeThreshold)
        //            {
        //                //Debug.Log("entro a la condicion");
        //                trailBehaviour.transform.position = mousePos;
        //                RaycastDestroy(mousePos);
        //            }
        //        }
        //    }
        //}
    }
    void RaycastDestroy()
    {
        for (int i = 0; i < almacenObjetos.transform.childCount; i++)
        {
            Destroy(almacenObjetos.transform.GetChild(i).gameObject);
        }

    }
    //void RaycastDetected(Touch touch)
    //{
    //    Vector2 pos1 = Camera.main.ScreenToWorldPoint(touch.position);
    //    RaycastHit2D hit = Physics2D.Raycast(pos1, Vector2.zero);
    //    if (hit.collider != null)
    //    {
    //        if (touch.tapCount == 2)
    //        {
    //            Destroy (hit.collider.gameObject);
    //        }
    //    }
    //    else
    //    {
    //        Instantiate(objeto[index], pos1, Quaternion.identity, almacenObjetos.transform);
    //    }
    //}


    public void OnPositionswipe(InputAction.CallbackContext position)
    {
        if (position.phase == InputActionPhase.Performed)
        {
            Vector2 touchPosition1 = position.ReadValue<Vector2>();
            touchposition = Camera.main.ScreenToWorldPoint(touchPosition1);
        }

    }
        //public void OnTouch(InputAction.CallbackContext value)
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector2.zero);
        //    if (hit.collider == null && touchposition!=Vector2.zero)
        //    {
        //        Instantiate(objeto[index], touchposition, Quaternion.identity, almacenObjetos.transform);
        //        tapCount=tapCount+1;           

        //    }
        //    else
        //    {
        //        tapCount = tapCount + 1;
        //        if (tapCount == 2)
        //            {
        //                Destroy(hit.collider.gameObject);
        //        }
        //        else
        //        {
        //            tapCount = 0;
        //        }
        //    }


        //}
        //public void OnSwipe(InputAction.CallbackContext swipe)
        //{
        //    //RaycastHit2D hit = Physics2D.Raycast(swipePosition, Vector2.zero);
        //    //if (swipe.phase == InputActionPhase.Started)
        //    //{
        //    //    startTouchPosition = swipePosition;
        //    //}
        //    //if (swipe.phase == InputActionPhase.Canceled)
        //    //{
        //    //    endTouchPosition = swipePosition;
        //    //}
        //    //if (hit.collider == null)
        //    //{
        //    //    if (swipe.phase == InputActionPhase.Performed)
        //    //    {
        //    //        float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
        //    //        float valueAbsolute = Mathf.Abs(swipeMagnitude);
        //    //        Debug.Log(valueAbsolute);;

        //    //        if (valueAbsolute > swipeThreshold)
        //    //        {
        //    //            //Debug.Log("entro a la condicion");
        //    //            trailBehaviour.transform.position = swipePosition;
        //    //            RaycastDestroy();
        //    //        }
        //    //        else
        //    //        {
        //    //            endTouchPosition = Vector2.zero;
        //    //            startTouchPosition = Vector2.zero;
        //    //        }
        //    //    }
        //    //}
        //    //if (hit.collider == null)
        //    //{
        //    //    float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
        //    //    float swipeAbsolutePosition = Mathf.Abs(swipeMagnitude);
        //    //    if (swipe.phase == InputActionPhase.Performed)
        //    //    {

        //    //        Debug.Log(swipeAbsolutePosition);
        //    //        Debug.Log("Umbral " + swipeThreshold);
        //    //        //Debug.Log("StartPosition:" + startTouchPosition);
        //    //        //Debug.Log("EndPosition:" + endTouchPosition);

        //    //        if (swipeAbsolutePosition >= swipeThreshold)
        //    //        {
        //    //            Debug.Log("entro a la condicion");
        //    //            trailBehaviour.transform.position = swipePosition;
        //    //            RaycastDestroy();
        //    //        }
        //    //    }
        //    //}

        //}
        public void CircleFigure()
        {
            index = 0;
        }
        public void TriangleFigure()
        {
            index = 1;
        }
        public void SquareFigure()
        {
            index = 2;
        }
}

