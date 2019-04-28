using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Enemy : MonoBehaviour {
    [SerializeField]
    private EnemyObject enemy;

    private int curHealth;

    private bool isAlive = true;

    public UnityEvent onEnemyDeath;

    void Start() {
        curHealth = enemy.MaxHealth;
    }

    public void ChangeHealth(int amount) {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth,0,enemy.MaxHealth);
        if (curHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        isAlive = false;
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
            ChangeHealth(-1);
            ObjectPoolReference opf = col.GetComponent<ObjectPoolReference>();
            if (opf != null) {
                opf.returnToPool();
            }
        }
    }
}