using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ActiveFalse());
    }

    IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
