using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Controller : MonoBehaviour
{
    public int motorSpeed;
    public int motorTorque;

    /*    Leg Back Right
     *==============================================================================================================
     */

    /*    Leg Back Right Bot    */
    public GameObject motorLegBackRightBot; //  The GameObject having the HingeJoint
    public GameObject motorLegBackRightBotSensor; // The Sensor attached on the pivot of the GameObject
    public float motorLegBackRightBotPosition; //  The target position of the motor
    public float motorLegBackRightBotXmin; //  The limit min angle
    public float motorLegBackRightBotXmax; //The limit max angle
    private HingeJoint _motorLegBackRightBot; //The HingeJoint of GameObject

    /*    Leg Back Right Top    */
    public GameObject motorLegBackRightTop;
    public GameObject motorLegBackRightTopSensor;
    public float motorLegBackRightTopPosition;
    public float motorLegBackRightTopXmin;
    public float motorLegBackRightTopXmax;
    private HingeJoint _motorLegBackRightTop;

    /*    Shoulder Back Right    */
    public GameObject motorShoulderBackRight;
    public GameObject motorShoulderBackRightSensor;
    public float motorShoulderBackRightPosition;
    public float motorShoulderBackRightZmin;
    public float motorShoulderBackRightZmax;
    private HingeJoint _motorShoulderBackRight;

    /*==============================================================================================================*/

    /*    Leg Back Left
     *==============================================================================================================
     */

    /*    Leg Back Left Bot    */
    public GameObject motorLegBackLeftBot;
    public GameObject motorLegBackLeftBotSensor;
    public float motorLegBackLeftBotPosition;
    public float motorLegBackLeftBotXmin;
    public float motorLegBackLeftBotXmax;
    private HingeJoint _motorLegBackLeftBot;

    /*    Leg Back Left Top    */
    public GameObject motorLegBackLeftTop;
    public GameObject motorLegBackLeftTopSensor;
    public float motorLegBackLeftTopPosition;
    public float motorLegBackLeftTopXmin;
    public float motorLegBackLeftTopXmax;
    private HingeJoint _motorLegBackLeftTop;

    /*    Shoulder Back Left    */
    public GameObject motorShoulderBackLeft;
    public GameObject motorShoulderBackLeftSensor;
    public float motorShoulderBackLeftPosition;
    public float motorShoulderBackLeftZmin;
    public float motorShoulderBackLeftZmax;
    private HingeJoint _motorShoulderBackLeft;

    /*==============================================================================================================*/

    /*    Leg Front Right
     *==============================================================================================================
     */

    /*    Leg Front Right Bot    */
    public GameObject motorLegFrontRightBot; //  The GameObject having the HingeJoint
    public GameObject motorLegFrontRightBotSensor; // The Sensor attached on the pivot of the GameObject
    public float motorLegFrontRightBotPosition; //  The target position of the motor
    public float motorLegFrontRightBotXmin; //  The limit min angle
    public float motorLegFrontRightBotXmax; //The limit max angle
    private HingeJoint _motorLegFrontRightBot; //The HingeJoint of GameObject

    /*    Leg Front Right Top    */
    public GameObject motorLegFrontRightTop;
    public GameObject motorLegFrontRightTopSensor;
    public float motorLegFrontRightTopPosition;
    public float motorLegFrontRightTopXmin;
    public float motorLegFrontRightTopXmax;
    private HingeJoint _motorLegFrontRightTop;

    /*    Shoulder Front Right    */
    public GameObject motorShoulderFrontRight;
    public GameObject motorShoulderFrontRightSensor;
    public float motorShoulderFrontRightPosition;
    public float motorShoulderFrontRightZmin;
    public float motorShoulderFrontRightZmax;
    private HingeJoint _motorShoulderFrontRight;

    /*==============================================================================================================*/

    /*    Leg Front Left
     *==============================================================================================================
     */

    /*    Leg Front Left Bot    */
    public GameObject motorLegFrontLeftBot; //  The GameObject having the HingeJoint
    public GameObject motorLegFrontLeftBotSensor; // The Sensor attached on the pivot of the GameObject
    public float motorLegFrontLeftBotPosition; //  The target position of the motor
    public float motorLegFrontLeftBotXmin; //  The limit min angle
    public float motorLegFrontLeftBotXmax; //The limit max angle
    private HingeJoint _motorLegFrontLeftBot; //The HingeJoint of GameObject

    /*    Leg Front Left Top    */
    public GameObject motorLegFrontLeftTop;
    public GameObject motorLegFrontLeftTopSensor;
    public float motorLegFrontLeftTopPosition;
    public float motorLegFrontLeftTopXmin;
    public float motorLegFrontLeftTopXmax;
    private HingeJoint _motorLegFrontLeftTop;

    /*    Shoulder Front Left    */
    public GameObject motorShoulderFrontLeft;
    public GameObject motorShoulderFrontLeftSensor;
    public float motorShoulderFrontLeftPosition;
    public float motorShoulderFrontLeftZmin;
    public float motorShoulderFrontLeftZmax;
    private HingeJoint _motorShoulderFrontLeft;

    /*==============================================================================================================*/


    // Start is called before the first frame update
    void Start()
    {
        /*Motors initializations
         *==============================================================================================================
         */

        /*    Leg Back Right Bot    */
        _motorLegBackRightBot = InitMotor(motorLegBackRightBot, motorLegBackRightBotXmin, motorLegBackRightBotXmax);

        /*    Leg Back Right Top    */
        _motorLegBackRightTop = InitMotor(motorLegBackRightTop, motorLegBackRightTopXmin, motorLegBackRightTopXmax);

        /*    Shoulder Back Right*/
        _motorShoulderBackRight =
            InitMotor(motorShoulderBackRight, motorShoulderBackRightZmin, motorShoulderBackRightZmax);

        /*    Leg Back Left Bot    */
        _motorLegBackLeftBot = InitMotor(motorLegBackLeftBot, motorLegBackLeftBotXmin, motorLegBackLeftBotXmax);

        /*    Leg Back Left Top    */
        _motorLegBackLeftTop = InitMotor(motorLegBackLeftTop, motorLegBackLeftTopXmin, motorLegBackLeftTopXmax);

        /*    Shoulder Back Left*/
        _motorShoulderBackLeft =
            InitMotor(motorShoulderBackLeft, motorShoulderBackLeftZmin, motorShoulderBackLeftZmax);

        /*    Leg Front Right Bot    */
        _motorLegFrontRightBot = InitMotor(motorLegFrontRightBot, motorLegFrontRightBotXmin, motorLegFrontRightBotXmax);

        /*    Leg Front Right Top    */
        _motorLegFrontRightTop = InitMotor(motorLegFrontRightTop, motorLegFrontRightTopXmin, motorLegFrontRightTopXmax);

        /*    Shoulder Front Right    */
        _motorShoulderFrontRight =
            InitMotor(motorShoulderFrontRight, motorShoulderFrontRightZmin, motorShoulderFrontRightZmax);

        /*    Leg Front Left Bot    */
        _motorLegFrontLeftBot = InitMotor(motorLegFrontLeftBot, motorLegFrontLeftBotXmin, motorLegFrontLeftBotXmax);

        /*    Leg Front Left Top    */
        _motorLegFrontLeftTop = InitMotor(motorLegFrontLeftTop, motorLegFrontLeftTopXmin, motorLegFrontLeftTopXmax);

        /*    Shoulder Front Left    */
        _motorShoulderFrontLeft =
            InitMotor(motorShoulderFrontLeft, motorShoulderFrontLeftZmin, motorShoulderFrontLeftZmax);

        /*==============================================================================================================*/
    }

    // Update is called once per frame
    void Update()
    {
        /*Refresh motors positions
         *==============================================================================================================
         */
        /*    Leg Back Right Bot    */
        SetMotorPosition(_motorLegBackRightBot, motorLegBackRightBotPosition, motorSpeed, motorTorque,
            motorLegBackRightBotXmin, motorLegBackRightBotXmax);

        /*    Leg Back Right Top    */
        SetMotorPosition(_motorLegBackRightTop, motorLegBackRightTopPosition, motorSpeed, motorTorque,
            motorLegBackRightTopXmin, motorLegBackRightTopXmax);

        /*    Shoulder Back Right*/
        SetMotorPosition(_motorShoulderBackRight, motorShoulderBackRightPosition, motorSpeed, motorTorque,
            motorShoulderBackRightZmin, motorShoulderBackRightZmax);

        /*    Leg Back Left Bot    */
        SetMotorPosition(_motorLegBackLeftBot, motorLegBackLeftBotPosition, motorSpeed, motorTorque,
            motorLegBackLeftBotXmin, motorLegBackLeftBotXmax);

        /*    Leg Front Right Top    */
        SetMotorPosition(_motorLegBackLeftTop, motorLegBackLeftTopPosition, motorSpeed, motorTorque,
            motorLegBackLeftTopXmin, motorLegBackLeftTopXmax);

        /*    Shoulder Front Right    */
        SetMotorPosition(_motorShoulderBackLeft, motorShoulderBackLeftPosition, motorSpeed, motorTorque,
            motorShoulderBackLeftZmin, motorShoulderBackLeftZmax);

        /*    Leg Front Right Bot    */
        SetMotorPosition(_motorLegFrontRightBot, motorLegFrontRightBotPosition, motorSpeed, motorTorque,
            motorLegFrontRightBotXmin, motorLegFrontRightBotXmax);

        /*    Leg Front Right Top    */
        SetMotorPosition(_motorLegFrontRightTop, motorLegFrontRightTopPosition, motorSpeed, motorTorque,
            motorLegFrontRightTopXmin, motorLegFrontRightTopXmax);

        /*    Shoulder Front Right    */
        SetMotorPosition(_motorShoulderFrontRight, motorShoulderFrontRightPosition, motorSpeed, motorTorque,
            motorShoulderFrontRightZmin, motorShoulderFrontRightZmax);

        /*    Leg Front Left Bot    */
        SetMotorPosition(_motorLegFrontLeftBot, motorLegFrontLeftBotPosition, motorSpeed, motorTorque,
            motorLegFrontLeftBotXmin, motorLegFrontLeftBotXmax);

        /*    Leg Front Left Top    */
        SetMotorPosition(_motorLegFrontLeftTop, motorLegFrontLeftTopPosition, motorSpeed, motorTorque,
            motorLegFrontLeftTopXmin, motorLegFrontLeftTopXmax);

        /*    Shoulder Front Left    */
        SetMotorPosition(_motorShoulderFrontLeft, motorShoulderFrontLeftPosition, motorSpeed, motorTorque,
            motorShoulderFrontLeftZmin, motorShoulderFrontLeftZmax);

        /*==============================================================================================================*/
    }

    private static float GetXAngle(GameObject sensor)
    {
        /*    return the angle of a GameObject on the X axis in degree [-180, 180]    */
        sensor.transform.localRotation.ToAngleAxis(out var angle, out var axis);
        angle *= axis.x;
        return angle;
    }

    private static float GetZAngle(GameObject sensor)
    {
        /*    return the angle of a GameObject on the Z axis in degree [-180, 180]    */
        sensor.transform.localRotation.ToAngleAxis(out var angle, out var axis);
        angle *= axis.z;
        return angle;
    }

    private static void SetMotorPosition(HingeJoint motor, float position, int speed, int torque)
    {
        /*    Use the motor component of the HingeJoint to put the GameObject to the right angle.
         *    It take the HingeJoint of the GameObject, the angle of the sensor attached to the pivot point, 
         *    the target position, the speed of the rotation and the torque of the motor as input.
         *    The target position is modulo 180 so the input can be over 180 or less than -180
         */
        position = position % 180;
        var temp = motor.motor;
        temp.force = torque;
        var angle = motor.angle;
        if (((int) position).Equals((int) angle))
        {
            /*    Stop the motor at the target position    */
            temp.targetVelocity = 0;
        }
        else
        {
            /*    Move the motor to the target position    */
            temp.targetVelocity = position < angle ? -speed : speed;
        }

        motor.motor = temp;
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
        var motorJoint = motor.GetComponent<HingeJoint>();
        var limits = motorJoint.limits;
        limits.max = xMax;
        limits.min = xMin;
        motorJoint.limits = limits;
        return motorJoint;
    }
}