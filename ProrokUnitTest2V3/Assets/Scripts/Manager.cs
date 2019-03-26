using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;
using System.Linq;

public class Manager : MonoBehaviour
{
    public string writeFilePath;

    private RobotDatas _datas;
    private string _path, _jsonString;

    private void Start()
    {
        _datas = new RobotDatas();
        _path = Application.streamingAssetsPath + writeFilePath;
    }

    private void Update()
    {
        _datas.RefreshDatas(Controller.GetMotorsDatas(), Lidar.GetMeasures());
        _jsonString = _datas.ToJson();
        File.WriteAllText(_path, _jsonString);
    }
}

public struct RobotDatas
{
    private IEnumerable<Point> _lidarDatas;

    public IEnumerable<Point> LidarDatas
    {
        get => _lidarDatas;
        set => _lidarDatas = value;
    }

    public IEnumerable<KeyValuePair<string, float>> MotorsDatas { get; set; }

    public void RefreshDatas(IEnumerable<KeyValuePair<string, float>> motorDatas, IEnumerable<Point> lidarDatas)
    {
        MotorsDatas = motorDatas;
        LidarDatas = lidarDatas;
    }

    private string MotorDataToJson()
    {
        var json = "    \"_motorsDatas\": {\n";
        foreach (var data in MotorsDatas.ToDictionary(x => x.Key, x => x.Value))
        {
            json += "        \"";
            json += data.Key;
            json += "\": ";
            json += data.Value.ToString(CultureInfo.InvariantCulture).Replace(",", ".");
            json += ",\n";
        }

        json = json.Remove(json.Length - 2);
        json += "\n    }";

        return json;
    }

    private string LidarDatasToJson()
    {
        var json = "    \"_lidarDatas\": [\n";

        foreach (var point in LidarDatas.ToList())
        {
            json += "        [";
            json += point.HorizontalAngle;
            json += ", ";
            json += point.VerticalAngle;
            json += " ,";
            json += point.Distance;
            json += "],\n";
        }

        json = json.Remove(json.Length - 2);
        json += "\n    ]";

        return json;
    }

    public string ToJson()
    {
        var json = "{\n";
        json += MotorDataToJson();
        json += ",\n";
        json += LidarDatasToJson();
        json += "\n}";

        return json;
    }
}