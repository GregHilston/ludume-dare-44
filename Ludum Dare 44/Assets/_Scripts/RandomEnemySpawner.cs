using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour {
    [SerializeField]
    private GameObject[] possibleEnemiesToSpawn;
    [SerializeField]
    private float minimumSecondsToWait = 1.0f;
    [SerializeField]
    private float maximumSecondsToWait = 5.0f;

    void Start() {
        StartCoroutine(this.spawnRandomEnemyAtRandomChildTransform());
    }

    IEnumerator spawnRandomEnemyAtRandomChildTransform() {
        while (true) {
            GameObject randomEnemy = this.getRandomEnemy();
            Vector3 randomLocation = this.getRandomChildPosition();

            if (randomEnemy != null) {
                Debug.Log($"Spawned random enemy {randomEnemy.name} at position {randomLocation}");
                Instantiate(randomEnemy, randomLocation, Quaternion.identity);
            }

            float secondsToWait = Random.Range(this.minimumSecondsToWait, this.maximumSecondsToWait);

            Debug.Log($"Waiting {secondsToWait} seconds till we spawn another enemy");
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    GameObject getRandomEnemy() {
        System.Random rnd = new System.Random();
        int randomIndex = rnd.Next(0, this.possibleEnemiesToSpawn.Length);

        if (randomIndex < this.possibleEnemiesToSpawn.Length) {
            return this.possibleEnemiesToSpawn[randomIndex];
        } else {
            Debug.LogError($"Attempted to get enemy at index {randomIndex} from array possibleEnemiesToSpawn, but this array only has {this.possibleEnemiesToSpawn.Length} elements");
        }

        return null;
    }

    Vector3 getRandomChildPosition() {
        System.Random rnd = new System.Random();
        int randomIndex = rnd.Next(0, this.transform.childCount);

        if (randomIndex < this.transform.childCount) {
            return this.transform.GetChild(randomIndex).position;
        } else {
            Debug.LogError($"Attempted to get child at index {randomIndex} from array this.transform.GetChild, but this array only has {this.possibleEnemiesToSpawn.Length} elements");
        }

        return new Vector3(0, 0, 10); // starting location
    }
}
