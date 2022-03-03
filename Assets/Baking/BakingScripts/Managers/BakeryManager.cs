using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BakeryManager : GameManager
{
    // Customer
    [SerializeField] GameObject customerPreFab;
    [SerializeField] Vector3 customerSpawnPoint;

    [SerializeField] UIManager uiManager;
    [SerializeField] TicketManager ticketManager;

    [SerializeField] Timer dayTimer;
    public float cash { get; private set; }

    public int day {get; private set; }
    private bool dayInAction = false;
    
    
    private void Start()
    {
        //spawnCustomer();
        day = 1;
        Cursor.lockState = CursorLockMode.Locked;
        startDay();

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P) || OVRInput.GetDown(OVRInput.Button.Two))
        //{
        //    if (uiManager.isModalOn())
        //    {
        //        uiManager.closeModal();
        //        startDay();
        //    } else
        //    {
        //        uiManager.openDayStartModal(day);
        //    }
        //}

        if (dayInAction)
        {
            dailyActivitiesUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void startDay()
    {
        dayTimer.timeUpEvent.AddListener(onDayEnd);
        dayTimer.setTimer(BalanceSheet.timePerLevel);
        dayInAction = true;
    }

    private void onDayEnd()
    {
        dayTimer.timeUpEvent.RemoveAllListeners();
        uiManager.openDayEndModal(day);
        dayInAction = false;
    }

    private void dailyActivitiesUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y) || OVRInput.GetDown(OVRInput.Button.One))
        {
            ticketManager.createCustomer();
        }

    }

}
