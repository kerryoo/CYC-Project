using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionPad : MonoBehaviour
{
    [SerializeField] BakeryManager bm;

    public void Start()
    {
    }

    /*
     * When an object touches the submission pad, check if it contains the Pastry
     * script. If it does, call bakeryManager's onPastrySubmit with the pastry's
     * list fo decorations.
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pastry")
        {
            Pastry pastry = collision.gameObject.GetComponent<Pastry>();
            bm.onPastrySubmit(pastry.decorations);
        }
    }
}
