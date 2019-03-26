using System.Collections.Generic;

[System.Serializable]
public struct Leg
{
    public Motor legBot;
    public Motor legTop;
    public Motor shoulder;

    public Dictionary<string, float> GetDatas()
    {
        var datas = new Dictionary<string, float>
        {
            /*    Return the actual angle of the leg's motors    */
            {legBot.Name, legBot.GetAngle()}, {legTop.Name, legTop.GetAngle()}, {shoulder.Name, shoulder.GetAngle()}
        };
        return datas;
    }
}