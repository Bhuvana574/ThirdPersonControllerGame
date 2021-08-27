using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShop : MonoBehaviour
{
    //if collision is with  a player and have a coin inventory needs to be update
    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
                {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if (player != null)
                {
                  if(player.hasCoin==true)
                    {
                        player.hasCoin = true;
                        UIManager uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                        if(uIManager!=null)
                        {
                            uIManager.RemovedCoins();
                        }
                        player.EnableWeapon();
                    }
                    else
                    {
                        Debug.Log("please go and collect coins");
                    }
                }
            }
            }
        }
    }

