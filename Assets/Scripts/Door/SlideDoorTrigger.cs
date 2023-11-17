using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorTrigger : MonoBehaviour
{
    public SlideDoor SlideDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SlideDoor.isDoorOpen == false)
            {
                SlideDoor.isDoorOpen = true;
                SlideDoor.DoOpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (SlideDoor.isDoorOpen == true)
            {
                SlideDoor.isDoorOpen = false;
                SlideDoor.DoCloseDoor();
            }
        }
    }
}
