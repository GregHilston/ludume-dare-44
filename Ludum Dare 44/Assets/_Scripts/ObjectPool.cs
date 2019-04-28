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
        GameObject returnItem = null;

        // we've never seen an item of this type before, so we'll write it back in our pool and create one now
        if (!PoolExists(type)) {
            returnItem = CreateItemToPool(type).itemInstance;
        } else {
            for (int i = 0; i < poolDict[type].Count; i++) {
                GameObject potentiallyNotActiveItemPooled = poolDict[type][i].itemInstance;

                if (!potentiallyNotActiveItemPooled.activeInHierarchy) {
                    returnItem = potentiallyNotActiveItemPooled;
                    break; // found a non active item, no need to make another
                }
            }

            // if we didn't find an already existing item that is not active, we'll create one
            if (returnItem == null) {
                returnItem = CreateItemToPool(type).itemInstance;
            }
        }

        if (returnItem != null) {
            returnItem.transform.position = position;
            returnItem.transform.rotation = rotation;
            returnItem.SetActive(true);
        }
        return returnItem;
    }

}

[System.Serializable]
public class ObjectPoolItem {
    public GameObject itemInstance {get;}

    public ObjectPoolItem(GameObject instance) {
        itemInstance = instance;
    }
}
