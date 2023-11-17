using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorTrigger : MonoBehaviour
{
    public SingleSideDoor door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (door.isDoorOpen == false)
            {
                door.isDoorOpen = true;
                door.DoOpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (door.isDoorOpen == true)
            {
                door.isDoorOpen = false;
                door.DoCloseDoor();
            }
        }
    }
}
