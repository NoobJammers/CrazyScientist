using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class CylinderHandler : MonoBehaviour
{
    [SerializeField] Transform flask, standToRise;

    [SerializeField] float targetposY;

    public bool canRise = false;

    private void Update()
    {
        if (canRise && standToRise.localPosition.y < targetposY)
        {
            flask.position = new Vector3(flask.position.x, flask.position.y - Time.deltaTime, flask.position.z);
            standToRise.position = new Vector3(standToRise.position.x, standToRise.position.y + Time.deltaTime, standToRise.position.z);
        }
    }
}
