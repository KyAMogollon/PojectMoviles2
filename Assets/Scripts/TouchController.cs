using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
public class TouchController : MonoBehaviour
{
    [SerializeField] GameObject[] objeto;
    [SerializeField] GameObject almacenObjetos;
    [SerializeField] GameObject trailBehaviour;
    private Camera camera;
    private int index=0;



    public float swipeThreshold = 3f; // Umbral del swipe

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera=FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastDetected(touch);
            }
            Vector2 pos2=Camera.main.ScreenToWorldPoint(touch.position);
            //Para mover la figura
            if(touch.phase == TouchPhase.Moved)
            { 
                MoveFigure(pos2);
            }
        }
        DetectSwipe();
    }
    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;

                
            }

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider == null)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    float swipeMagnitude = endTouchPosition.x - startTouchPosition.x;
                    Mathf.Abs(swipeMagnitude);
                    Debug.Log(swipeMagnitude);
                    Debug.Log("StartPosition:" + startTouchPosition);
                    Debug.Log("EndPosition:" + endTouchPosition);

                    if (swipeMagnitude > swipeThreshold)
                    {
                        //Debug.Log("entro a la condicion");
                        trailBehaviour.transform.position = mousePos;
                        RaycastDestroy(mousePos);
                    }
                }
            }
        }
    }
    void RaycastDestroy(Vector2 pos2)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos2, Vector2.zero);
        if (hit.collider == null)
        {
            for (int i = 0; i < almacenObjetos.transform.childCount; i++)
            {
                Destroy(almacenObjetos.transform.GetChild(i).gameObject);
            }
        }
    }
    void MoveFigure(Vector2 pos2)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos2, Vector2.zero);
        if (hit.collider != null)
        {
            hit.collider.gameObject.transform.position = new Vector2(pos2.x, pos2.y);
        }
    }
    void RaycastDetected(Touch touch)
    {
        Vector2 pos1 = Camera.main.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(pos1, Vector2.zero);
        if (hit.collider != null)
        {
            if (touch.tapCount == 2)
            {
                Destroy (hit.collider.gameObject);
            }
        }
        else
        {
            Instantiate(objeto[index], pos1, Quaternion.identity, almacenObjetos.transform);
        }
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
