using UnityEngine;

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
        /*    Refresh the datas of the robot    */
        _datas.RefreshDatas(Controller.GetMotorsDatas(), Lidar.GetMeasures(), Controller.GetSensorValues());
        
        if(Server.isActive)_targetPositions = TargetPositions.ReadValues(Server.targetPositions);
        
        status = _datas.ToJson();

    }

    public static TargetPositions TargetPositions
    {
        get => _targetPositions;
        set => _targetPositions = value;
    }
}