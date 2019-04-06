using System.IO;
using UnityEngine;

public class Settings
{
    /*    Video Settings    */
    public float cameraViewDistance;
    public float xSensivity;
    public float ySensivity;
    public float scrollSensivity;
    public float cameraDefaultDistance;
    
    /*    Robot Settings    */
    public float legBotSpeed;
    public float legTopSpeed;
    public float shoulderSpeed;
    public float legBotTorque;
    public float legTopTorque;
    public float shoulderTorque;
    public float legBotAngleMin;
    public float legTopAngleMin;
    public float shoulderAngleMin;
    public float legBotAngleMax;
    public float legTopAngleMax;
    public float shoulderAngleMax;
    
    /*    Lidar Settings    */

    public int rayRange;
    public int horizontalRange;
    public int verticalRange;
    public float horizontalStep;
    public float verticalStep;
    public float horizontalOffset;
    public float verticalOffset;
    
    /*    Server Settings    */
    public int portDefault;

    public void SaveSettings(string path)
    {
        /*    Save current settings to the specified Json file    */
        File.WriteAllText(path, ToJson());
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    
    public static Settings FromJson(string path)
    {
        return JsonUtility.FromJson<Settings>(File.ReadAllText(path));
    }
}
