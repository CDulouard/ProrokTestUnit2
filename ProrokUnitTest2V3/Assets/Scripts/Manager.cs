using UnityEngine;
using System.IO;
using UnityEngine.Serialization;

public class Manager : MonoBehaviour
{
    public string robotDatasFilePath;
    public string targetPositionsFilePath;

    private RobotDatas _datas;    /*    Contain all the datas to put in the Json file.    */
    private static TargetPositions _targetPositions; 
    
    private string _pathRobotDatas; /*    the path to Robot Datas Json file.    */
    private string _pathTargetPositions;
    private string _jsonString;
    private void Start()
    {
        _targetPositions = new TargetPositions();
        _datas = new RobotDatas();
        /*    path initialization    */
        _pathRobotDatas = Application.streamingAssetsPath + robotDatasFilePath;
        _pathTargetPositions = Application.streamingAssetsPath + targetPositionsFilePath;
    }

    private void Update()
    {
        _datas.RefreshDatas(Controller.GetMotorsDatas(), Lidar.GetMeasures(), Controller.GetSensorValues());
        _targetPositions.ReadValues(_pathTargetPositions);
        Debug.Log(_targetPositions.legBackLeftBot);
        /*    Write the datas in the Json file    */
        _jsonString = _datas.ToJson();
        File.WriteAllText(_pathRobotDatas, _jsonString);
    }
   
    public static TargetPositions TargetPositions
    {
        get => _targetPositions;
        set => _targetPositions = value;
    }
}