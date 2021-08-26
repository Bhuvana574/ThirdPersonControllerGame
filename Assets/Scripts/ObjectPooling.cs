using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
   
    public Stack<GameObject> particlePool = new Stack<GameObject>();
    public GameObject effectPrefab;
    public GameObject currentEffect;
    public static ObjectPooling instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePool()
    {
        print("Pool created");
        particlePool.Push(effectPrefab);
        particlePool.Peek().SetActive(false);
        particlePool.Peek().tag = "Hitm";
    }
    public void AddParticleEffect(GameObject effectTemp)
    {
        print("Added to pool");
        particlePool.Push(effectTemp);
        particlePool.Peek().SetActive(false);
    }
    public void Spawning(RaycastHit hit)
    {
        print("spwning effect");
        if (particlePool.Count <= 1)
        {
            CreatePool();
        }
        GameObject temp = particlePool.Pop();
        if (temp.tag == "Hitm")
        {
            temp.SetActive(true);
            temp.transform.position = hit.point;
            currentEffect = temp;
        }
    }
}
