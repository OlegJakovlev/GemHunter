﻿using UnityEngine;

public class Pistol : Gun
{
    void Start()
    {
        // Damage
        DamagePerShot = 15;

        // Ammo
        ammoInClip = 17;
        maxAmmo = 108;
        ammoLeft = ammoInClip;
        CurrentAmmo = ammoInClip;

        // Range
        effectiveRange = 10f;
        maxRange = 200f;

        // Reload
        reloadTime = 2f;
        cooldownTime = 0.15f;

        // Unlocked
        Unlocked = true;
        print("UNLOCKED!!!");
    }
}
