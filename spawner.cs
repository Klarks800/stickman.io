using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    public GameObject whites;
    public GameObject playerBot;
    public GameObject player;
   // public List<SimpleSampleCharacterControl> players;
    public List<int> score;
    public List<Dictionary<Color,int>> colors;
    public Text[] text;
    public Text extra_place;
    public GameObject extra;
    public GameObject pointers;
    public GameObject pointer;
    public Camera came;
    public List<GameObject> points;
    float timer = 2f;
    float timer_rune = 0f;
    menu men;
    public GameObject[] rune;
    SimpleSampleCharacterControl first;
    SimpleSampleCharacterControl second;
    SimpleSampleCharacterControl third;

    public GameObject play;
    private void Start()
    {
        men = GetComponent<menu>();
        extra.SetActive(false);
      
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);


        //text[0].text = players[0].name + " -  0";
        //text[0].color = players[0].head.material.color;
        //text[1].text = players[1].name + " -  0";
        //text[1].color = players[1].head.material.color;
        //text[2].text = players[2].name + " -  0" ;
        //text[2].color = players[2].m.color;

        
        GameObject[] game = GameObject.FindGameObjectsWithTag("PlayerTail");
        for (int i = 0; i < game.Length; i++)
            {
            if (game[i] != play)
            {
                GameObject o = Instantiate(pointer, pointers.transform.position, Quaternion.identity, pointers.transform);

                Follower fol = o.GetComponent<Follower>();
                fol.target = game[i].gameObject;
                fol.pointer.color = game[i].GetComponent<SimpleSampleCharacterControl>().head.material.color;
                //fol.player = play.GetComponent<SimpleSampleCharacterControl>();
                points.Add(o);
            }
               
        }
      
    }
    public List<SimpleSampleCharacterControl> pp;
    public void CheckPlacee()
    {
     
      
        
        
        //bool play = false;
       GameObject[] g = GameObject.FindGameObjectsWithTag("PlayerTail");
      List<SimpleSampleCharacterControl> players = new List<SimpleSampleCharacterControl>();




        for (int i = 0;i < g.Length;i++)
        {

            players.Add(g[i].GetComponent<SimpleSampleCharacterControl>());
        }
        
        if(players.FindAll(o => o.Type == SimpleSampleCharacterControl.TypeOfUnit.Player).Count > 1)
        {
            //Debug.Log("TWO");
            players.Remove(players.Find(o => o.Type == SimpleSampleCharacterControl.TypeOfUnit.Player));
        }
          
        List<SimpleSampleCharacterControl> SortedList = players.OrderBy(o => o.tailObjects.Count).ToList();
        pp = SortedList;
        bool pl = false;
        if(SortedList.Count == 1)
        {
            //Debug.Log("ddd");
            if(SortedList[0].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
            {
                men.winn(SortedList[0].tailObjects.Count);
                text[1].text = "-";
                text[1].color = Color.white;
                text[2].text = "-";
                text[2].color = Color.white;
            }
        }

        else if(SortedList.Count == 2)
        {
            text[0].text = SortedList[1].nam + " - " + (SortedList[1].tailObjects.Count - 1);
            text[0].color = SortedList[1].head.material.color;
            text[1].text = SortedList[0].nam + " - " + (SortedList[0].tailObjects.Count - 1);
            text[1].color = SortedList[0].head.material.color;
            text[2].text = "-";
            text[2].color = Color.white;

            if (SortedList[1].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                pl = true;

            if (SortedList[0].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                pl = true;

        }
        else if (SortedList.Count > 2)
        {
            text[0].text = SortedList[SortedList.Count-1].nam + " - " + (SortedList[SortedList.Count - 1].tailObjects.Count - 1);
            text[0].color = SortedList[SortedList.Count - 1].head.material.color;
            text[1].text = SortedList[SortedList.Count - 2].nam + " - " + (SortedList[SortedList.Count - 2].tailObjects.Count - 1);
            text[1].color = SortedList[SortedList.Count - 2].head.material.color;
            text[2].text = SortedList[SortedList.Count - 3].nam + " - " + (SortedList[SortedList.Count - 3].tailObjects.Count - 1);
            text[2].color = SortedList[SortedList.Count - 3].head.material.color;


            if (SortedList[SortedList.Count - 1].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                pl = true;

            if (SortedList[SortedList.Count - 2].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                pl = true;

            if (SortedList[SortedList.Count - 3].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
                pl = true;
        }

        int p = 0;
        for (int i = 0; i < SortedList.Count;i++)
        {
            if (SortedList[i].Type == SimpleSampleCharacterControl.TypeOfUnit.Player)
            {
                p = i;
                break;
            }

        }

       
        
        
      

        if(!pl)
        {
            int place = SortedList.Count - (p);
            //Debug.Log("p =" + p + "  place = " + place + "  Count = " + SortedList.Count);

            if (place > 2)
            {
                extra.SetActive(true);
                extra_place.text = (place) + "";
                text[3].text = SortedList[p].nam + " - " + (SortedList[p].tailObjects.Count - 1);
                text[3].color = SortedList[p].head.material.color;
            }
        }
      else
        {
            extra.SetActive(false);
        }
            
          
        
      
       
    }







    public void st1()
    {
        extra.SetActive(false);
        play = Instantiate(player, new Vector3(0, 0.225f, 0), Quaternion.identity);
        Time.timeScale = 0;
    }
    public void st()
    {
        int white_bot = 0;
        int player_bot = 0;
        
        if(PlayerPrefs.GetInt("Lvl")== 1)
        {
            white_bot = 30;
            player_bot = 3;
            tiru = 7f;
        }

        if (PlayerPrefs.GetInt("Lvl") == 2)
        {
            white_bot = 30;
            player_bot = 4;
            tiru = 6f;
        }
        
        if (PlayerPrefs.GetInt("Lvl") == 3)
        {
            white_bot = 35;
            player_bot = 5;
            tiru = 5f;
        }

        if (PlayerPrefs.GetInt("Lvl") > 3 && PlayerPrefs.GetInt("Lvl") < 21)
        {
            white_bot = (int)(30 + ((PlayerPrefs.GetInt("Lvl") + 3)* 1.5)) ;
            player_bot = PlayerPrefs.GetInt("Lvl")+3;
            tiru = 5f;
        }
        
        if (PlayerPrefs.GetInt("Lvl") > 20)
        {
            white_bot = (int)(30 + (21 * 1.5));
            player_bot = 21 + 3;
            tiru = 5f;
        }


        for (int i = 0; i < white_bot; i++)
            Instantiate(whites, new Vector3(Random.Range(13f, -13f), 0.225f, Random.Range(13f, -13f)), Quaternion.identity);

        for (int i = 0; i < player_bot; i++)
            Instantiate(playerBot, new Vector3(Random.Range(13f, -13f), 0.225f, Random.Range(13f, -13f)), Quaternion.identity); 
        
        StartCoroutine(ExampleCoroutine());
    }
    float tiru = 10f;
    float d = 3;
    void regUpdate()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            Instantiate(whites, new Vector3(Random.Range(13f, -13f), 0.225f, Random.Range(13f, -13f)), Quaternion.identity);
            timer = 0.5f;
        }
        
        if (timer_rune > 0)
            timer_rune -= Time.deltaTime;
        else
        {
            int r = Random.Range(1, 101);
            int n = 0;
            if(r >= 1 && r <= 40)
            {
                n = 0;
            }
            if (r >= 41 && r <= 70)
            {
                n = 1;
            }

            if (r >= 71 && r <= 90)
            {
                n = 3;
            }
            if (r >= 91 && r <= 100)
            {
                n = 2;
            }
           
            Vector3 v3 = new Vector3( Random.Range(play.transform.position.x - d, play.transform.position.x + d), 0.225f, Random.Range(play.transform.position.z - d, play.transform.position.z + d));

            if (v3.x > 13)
                v3.x = 13;

            if (v3.x < -13)
                v3.x = -13;

            if (v3.y > 13)
                v3.y = 13;

            if (v3.y < -13)
                v3.y = -13;
          
            Instantiate(rune[n], v3, Quaternion.identity);
            timer_rune = tiru;
        }


        try
        {
            GameObject[] game = GameObject.FindGameObjectsWithTag("PlayerTail");
            
           

            for(int i = 0; i < game.Length; i++)
            {
                if(game[i] != play)
                {
                    
                    Follower p= points.Find(a => a.GetComponent<Follower>().pointer.color == game[i].GetComponent<SimpleSampleCharacterControl>().head.material.color).GetComponent<Follower>();
                    p.player = play;
                    p.target = game[i];
                   
                    Vector3 screenPoint = came.WorldToViewportPoint(game[i].transform.position);
                    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

                    if (onScreen)
                        p.gameObject.SetActive(false);
                     else
                        p.gameObject.SetActive(true);
                }

          
            }

           
        }
       catch
        {

        }

        for (int i = 0; i < points.Count; i++)
        {
            //Debug.Log(points[i].GetComponent<Follower>().target.GetComponent<SimpleSampleCharacterControl>().Type);
            if (points[i].GetComponent<Follower>().target.GetComponent<SimpleSampleCharacterControl>().Type != SimpleSampleCharacterControl.TypeOfUnit.PlayBot)
            {
                points[i].gameObject.SetActive(false);
            }
        }





    }


  
    
    
   
   
    public void Switch()
    {
        points.Clear();
    }



    void Update()
    {
            regUpdate();
    }
}
