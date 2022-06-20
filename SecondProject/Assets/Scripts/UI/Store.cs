using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private GameObject Go_StoreBase;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void QuitButton()
    {
        Go_StoreBase.SetActive(false);
    }
}
