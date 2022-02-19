using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BakeryManager : GameManager
{
    // Game management
    private List<string> targetCake = new List<string>();

    // Player
    [SerializeField] BakeryPlayer bp;

    // Customer
    [SerializeField] GameObject customerPreFab;
    [SerializeField] Vector3 customerSpawnPoint;

    [SerializeField] UIManager uiManager;
    [SerializeField] TicketManager ticketManager;

    [SerializeField] Timer dayTimer;
    public float cash { get; private set; }

    private CustomerControler customer;
    public int day {get; private set; }
    private bool dayInAction = false;

    float time_interval = 5.0f;
    float next_customer = 0.0f;

    private void Start()
    {
        day = 1; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (uiManager.isModalOn())
            {
                uiManager.closeModal();
                startDay();
            } else
            {
                uiManager.openDayStartModal(day);
            }
        }
        if (dayInAction)
        {
            dailyActivitiesUpdate();
        }
    }

    public void setTargetCake(List<string> target)
    {
        targetCake = target;
    }

    /*
     * Check if the targetCake is the same as the textures. If it is, call
     * onCakeSuccess. If not, call onCakeFailure.
     */
    public void onCakeSubmit(List<string> toppings)
    {
        if (targetCake.Count != toppings.Count)
        {
            onCakeFailure();
            return;
        }

        for (int i = 0; i < toppings.Count; ++i) 
        {
            if (targetCake[i] != toppings[i])
            {
                onCakeFailure();
                return;
            }
        }

        onCakeSuccess();
    }

    /*
     * Do something when the player loses.
     */
    private void onCakeFailure()
    {
        bp.setText("That is not what I wanted");
    }

    /*
     * Incremement the level and add to the score.
     */
    private void onCakeSuccess()
    {
        customer.leaveStore();
        
    }

    /*
     * Creates a new customer
     */
    public void spawnCustomer()
    {
        customer = Instantiate(customerPreFab, customerSpawnPoint, Quaternion.identity).GetComponent<CustomerControler>();
    }

    private void startDay()
    {
        uiManager.updateDayUI(day);
        dayTimer.timeUpEvent.AddListener(onDayEnd);
        dayTimer.setTimer(BalanceSheet.timePerLevel);
        dayInAction = true;
    }

    private void onDayEnd()
    {
        dayTimer.timeUpEvent.RemoveAllListeners();
        uiManager.openDayEndModal(day);
        day = day + 1;
        dayInAction = false;
    }

    private void dailyActivitiesUpdate()
    {
        if (Time.time > next_customer)
        {
            float max = (float)(21.0 - 0.5 * day);
            time_interval = Random.Range(5, max); //may need to adjust this equation
            next_customer = Time.time + time_interval;

            ticketManager.createCustomer();
        }
    }

}
