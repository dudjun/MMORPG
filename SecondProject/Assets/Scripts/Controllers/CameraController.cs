using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 _delta = new Vector3(0.0f, 10.0f, -10.0f);

    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }
    void LateUpdate()
    {
        if (_player.IsValid() == false)
            return;
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
        {
            float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
            transform.position = _player.transform.position + _delta.normalized * dist;
        }
        else
        {
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }

        CameraZoom();
    }

    private void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * 10.0f;
        float dist = (_player.transform.position - transform.position).magnitude;
        if (scroll > 0f && dist < 5f) return;
        if (scroll < 0f && dist > 20f) return;
        _delta -= _delta.normalized * scroll;
    }
}
