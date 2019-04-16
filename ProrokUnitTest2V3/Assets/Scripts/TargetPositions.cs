using UnityEngine;
using System.IO;


public class TargetPositions
{
    /*    Can read the target values in the json file and stores the values    */
    public float legFrontLeftBot;
    public float legFrontLeftTop;
    public float shoulderFrontLeft;
    public float legFrontRightBot;
    public float legFrontRightTop;
    public float shoulderFrontRight;
    public float legBackLeftBot;
    public float legBackLeftTop;
    public float shoulderBackLeft;
    public float legBackRightBot;
    public float legBackRightTop;
    public float shoulderBackRight;

    /*    {'legFrontLeftBot': 10.5, 'legFrontLeftTop': 10.5, 'shoulderFrontLeft': 10.5, 'legFrontRightBot': 10.5, 'legFrontRightTop': 10.5,
     'shoulderFrontRight': 10.5, 'legBackLeftBot': 10.5, 'legBackLeftTop': 10.5, 'shoulderBackLeft': 10.5, 'legBackRightBot': 10.5, 'legBackRightTop': 10.5, 'shoulderBackRight': 10.5}*/

    public static TargetPositions ReadValues(string jsonDatas)
    {
        return JsonUtility.FromJson<TargetPositions>(jsonDatas);
    }
}