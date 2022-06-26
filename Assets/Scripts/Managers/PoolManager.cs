using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, List<GameObject>> objectsPool = new Dictionary<string, List<GameObject>>();

    public T GetObject<T>(string path) where T : MonoBehaviour
    {
        if (!objectsPool.ContainsKey(path))
        {
            objectsPool.Add(path, new List<GameObject>());
        }

        if (objectsPool[path].Count == 0)
        {
            AddObject(path);
        }

        return AllocateObject(path).GetComponent<T>();
    }

    public void ReleaseObject(string path, GameObject prefab)
    {
        prefab.gameObject.SetActive(false);

        if (!objectsPool.ContainsKey(path))
        {
            objectsPool.Add(path, new List<GameObject>());
        }
        prefab.transform.SetParent(this.transform);
        objectsPool[path].Add(prefab);
    }

    private void AddObject(string path)
    {
        GameObject instance = Instantiate(ResourcesManager.Instance.GetGameObject(path), transform);
        instance.gameObject.SetActive(false);
        instance.transform.position = this.transform.position;
        objectsPool[path].Add(instance);
    }

    private GameObject AllocateObject(string path)
    {
        GameObject objectPool = objectsPool[path][0];
        objectsPool[path].RemoveAt(0);
        objectPool.gameObject.SetActive(true);
        return objectPool;
    }
}
