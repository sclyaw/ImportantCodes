using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;

    }


    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

   
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMovDir = (cameraFollowPosition - transform.position).normalized;

        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2f;

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMovDir  * distance* cameraMoveSpeed* Time.deltaTime ;
            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if(distanceAfterMoving > distance)
            {
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }
}
