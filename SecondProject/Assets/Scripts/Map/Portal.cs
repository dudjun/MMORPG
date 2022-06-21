using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private GameObject _otherPortal;

    public bool isFirstPortal = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isFirstPortal)
        {
            _otherPortal.GetComponent<Portal>().isFirstPortal = false;
            other.transform.position = _otherPortal.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isFirstPortal = true;
    }
}
