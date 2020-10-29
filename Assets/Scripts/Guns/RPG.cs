﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
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
        fireRate = 0.15f;
    }
}
