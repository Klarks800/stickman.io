using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itemrune : MonoBehaviour
{
    public Item.ItemType Type;
    public GameObject[] models;
    public ParticleSystem ps;
    public Collider col;
    public AudioSource asd;
    bool check = false;
    private void Awake()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }

        models[(int)Type].SetActive(true);
    }

    IEnumerator Destroy()
    { 
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!check)
        {
            check = true;
            for (int i = 0; i < models.Length; i++)
            {
                models[i].SetActive(false);
            }
            col.enabled = false;
            GameObject gameObject = collision.gameObject;
            if (gameObject.tag == "PlayerTail")
            {
                SimpleSampleCharacterControl sscc = gameObject.GetComponent<SimpleSampleCharacterControl>();
                if (sscc.Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                {
                    if(Type == Item.ItemType.Money)
                    {
                        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+5);
                        GameObject.FindGameObjectWithTag("money").GetComponent<Text>().text = PlayerPrefs.GetInt("Money") + "";
                    }
                    else
                    {
                        sscc.GetReadySkill(Type);
                    }
                   
                   
                    ps.Play();
                    asd.Play();
                    StartCoroutine(Destroy());
                }
            }
        }
        
     
    }
}
