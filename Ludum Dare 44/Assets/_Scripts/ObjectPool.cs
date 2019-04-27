using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private Dictionary<GameObject, List<ObjectPoolItem>> poolDict = new Dictionary<GameObject, List<ObjectPoolItem>>();

    public ObjectPoolItem CreateItemToPool(GameObject type) {
        ObjectPoolItem thisPoolItem = new ObjectPoolItem(Instantiate(type));
        ObjectPoolReference thisPoolReference = thisPoolItem.itemInstance.GetComponent<ObjectPoolReference>();
        if (thisPoolReference != null) {
            thisPoolReference.objectPool = this;
            thisPoolReference.objectType = type;
        } 
        
        AddItemToPool(type,thisPoolItem);
        return thisPoolItem;
    }

    public void CreatePool(GameObject type) {
        if (!PoolExists(type)) {
            poolDict[type] = new List<ObjectPoolItem>();
        }
    }

    public bool PoolExists(GameObject type) {
        return poolDict.ContainsKey(type);
    }

    public void AddItemToPool(GameObject type, ObjectPoolItem poolItem) {
        if (!PoolExists(type)) {
            poolDict[type] = new List<ObjectPoolItem>();
        }
        poolDict[type].Add(poolItem);
    }

    public void RemoveItemFromPool(GameObject type, ObjectPoolItem poolItem) {
        if (PoolExists(type)) {
            poolDict[type].Remove(poolItem);   
        }
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
        return GetObjectFromPool(type, position, rotation, transform);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position, Quaternion rotation, Transform parent) {
        ObjectPoolItem returnItem = null;
        if (PoolExists(type)) {
            if (poolDict[type].Count > 0) {
                returnItem = poolDict[type][0];
            } else {
                returnItem = CreateItemToPool(type);
            }
        } else {
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

    public ObjectPoolItem(GameObject instance) {
        itemInstance = instance;
    }
}
