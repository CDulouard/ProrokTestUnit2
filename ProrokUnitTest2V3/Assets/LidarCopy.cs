using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarCopy : MonoBehaviour
{
    public int rayRange; /*    lidar's range   */
    public int horizontalRange; /*    half of lidar's horizontal angular range    */
    public int verticalRange; /*    half of lidar's vertical angular range    */
    public float horizontalStep; /*    angular step between to horizontal ray    */
    public float verticalStep; /*    angular step between to vertical ray    */
    public float horizontalOffset;    /*    horizontal offset the lidar (in degrees)     */
    public float verticalOffset;    /*    vertical offset the lidar (in degrees)     */

    public GameObject lidar;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var measures = Scan();
        
        
    }

    private List<Point> Scan()
    {
        var measures = new List<Point>();
        var position = lidar.transform.position;
        Vector3 direction;
        RaycastHit hit;
        var lidarDirection = Quaternion.Euler(verticalOffset, horizontalOffset, 0) * lidar.transform.forward;

        for (float hAngle = -horizontalRange; hAngle <= horizontalRange; hAngle += horizontalStep)
        {
            for (float vAngle = -verticalRange; vAngle <= verticalRange; vAngle += verticalStep)
            {
                direction = Quaternion.Euler(vAngle, hAngle, 0) * lidarDirection;
                if (Physics.Raycast(position, direction, out hit, rayRange))
                {
                    Debug.DrawRay(position, direction * hit.distance, Color.red);
                }
                else
                {
                    Debug.DrawRay(position, direction * rayRange, Color.green);
                }
                measures.Add(new Point(hAngle, vAngle, hit.distance));
            }
        }
        return measures;
    }
}
/*
public class Point
{
    private float _horizontalAngle;
    private float _verticalAngle;
    private float _distance;

    public Point(float hAngle, float vAngle, float distance)
    {
        HorizontalAngle = hAngle;
        VerticalAngle = vAngle;
        Distance = distance;
    }

    public float Distance
    {
        get => _distance;
        set => _distance = value;
    }

    public float VerticalAngle
    {
        get => _verticalAngle;
        set => _verticalAngle = value;
    }

    public float HorizontalAngle
    {
        get => _horizontalAngle;
        set => _horizontalAngle = value;
    }
}

*/