using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 51)]
public class EnemyObject : ScriptableObject {

    [SerializeField]
    [Tooltip("Enemy Name")]
    private string enemyName;
    public string EnemyName {
        get {
            return enemyName;
        }
    }

    [SerializeField]
    [Tooltip("Enemy Health")]
    private int maxHealth;
    public int MaxHealth {
        get {
            return maxHealth;
        }
    }
    
    [SerializeField]
    [Tooltip("Enemy Damage Rate")]
    private float damageRate;
    public float DamageRate {
        get {
            return damageRate;
        }
    }
    
    [SerializeField]
    [Tooltip("Enemy Damage Amount")]
    private int damageAmount;
    public int DamageAmount {
        get {
            return damageAmount;
        }
    }
    
    [SerializeField]
    [Tooltip("Enemy Movement Speed")]
    private float moveSpeed;
    public float MoveSpeed {
        get {
            return moveSpeed;
        }
    }
    
    [SerializeField]
    [Tooltip("Prefab for the enemy")]
    private GameObject prefab;
    public GameObject GetPrefab {
        get {
            return prefab;
        }
    }

    [SerializeField]
    [Tooltip("Objects spawned when enemy dies")]
    private GameObject[] staticDrops;
    public GameObject[] StaticDrops {
        get {
            return staticDrops;
        }
    }

    [SerializeField]
    [Tooltip("Random objects spawned when enemy dies")]
    private GameObject[] randomDrops;
    public GameObject[] RandomDrops {
        get {
            return randomDrops;
        }
    }
}