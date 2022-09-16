using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //public GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    //public float cameraDistance = 11.85f;
    //public float cameraPositionYOffset = 1.65f;


    void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
        //transform.position = player.transform.position - player.transform.forward * cameraDistance;
        //transform.LookAt(player.transform.position);
        //transform.position = new Vector3(transform.position.x, transform.position.y + cameraPositionYOffset, transform.position.z);
    }

    private void HandleRotation()
    {
        var targetPosiotion = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosiotion, translateSpeed * Time.deltaTime);
    }

    private void HandleTranslation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
