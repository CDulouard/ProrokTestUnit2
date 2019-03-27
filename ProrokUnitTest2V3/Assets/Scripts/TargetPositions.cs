using UnityEngine;
using System.IO;


public struct TargetPositions
{
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

    public void ReadValues(string path)
    {
        this = JsonUtility.FromJson<TargetPositions>(File.ReadAllText(path));
    }

}