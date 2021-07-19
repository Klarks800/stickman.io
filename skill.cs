using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skill : MonoBehaviour
{
    public Image skillReady;

    public Image mid;
    public Sprite[] images;
    public Button but;
    Item.ItemType cur;
    private void Start()
    {
        skillReady.enabled = false;
        mid.enabled = false;
        but.interactable = false;
        but.onClick.AddListener(() => { mid.enabled = false; skillReady.enabled = false; but.interactable = false; } );
    }

    public void butApp()
    {
        but.onClick.AddListener(() => { mid.enabled = false; skillReady.enabled = false; but.interactable = false; });
    }

    public void Set(Item.ItemType it)
    {
       
        cur = it;
        switch (it)
        {
            case Item.ItemType.Bomb:
                mid.sprite = images[0];
                skillReady.enabled = false;
                but.interactable = false;
                break;

            case Item.ItemType.Fast:
                mid.sprite = images[2];
                skillReady.enabled = true;
                but.interactable = true;
                break;
           
            case Item.ItemType.Firework:
                mid.sprite = images[1];
                skillReady.enabled = false;
                but.interactable = false;
                break;

        }
        mid.enabled = true;
    }


    public void SkillUpdate(bool a)
    {
        switch (cur)
        {
            case Item.ItemType.Bomb:
                skillReady.enabled = a;
                but.interactable = a;
                break;

          
            case Item.ItemType.Firework:
                skillReady.enabled = a;
                but.interactable = a;
                break;

        }
    }

}
