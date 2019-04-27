using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolReference : MonoBehaviour {

    public ObjectPool objectPool;
    public GameObject objectType;

    public void returnToPool() {
        objectPool.AddItemToPool(objectType, new ObjectPoolItem(gameObject));
        gameObject.SetActive(false);
    }

    public ObjectPoolReference(GameObject object_type, ObjectPool object_pool) {
        objectType = object_type;
        objectPool = object_pool;
    }

}
