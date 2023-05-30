using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform body;
    public float sensitivity = 500f;
    public float xClamp;
    public Transform[] weapons;
    public int currentWeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SelectWeapon(currentWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseXaxis = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseYaxis = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xClamp -= mouseYaxis;
        xClamp = Mathf.Clamp(xClamp, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xClamp, 0f, 0f);
        body.Rotate(Vector3.up * mouseXaxis);

        // Switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
    }

    void SelectWeapon(int weaponIndex)
    {
        // Deactivate all weapons
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        // Activate the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = weaponIndex;
    }
}
