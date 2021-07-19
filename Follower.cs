using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follower : MonoBehaviour
{
    public GameObject target;
   public GameObject player;
    public RectTransform rt;
    public Image pointer;
  
    
    private void Start()
    {
        
    }

    void Update()
    {
        if(target != null )
        {
            
            
            
            Vector3 dir = target.transform.position - player.transform.position;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            rt.eulerAngles = new Vector3(0, 0, -angle);
        }
    }
}
