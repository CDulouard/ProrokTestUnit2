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

    public static TargetPositions ReadValues(string jsonDatas)
    {
        return JsonUtility.FromJson<TargetPositions>(jsonDatas);
    }
}