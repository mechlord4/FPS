using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

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

    public Animator animator;

    private void Start()
    {
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
        if (isReloading)//dont allow anything if reloading
        {
            return;
        }

        if (currentAmmo <= 0)// start reload 
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

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);//set the wepaon camera false when zoomed in

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward,out hit, range))//ray cast to the target 
        {
            Debug.Log(hit.transform.name);
           Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);//kill the enemy
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);//adding impact force to the shots
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));//create impact effect at the target of the bullet
            Destroy(impact, 2f);//destroy the effect after seconds
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);//reload animation  beginning 

        yield return new WaitForSeconds(reloadTime -.25f);//wait for however long the reload is to reload

        animator.SetBool("Reloading", false);//reload animation returning to idle
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;//resets the ammo count
        isReloading = false;//reloaidng is false
        
    }
}
