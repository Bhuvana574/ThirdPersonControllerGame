using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //ontrigger stay if its player can collect coin and destroy
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if(player!=null)
                {
                  
                    Debug.Log("coin collected");
                    UIManager uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                    if(uIManager!=null)
                    {
                        uIManager.CollectedCoins();
                    }
                    Destroy(this.gameObject);
                    player.hasCoin = true;
                }
               
            }
        }
    }

}
