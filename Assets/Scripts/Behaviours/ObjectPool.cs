using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField] private GameObject prefab;
  [SerializeField] private int initialPoolSize = 10;

  private List<GameObject> pool;

  private void Start()
  {
    InitializePool();
  }

  private void InitializePool()
  {
    pool = new List<GameObject>();

    for (int i = 0; i < initialPoolSize; i++)
    {
      GameObject obj = InstantiateObject();
      obj.SetActive(false);
      pool.Add(obj);
    }
  }

  private GameObject InstantiateObject()
  {
    GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    return obj;
  }

  public GameObject GetObject()
  {
    foreach (GameObject obj in pool)
    {
      if (!obj.activeInHierarchy)
      {
        obj.SetActive(true);
        return obj;
      }
    }

    // If no inactive object is found, create a new one
    GameObject newObj = InstantiateObject();
    newObj.SetActive(true);
    pool.Add(newObj);

    return newObj;
  }

  public void ReturnObject(GameObject obj)
  {
    obj.SetActive(false);
  }
}
