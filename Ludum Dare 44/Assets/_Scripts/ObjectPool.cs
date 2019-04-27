using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private Dictionary<GameObject, List<ObjectPoolItem>> poolDict = new Dictionary<GameObject, List<ObjectPoolItem>>();

    public ObjectPoolItem CreateItemToPool(GameObject type) {
        ObjectPoolItem thisPoolItem = new ObjectPoolItem(
            Instantiate(type),
            this
        );
        if (!poolDict.ContainsKey(type)) {
            poolDict[type] = new List<ObjectPoolItem>();
        }
        AddItemToPool(type,thisPoolItem);
        return thisPoolItem;
    }

    public void AddItemToPool(GameObject type, ObjectPoolItem poolItem) {
        poolDict[type].Add(poolItem);
    }
    public void RemoveItemFromPool(GameObject type, ObjectPoolItem poolItem) {
        poolDict[type].Remove(poolItem);
    }

    public void ClearAllPools() {
        foreach(GameObject key in poolDict.Keys) {
            ClearPool(key);
        }
    }

    public void ClearPool(GameObject type) {
        foreach(ObjectPoolItem poolItem in poolDict[type]) {
            Destroy(poolItem.itemInstance);
        }
        poolDict[type].Clear();
    }

    public GameObject GetObjectFromPool(GameObject type) {
        return GetObjectFromPool(type, Vector3.zero,Quaternion.identity, transform);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position) {
        return GetObjectFromPool(type, position,Quaternion.identity, transform);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position, Quaternion rotation) {
        return GetObjectFromPool(type, Vector3.zero,rotation, transform);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position, Quaternion rotation, Transform parent) {
        ObjectPoolItem returnItem = null;
        if (poolDict[type].Count > 0) { // We have an object in our pool to grab
            returnItem = poolDict[type][0];
        } else { // Gotta create one and return it
            returnItem = CreateItemToPool(type);
        }
        if (returnItem != null) {
            returnItem.itemInstance.transform.position = position;
            returnItem.itemInstance.transform.rotation = rotation;
            RemoveItemFromPool(type,returnItem);
            returnItem.itemInstance.SetActive(true);
        }
        return returnItem.itemInstance;
    }

}

[System.Serializable]
public class ObjectPoolItem {
    public GameObject itemInstance {get;}
    public ObjectPool itemObjectPool {get;}

    public ObjectPoolItem(GameObject instance, ObjectPool objectPool) {
        itemInstance = instance;
        itemObjectPool = objectPool;
    }
}
