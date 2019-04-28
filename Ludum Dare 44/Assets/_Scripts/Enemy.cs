using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class Enemy : MonoBehaviour {
    [SerializeField]
    private EnemyObject enemy;

    private int curHealth;

    private bool isAlive {
        get {
            return curHealth > 0;
        }
    }

    public UnityEvent onEnemyDeath;

    private Transform target;
    private NavMeshAgent nav;

    private float damageTimer;

    void Start() {
        curHealth = enemy.MaxHealth;
        nav = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerMain>().transform;
    }
    
    void Update() {
        FollowPlayer();
        DamageTimer();
    }

    void FollowPlayer() {
        if (nav != null) {
            nav.SetDestination(target.position);
        } else  {
            Debug.LogError( gameObject.name + "does not have NavMeshAgent attached");
        }
    }

    public void ChangeHealth(int amount) {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth,0,enemy.MaxHealth);
        if (!isAlive) {
            Die();
        }
    }

    public void Die() {
        onEnemyDeath.Invoke();
    }

    public void RandomDrop() {
        int randIndex = Random.Range(0,enemy.RandomDrops.Length);
        Instantiate(enemy.RandomDrops[randIndex],transform.position,Quaternion.identity);
    }

    public void StaticDrops() {
        foreach(GameObject obj in enemy.StaticDrops) {
            Instantiate(obj,transform.position,Quaternion.identity);
        }
    }

    void OnTriggerEnter(Collider col) {
        Coin coin = col.GetComponent<Coin>();
        if (coin != null) {
            ChangeHealth(coin.CoinDamage);
            ObjectPoolReference opf = col.GetComponent<ObjectPoolReference>();
            if (opf != null) {
                opf.returnToPool();
            } else {
                Destroy(coin.gameObject);
            }
        }
    }

    void OnTriggerStay(Collider col) {
        PlayerMain player = col.GetComponent<PlayerMain>();
        if (player != null) {
            if (damageTimer <= 0) {
                DamagePlayer();
            }
        }
    }

    void DamageTimer() {
        if (damageTimer > 0) {
            damageTimer -= Time.deltaTime;
            damageTimer = Mathf.Clamp(damageTimer,0,damageTimer);
        }
    }

    void ResetDamageTimer() {
        damageTimer = enemy.DamageRate;
    }

    void DamagePlayer() {
        PlayerCurrency playerCurrency = FindObjectOfType<PlayerCurrency>();
        if (playerCurrency != null) {
            playerCurrency.ChangeCurrency(-enemy.DamageAmount);
            ResetDamageTimer();
        }

    }
}