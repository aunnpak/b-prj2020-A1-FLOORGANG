using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourA : MonoBehaviour
{
    // Helper function to let agent know that given transform is in aggro range or not
    bool IsTargetInAggroRange(Transform theTarget)
    {
        return Vector2.Distance(
        transform.position,
        theTarget.position
        ) <= minAggroDistance;
    }

    public UnitData unitData;

    // We need rigidBody2D to make a movement

    public Rigidbody2D rigidbody2d;
    
    // We need Collider2D to allow collision detection
    
    public Collider2D collider2d;

    public GameObject target;

    // Start is called before the first frame update

    // the option to let agent be active if target is in range
    
    public float minAggroDistance = 10.0f;
    
    public bool EnableAggroByDistance;


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
            if (EnableAggroByDistance)
            {
                if (IsTargetInAggroRange(target.transform))
                {
                    FollowTarget();
                }
            }
            else
            {
                FollowTarget();
            }

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
    void FollowTarget()
    {
        // Follow Target
        transform.position = Vector2.MoveTowards(
        transform.position,
        target.transform.position,
        unitData.speed * Time.deltaTime);
    }

    // Debug Gizmo on selected gameobject
    private void OnDrawGizmosSelected()
    { // Aggro Distance
        if (EnableAggroByDistance)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minAggroDistance);
        }
    }

}
