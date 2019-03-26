using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    [FormerlySerializedAs("motorSpeed")] public int speed;
    [FormerlySerializedAs("motorTorque")] public int torque;

    [FormerlySerializedAs("prorokUnitTest2")] public ProrokTestUnit2 ProrokTestUnit2;

    private static IEnumerable<KeyValuePair<string, float>> _motorsDatas;

    public static IEnumerable<KeyValuePair<string, float>> GetMotorsDatas()
    {
        return _motorsDatas;
    }

    void Start()
    {
        InitRobot();
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
                /*    Set new limit to avoid any movements    */
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
                /*    Set new limits if the motor was blocked    */
                limits.max = xMax;
                limits.min = xMin;
                motor.limits = limits;
            }

            /*    Chose the right velocity to reach the target position    */
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
        /*    Refresh motors positions and update motors datas    */
        /*    Leg Back Right Bot    */
        SetMotorPosition(ProrokTestUnit2.backRight.legBot.MotorJoint, ProrokTestUnit2.backRight.legBot.targetPosition, speed, torque,
            ProrokTestUnit2.backRight.legBot.angleMin, ProrokTestUnit2.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        SetMotorPosition(ProrokTestUnit2.backRight.legTop.MotorJoint, ProrokTestUnit2.backRight.legTop.targetPosition, speed, torque,
            ProrokTestUnit2.backRight.legTop.angleMin, ProrokTestUnit2.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        SetMotorPosition(ProrokTestUnit2.backRight.shoulder.MotorJoint, ProrokTestUnit2.backRight.shoulder.targetPosition, speed, torque,
            ProrokTestUnit2.backRight.shoulder.angleMin, ProrokTestUnit2.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        SetMotorPosition(ProrokTestUnit2.backLeft.legBot.MotorJoint, ProrokTestUnit2.backLeft.legBot.targetPosition, speed, torque,
            ProrokTestUnit2.backLeft.legBot.angleMin, ProrokTestUnit2.backLeft.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(ProrokTestUnit2.backLeft.legTop.MotorJoint, ProrokTestUnit2.backLeft.legTop.targetPosition, speed, torque,
            ProrokTestUnit2.backLeft.legTop.angleMin, ProrokTestUnit2.backLeft.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(ProrokTestUnit2.backLeft.shoulder.MotorJoint, ProrokTestUnit2.backLeft.shoulder.targetPosition, speed, torque,
            ProrokTestUnit2.backLeft.shoulder.angleMin, ProrokTestUnit2.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        SetMotorPosition(ProrokTestUnit2.frontRight.legBot.MotorJoint, ProrokTestUnit2.frontRight.legBot.targetPosition, speed, torque,
            ProrokTestUnit2.frontRight.legBot.angleMin, ProrokTestUnit2.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(ProrokTestUnit2.frontRight.legTop.MotorJoint, ProrokTestUnit2.frontRight.legTop.targetPosition, speed, torque,
            ProrokTestUnit2.frontRight.legTop.angleMin, ProrokTestUnit2.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(ProrokTestUnit2.frontRight.shoulder.MotorJoint, -ProrokTestUnit2.frontRight.shoulder.targetPosition, speed, torque,
            ProrokTestUnit2.frontRight.shoulder.angleMin, ProrokTestUnit2.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        SetMotorPosition(ProrokTestUnit2.frontLeft.legBot.MotorJoint, ProrokTestUnit2.frontLeft.legBot.targetPosition, speed, torque,
            ProrokTestUnit2.frontLeft.legBot.angleMin, ProrokTestUnit2.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        SetMotorPosition(ProrokTestUnit2.frontLeft.legTop.MotorJoint, ProrokTestUnit2.frontLeft.legTop.targetPosition, speed, torque,
            ProrokTestUnit2.frontLeft.legTop.angleMin, ProrokTestUnit2.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        SetMotorPosition(ProrokTestUnit2.frontLeft.shoulder.MotorJoint, -ProrokTestUnit2.frontLeft.shoulder.targetPosition, speed, torque,
            ProrokTestUnit2.frontLeft.shoulder.angleMin, ProrokTestUnit2.frontLeft.shoulder.angleMax);

        _motorsDatas = ProrokTestUnit2.GetMotorDatas();
    }

    private void InitMotors()
    {
        /*    Initialize motors    */

        /*    Leg Back Right Bot    */
        ProrokTestUnit2.backRight.legBot.MotorJoint = InitMotor(ProrokTestUnit2.backRight.legBot.motor, ProrokTestUnit2.backRight.legBot.angleMin,
            ProrokTestUnit2.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        ProrokTestUnit2.backRight.legTop.MotorJoint = InitMotor(ProrokTestUnit2.backRight.legTop.motor, ProrokTestUnit2.backRight.legTop.angleMin,
            ProrokTestUnit2.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        ProrokTestUnit2.backRight.shoulder.MotorJoint =
            InitMotor(ProrokTestUnit2.backRight.shoulder.motor, ProrokTestUnit2.backRight.shoulder.angleMin,
                ProrokTestUnit2.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        ProrokTestUnit2.backLeft.legBot.MotorJoint = InitMotor(ProrokTestUnit2.backLeft.legBot.motor, ProrokTestUnit2.backLeft.legBot.angleMin,
            ProrokTestUnit2.backLeft.legBot.angleMax);

        /*    Leg Back Left Top    */
        ProrokTestUnit2.backLeft.legTop.MotorJoint = InitMotor(ProrokTestUnit2.backLeft.legTop.motor, ProrokTestUnit2.backLeft.legTop.angleMin,
            ProrokTestUnit2.backLeft.legTop.angleMax);

        /*    Shoulder Back Left*/
        ProrokTestUnit2.backLeft.shoulder.MotorJoint =
            InitMotor(ProrokTestUnit2.backLeft.shoulder.motor, ProrokTestUnit2.backLeft.shoulder.angleMin,
                ProrokTestUnit2.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        ProrokTestUnit2.frontRight.legBot.MotorJoint = InitMotor(ProrokTestUnit2.frontRight.legBot.motor, ProrokTestUnit2.frontRight.legBot.angleMin,
            ProrokTestUnit2.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        ProrokTestUnit2.frontRight.legTop.MotorJoint = InitMotor(ProrokTestUnit2.frontRight.legTop.motor, ProrokTestUnit2.frontRight.legTop.angleMin,
            ProrokTestUnit2.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        ProrokTestUnit2.frontRight.shoulder.MotorJoint =
            InitMotor(ProrokTestUnit2.frontRight.shoulder.motor, ProrokTestUnit2.frontRight.shoulder.angleMin,
                ProrokTestUnit2.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        ProrokTestUnit2.frontLeft.legBot.MotorJoint = InitMotor(ProrokTestUnit2.frontLeft.legBot.motor, ProrokTestUnit2.frontLeft.legBot.angleMin,
            ProrokTestUnit2.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        ProrokTestUnit2.frontLeft.legTop.MotorJoint = InitMotor(ProrokTestUnit2.frontLeft.legTop.motor, ProrokTestUnit2.frontLeft.legTop.angleMin,
            ProrokTestUnit2.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        ProrokTestUnit2.frontLeft.shoulder.MotorJoint =
            InitMotor(ProrokTestUnit2.frontLeft.shoulder.motor, ProrokTestUnit2.frontLeft.shoulder.angleMin,
                ProrokTestUnit2.frontLeft.shoulder.angleMax);

    }

    private void InitRobot()
    {
        /*    Initialize the robot    */
        InitMotors();
    }

    private void WalkDemo()
    {
        if (ProrokTestUnit2.backRight.legBot.MotorJoint.angle > 28 & ProrokTestUnit2.frontLeft.legBot.MotorJoint.angle > 28 &
            ProrokTestUnit2.backRight.legTop.MotorJoint.angle > 28 &
            ProrokTestUnit2.frontLeft.legTop.MotorJoint.angle > 28)
        {
            ProrokTestUnit2.backRight.legBot.targetPosition = -30;
            ProrokTestUnit2.backRight.legTop.targetPosition = 23;
            ProrokTestUnit2.frontLeft.legBot.targetPosition = -30;
            ProrokTestUnit2.frontLeft.legTop.targetPosition = 23;

            ProrokTestUnit2.backLeft.legBot.targetPosition = 30;
            ProrokTestUnit2.backLeft.legTop.targetPosition = 30;
            ProrokTestUnit2.frontRight.legBot.targetPosition = 30;
            ProrokTestUnit2.frontRight.legTop.targetPosition = 30;
        }

        if (ProrokTestUnit2.backLeft.legBot.MotorJoint.angle > 28 & ProrokTestUnit2.frontRight.legBot.MotorJoint.angle > 28 &
            ProrokTestUnit2.backLeft.legTop.MotorJoint.angle > 28 &
            ProrokTestUnit2.frontRight.legTop.MotorJoint.angle > 28)
        {
            ProrokTestUnit2.backRight.legBot.targetPosition = 30;
            ProrokTestUnit2.backRight.legTop.targetPosition = 30;
            ProrokTestUnit2.frontLeft.legBot.targetPosition = 30;
            ProrokTestUnit2.frontLeft.legTop.targetPosition = 30;

            ProrokTestUnit2.backLeft.legBot.targetPosition = -30;
            ProrokTestUnit2.backLeft.legTop.targetPosition = 23;
            ProrokTestUnit2.frontRight.legBot.targetPosition = -30;
            ProrokTestUnit2.frontRight.legTop.targetPosition = 23;
        }
    }

    private void InitWalkDemo()
    {
        ProrokTestUnit2.backRight.legBot.targetPosition = 30;
        ProrokTestUnit2.backRight.legTop.targetPosition = 30;
        ProrokTestUnit2.frontLeft.legBot.targetPosition = 30;
        ProrokTestUnit2.frontLeft.legTop.targetPosition = 30;

        ProrokTestUnit2.backLeft.legBot.targetPosition = -30;
        ProrokTestUnit2.backLeft.legTop.targetPosition = 23;
        ProrokTestUnit2.frontRight.legBot.targetPosition = -30;
        ProrokTestUnit2.frontRight.legTop.targetPosition = 23;
    }
}





