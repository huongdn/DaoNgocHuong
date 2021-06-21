using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefabs;
        //public int size;
    }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static ObjectPooler m_sInstance;
    private void Awake()
    {
        _MakeInstance();
    }

    private void _MakeInstance()
    {
        if (m_sInstance == null)
        {
            m_sInstance = this;
            //DontDestroyOnLoad(this);
        }
        else if (m_sInstance != null)
        {
            Destroy(this);
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPools = new Queue<GameObject>();

            //for( int i = 0; i < pool.size; i++)
            //{
            //    GameObject gameObject = Instantiate(pool.prefabs);
            //    gameObject.SetActive(false);
            //    objectPools.Enqueue(gameObject);
            //}

            poolDictionary.Add(pool.tag, objectPools);
        }
    }

    public GameObject _SpawnFromPool (string tag/*, Vector3 position, Quaternion rotation*/)
    {

        if (!_IsValidTag(tag))
        {
            Debug.LogWarning("_SpawnFromPool not valid tag: " + tag);
            return null;
        }
        GameObject objectToSpawn = null;

        if (poolDictionary[tag].Count > 0)
        {
            objectToSpawn =  poolDictionary[tag].Dequeue();
            
            //poolDictionary[tag].Enqueue(objectToSpawn);
        }
        else
        {
            for(int i = 0; i < pools.Count; i++)
            {
                if (pools[i].tag == tag)
                {
                    objectToSpawn = Instantiate(pools[i].prefabs);
                }
            }
        }

        objectToSpawn.SetActive(true);
        //objectToSpawn.transform.position = position;
        //objectToSpawn.transform.rotation = rotation;
        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if(pooledObject!=null)
        {
            pooledObject._OnObjectSpawn();
        }

        return objectToSpawn;
    }

    public void _ReturnObjectToPool(GameObject gameObject, string tag)
    {
        if(gameObject != null)
        {
            gameObject.SetActive(false);

            IPooledObject returnedObject = gameObject.GetComponent<IPooledObject>();
            if (returnedObject != null)
            {
                returnedObject._OnObjectReturn();
            }

            if(_IsValidTag(tag))
            {
                poolDictionary[tag].Enqueue(gameObject);
            }
        }
    }

    private bool _IsValidTag(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with Tag " + tag + " doesn't exist !!");
            return false;
        }
        return true;
    }
}
