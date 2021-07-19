using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputMove : MonoBehaviour
{
    public Joystick jst;
    public RectTransform joystick;
    public RectTransform canvas;
    public menu men;
    public Image[] images;
    bool touch = false;

    private void Start()
    {
        for (int i = 0; i < images.Length; i++)
            images[i].enabled = false;
    }
    void Update()
    {
        if(men.play)
        {

            if (Input.GetMouseButton(0))//Input.touches.Length > 0
            {
                for (int i = 0; i < images.Length; i++)
                    images[i].enabled = true;
          
                 Vector2 v2 = new Vector2( Input.mousePosition.x,Input.mousePosition.y);
                if ( !touch)
                {
                    touch = true;
                    Vector2 lp;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.gameObject.GetComponent<RectTransform>(), v2, null, out lp);
                    joystick.anchoredPosition = lp;
                }
                else
                {
                    PointerEventData pData = new PointerEventData(EventSystem.current);
                    pData.position = v2;
                    jst.OnPointerDown(pData);
                }
            }
            else
            {
                PointerEventData pData = new PointerEventData(EventSystem.current);
                pData.position = Vector2.zero;
                jst.OnPointerUp(pData);
                touch = false;
                for (int i = 0; i < images.Length; i++)
                    images[i].enabled = false;
            }
        }
       

        
    }
}
