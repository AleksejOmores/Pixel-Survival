using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        CameraPlayerPosition();
    }

    void CameraPlayerPosition()
    {
        Vector3 playerPosition = player.position;

        playerPosition.x = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);

        Vector3 smootherPosition = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothTime);
        smootherPosition.z = transform.position.z;

        transform.position = smootherPosition;
    }
}
