using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)// if scrollig up add to the selected weapon 
        {
            if(selectedWeapon >= transform.childCount -1)//iff you reach the last weapon in the index wrap back to start
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
        
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)// all weapons in weapon holder
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
