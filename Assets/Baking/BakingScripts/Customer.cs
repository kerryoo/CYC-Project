using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Animator customerAnimator;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] float targetPositionTolerance = 1f;
    [SerializeField] float targetRotationTolerance = 0.1f;

    [SerializeField] GameObject[] angryFxs;
    [SerializeField] GameObject[] disgustedFxs;
    [SerializeField] GameObject[] satisfiedFxs;
    [SerializeField] GameObject[] greatFxs;
    [SerializeField] GameObject[] perfectFxs;


    public bool acting { get; private set; }
    private int ticketID = -1;

    public OrderPlacedEvent orderPlacedEvent;
    public CustomerOrderingEvent customerOrderingEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("pick up already");
            customerAnimator.SetBool("Pickup", true);
            customerAnimator.SetFloat("MoveSpeed", 1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            customerAnimator.SetBool("SitDown", true);
        }
    }

    public void initializeCustomer(int ticketID)
    {
        this.ticketID = ticketID;
    }

    public void moveInLine(Vector3 lineLocation)
    {
        StartCoroutine(goToLinePositionRoutine(lineLocation));
    }

    public void startOrder(Vector3 registerLocation, Vector3 registerLookLocation)
    {
        acting = true;
        StartCoroutine(goToRegisterRoutine(registerLocation, registerLookLocation));
    }

    public void goWait(Vector3 waitingLocation)
    {
        StartCoroutine(goToWaitingRoutine(waitingLocation));
    }

    public void onOrderCompleted(int reactionId, Vector3 pickUpLocation)
    {
        StartCoroutine(pickUpRoutine(reactionId, pickUpLocation));
    }

    IEnumerator goToLinePositionRoutine(Vector3 lineLocation)
    {
        yield return StartCoroutine(goToLocation(lineLocation));
    }

    IEnumerator goToRegisterRoutine(Vector3 registerLocation, Vector3 registerLookLocation)
    {
        yield return StartCoroutine(goToLocation(registerLocation));
        yield return StartCoroutine(goToRotation(registerLookLocation));

        StartCoroutine(placeOrderRoutine());
    }

    IEnumerator placeOrderRoutine()
    {
        customerOrderingEvent.Invoke(ticketID);
        customerAnimator.SetBool("Conversation", true);
        yield return new WaitForSeconds(10f);
        if (orderPlacedEvent != null)
        {
            orderPlacedEvent.Invoke(ticketID);
        } else
        {
            Debug.Log("No order event!");
        }
    }

    IEnumerator goToWaitingRoutine(Vector3 waitingLocation)
    {
        yield return StartCoroutine(goToLocation(waitingLocation));
    }

    IEnumerator pickUpRoutine(int reactionId, Vector3 pickUpLocation)
    {
        yield return StartCoroutine(goToLocation(pickUpLocation));

        if (reactionId == (int)ID.ReactionID.Disgusted)
        {

        } else if (reactionId == (int)ID.ReactionID.Satisfied)
        {

        } else if (reactionId == (int)ID.ReactionID.Great)
        {

        } else if (reactionId == (int)ID.ReactionID.Perfect)
        {

        } else
        {

        }

        customerAnimator.SetBool("Dance", true);
    }

    IEnumerator goToLocation(Vector3 targetPos)
    {
        bool inPosition = false;

        while (!inPosition)
        {
            navMeshAgent.destination = targetPos;
            customerAnimator.SetFloat("MoveSpeed", 1f);

            if (Vector3.Distance(targetPos, transform.position) <= targetPositionTolerance)
            {
                inPosition = true;
            }
            yield return null;
        }
        customerAnimator.SetFloat("MoveSpeed", 0);
    }

    IEnumerator goToRotation(Vector3 lookLocation)
    {
        bool inRotation = false;
        Vector3 relativePos = lookLocation - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(relativePos, Vector3.up);
        float timeCount = 0;

        while (!inRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, timeCount);
            timeCount = timeCount + Time.deltaTime;
            if (Quaternion.Angle(transform.rotation, targetRot) <= targetRotationTolerance)
            {
                inRotation = true;
            }
            yield return null;
        }
    }



    

    public void onTicketTimeOut()
    {

    }
}
