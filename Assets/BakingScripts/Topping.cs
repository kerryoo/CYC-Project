using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base class for all decorations that can be added to a pastry. The idea will
 * be that every decoration will have a unique way to attach to a pastry and
 * will have a unique tag used for testing a submission.
 */
public class Topping : MonoBehaviour
{
    [SerializeField] private new string name;

    public string getName()
    {
        return name;
    }

    public void attachToPastry(GameObject pastry)
    {
        transform.parent = pastry.transform;
    }
}
