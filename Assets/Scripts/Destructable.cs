using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject crateDestroy;

    public void OnCrateDestroyed()
    {
        Instantiate(crateDestroy, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
