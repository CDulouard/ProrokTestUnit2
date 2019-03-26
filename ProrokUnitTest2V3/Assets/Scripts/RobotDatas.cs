﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public struct RobotDatas
{
    private IEnumerable<LidarPoint> LidarDatas { get; set; }

    private IEnumerable<KeyValuePair<string, float>> MotorsDatas { get; set; }

    public void RefreshDatas(IEnumerable<KeyValuePair<string, float>> motorDatas, IEnumerable<LidarPoint> lidarDatas)
    {
        /*    Store the values in the RobotDatas object    */
        MotorsDatas = motorDatas;
        LidarDatas = lidarDatas;
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
        json += "\n}";

        return json;
    }
}