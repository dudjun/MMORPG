using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private string _zoneName;
    [SerializeField]
    private GameObject _ui;
    [SerializeField]
    private Text _txt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _txt.text = _zoneName;
            _ui.SetActive(true);
            StartCoroutine(WaitAndInactive());
        }
    }

    IEnumerator WaitAndInactive()
    {
        yield return new WaitForSeconds(3f);
        _ui.SetActive(false);
    }
}
