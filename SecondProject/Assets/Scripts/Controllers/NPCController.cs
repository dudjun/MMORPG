using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCController : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private int NpsMask = (1 << 11);

    private float m_DoubleClickSecond = 0.25f;
    private bool m_isOneClick = false;
    private double m_Timer = 0;

    [SerializeField]
    private GameObject Storebase;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (m_isOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
            m_isOneClick = false;

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, NpsMask))
            {
                if (!m_isOneClick)
                {
                    m_Timer = Time.time;
                    m_isOneClick = true;
                }
                else if (m_isOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    Storebase.SetActive(true);
                    m_isOneClick = false;
                }
            }
        }
    }
}
