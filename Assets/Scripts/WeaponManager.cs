using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;


    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    AimStateManager aimStateManager;
    //For Shotguns is 8, Regulat rifles is 1
    [SerializeField] int bulletPerShot =1;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;

    WeaponAmmo ammo;
    ActionStateManager actions;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aimStateManager = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFire()) Fire();
    }

    bool shouldFire() 
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) 
        {
            return false;
        }
        if (ammo.currentAmmo == 0) 
        {
            return false;
        }
        if (actions.currentState == actions.reloadState) 
        {
            return false;
        }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            return true;
        }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        return false;

    }
    void Fire() 
    {
        fireRateTimer = 0;
        barrelPosition.LookAt(aimStateManager.actualAimPosition);
        audioSource.PlayOneShot(gunShot);
        ammo.currentAmmo--;
        
        for (int i = 0; i < bulletPerShot; i++) 
        {
            GameObject currentBullet = Instantiate(bullet,barrelPosition.position,barrelPosition.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Force);
        }
        
    }
}
