using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory/Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    [Space(10)]
    public string desc;
    public bool unlocked;
    public int cost;
    public int maxAmmo;
    public float fireRate;
    public bool autoFire;
    public GameObject bullet, muzzle, gun;
    public float shakeTime = 0.05f;
    public bool bigWeapon, destroyAfterThrow = false;

    public AudioClip[] fireClip;

    public AudioClip GetRandomClip()
    {
        return fireClip[Random.Range(0, fireClip.Length - 1)];
    }
}
