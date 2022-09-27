using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public float range;

    private Inventory inventory;
    private PlayerXP xp;
    private Transform target;
    private bool startUpdate = false;

    private float waitTime;
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
        damage = inventory.getWeaponDamage();
        fireRate = inventory.getWeaponFireRate();
        range = inventory.getWeaponRange();
        waitTime = 1 / fireRate;
        xp = GetComponent<PlayerXP>();
    }
    private void Update()
    {
        if (startUpdate)
        {
            damage = inventory.getWeaponDamage();
            fireRate = inventory.getWeaponFireRate();
            range = inventory.getWeaponRange();
            float nextFireTime = Time.time + waitTime;
            if(nextFireTime <= Time.time)
            {
                doAttack();
            }
        }
    }

    public void startAttack(Transform Enemy)
    {
        startUpdate = true;
        target = Enemy;
    }

    public void endAttack()
    {
        target = null;
        startUpdate = false;
    }

    private void doAttack()
    {
        
        if (xp != null)
        {
            int level = (int)xp.getLevel(inventory.weaponTypeToXP());
            float accuracy = xp.accuracyPerLevel[level];

            float random = Random.Range(0, 1);
            if(random <= accuracy)
            {
                target.GetComponent<Health>().attack(damage);
            }
            else
            {
                target.GetComponent<Health>().attack(0);
            }
            
        }
        
    }
}