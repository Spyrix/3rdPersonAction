using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputScript))]
public class PlayerInventoryController : MonoBehaviour
{
    //PlayerInventoryGraphics graphicsScript;
    [SerializeField]
    PlayerInputScript input;

    List<GameObject> inventorySpace;
    int inventorySize;

    void Awake()
    {
        inventorySize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
