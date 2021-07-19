using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;
    public GameObject target;
    public GameObject from;
    public ParticleSystem ps;
    public AudioSource ass;
    public AudioSource ass2;
    public GameObject model;
    public enum ItemType
    {
        Bomb,
        Fast,
        Firework,
    Money
    }

   
    bool mid = false;
    bool end = false;

    public void Set(GameObject o1,GameObject o2)
    {
        target = o1; ;
        from = o2 ;
       
    }

    Vector3 GetMid(Vector3 v1,Vector3 v2)
    {
        Vector3 v3 = Vector3.zero;
        v3.x = v1.x + (v2.x - v1.x) / 2;
        v3.y = v1.y + (v2.y - v1.y) / 2;
        v3.z = v1.z + (v2.z - v1.z) / 2;
        return v3;
    }

    IEnumerator ExampleCoroutine()
    {
      

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2f);
      
        
        Destroy(gameObject);
       
    }
  
    private void Update()
    {
        if(target != null)
        {
            if(!mid)
            {
                switch (type)
                {
                    case ItemType.Bomb:
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5f * Time.deltaTime);
                        break;

                    case ItemType.Firework:
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 7f * Time.deltaTime);
                        transform.LookAt(target.transform.position);
                        break;
                }

                       
                

                if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
                {
                    mid = true;
                    switch(type)
                    {
                        case ItemType.Bomb:
                            ps.Play();
                            ass.Play();
                            model.SetActive(false);
                            ass2.enabled = false;
                            StartCoroutine(ExampleCoroutine());
                            target.GetComponent<SimpleSampleCharacterControl>().main.Hit(ItemType.Bomb);
                            // StartCoroutine(Hit(target,ItemType.Bomb));
                            break;

                        case ItemType.Firework:
                            ps.Play();
                            ass.Play();
                            model.SetActive(false);
                            ass2.enabled = false;
                            StartCoroutine(ExampleCoroutine());
                            Debug.Log(target);
                            target.GetComponent<SimpleSampleCharacterControl>().main.Hit(ItemType.Firework);
                            // StartCoroutine(Hit(target,ItemType.Bomb));
                            break;
                    }
                }
            }
        }
    }



}
