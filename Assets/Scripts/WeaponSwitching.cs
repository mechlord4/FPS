using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public TextMeshProUGUI ammo;
    public GameObject currentwep;
    public Image uiGun;
    public Sprite currentGun;
    
    void Start()
    {
        
        SelectWeapon();
        ammo.text = "" + currentwep.GetComponent<Gun>().GetAmmo() + "/" + currentwep.GetComponent<Gun>().GetMaxAmmo();

    }

    // Update is called once per frame
    void Update()
    {
        uiGun.sprite = currentGun;
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)// if scrollig up add to the selected weapon 
        {
            if(selectedWeapon >= transform.childCount -1)//if you reach the last weapon in the index wrap back to start
            {
                selectedWeapon = 0;
            }
            else
            selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f )//same as above but going down
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount -1;//wrap back around to the top
            }
            else
                selectedWeapon--;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))// change weapons 
        {
            print("Change weapons to slot 1");
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            print("Change weapons to slot 2");
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            print("Change weapons to slot 3");
            selectedWeapon = 2;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        print(currentwep);
        ammo.text = "" +currentwep.GetComponent<Gun>().GetAmmo() + " / " + currentwep.GetComponent<Gun>().GetMaxAmmo();// seyt the UI current ammo to the ammo of the gun
        
    }

    void SelectWeapon()// return a weapon variable to display ammo
    {
        int i = 0;
        
        foreach(Transform weapon in transform)// all weapons in weapon holder
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentwep = weapon.gameObject;
                currentGun = weapon.GetComponent<Gun>().gun;
            }
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
        
    }
}
