using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    public float throwForce = 40f;
    public GameObject grenadePrefab;

    PlayerMovement player;

    public AudioSource audio;
    public AudioClip sound;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //input based on mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        if (Input.GetKeyDown(KeyCode.Q) && player.grenadeCount >0)// if the player presses q and has grenades
        {
            audio.PlayOneShot(sound);//grenade throwing sound effect
            ThrowGrenade();
            player.grenadeCount--;
        }


    }
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
