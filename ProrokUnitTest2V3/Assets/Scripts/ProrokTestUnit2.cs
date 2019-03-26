using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct ProrokTestUnit2
{
    public Leg frontLeft;
    public Leg frontRight;
    public Leg backLeft;
    public Leg backRight;

    public IEnumerable<KeyValuePair<string, float>> GetMotorDatas()
    {
        var datas = frontLeft.GetDatas().Concat(frontRight.GetDatas()).Concat(backLeft.GetDatas())
            .Concat(backRight.GetDatas());
        return datas;
    }
}