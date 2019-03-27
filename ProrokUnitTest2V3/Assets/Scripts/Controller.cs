﻿using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool testLeg;
    public bool demoWalk;
    public int speed;
    public int torque;


    public GameObject generalPurposeSensor;
    public ProrokTestUnit2 prorokTestUnit2;

    private int _testStep;

    private static IEnumerable<KeyValuePair<string, float>>
        _motorsDatas; /*    Stores the actual angle of each motors    */

    private static IEnumerable<KeyValuePair<string, float>>
        _sensorValues; /*    Stores roll, pitch, yaw and  altitude of the robot    */

    void Start()
    {
        InitRobot();
        if (demoWalk) InitWalkDemo();
        RefreshMotors();
        RefreshSensorValues();
    }

    void Update()
    {
        if (!testLeg) _testStep = 0;
        if (testLeg) BeginTest();
        if (demoWalk) WalkDemo();
        RefreshMotors();
        RefreshSensorValues();
    }

    public static IEnumerable<KeyValuePair<string, float>> GetMotorsDatas()
    {
        return _motorsDatas;
    }

    public static IEnumerable<KeyValuePair<string, float>> GetSensorValues()
    {
        return _sensorValues;
    }

    private void RefreshSensorValues()
    {
        /*    Stores the new attitude values in _sensorValues    */
        var newValues = new Dictionary<string, float>();
        var sensorTransform = generalPurposeSensor.transform;
        sensorTransform.localRotation.ToAngleAxis(out var angle, out var axis);
        newValues.Add("roll", angle * axis.z);
        newValues.Add("pitch", angle * axis.x);
        newValues.Add("yaw", angle * axis.y);
        newValues.Add("altitude", sensorTransform.localPosition.y);
        _sensorValues = newValues;
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
        position %= 180;
        var newMotor = motor.motor;
        newMotor.force = torque;
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

            newMotor.targetVelocity = 0;
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
            newMotor.targetVelocity = position < angle ? -speed : speed;
        }

        motor.motor = newMotor;
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
        SetMotorPosition(prorokTestUnit2.backRight.legBot.MotorJoint, prorokTestUnit2.backRight.legBot.targetPosition,
            speed, torque,
            prorokTestUnit2.backRight.legBot.angleMin, prorokTestUnit2.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        SetMotorPosition(prorokTestUnit2.backRight.legTop.MotorJoint, prorokTestUnit2.backRight.legTop.targetPosition,
            speed, torque,
            prorokTestUnit2.backRight.legTop.angleMin, prorokTestUnit2.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        SetMotorPosition(prorokTestUnit2.backRight.shoulder.MotorJoint,
            prorokTestUnit2.backRight.shoulder.targetPosition, speed, torque,
            prorokTestUnit2.backRight.shoulder.angleMin, prorokTestUnit2.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        SetMotorPosition(prorokTestUnit2.backLeft.legBot.MotorJoint, prorokTestUnit2.backLeft.legBot.targetPosition,
            speed, torque,
            prorokTestUnit2.backLeft.legBot.angleMin, prorokTestUnit2.backLeft.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(prorokTestUnit2.backLeft.legTop.MotorJoint, prorokTestUnit2.backLeft.legTop.targetPosition,
            speed, torque,
            prorokTestUnit2.backLeft.legTop.angleMin, prorokTestUnit2.backLeft.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(prorokTestUnit2.backLeft.shoulder.MotorJoint, prorokTestUnit2.backLeft.shoulder.targetPosition,
            speed, torque,
            prorokTestUnit2.backLeft.shoulder.angleMin, prorokTestUnit2.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        SetMotorPosition(prorokTestUnit2.frontRight.legBot.MotorJoint, prorokTestUnit2.frontRight.legBot.targetPosition,
            speed, torque,
            prorokTestUnit2.frontRight.legBot.angleMin, prorokTestUnit2.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        SetMotorPosition(prorokTestUnit2.frontRight.legTop.MotorJoint, prorokTestUnit2.frontRight.legTop.targetPosition,
            speed, torque,
            prorokTestUnit2.frontRight.legTop.angleMin, prorokTestUnit2.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        SetMotorPosition(prorokTestUnit2.frontRight.shoulder.MotorJoint,
            -prorokTestUnit2.frontRight.shoulder.targetPosition, speed, torque,
            prorokTestUnit2.frontRight.shoulder.angleMin, prorokTestUnit2.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        SetMotorPosition(prorokTestUnit2.frontLeft.legBot.MotorJoint, prorokTestUnit2.frontLeft.legBot.targetPosition,
            speed, torque,
            prorokTestUnit2.frontLeft.legBot.angleMin, prorokTestUnit2.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        SetMotorPosition(prorokTestUnit2.frontLeft.legTop.MotorJoint, prorokTestUnit2.frontLeft.legTop.targetPosition,
            speed, torque,
            prorokTestUnit2.frontLeft.legTop.angleMin, prorokTestUnit2.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        SetMotorPosition(prorokTestUnit2.frontLeft.shoulder.MotorJoint,
            -prorokTestUnit2.frontLeft.shoulder.targetPosition, speed, torque,
            prorokTestUnit2.frontLeft.shoulder.angleMin, prorokTestUnit2.frontLeft.shoulder.angleMax);

        _motorsDatas = prorokTestUnit2.GetMotorDatas();
    }

    private void InitMotors()
    {
        /*    Initialize motors    */

        /*    Leg Back Right Bot    */
        prorokTestUnit2.backRight.legBot.MotorJoint = InitMotor(prorokTestUnit2.backRight.legBot.motor,
            prorokTestUnit2.backRight.legBot.angleMin,
            prorokTestUnit2.backRight.legBot.angleMax);

        /*    Leg Back Right Top    */
        prorokTestUnit2.backRight.legTop.MotorJoint = InitMotor(prorokTestUnit2.backRight.legTop.motor,
            prorokTestUnit2.backRight.legTop.angleMin,
            prorokTestUnit2.backRight.legTop.angleMax);

        /*    Shoulder Back Right*/
        prorokTestUnit2.backRight.shoulder.MotorJoint =
            InitMotor(prorokTestUnit2.backRight.shoulder.motor, prorokTestUnit2.backRight.shoulder.angleMin,
                prorokTestUnit2.backRight.shoulder.angleMax);

        /*    Leg Back Left Bot    */
        prorokTestUnit2.backLeft.legBot.MotorJoint = InitMotor(prorokTestUnit2.backLeft.legBot.motor,
            prorokTestUnit2.backLeft.legBot.angleMin,
            prorokTestUnit2.backLeft.legBot.angleMax);

        /*    Leg Back Left Top    */
        prorokTestUnit2.backLeft.legTop.MotorJoint = InitMotor(prorokTestUnit2.backLeft.legTop.motor,
            prorokTestUnit2.backLeft.legTop.angleMin,
            prorokTestUnit2.backLeft.legTop.angleMax);

        /*    Shoulder Back Left*/
        prorokTestUnit2.backLeft.shoulder.MotorJoint =
            InitMotor(prorokTestUnit2.backLeft.shoulder.motor, prorokTestUnit2.backLeft.shoulder.angleMin,
                prorokTestUnit2.backLeft.shoulder.angleMax);

        /*    Leg Front Right Bot    */
        prorokTestUnit2.frontRight.legBot.MotorJoint = InitMotor(prorokTestUnit2.frontRight.legBot.motor,
            prorokTestUnit2.frontRight.legBot.angleMin,
            prorokTestUnit2.frontRight.legBot.angleMax);

        /*    Leg Front Right Top    */
        prorokTestUnit2.frontRight.legTop.MotorJoint = InitMotor(prorokTestUnit2.frontRight.legTop.motor,
            prorokTestUnit2.frontRight.legTop.angleMin,
            prorokTestUnit2.frontRight.legTop.angleMax);

        /*    Shoulder Front Right    */
        prorokTestUnit2.frontRight.shoulder.MotorJoint =
            InitMotor(prorokTestUnit2.frontRight.shoulder.motor, prorokTestUnit2.frontRight.shoulder.angleMin,
                prorokTestUnit2.frontRight.shoulder.angleMax);

        /*    Leg Front Left Bot    */
        prorokTestUnit2.frontLeft.legBot.MotorJoint = InitMotor(prorokTestUnit2.frontLeft.legBot.motor,
            prorokTestUnit2.frontLeft.legBot.angleMin,
            prorokTestUnit2.frontLeft.legBot.angleMax);

        /*    Leg Front Left Top    */
        prorokTestUnit2.frontLeft.legTop.MotorJoint = InitMotor(prorokTestUnit2.frontLeft.legTop.motor,
            prorokTestUnit2.frontLeft.legTop.angleMin,
            prorokTestUnit2.frontLeft.legTop.angleMax);

        /*    Shoulder Front Left    */
        prorokTestUnit2.frontLeft.shoulder.MotorJoint =
            InitMotor(prorokTestUnit2.frontLeft.shoulder.motor, prorokTestUnit2.frontLeft.shoulder.angleMin,
                prorokTestUnit2.frontLeft.shoulder.angleMax);
    }

    private void InitRobot()
    {
        /*    Initialize the robot    */
        //InitMotors();
        prorokTestUnit2.InitTestUnit();
    }

    private void WalkDemo()
    {
        if (prorokTestUnit2.backRight.legBot.MotorJoint.angle > 28 &
            prorokTestUnit2.frontLeft.legBot.MotorJoint.angle > 28 &
            prorokTestUnit2.backRight.legTop.MotorJoint.angle > 28 &
            prorokTestUnit2.frontLeft.legTop.MotorJoint.angle > 28)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = -30;
            prorokTestUnit2.backRight.legTop.targetPosition = 23;
            prorokTestUnit2.frontLeft.legBot.targetPosition = -30;
            prorokTestUnit2.frontLeft.legTop.targetPosition = 23;

            prorokTestUnit2.backLeft.legBot.targetPosition = 30;
            prorokTestUnit2.backLeft.legTop.targetPosition = 30;
            prorokTestUnit2.frontRight.legBot.targetPosition = 30;
            prorokTestUnit2.frontRight.legTop.targetPosition = 30;
        }

        if (prorokTestUnit2.backLeft.legBot.MotorJoint.angle > 28 &
            prorokTestUnit2.frontRight.legBot.MotorJoint.angle > 28 &
            prorokTestUnit2.backLeft.legTop.MotorJoint.angle > 28 &
            prorokTestUnit2.frontRight.legTop.MotorJoint.angle > 28)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 30;
            prorokTestUnit2.backRight.legTop.targetPosition = 30;
            prorokTestUnit2.frontLeft.legBot.targetPosition = 30;
            prorokTestUnit2.frontLeft.legTop.targetPosition = 30;

            prorokTestUnit2.backLeft.legBot.targetPosition = -30;
            prorokTestUnit2.backLeft.legTop.targetPosition = 23;
            prorokTestUnit2.frontRight.legBot.targetPosition = -30;
            prorokTestUnit2.frontRight.legTop.targetPosition = 23;
        }
    }

    private void InitWalkDemo()
    {
        prorokTestUnit2.backRight.legBot.targetPosition = 30;
        prorokTestUnit2.backRight.legTop.targetPosition = 30;
        prorokTestUnit2.frontLeft.legBot.targetPosition = 30;
        prorokTestUnit2.frontLeft.legTop.targetPosition = 30;

        prorokTestUnit2.backLeft.legBot.targetPosition = -30;
        prorokTestUnit2.backLeft.legTop.targetPosition = 23;
        prorokTestUnit2.frontRight.legBot.targetPosition = -30;
        prorokTestUnit2.frontRight.legTop.targetPosition = 23;
    }

    private void BeginTest()
    {
        /*    Test all the positions of the back right leg.    */
        if (_testStep == 0)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 0;
            prorokTestUnit2.backRight.legTop.targetPosition = 0;
            prorokTestUnit2.backRight.shoulder.targetPosition = 0;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 1 & (int) prorokTestUnit2.backRight.legBot.MotorJoint.angle == 0 &
            (int) prorokTestUnit2.backRight.legTop.MotorJoint.angle == 0 &
            (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = -30;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 2 & prorokTestUnit2.backRight.legBot.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 30;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 3 & prorokTestUnit2.backRight.legBot.MotorJoint.angle >= 27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 0;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 4 & (int) prorokTestUnit2.backRight.legBot.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = -30;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 5 & prorokTestUnit2.backRight.legTop.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = 30;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 6 & prorokTestUnit2.backRight.legTop.MotorJoint.angle >= 27)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = 0;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 7 & (int) prorokTestUnit2.backRight.legTop.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = -20;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 8 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle <= -17)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = 20;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 9 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle >= 17)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = 0;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 10 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = -30;
            prorokTestUnit2.backRight.legTop.targetPosition = -30;
            prorokTestUnit2.backRight.shoulder.targetPosition = -20;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 11 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle <= -17 &
            prorokTestUnit2.backRight.legBot.MotorJoint.angle <= -27 &
            prorokTestUnit2.backRight.legTop.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 30;
            prorokTestUnit2.backRight.legTop.targetPosition = 30;
            prorokTestUnit2.backRight.shoulder.targetPosition = 20;
            _testStep++;
            Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 12 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle >= 17 &
            prorokTestUnit2.backRight.legBot.MotorJoint.angle >= 27 &
            prorokTestUnit2.backRight.legTop.MotorJoint.angle >= 27)
        {
            _testStep = 0;
        }
    }
}