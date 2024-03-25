using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class TouchController : MonoBehaviour
{
    [SerializeField] GameObject[] objeto;
    [SerializeField] GameObject almacenObjetos;
    [SerializeField] GameObject trailBehaviour;
    private Camera camera;
    private int index=0;

    public Vector2 touchposition;
    public Vector2 swipePosition;

    public float swipeThreshold = 3f; // Umbral del swipe

    public Vector2 startTouchPosition;
    public Vector2 endTouchPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera=FindObjectOfType<Camera>();
    }

    // Update is called once per frame
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
    public void OnPosition(InputAction.CallbackContext position)
    {
        Vector2 touchPosition1 = position.ReadValue<Vector2>();
        touchposition = Camera.main.ScreenToWorldPoint(touchPosition1);
        swipePosition = Camera.main.ScreenToWorldPoint(touchPosition1);
    }
    public void OnTouch(InputAction.CallbackContext value)
    {
        RaycastHit2D hit = Physics2D.Raycast(touchposition, Vector2.zero);
        if (hit.collider == null)
        {
            if (touchposition != Vector2.zero)
            {
                Instantiate(objeto[index], touchposition, Quaternion.identity, almacenObjetos.transform);
            }
            
        }
        else
        {
            //if (touch.tapCount == 2)
            //{
            //    Destroy(hit.collider.gameObject);
            //}
        }


    }
    public void OnSwipe(InputAction.CallbackContext swipe)
    {
        RaycastHit2D hit = Physics2D.Raycast(swipePosition, Vector2.zero);
        if (swipe.phase == InputActionPhase.Started)
        {
            startTouchPosition = swipePosition;
        }
        if (swipe.phase == InputActionPhase.Canceled)
        {
            endTouchPosition = swipePosition;
            //if (hit.collider == null)
            //{
                float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
                float swipeAbsolutePosition = Mathf.Abs(swipeMagnitude);
                //if (swipe.phase == InputActionPhase.Performed)
                //{

                    Debug.Log(swipeAbsolutePosition);
                    Debug.Log("Umbral " + swipeThreshold);

                    if (swipeAbsolutePosition >= swipeThreshold)
                    {
                        if(hit.collider == null)
                        {
                            Debug.Log("entro a la condicion");
                            trailBehaviour.transform.position = swipePosition;
                            RaycastDestroy();
                        }
                        
                    }
                //}
            //}
        }
        //if (hit.collider == null)
        //{
        //    float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
        //    float swipeAbsolutePosition = Mathf.Abs(swipeMagnitude);
        //    if (swipe.phase == InputActionPhase.Performed)
        //    {
                
        //        Debug.Log(swipeAbsolutePosition);
        //        Debug.Log("Umbral " + swipeThreshold);
        //        //Debug.Log("StartPosition:" + startTouchPosition);
        //        //Debug.Log("EndPosition:" + endTouchPosition);

        //        if (swipeAbsolutePosition >= swipeThreshold)
        //        {
        //            Debug.Log("entro a la condicion");
        //            trailBehaviour.transform.position = swipePosition;
        //            RaycastDestroy();
        //        }
        //    }
        //}

    }
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
