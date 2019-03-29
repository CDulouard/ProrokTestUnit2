using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public string robotDatasFilePath;
    public static string status;

    private RobotDatas _datas; /*    Contain all the datas to put in the Json file.    */
    private static TargetPositions _targetPositions;

    private void Start()
    {
        _targetPositions = new TargetPositions();
        _datas = new RobotDatas();
    }

    private void Update()
    {
        _datas.RefreshDatas(Controller.GetMotorsDatas(), Lidar.GetMeasures(), Controller.GetSensorValues());
        /*    Read the datas from the Json file*/
        _targetPositions.ReadValues(Server.targetPositions);
        /*    Write the datas in the Json file    */
        status = _datas.ToJson();
    }

    public static TargetPositions TargetPositions
    {
        get => _targetPositions;
        set => _targetPositions = value;
    }
}