using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourB : MonoBehaviour
{
    public UnitData unitData;

    // We need rigidBody2D to make a movement

    public Rigidbody2D rigidbody2d;
    
    // We need Collider2D to allow collision detection
    
    public Collider2D collider2d;

    public GameObject target;

    // Agent need to have a weapon system here to shoot bullet
    public ShootObjectMultiple weaponProjectileSystem;

    // The direction that will face the target, we use playground’s enums

    public Enums.Directions orientationSide = Enums.Directions.Up;

    public bool EnableFaceToTarget;


    private float timeBtwShots;
    public float startTimeBtwshots;

    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        if (!rigidbody2d)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        if (!collider2d)
        {
            collider2d = GetComponent<Collider2D>();
        }

        SetupUnitData();

        timeBtwShots = startTimeBtwshots;
    
    }

    // Update is called once per frame
    void Update()
    {
        if( !target )
        { 
            DetectEnemy();
        }
        else 
        {
            FollowTarget();
            AdjustRotationToTarget();
        }

        if(timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwshots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    public void SetupUnitData()
    {
        // Reference Playground HealthSystemAttribute component
        HealthSystemAttribute healthSystem = GetComponent<HealthSystemAttribute>();
        if (healthSystem)
        {
            healthSystem.maxHealth = (int)unitData.maxHealth;
            healthSystem.health = (int) unitData.health;
        }

        ModifyHealthAttribute modifyHealthAttrb = GetComponent<ModifyHealthAttribute>();
        if (modifyHealthAttrb)
        {
            modifyHealthAttrb.healthChange = (int)-unitData.collisionDamage;
        }
    }



    //Agent's action
    public void DetectEnemy()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Agent's Action
    public void FollowTarget()
    {
        // Follow Target
        transform.position = Vector2.MoveTowards(
        transform.position,
        target.transform.position,
        unitData.speed * Time.deltaTime);
    }

    // Agent's Action
    public void ShootWeapon()
    {
        if (weaponProjectileSystem)
        {
            weaponProjectileSystem.ShootObject();
        }
    }

    public void AdjustRotationToTarget()
    {
        if (EnableFaceToTarget)
        {//look towards the target, Playground utilities
            Utils.SetAxisTowards(
                  orientationSide,
                  transform,
                   target.transform.position - transform.position);
        }
    }

}
