using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    [FormerlySerializedAs("motorSpeed")] public int speed;
    [FormerlySerializedAs("motorTorque")] public int torque;


    public Robot robot;

    void Start()
    {
        InitMotors();
        InitWalkDemo(); //    <===== DEMO
    }

    // Update is called once per frame
    void Update()
    {
        WalkDemo(); //    <===== DEMO
        RefreshMotors();
    }

    private static void SetMotorPosition(HingeJoint motor, float position, int speed, int torque, float xMin,
        float xMax)
    {
        /*    Use the motor component of the HingeJoint to put the GameObject to the right angle.
         *    It take the HingeJoint of the GameObject, the angle of the sensor attached to the pivot point, 
         *    the target position, the speed of the rotation and the torque of the motor as input.
         *    The target position is modulo 180 so the input can be over 180 or less than -180
         */
        var limits = motor.limits;
        position = position % 180;
        var temp = motor.motor;
        temp.force = torque;
        var angle = motor.angle;
        if (((int) position).Equals((int) angle))
        {
            /*    Stop the motor at the target position    */
            if ((int) limits.max != (int) position & (int) limits.min != (int) position)
            {
                limits.max = (int) position + 0.1f;
                limits.min = (int) position - 0.1f;
                motor.limits = limits;
            }

            temp.targetVelocity = 0;
        }
        else
        {
            /*    Move the motor to the target position    */
            if ((int) limits.max != (int) xMax & (int) limits.min != (int) xMin)
            {
                limits.max = xMax;
                limits.min = xMin;
                motor.limits = limits;
            }

            temp.targetVelocity = position < angle ? -speed : speed;
        }

        motor.motor = temp;
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

    private void RefreshMotors()
    {
        /*Refresh motors positions
         *==============================================================================================================
         */
        /*    Leg Back Right Bot    */
        SetMotorPosition(robot.backRight.legBot.MotorJoint, robot.backRight.legBot.targetPosition, speed, torque,
            robot.backRight.legBot.angleMin, robot.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        SetMotorPosition(robot.backRight.legTop.MotorJoint, robot.backRight.legTop.targetPosition, speed, torque,
            robot.backRight.legTop.angleMin, robot.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        SetMotorPosition(robot.backRight.shoulder.MotorJoint, robot.backRight.shoulder.targetPosition, speed, torque,
            robot.backRight.shoulder.angleMin, robot.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        SetMotorPosition(robot.backLeft.legBot.MotorJoint, robot.backLeft.legBot.targetPosition, speed, torque,
            robot.backLeft.legBot.angleMin, robot.backLeft.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(robot.backLeft.legTop.MotorJoint, robot.backLeft.legTop.targetPosition, speed, torque,
            robot.backLeft.legTop.angleMin, robot.backLeft.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(robot.backLeft.shoulder.MotorJoint, robot.backLeft.shoulder.targetPosition, speed, torque,
            robot.backLeft.shoulder.angleMin, robot.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        SetMotorPosition(robot.frontRight.legBot.MotorJoint, robot.frontRight.legBot.targetPosition, speed, torque,
            robot.frontRight.legBot.angleMin, robot.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(robot.frontRight.legTop.MotorJoint, robot.frontRight.legTop.targetPosition, speed, torque,
            robot.frontRight.legTop.angleMin, robot.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(robot.frontRight.shoulder.MotorJoint, -robot.frontRight.shoulder.targetPosition, speed, torque,
            robot.frontRight.shoulder.angleMin, robot.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        SetMotorPosition(robot.frontLeft.legBot.MotorJoint, robot.frontLeft.legBot.targetPosition, speed, torque,
            robot.frontLeft.legBot.angleMin, robot.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        SetMotorPosition(robot.frontLeft.legTop.MotorJoint, robot.frontLeft.legTop.targetPosition, speed, torque,
            robot.frontLeft.legTop.angleMin, robot.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        SetMotorPosition(robot.frontLeft.shoulder.MotorJoint, -robot.frontLeft.shoulder.targetPosition, speed, torque,
            robot.frontLeft.shoulder.angleMin, robot.frontLeft.shoulder.angleMax);

        /*==============================================================================================================*/
    }

    private void InitMotors()
    {
        /*Motors initializations
         *==============================================================================================================
         */

        /*    Leg Back Right Bot    */
        robot.backRight.legBot.MotorJoint = InitMotor(robot.backRight.legBot.motor, robot.backRight.legBot.angleMin,
            robot.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        robot.backRight.legTop.MotorJoint = InitMotor(robot.backRight.legTop.motor, robot.backRight.legTop.angleMin,
            robot.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        robot.backRight.shoulder.MotorJoint =
            InitMotor(robot.backRight.shoulder.motor, robot.backRight.shoulder.angleMin,
                robot.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        robot.backLeft.legBot.MotorJoint = InitMotor(robot.backLeft.legBot.motor, robot.backLeft.legBot.angleMin,
            robot.backLeft.legBot.angleMax);

        /*    Leg Back Left Top    */
        robot.backLeft.legTop.MotorJoint = InitMotor(robot.backLeft.legTop.motor, robot.backLeft.legTop.angleMin,
            robot.backLeft.legTop.angleMax);

        /*    Shoulder Back Left*/
        robot.backLeft.shoulder.MotorJoint =
            InitMotor(robot.backLeft.shoulder.motor, robot.backLeft.shoulder.angleMin,
                robot.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        robot.frontRight.legBot.MotorJoint = InitMotor(robot.frontRight.legBot.motor, robot.frontRight.legBot.angleMin,
            robot.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        robot.frontRight.legTop.MotorJoint = InitMotor(robot.frontRight.legTop.motor, robot.frontRight.legTop.angleMin,
            robot.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        robot.frontRight.shoulder.MotorJoint =
            InitMotor(robot.frontRight.shoulder.motor, robot.frontRight.shoulder.angleMin,
                robot.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        robot.frontLeft.legBot.MotorJoint = InitMotor(robot.frontLeft.legBot.motor, robot.frontLeft.legBot.angleMin,
            robot.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        robot.frontLeft.legTop.MotorJoint = InitMotor(robot.frontLeft.legTop.motor, robot.frontLeft.legTop.angleMin,
            robot.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        robot.frontLeft.shoulder.MotorJoint =
            InitMotor(robot.frontLeft.shoulder.motor, robot.frontLeft.shoulder.angleMin,
                robot.frontLeft.shoulder.angleMax);

        /*==============================================================================================================*/
    }

    private void WalkDemo()
    {
        if (robot.backRight.legBot.MotorJoint.angle > 28 & robot.frontLeft.legBot.MotorJoint.angle > 28 &
            robot.backRight.legTop.MotorJoint.angle > 28 &
            robot.frontLeft.legTop.MotorJoint.angle > 28)
        {
            robot.backRight.legBot.targetPosition = -30;
            robot.backRight.legTop.targetPosition = 23;
            robot.frontLeft.legBot.targetPosition = -30;
            robot.frontLeft.legTop.targetPosition = 23;

            robot.backLeft.legBot.targetPosition = 30;
            robot.backLeft.legTop.targetPosition = 30;
            robot.frontRight.legBot.targetPosition = 30;
            robot.frontRight.legTop.targetPosition = 30;
        }

        if (robot.backLeft.legBot.MotorJoint.angle > 28 & robot.frontRight.legBot.MotorJoint.angle > 28 &
            robot.backLeft.legTop.MotorJoint.angle > 28 &
            robot.frontRight.legTop.MotorJoint.angle > 28)
        {
            robot.backRight.legBot.targetPosition = 30;
            robot.backRight.legTop.targetPosition = 30;
            robot.frontLeft.legBot.targetPosition = 30;
            robot.frontLeft.legTop.targetPosition = 30;

            robot.backLeft.legBot.targetPosition = -30;
            robot.backLeft.legTop.targetPosition = 23;
            robot.frontRight.legBot.targetPosition = -30;
            robot.frontRight.legTop.targetPosition = 23;
        }
    }

    private void InitWalkDemo()
    {
        robot.backRight.legBot.targetPosition = 30;
        robot.backRight.legTop.targetPosition = 30;
        robot.frontLeft.legBot.targetPosition = 30;
        robot.frontLeft.legTop.targetPosition = 30;

        robot.backLeft.legBot.targetPosition = -30;
        robot.backLeft.legTop.targetPosition = 23;
        robot.frontRight.legBot.targetPosition = -30;
        robot.frontRight.legTop.targetPosition = 23;
    }
}

[System.Serializable]
public struct Motor
{
    //public string motorName;
    [Range(-180, 180)] public float targetPosition;
    public GameObject motor;
    public GameObject sensor;
    [Range(-180, 180)] public float angleMin;
    [Range(-180, 180)] public float angleMax;

    private HingeJoint _motorJoint;

    public HingeJoint MotorJoint
    {
        get => _motorJoint;
        set => _motorJoint = value;
    }

    public float GetAngle()
    {
        return MotorJoint.angle;
    }
}

[System.Serializable]
public struct Leg
{
    //public string legName;
    public Motor legBot;
    public Motor legTop;
    public Motor shoulder;

    public IEnumerable<float> GetAngles()
    {
        var angles = new List<float> {legBot.GetAngle(), legTop.GetAngle(), shoulder.GetAngle()};
        return angles;
    }
}

[System.Serializable]
public struct Robot
{
    public Leg frontLeft;
    public Leg frontRight;
    public Leg backLeft;
    public Leg backRight;

    public IEnumerable<float> GetStatus()
    {
        var angles = new List<float>();
        angles.AddRange(frontLeft.GetAngles());
        angles.AddRange(frontRight.GetAngles());
        angles.AddRange(backLeft.GetAngles());
        angles.AddRange(backRight.GetAngles());
        return angles;
    }
}