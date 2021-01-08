﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AmmoBox : MonoBehaviour
{
    protected WeaponSwitcher playerInventory;
    protected int weaponID;
    public event Action OnPickUP;

    protected void Start()
    {
        playerInventory = PlayerManager.instance.player.GetComponent<Player>().inventory;
    }

    protected virtual void PickUP()
    {
        OnPickUP?.Invoke();

        // If player has this type of weapon - add ammo
        if (playerInventory.availableGuns[weaponID])
        {
            playerInventory.availableGuns[weaponID].RefillAmmo();
        }

        // Destroy a ammo box object
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUP();
        }
    }
}