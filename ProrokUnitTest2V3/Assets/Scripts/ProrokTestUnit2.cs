using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

[System.Serializable]
public struct ProrokTestUnit2
{
    /*    Contain all the motors of ProrokTestUnit2    */
    public Leg frontLeft;
    public Leg frontRight;
    public Leg backLeft;
    public Leg backRight;

    public IEnumerable<KeyValuePair<string, float>> GetMotorDatas()
    {
        /*    Return the data for each motors of the ProrokTestUnit2    */
        var datas = frontLeft.GetDatas().Concat(frontRight.GetDatas()).Concat(backLeft.GetDatas())
            .Concat(backRight.GetDatas());
        return datas;
    }
}