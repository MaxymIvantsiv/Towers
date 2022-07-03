using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool Free = true;

    public void SetFree(bool free)
    {
        Free = free;
        Debug.Log(Free);
    }

    private void OnTriggerEnter(Collider other)
    {
        var tower = other.gameObject.GetComponent<Tower>();

        if (tower != null)
        {
            Free = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tower = other.gameObject.GetComponent<Tower>();

        if (tower != null)
        {
            Free = true;
        }
    }
}
