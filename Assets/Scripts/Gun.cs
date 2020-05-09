using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    //stats
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    //Hud
    public bool hudUp = false;
    public TextMeshProUGUI enemyHp;
    public TextMeshProUGUI enemyName;
    public Slider eBar;
    //Ammo
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    //Scoped efffect
    public Camera mainCamera;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    
    private bool isScoped = false;
    public float scopedFOV = 15f;
    private float normalFOV;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public float playerSpeed;
    
    PlayerMovement player;

    public Animator animator;
    // Sound
    public AudioSource audio;
    public AudioClip shot, reload;

    public Sprite gun;//image already set
    
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        currentAmmo = maxAmmo;//always top off your ammo 
    }
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        player.setSpeed(playerSpeed);
        player.setScoped(isScoped);

        if (hudUp == true)
        {
            displayUp();
            StartCoroutine(displayDown());
            
        }
        else if (hudUp == false)
        {
            enemyHp.gameObject.SetActive(false);
            enemyName.gameObject.SetActive(false);
            eBar.gameObject.SetActive(false);
            
        }
        
        if (isReloading)//dont allow anything if reloading
        {
            return;
        }

        if ((currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)) && currentAmmo != maxAmmo)// start reload 
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)//shooting 
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            
            animator.SetBool("Scoped", isScoped);
            if (isScoped)
                StartCoroutine(OnScoped());//scope in 
            else
                OnUnscoped();
        }
        
    }
    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);//set weapon camera true when we are scoped in

        mainCamera.fieldOfView = normalFOV;
    }
    void displayUp()
    {
        enemyHp.gameObject.SetActive(true);
        enemyName.gameObject.SetActive(true);
        eBar.gameObject.SetActive(true);
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);// negates the delay so it syncs properly
        enemyHp.gameObject.SetActive(false);
        enemyName.gameObject.SetActive(false);
        eBar.gameObject.SetActive(false);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);//set the wepaon camera false when zoomed in

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void Shoot()// shoot function
    {
        muzzleFlash.Play();// muzzle flash effect
        audio.PlayOneShot(shot);

        currentAmmo--;// use a bullet

        RaycastHit hit;// an object that got hit

        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit, range))//ray cast to the target 
        {
            Debug.Log(hit.transform.name);
           Enemy enemy = hit.transform.GetComponent<Enemy>();// get the enenmy
            if (enemy != null)
            {
                hudUp = true;//display the stats
                enemy.TakeDamage(damage);//kill the enemy
                eBar.maxValue = enemy.getMaxHp();//display the enemy at the top of the screen
                enemyHp.text = "" + enemy.getHp() + " / " + enemy.getMaxHp();
                enemyName.text = enemy.getName();
                eBar.value = enemy.getHp();
            }
            if(hit.rigidbody != null)//if it has a rigidbody knock it over
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);//adding impact force to the shots
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));//create impact effect at the target of the bullet
            Destroy(impact, 2f);//destroy the effect after seconds
        }
    }

    IEnumerator Reload()//reloading Ienumerator
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);//reload animation  beginning 
        audio.PlayOneShot(reload);

        yield return new WaitForSeconds(reloadTime -.25f);//wait for however long the reload is to reload

        animator.SetBool("Reloading", false);//reload animation returning to idle
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;//resets the ammo count
        isReloading = false;//reloading is false
        
    }

    IEnumerator displayDown()// after  seconds set the enemy display hud false
    {
        yield return new WaitForSeconds(5f);
        hudUp = false;
    }
   
    public void hudDown()
    {
        hudUp = false;
    }

   public int GetAmmo()//used for ui;
    {
        return currentAmmo;
    }
    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
}
