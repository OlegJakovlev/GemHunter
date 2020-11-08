﻿using System;
using System.Collections;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public int DamagePerBullet { get; protected set; }
    public int CurrentAmmo { get; protected set;}

    // Ammo block
    protected int ammoInClip;
    public int MaxAmmo { protected get; set; }
    protected int ammoLeft;

    // Range
    public float EffectiveRange { get; protected set; }
    protected float maxRange;

    // Reload
    protected float reloadTime;
    protected float fireRate;
    private float nextShootTime = 1f;
    private bool reloadProcess = false;

    // Attack point/Bullet point reference
    public Transform attackPoint;

    // LayerMask for specific layers (10 stands for enemy, 9 for player)
    protected LayerMask enemyLayers = 1 << 10;

    // Projectile
    public Projectile bullet;
    public int BulletSpeed { get; protected set; }

    private void OnEnable()
    {
        // If player switches the weapon while reloading, we need to reset reload process
        reloadProcess = false;
    }

    public void Shoot()
    {
        print(CurrentAmmo);
        // Check if gun have ammo
        if (CurrentAmmo == 0)
        {
            ForceReload();
            return;
        }

        // Check if we can shoot
        if (Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + 1f / fireRate;
            ShootBullet();
        }
        else return;
    }

    protected virtual void ShootBullet()
    {
        // Check if power up is activated
        int powerUPMultiplier = PlayerManager.instance.player.GetComponent<Player>().GunPowerUPMultiplier;

        // Substract each shot
        CurrentAmmo--;

        // Create new bullet with passing Gun there
        Projectile newShoot = Instantiate(bullet, attackPoint.position, attackPoint.rotation);
        newShoot.CurrentGun = this;
        newShoot.PowerUPMultiplier = powerUPMultiplier;
        newShoot.Speed = BulletSpeed;
    }

    public void ForceReload()
    {
        StartCoroutine(Reload());
    }

    protected IEnumerator Reload()
    {
        if (!reloadProcess)
        {
            if (ammoLeft != 0 && ammoInClip != CurrentAmmo)
            {
                
                reloadProcess = true;

                // Play animation + sound

                // Wait for time
                yield return new WaitForSeconds(reloadTime);

                // Enemies have infinite ammo
                if (MaxAmmo == -1)
                {
                    CurrentAmmo = ammoInClip;
                }
                // Check if we have enought ammo for whole clip
                else if (ammoInClip > ammoLeft)
                {
                    CurrentAmmo = ammoLeft;
                    ammoLeft = 0;
                }
                else
                {
                    CurrentAmmo = ammoInClip;
                    ammoLeft -= ammoInClip;
                }
                reloadProcess = false;
            }
        }
    }
}