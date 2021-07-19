using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyboardhelper : MonoBehaviour
{
    public Button btn;
    bool presed = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            if(!presed)
            {
                presed = true;
                btn.onClick.Invoke();          
            }
        }
        else
        {
            presed = false;
        }
    }
}
