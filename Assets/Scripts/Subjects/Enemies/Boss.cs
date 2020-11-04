﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private byte phase;

    void Start()
    {
        // HP and Speed of movement
        HP = 500;
        Speed = 5f;

        // Weapon
        meleeChance = 0;
        pistolChance = 0;
        ShotgunChance = 0;
        AssaultChance = 0;
        RPGChance = 0;
    }
}
