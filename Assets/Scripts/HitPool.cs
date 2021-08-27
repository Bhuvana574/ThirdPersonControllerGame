using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPool : MonoBehaviour
{
    public Stack<GameObject> particleEffectPool = new Stack<GameObject>();
    public GameObject effectPrefab;
    public GameObject currentEffect;
    public static HitPool instance;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePool()
    {
        print("Pool created ");
        particleEffectPool.Push(effectPrefab);
        particleEffectPool.Peek().SetActive(false);
        particleEffectPool.Peek().tag = "ParticleEffect";
    }
    public void AddParticleEffect(GameObject effectTemp)
    {
        print("Added to pool");
        particleEffectPool.Push(effectTemp);
        particleEffectPool.Peek().SetActive(false);
    }
    public void Spawning(RaycastHit hit)
    {
        print("spwning effect");
        if (particleEffectPool.Count <= 1)
        {
            CreatePool();
        }
        GameObject temp = particleEffectPool.Pop();
        if (temp.tag == "ParticleEffect")
        {
            temp.SetActive(true);
            temp.transform.position = hit.point;
            currentEffect = temp;
        }
    }
}