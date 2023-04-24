using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;
    public DefaultState defaultState = new DefaultState();
    public ReloadState reloadState = new ReloadState();

    public GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
    AudioSource audioSource;
    [HideInInspector] public Animator anim;

    public MultiAimConstraint rightHandAim;
    public TwoBoneIKConstraint leftHandIK;
    // Start is called before the first frame update
    void Start()
    {
        SwitchState(defaultState);
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
        anim = GetComponent<Animator>();
        audioSource = currentWeapon.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        
    }

    public void SwitchState(ActionBaseState newState) 
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void WeaponReloaded() 
    {
        ammo.Reload();
        SwitchState(defaultState);
    }

    public void MagOut() 
    {
        audioSource.PlayOneShot(ammo.magOutSound);
    }
    public void MagIn() 
    {
        audioSource.PlayOneShot(ammo.magInSound);

    }
    public void ReleseSlide() 
    {
        audioSource.PlayOneShot(ammo.releaseSlide);
    }
}
