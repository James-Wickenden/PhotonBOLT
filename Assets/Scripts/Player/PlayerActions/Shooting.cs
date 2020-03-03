using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : Bolt.EntityBehaviour<ICustomCubeState>
{
    public Rigidbody bulletPrefab;
    public float bulletSpeed;
    public GameObject muzzle;

    private float resetMuzzleFlashTime;

    [SerializeField]
    private float muzzleFlashDuration = 0.01f;

    [SerializeField]
    private float muzzleFlashIntensity = 0.5f;

    private Light muzzleFlash;

    private bool animStart = false;
    private float startTime;


    public override void Attached()
    {
        state.OnShoot = Shoot;

        muzzleFlash = muzzle.AddComponent<Light>();
        muzzleFlash.color = Color.yellow;
        muzzleFlash.intensity = 0.0f;
    }

    private void Start()
    {
        if (entity.IsOwner)
        {
            Button shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
            shootButton.onClick.AddListener(() => OnShootButtonClick());
        }
    }

    private void Shoot()
    { 
        Rigidbody bulletClone = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);
        

        Projectile projectile = bulletClone.GetComponent<Projectile>();
        projectile.setSourceID(GetInstanceID());

        bulletClone.velocity = muzzle.transform.forward * projectile.getSpeed();
        bulletClone.transform.rotation = muzzle.transform.rotation;


        startTime = Time.time;
        animStart = true;
    }

    private void Update()
    {
        if (animStart)
        {
            float phi = (Time.time - startTime) / muzzleFlashDuration * 2 * Mathf.PI;
            float amplitude = Mathf.Cos(phi) * muzzleFlashIntensity + muzzleFlashIntensity;
            muzzleFlash.intensity = amplitude;
            if (amplitude <= muzzleFlashIntensity)
            {
                animStart = false;
                muzzleFlash.intensity = 0.0F;
            }
        }

        if (entity.IsOwner)
        {
            if (Input.GetKeyDown("joystick button 0") ||
                Input.GetKeyDown("joystick button 7") ||
                Input.GetKeyDown("joystick button 9"))
            {
                state.Shoot();
                Debug.Log("SHOOT");
            }
        }
    }

    private void OnShootButtonClick()
    {
        if (entity.IsOwner)
        {
            state.Shoot();
        }
    }
}
