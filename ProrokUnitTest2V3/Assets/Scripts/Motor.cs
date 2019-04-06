using UnityEngine;

[System.Serializable]
public struct Motor
{
    public string name;
    [Range(-180, 180)] public float targetPosition;
    public GameObject motor; /*    The GameObject we want to move    */
    public GameObject sensor;
    [Range(-180, 180)] public float angleMin;
    [Range(-180, 180)] public float angleMax;
    public float speed;
    public float torque;
    
    private HingeJoint _motorJoint; /*    The HingeJoint of the GameObject    */

    public HingeJoint MotorJoint
    {
        get => _motorJoint;
        set => _motorJoint = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public float GetAngle()
    {
        /*    Return the actual position of the motor    */
        return MotorJoint.angle;
    }
}