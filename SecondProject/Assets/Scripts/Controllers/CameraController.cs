using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 _delta = new Vector3(0.0f, 10.0f, -10.0f);
    List<GameObject> TransparentBuildings = new List<GameObject>();

    Material TransparentMat;
    Material DefaltMat;

    GameObject _player = null;

    public void SetPlayer(GameObject player) { _player = player; }

    void Start()
    {
        TransparentMat = (Material)Resources.Load("Town/Materials/Building_Texture_Transparent");
        DefaltMat = (Material)Resources.Load("Town/Materials/Building_Texture");
    }

    void LateUpdate()
    {
        if (_player.IsValid() == false)
            return;
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, 1 << (int)Define.Layer.Block))
        {
            hit.collider.GetComponent<MeshRenderer>().material = TransparentMat;
            TransparentBuildings.Add(hit.collider.gameObject);
        }
        else
        {
            foreach(GameObject Building in TransparentBuildings)
            {
                Building.GetComponent<MeshRenderer>().material = DefaltMat;
            }
        }

        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform);

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
