using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private float speed = 0.05f;
    private Transform staffPos;
    private GameObject player;
    private PlayerController playerCont;
    private Vector3 dir;

    private void Start()
    {
        player = Managers.Game.GetPlayer();
        playerCont = player.GetComponent<PlayerController>();
        staffPos = GameObject.Find("StaffPos").transform;
        transform.position = staffPos.position;
        dir = playerCont.destPos - player.transform.position;
        transform.rotation = Quaternion.LookRotation(dir);
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Building")
            Destroy(gameObject);
        if (other.tag == "Enemy")
        {
            Stat targetStat = other.GetComponent<Stat>();
            Stat PlayerStat = player.GetComponent<Stat>();
            if (targetStat == null)
                targetStat = other.GetComponentInParent<Stat>();
            if (targetStat != null)
                targetStat.OnFired(50, PlayerStat);
        }
    }
}
