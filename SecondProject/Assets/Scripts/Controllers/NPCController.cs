using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCController : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    private int NpsMask = (1 << 11);
    private PlayerStat stat;
    public bool isCheckQuest;
    public bool isCheckSuccess;

    private float m_DoubleClickSecond = 0.25f;
    private bool m_isOneClick = false;
    private double m_Timer = 0;

    [SerializeField]
    private GameObject Storebase;
    [SerializeField]
    private GameObject Quest;
    [SerializeField]
    private GameObject QuestSuccess;
    [SerializeField]
    private GameObject QuestState;

    private void Start()
    {
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    }

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
                    // 더블클릭 한 부분
                    if (QuestState.GetComponent<Quest>().GetQuestFinish() && !isCheckSuccess)
                    {
                        QuestSuccess.SetActive(true);
                        QuestState.SetActive(false);
                        isCheckSuccess = true;
                    }
                    else if (stat.Level >= 2 && !isCheckQuest)
                    {
                        Quest.SetActive(true);
                        isCheckQuest = true;
                    }
                    else
                        Storebase.SetActive(true);
                    m_isOneClick = false;
                }
            }
        }
    }
}
