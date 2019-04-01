using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuCamera : MonoBehaviour
{
    public float speed;
    public float distance;
    public Transform target;
    public GameObject cameraObject;

    private Transform _cameraTransform;

    private float _currentX;
    private float _currentY;


    private void Start()
    {
        _cameraTransform = cameraObject.transform;
        _currentY = 20;
    }

    private void LateUpdate()
    {
        var position = target.position;
        var direction = new Vector3(0, 0, -distance);
        var rotation = Quaternion.Euler(_currentY, _currentX, 0);
        _cameraTransform.position = position + rotation * direction;
        _cameraTransform.LookAt(position);
    }

    private void Update()
    {
        _currentX += speed * Time.deltaTime;        
    }
}