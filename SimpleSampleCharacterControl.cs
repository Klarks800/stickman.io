using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class SimpleSampleCharacterControl : MonoBehaviour
{

    public TypeOfUnit Type;
    public Animator animator;
    public List<GameObject> tailObjects = new List<GameObject>();
    public ParticleSystem[] particles;
    public AudioSource[] sources;
    public MeshRenderer head;
    public SkinnedMeshRenderer body;
   
    public Rigidbody rb;
    public Collider col;



    private Vector3 m_currentDirection = Vector3.zero;
    private Joystick joystick; 
    public float m_moveSpeed = 2;
    private float m_turnSpeed = 400;
    private float m_currentV = 0;
    private float m_currentH = 0;
    private readonly float m_interpolation = 10;
    private float z_offset = 0.5f;
    private spawner spw;
    public string nam;
    private GameObject FollowCam;
    private Camera RealCam;
    private GameObject NorCam;

    GameObject[] fuels;
    GameObject[] bot;





     bool toMove = false;
     bool moveing = false;
    bool die = false;
    public bool stuned = false;





    public enum TypeOfUnit
    {
        Player,
        PlayBot,
        Chaiser,
        White
    }


    public void stWhite()
    {
        Type = TypeOfUnit.White;
        tag = "fuel";
        Material material = new Material(Shader.Find("Standard"));
        material.SetFloat("_Metallic", 0);
        material.SetFloat("_Glossiness", 0);
        material.color = Color.white;
        
        body.material = material;
        head.material = material;
        toMove = false;
        moveing = false;
        die = false;
        stuned = false;
        animator.SetFloat("MoveSpeed", 0);
        m_moveSpeed = 2f;
        rb.detectCollisions = false;
        col.enabled = false;
        tailObjects.Clear();
        main = null;
        bot = new GameObject[0];
        fuels = new GameObject[0];
    }

      Button skill_but;
    skill skill_log;
    public void stPlayer(Material mat, List<GameObject> o, string na,SimpleSampleCharacterControl sscc)
    {  
        Type = TypeOfUnit.Player;
       

     

        tailObjects = o;
        tailObjects.Add(gameObject);
        tag = "PlayerTail";
        body.material = mat;
        head.material = mat;
        toMove = false;
        moveing = false;
        die = false;
        stuned = false;
        m_moveSpeed = sscc.m_moveSpeed;
        rb.detectCollisions = true;
        col.enabled = true;
        main = this;
        nam = na;
        bot = new GameObject[0];
        fuels = new GameObject[0];
        rtt = sscc.rtt;
        rt = sscc.rt;
        trail = sscc.trail;
        skill_but = sscc.skill_but;
        skill = sscc.skill;
        skillType = sscc.skillType;
        skill_log = sscc.skill_log;
        skill_but.onClick.AddListener(Skill);
        fastRun = sscc.fastRun;
        fastTimer = sscc.fastTimer;

    }

    public void stPlayerBot(SimpleSampleCharacterControl mat, List<GameObject> o, string na)
    {
        if (Type == TypeOfUnit.White)
        {
            Type = TypeOfUnit.PlayBot;

            main = this;
            tailObjects = o;
            tailObjects.Add(gameObject);
            tag = "PlayerTail";
            body.material = mat.body.material;
            head.material = mat.head.material;
            toMove = false;
            moveing = false;
            die = false;
            stuned = false;
            m_moveSpeed = 2f;
            rb.detectCollisions = false;
            col.enabled = false;
            nam = na;
            bot = new GameObject[0];
            fuels = new GameObject[0];
        }


    
    }

    public SimpleSampleCharacterControl main;
    public void stChaiser(GameObject main, float speed)
    {
      
        if(Type == TypeOfUnit.Player || Type == TypeOfUnit.PlayBot)
        {
         
            Type = TypeOfUnit.Chaiser;
            this.main = main.GetComponent<SimpleSampleCharacterControl>();
            Material mat = main.GetComponent<SimpleSampleCharacterControl>().head.material;   
            tag = "bot";
            
            body.material = mat;
            head.material = mat;
            toMove = true;
            moveing = false;
            die = false;
            stuned = false;
            m_moveSpeed = this.main.m_moveSpeed + 2.5f;
            rb.detectCollisions = false;
            col.enabled = false;
            //tailObjects.Clear();           
            bot = new GameObject[0];
            fuels = new GameObject[0];
        }
        
    }
    GameObject trail;
    menu men;
    private void Awake()
    {
        men = GameObject.FindGameObjectWithTag("spawner").GetComponent<menu>();
        skill_but = GameObject.FindGameObjectWithTag("skill").GetComponent<Button>();
        skill_log = skill_but.GetComponent<skill>();
         canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>();
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<FixedJoystick>();
        FollowCam = GameObject.FindGameObjectWithTag("FollowCam");
        RealCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        NorCam = GameObject.FindGameObjectWithTag("camera");
        spw = GameObject.FindGameObjectWithTag("spawner").GetComponent<spawner>();
        switch (Type)
        {
            case TypeOfUnit.Player:
                spw.play = gameObject;
                trail = GameObject.FindGameObjectWithTag("trail");
                trail.SetActive(false);
                main = this;
                rtt = GameObject.FindGameObjectWithTag("target");
                rt = rtt.GetComponent<RectTransform>();
                Material material = new Material(Shader.Find("Standard"));  
                material.SetFloat("_Metallic", 0);
                material.SetFloat("_Glossiness", 0);
                material.color = new Color(0.26f,0.43f,0.85f);
                body.material = material;
                head.material = material;
                tailObjects.Add(gameObject);
                nam = "Player" + Random.Range(1, 100);
                toMove = false;
                moveing = false;
                die = false;
                stuned = false;
                m_moveSpeed = 2;
                skill_but.onClick.AddListener(Skill);
                break;
            
            case TypeOfUnit.White:
                stWhite();
                break;

            case TypeOfUnit.PlayBot:
                Material material1 = new Material(Shader.Find("Standard"));
                nam = "Player" + Random.Range(1, 100);
                material1.SetFloat("_Metallic", 0);
                material1.SetFloat("_Glossiness", 0);
                main = this;

                material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);

                if (material1.color.r < 0.2f && material1.color.g < 0.2f && material1.color.b < 0.2f)
                    material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);

                if (material1.color.r < 0.2f && material1.color.g < 0.2f && material1.color.b < 0.2f)
                    material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);

                if (material1.color.r < 0.2f && material1.color.g < 0.2f && material1.color.b < 0.2f)
                    material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);

                if (material1.color.r < 0.2f && material1.color.g < 0.2f && material1.color.b < 0.2f)
                    material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);

                if (material1.color.r < 0.2f && material1.color.g < 0.2f && material1.color.b < 0.2f)
                    material1.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);



                toMove = false;
                moveing = false;
                die = false;
                stuned = false;
                m_moveSpeed = 2f;
                body.material = material1;
                head.material = material1;
                tailObjects.Add(gameObject);
                rb.detectCollisions = false;
                col.enabled = false;
                break;
        }


    }

    void Death()
    {
        for (int i = 0; i < tailObjects.Count; i++)
        {
            tailObjects[i].GetComponent<SimpleSampleCharacterControl>().stWhite();
        }
    }

    public GameObject head_sph;
    float dist  = 5f;
    GameObject rtt;
    RectTransform rt;
    RectTransform canvas;
    Vector2 uiOffset;

   
    public void MoveToClickPoint(Vector3 objectTransformPosition)
    {
        uiOffset = new Vector2((float)canvas.sizeDelta.x / 2f, (float)canvas.sizeDelta.y / 2f);
        Vector2 ViewportPosition = RealCam.WorldToViewportPoint(objectTransformPosition);
        //Debug.Log(ViewportPosition);
        Vector2 proportionalPosition = new Vector2(ViewportPosition.x * canvas.sizeDelta.x, ViewportPosition.y * canvas.sizeDelta.y);
        rt.localPosition = proportionalPosition - uiOffset;
    }



    private void Update()
    {
       
        switch (Type)
        {
            case TypeOfUnit.Player:

                
               
                if (!die )
                {
                    spw.play = gameObject;


                    if (skill) //Пасивное побочка скилы в виде мешени
                    {
                       if(skillType == ItemType.Bomb || skillType == ItemType.Firework)
                        {
                            float dis = 999999;

                            GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerTail");
                            targ = null;

                            switch(skillType)
                            {
                                case ItemType.Bomb:
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (Vector3.Distance(transform.position, players[i].transform.position) < dis && players[i] != gameObject && Vector3.Distance(transform.position, players[i].transform.position) < 5)
                                        {
                                            rtt.SetActive(true);
                                            dis = Vector3.Distance(transform.position, players[i].transform.position);
                                            targ = players[i];
                                            skill_log.SkillUpdate(true);
                                        }

                                    }
                                    break;
                                case ItemType.Firework:
                                    for (int i = 0; i < players.Length; i++)
                                    {
                                        if (Vector3.Distance(transform.position, players[i].transform.position) < dis && players[i] != gameObject && Vector3.Distance(transform.position, players[i].transform.position) < 10)
                                        {
                                            rtt.SetActive(true);
                                            dis = Vector3.Distance(transform.position, players[i].transform.position);
                                            targ = players[i];
                                            skill_log.SkillUpdate(true);
                                        }

                                    }
                                    break;
                            }

                            


                            if (targ == null)
                            {
                                rtt.SetActive(false);
                                skill_log.SkillUpdate(false);
                            }


                            if (targ != null)
                                switch (skillType)
                                {
                                    case ItemType.Bomb:
                                        MoveToClickPoint(targ.GetComponent<SimpleSampleCharacterControl>().head_sph.transform.position);
                                        break;
                                    case ItemType.Firework:
                                        MoveToClickPoint(targ.GetComponent<SimpleSampleCharacterControl>().head_sph.transform.position);
                                        break;
                                }
                           
                        }
                        else
                        {
                            rtt.SetActive(false);
                        }
                    }
                    else
                    {
                        rtt.SetActive(false);
                    }



                   
                        
                            if (fastRun)
                            {
                                if (fastTimer > 0)
                                {
                                 
                                   // main.m_moveSpeed = 4;                                 
                                    fastTimer -= Time.deltaTime;
                                    trail.transform.position = Vector3.MoveTowards(trail.transform.position, main.tailObjects[0].transform.position, Time.deltaTime * 6);
                                }
                                else
                                {
                                    fastRun = false;
                                   for (int i = 0; i < main.tailObjects.Count; i++)
                                    {
                                        main.tailObjects[i].GetComponent<SimpleSampleCharacterControl>().m_moveSpeed -= 2;
                                    }
                                    // main.m_moveSpeed = 2;
                                    trail.SetActive(false);
                                   
                                    //for(int i = 0; i < main.tailObjects.Count;i++)
                                    //{
                                    //    main.tailObjects[i].GetComponent<SimpleSampleCharacterControl>().particles[3].Stop();
                                    //}


                                    //particles[3].Stop();
                                }
                            }

                       
                    




                    fuels = GameObject.FindGameObjectsWithTag("fuel");

                    for (int i = 0; i < fuels.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, fuels[i].transform.position) < 0.5f)
                        {
                            SimpleSampleCharacterControl ss = fuels[i].GetComponent<SimpleSampleCharacterControl>();

                            if (ss.Type == TypeOfUnit.White)
                            {
                                
                                NorCam.transform.Translate(new Vector3(0, 0, -0.2f), Space.Self);
                                sources[0].Play();
                                skill_but.onClick.RemoveAllListeners();
                                skill_log.butApp();
                                ss.stPlayer(head.material, tailObjects, nam,this);
                                ss.particles[0].Play();
                               ss.spw.CheckPlacee();
                                stChaiser(ss.gameObject, m_moveSpeed);   
                                break;
                            }
                        }
                    }

                    bot = GameObject.FindGameObjectsWithTag("bot");
                    for (int i = 0; i < bot.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, bot[i].transform.position) < 0.2f && bot[i].GetComponent<SimpleSampleCharacterControl>().head.material.color != head.material.color)
                        {
                            men.tryagainn(tailObjects.Count);
                            animator.SetFloat("MoveSpeed", 0);
                            die = true;
                            particles[1].Play();
                            sources[1].Play();
                            m_moveSpeed = 0;
                            stWhite();
                            
                            spw.CheckPlacee();
                            FollowCam.transform.Translate(new Vector3(0, 0, (-0.2f) * tailObjects.Count - 1), Space.Self);
                            break;
                        }
                    }

                }
               
                
                
               
               
                
                break;

            case TypeOfUnit.PlayBot:
                if (!die )
                {
                   
                       
                            if (stuned)
                            {
                               
                                if (stunTimer > 0)
                                {

                                   
                                    stunTimer -= Time.deltaTime;
                                   
                                }
                                else
                                {
                                    stuned = false;
                                  
                                    particles[2].Stop();
                                }
                            }

                          

                    


                    fuels = GameObject.FindGameObjectsWithTag("fuel");

                    for (int i = 0; i < fuels.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, fuels[i].transform.position) < 0.5f)
                        {
                            SimpleSampleCharacterControl ss = fuels[i].GetComponent<SimpleSampleCharacterControl>();

                            if (ss.Type == TypeOfUnit.White)
                            {
                               
                                ss.stPlayerBot(this, tailObjects, nam);
                                stChaiser(ss.gameObject, m_moveSpeed);
                                ss.spw.CheckPlacee();
                                break;
                            }
                        }
                    }


                    bot = GameObject.FindGameObjectsWithTag("bot");
                    for (int i = 0; i < bot.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, bot[i].transform.position) < 0.2f && bot[i].GetComponent<SimpleSampleCharacterControl>().head.material.color != head.material.color)
                        {
                            animator.SetFloat("MoveSpeed", 0);
                            die = true;
                            particles[1].Play();
                            sources[1].Play();
                            m_moveSpeed = 0;
                            stWhite();
                            spw.CheckPlacee();
                            //Death();
                            break;
                        }
                    }
                }
                break;


            case TypeOfUnit.Chaiser:
                body.material.color = main.body.material.color;
                head.material.color = main.head.material.color;
                break;
        }
    



    }

    public bool skill = false;
    public ItemType skillType ;
    GameObject targ;
    public GameObject[] items;

    public void GetReadySkill(ItemType it)
    {
        Debug.Log("lol");
        skill_log.Set(it);
        skill = true;
        skillType = it;
    }

    

    public void Skill()
    {
       if(skill)
        {

            switch (skillType)
            {
                case ItemType.Bomb:
                    if (targ != null)
                    {
                        GameObject o = Instantiate(items[0], transform.position, Quaternion.identity);
                        o.GetComponent<Item>().Set(targ, gameObject);
                        skill = false;
                        targ = null;
                    }  
                    break;

                case ItemType.Firework:
                    if (targ != null)
                    {
                        GameObject o1 = Instantiate(items[1], transform.position, Quaternion.identity);
                        o1.GetComponent<Item>().Set(targ, gameObject);
                        skill = false;
                        targ = null;
                    }  
                    break;
               
                case ItemType.Fast:
                    for (int i = 0; i < main.tailObjects.Count; i++)
                    {
                        main.tailObjects[i].GetComponent<SimpleSampleCharacterControl>().m_moveSpeed += 2;
                    }

                    trail.transform.position = main.tailObjects[0].transform.position;
                    trail.SetActive(true);
                    fastTimer = 5f;
                    fastRun = true;
                    skill = false;
                    break;
            }
        }  
    }

    float fastTimer;
    bool fastRun = false;

    float stunTimer;
  



    public void Hit(ItemType hit)
    {
        switch (hit)
        {
            case ItemType.Bomb:
                if (main == this)
                {
                    stWhite();
                }
               else
                {
                    main.Hit(hit);
                }





                // SimpleSampleCharacterControl sscc = tailObjects[tailObjects.Count - 3].GetComponent<SimpleSampleCharacterControl>();
                //  sscc.stPlayerBot(this, tailObjects, name);
                //  sscc.stuned = true;
                //  sscc.stunTimer = 2f;
                //  sscc.particles[2].Play();




                break;

            case ItemType.Firework:
                main.particles[2].Play();
                main.stunTimer = 3f;
                main.stuned = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(Type)
        {
            case TypeOfUnit.Player:
                if (!die || !stuned)
                {
                    FollowCam.transform.position = Vector3.Slerp(FollowCam.transform.position, transform.position, Time.deltaTime * 3);
                    DirectUpdate();
                }
   
                break;
            case TypeOfUnit.Chaiser:
                ChaiserUpdate();
                break;

            case TypeOfUnit.PlayBot:
                if (!die  )
                {
                 if(!stuned)
                    BotDirectUpdate();
                }
                break;
        }
    }



    private void DirectUpdate()
    {
        float v = joystick.Vertical;
        float h = joystick.Horizontal;

        Transform camera = RealCam.transform;
   
        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
           
          
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        for (int i = 0; i < tailObjects.Count; i++)
        {
            SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();

            sscc.head.material.color = head.material.color;
            sscc.body.material.color = body.material.color;
        }

        if (v != 0 || h != 0)
        {

            if (!moveing)
            {
                //Debug.Log("Move");

                moveing = true;

                for (int i = 0; i < tailObjects.Count; i++)
                {
                    SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();
                    sscc.toMove = true;

                    sscc.head.material = head.material;
                    sscc.body.material = body.material;
                }
                    
            }
        }
        else
        {
            //Debug.Log("Do not move");
            for (int i = 0; i < tailObjects.Count; i++)
            {
                SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();
                sscc.toMove = false;

                sscc.head.material = head.material;
                sscc.body.material = body.material;
            }
            moveing = false;
        }
    }

    Vector3 tailTarget;
    private void ChaiserUpdate()
    {
        if (main.GetComponent<SimpleSampleCharacterControl>().Type == TypeOfUnit.White)
            stWhite();
        else
        {
            if (toMove)
            {
               try
                {
                    tailTarget = main.tailObjects[main.tailObjects.IndexOf(gameObject) + 1].transform.position;
                    transform.LookAt(tailTarget);
                    transform.position = Vector3.Lerp(transform.position, tailTarget, Time.deltaTime * m_moveSpeed);
                    animator.SetFloat("MoveSpeed", 1);
                }
               catch
                {
                    stWhite();
                }

            }
            else
                animator.SetFloat("MoveSpeed", 0);
        }
    }

    public Vector3 target = new Vector3(999, 999, 999);

    bool f = false;
    bool g = false;



    void FindTarget()
    {
        fuels = GameObject.FindGameObjectsWithTag("fuel");
        try
        {
            target = fuels[Random.Range(0, fuels.Length)].transform.position;
        }
        catch
        {
            target = new Vector3(Random.Range(13f, -13f), 0.225f, Random.Range(13f, -13f));
        }

    }

  
    private void BotDirectUpdate()
    {
        if (target != new Vector3(999, 999, 999))
        {
            if (!g)
            {
                g = true;

                for (int i = 0; i < tailObjects.Count; i++)
                {
                    SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();
                    sscc.toMove = true;

                    //sscc.head.material.color = head.material.color;
                    //sscc.body.material.color = body.material.color;
                }
            }
            
            for (int i = 0; i < tailObjects.Count; i++)
            {
                SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();

                sscc.head.material.color = head.material.color;
                sscc.body.material.color = body.material.color;
            }
            
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target, m_moveSpeed * Time.deltaTime);

            animator.SetFloat("MoveSpeed", 1);

            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                target = new Vector3(999, 999, 999);
                f = false;
            }
        }
        else
        {
            if (!f)
            {
                f = true;
                FindTarget();
            }



            animator.SetFloat("MoveSpeed", 0);
            for (int i = 0; i < tailObjects.Count; i++)
            {
                SimpleSampleCharacterControl sscc = tailObjects[i].GetComponent<SimpleSampleCharacterControl>();
                sscc.toMove = false;

                //sscc.head.material.color = head.material.color;
                //sscc.body.material.color = body.material.color;
            }
            g = false;
        }

    }
}
