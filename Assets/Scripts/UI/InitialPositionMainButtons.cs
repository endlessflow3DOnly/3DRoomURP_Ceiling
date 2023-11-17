using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPositionMainButtons : MonoBehaviour
{
    public Vector2 initialPosition;

    private void Start()
    {
        initialPosition = this.transform.position;
    }
}
