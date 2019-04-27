using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    [SerializeField]
    private List<ObjectPoolItem> poolList = new List<ObjectPoolItem>();
    [SerializeField]
    private int maxItemsInPool = 10;
    [SerializeField]

    public void AddItemToPool(ObjectPoolItem poolItem) {
        if (poolList.Count < maxItemsInPool) {
            poolList.Add(poolItem);
        }
    }

    public void ClearPool() {
        poolList.Clear();
    }

    public void MoveItemToEnd(ObjectPoolItem poolItem) {
        poolList.Remove(poolItem);
        poolList.Add(poolItem);
    }

    public bool AtCapacity() {
        return poolList.Count == maxItemsInPool;
    }

    public GameObject GetObjectFromPool(GameObject type, bool mustBeState = false, bool state = false) {
        return GetObjectFromPool(type, Vector3.zero,Quaternion.identity,mustBeState,state);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position, bool mustBeState = false, bool state = false) {
        return GetObjectFromPool(type, position,Quaternion.identity,mustBeState,state);
    }

    public GameObject GetObjectFromPool(GameObject type, Quaternion rotation, bool mustBeState = false, bool state = false) {
        return GetObjectFromPool(type, Vector3.zero,rotation,mustBeState,state);
    }

    public GameObject GetObjectFromPool(GameObject type, Vector3 position, Quaternion rotation, bool mustBeState = false, bool state = false) {
        GameObject returnItem = null;
        if (!AtCapacity()) {
            returnItem = Instantiate(type,position,rotation,transform);
            AddItemToPool(new ObjectPoolItem(returnItem,type));
        } else {
            foreach(ObjectPoolItem poolItem in poolList) {
                if (poolItem.itemSource == type) {
                    if (mustBeState) {
                        if (poolItem.itemInstance.activeInHierarchy == state) {
                            returnItem = poolItem.itemInstance;
                            MoveItemToEnd(poolItem);
                            break;
                        }
                    } else {
                        returnItem = poolItem.itemInstance;
                        MoveItemToEnd(poolItem);
                        break;
                    }
                }
            }
        }
        if (returnItem != null) {
            returnItem.transform.position = position;
            returnItem.transform.rotation = rotation;
            returnItem.SetActive(true);
            ObjectLifetime returnItemLifetime = returnItem.GetComponent<ObjectLifetime>();
            if (returnItemLifetime != null) {
                returnItemLifetime.ResetTimer();
            }
        }
        return returnItem;
    }

}

[System.Serializable]
public class ObjectPoolItem {
    public GameObject itemInstance {get;}
    public GameObject itemSource {get;}

    public ObjectPoolItem(GameObject instance, GameObject source) {
        itemInstance = instance;
        itemSource = source;
    }
}
