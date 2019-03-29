using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class RobotDatas
{
    private IEnumerable<LidarPoint> LidarDatas { get; set; }

    private IEnumerable<KeyValuePair<string, float>> MotorsDatas { get; set; }

    private IEnumerable<KeyValuePair<string, float>> SensorDatas { get; set; }

    public void RefreshDatas(IEnumerable<KeyValuePair<string, float>> motorDatas, IEnumerable<LidarPoint> lidarDatas,
        IEnumerable<KeyValuePair<string, float>> sensorValues)
    {
        /*    Store the values in the RobotDatas object    */
        MotorsDatas = motorDatas;
        LidarDatas = lidarDatas;
        SensorDatas = sensorValues;
    }

    private string MotorDatasToJson()
    {
        /*    Return a json string with all the motors's datas    */
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

    private string SensorDatasToJson()
    {
        var json = "    \"_sensorDatas\": {\n";
        
        foreach (var data in SensorDatas.ToDictionary(x => x.Key, x => x.Value))
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
        /*    Return a json string with all the Lidar's datas    */
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
        /*    Return a json string with all the datas*/
        var json = "{\n";
        json += MotorDatasToJson();
        json += ",\n";
        json += LidarDatasToJson();
        json += ",\n";
        json += SensorDatasToJson();
        json += "\n}";
        return json;
    }
}