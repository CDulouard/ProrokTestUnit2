using UnityEngine;
using System.IO;

public class Manager : MonoBehaviour
{
    public string writeFilePath;

    private RobotDatas _datas;    /*    Contain all the datas to put in the Json file.    */
    private string _path, _jsonString; /*    _path is the path to the Json file.    */

    private void Start()
    {
        _datas = new RobotDatas();
        _path = Application.streamingAssetsPath + writeFilePath;    /*    _path initialization    */
    }

    private void Update()
    {
        _datas.RefreshDatas(Controller.GetMotorsDatas(), Lidar.GetMeasures(), Controller.GetSensorValues());
        /*    Write the datas in the Json file    */
        _jsonString = _datas.ToJson();
        File.WriteAllText(_path, _jsonString);
    }
}