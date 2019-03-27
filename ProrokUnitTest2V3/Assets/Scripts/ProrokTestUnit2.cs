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
    
    public Dictionary<string, Leg> legs;

    public void InitTestUnit()
    {
        frontLeft.InitLeg();
        frontRight.InitLeg();
        backLeft.InitLeg();
        backRight.InitLeg();
        legs = new Dictionary<string, Leg>
        {
            {frontLeft.name, frontLeft},
            {frontRight.name, frontRight},
            {backLeft.name, backLeft},
            {backRight.name, backRight}
        };
        InitMotors();
    }
    
    private static HingeJoint InitMotor(GameObject motor, float xMin, float xMax)
    {
        /*  return the HingeJoint of the GameObject with its limits  */
        var joint = motor.GetComponent<HingeJoint>();
        var limits = joint.limits;
        limits.max = xMax;
        limits.min = xMin;
        joint.limits = limits;
        return joint;
    }
    
    private void InitMotors()
    {
        /*    Initialize motors    */
        var legsTemp = new Dictionary<string, Leg>();
        foreach (var leg in legs)
        {
            var legTemp = new Leg();
            foreach (var motor in leg.Value.motors)
            {
               var motorTemp = motor.Value;
               motorTemp.MotorJoint = InitMotor(motorTemp.motor, motorTemp.angleMin,
                    motorTemp.angleMax);
               legTemp.motors.Add(motor.Key, motorTemp);
            }
            legsTemp.Add(leg.Key, legTemp);
        }
        legs = legsTemp;
    }

    public IEnumerable<KeyValuePair<string, float>> GetMotorDatas()
    {
        /*    Return the data for each motors of the ProrokTestUnit2    */
        var datas = frontLeft.GetDatas().Concat(frontRight.GetDatas()).Concat(backLeft.GetDatas())
            .Concat(backRight.GetDatas());
        return datas;
    }
}