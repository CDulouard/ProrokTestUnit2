using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Leg
{
    public string name;
    public Motor legBot;
    public Motor legTop;
    public Motor shoulder;
    public Dictionary<string, Motor> motors;

    public void InitLeg()
    {
        motors = new Dictionary<string, Motor>
        {
            {legBot.name, legBot}, {legTop.name, legTop}, {shoulder.name, shoulder}
        };
    }

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