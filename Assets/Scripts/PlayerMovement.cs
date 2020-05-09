using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;
    public CharacterController controller;
    static int scene = 0;
    private float speed;
    private float gravity = -9.81f;
    private float jumpHeight = 3f;
    public int grenadeCount;
    private int maxGrenadeCount = 3;
    public int bosses = 2;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    //Hud
    public Slider healthBar;
    public TextMeshProUGUI healthtxt;
    public TextMeshProUGUI grenadetxt;
    public TextMeshProUGUI killtxt;
    
    private float maxHp = 100;
    public float hp;

    public AudioSource audio;
    public AudioClip heal,restock;

    private bool recentlyhit;

    Vector3 veloctiy;
    bool isGrounded;

    private int killCount;
    public bool isInjured = false;

    private bool isScoped;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        hp = maxHp;
        healthBar.maxValue = hp;
        grenadeCount = maxGrenadeCount;
        killCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        killtxt.text = "" + getKillCount();
        healthBar.value = hp;
        healthtxt.text = "" + getHp() + " / " + getMaxHp();
        grenadetxt.text = "" + getGrenadeCount();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grenadeCount > maxGrenadeCount)
        {
            grenadeCount = maxGrenadeCount;
        }
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp < 0)
        {
            gm.toDeath();
        }
        if (isGrounded && veloctiy.y <0)
        {
            veloctiy.y = -2f;
        }
        if (isScoped)
        {
            healthBar.gameObject.SetActive(false);
        }
        else if (!isScoped)
        {
            healthBar.gameObject.SetActive(true);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;//controls the local direction of the player to move and rotate
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            veloctiy.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (scene == 0 && killCount >= 20)
        {
            gm.ToGame2();
            scene++;
        }
        if (bosses ==0)
        {
            gm.toWin();
        }
        veloctiy.y += gravity * Time.deltaTime;//creating gravity

        controller.Move(veloctiy * Time.deltaTime);// the equation is time squared
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.toMenu();
        }
    }

    
    //getter and setter methods
    float getMaxHp()
    {
        return maxHp;
    }
    int getGrenadeCount()
    {
        return grenadeCount;
    }
    int getMaxGrenadeCount()
    {
        return maxGrenadeCount;
    }
    public void addKill()
    {
        killCount++;
    }
    public int getKillCount()
    {
        return killCount;
    }
    float getHp()
    {
        return hp;
    }
    public void takeDamage(int damage)
    {
        hp -= damage;
        
    }
 
    public void addHp(int health)
    {
        hp += health;
        audio.PlayOneShot(heal);
    }
    public void addGrenade()
    {
        audio.PlayOneShot(restock);
        grenadeCount++;
    }

    public void killBoss()
    {
        bosses--;
    }
   public void setScoped(bool a)// displays the hp hud based on if it is scoped
   {
        isScoped = a;
   }

    public void setSpeed(float gspd)// the speed stat on the gun determines the players movement speed
    {
        speed = gspd;
    }
}
