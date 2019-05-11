using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool debugMode;
    public bool showDebugMessage;
    public bool testLeg;
    public bool demoWalk;
    public int speed;
    public int torque;

    public static float legBotSpeed;
    public static float legTopSpeed;
    public static float shoulderSpeed;
    public static float legBotTorque;
    public static float legTopTorque;
    public static float shoulderTorque;
    public static float legBotAngleMin;
    public static float legTopAngleMin;
    public static float shoulderAngleMin;
    public static float legBotAngleMax;
    public static float legTopAngleMax;
    public static float shoulderAngleMax;

    public GameObject generalPurposeSensor;
    public ProrokTestUnit2 prorokTestUnit2;
    private static ProrokTestUnit2 _prorokTestUnit2Copy;

    private int _testStep;
    private static int _score;
    private static int _timeOut;    //-10
    private static int _isDown;    //-10
    private static int _isColliding;    //-10
    private static bool _respawnFlag;
    private static int _finished;    //+100
    
    private static MapDatas _currentMap;

    private static IEnumerable<KeyValuePair<string, float>>
        _motorsDatas; /*    Stores the actual angle of each motors    */

    private static IEnumerable<KeyValuePair<string, float>>
        _sensorValues; /*    Stores roll, pitch, yaw and  altitude of the robot    */
    
    /*    DEBUG    */
    private int _frameCount;

    private IEnumerator CheckForProgress()
    {
        /*    Refresh the robot's score    */
        var finish = _currentMap.GetFinishPoint();
        var prevDistance = Vector3.Distance(generalPurposeSensor.transform.position, finish);
        while (true)
        {
            var dFromFinish = Vector3.Distance(generalPurposeSensor.transform.position,finish);
            _score = (int)(prevDistance - dFromFinish - 2);
            if (_isDown == 1)
            {
                _score = -10;
            }
            if (_isColliding == 1)
            {
                _score = -10;
            }
            if (_timeOut == 1)
            {
                _score = -10;
            }
            if (dFromFinish <= 30)
            {
                _score = 100;
                _finished = 1;
            }
            prevDistance = dFromFinish;
            yield return new WaitForSeconds(0.1f);
        }   
    }

    private IEnumerator CheckIfDown()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            
            var datas = Manager.datas.GetSensorDatas().ToDictionary(x => x.Key, x => x.Value);
            if (datas["roll"] > 90f || datas["roll"] < -90f || datas["pitch"] > 90f || datas["pitch"] < -90f )
            {
                _isDown = 1;
            }
            else
            {
                _isDown = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }       
    }

    private IEnumerator Timer(int time)
    {
        var count = time;
        while (count != 0)
        {
            yield return new WaitForSeconds(1);
            count -= 1;
        }
        _timeOut = 1;
    }
    
    private void Start()
    {
        Application.runInBackground = true;
        _finished = 0;
        _score = 0;
        _timeOut = 0;
        _isDown = 0;
        _currentMap = new MapDatas(SceneManager.GetActiveScene().buildIndex);
        InitRobot();
        if (demoWalk & debugMode) InitWalkDemo();
        RefreshMotors();
        RefreshSensorValues();
        _prorokTestUnit2Copy = prorokTestUnit2;
        StartCoroutine(CheckForProgress());
        StartCoroutine(Timer(_currentMap.GetGameDuration()));
        StartCoroutine(CheckIfDown());
        /*    DEBUG    */

        _frameCount = 0;

    }

    private void Update()
    {
        if (!testLeg & debugMode) _testStep = 0;
        if (testLeg & debugMode) BeginTest();
        if (demoWalk & debugMode) WalkDemo();
        if (!debugMode) ApplyAskedPositions();
        RefreshMotors();
        RefreshSensorValues();
        _prorokTestUnit2Copy = prorokTestUnit2;
        CheckRespawn();
        
        /*    DEBUG    */
        _frameCount += 1;
        if (_frameCount == 30)
        {
            _frameCount = 0;
            Debug.Log(_score);
            //Debug.Log(_timeOut);
            //Debug.Log(_isDown);
            //Debug.Log(_isColliding);
        }

    }

    public static void SetIsColliding(int val)
    {
        _isColliding = val;
    }

    public static int GetIsColliding()
    {
        return _isColliding;
    }

    public static int GetTimeOut()
    {
        return _timeOut;
    }
    
    public static int GetIsDown()
    {
        return _isDown;
    }

    public static int GetFinished()
    {
        return _finished;
    }

    public static int GetScore()
    {
        return _score;
    }

    public static void Respawn()
    {
        /*    Set _respawnFlag    */
        _respawnFlag = true;
    }

    private void CheckRespawn()
    {
        /*    Reload the scene if _respawnFlag is set    */
        if (!_respawnFlag) return;
        _respawnFlag = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        var sensorTransformPosition = sensorTransform.position;
        sensorTransform.rotation.ToAngleAxis(out var angle, out var axis);
        newValues.Add("roll", angle * axis.z);
        newValues.Add("pitch", angle * axis.x);
        newValues.Add("yaw", angle * axis.y);
        newValues.Add("posX", sensorTransformPosition.x);
        newValues.Add("posY", sensorTransformPosition.y);
        newValues.Add("posZ", sensorTransformPosition.z);
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
                limits.max = (int) position + 0.15f;
                limits.min = (int) position - 0.15f;
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

        /*    Leg Back Left Top    */
        SetMotorPosition(prorokTestUnit2.backLeft.legTop.MotorJoint, prorokTestUnit2.backLeft.legTop.targetPosition,
            speed, torque,
            prorokTestUnit2.backLeft.legTop.angleMin, prorokTestUnit2.backLeft.legTop.angleMax);

        /*    Shoulder Back Left    */
        SetMotorPosition(prorokTestUnit2.backLeft.shoulder.MotorJoint,
            -prorokTestUnit2.backLeft.shoulder.targetPosition,
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
            prorokTestUnit2.frontRight.shoulder.targetPosition, speed, torque,
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

    private void ImportMotorsSettings()
    {
        /*    Import motors Settings    */

        /*    Leg Back Right Bot    */
        prorokTestUnit2.backRight.legBot = ImportMotorSetting(prorokTestUnit2.backRight.legBot, legBotAngleMin,
            legBotAngleMax, legBotSpeed, legBotTorque);

        /*    Leg Back Right Top    */
        prorokTestUnit2.backRight.legTop = ImportMotorSetting(prorokTestUnit2.backRight.legTop, legTopAngleMin,
            legTopAngleMax, legTopSpeed, legTopTorque);
        
        /*    Shoulder Back Right*/
        prorokTestUnit2.backRight.shoulder = ImportMotorSetting(prorokTestUnit2.backRight.shoulder, shoulderAngleMin,
            shoulderAngleMax, shoulderSpeed, shoulderTorque);

        /*    Leg Back Left Bot    */
        prorokTestUnit2.backLeft.legBot = ImportMotorSetting(prorokTestUnit2.backLeft.legBot, legBotAngleMin,
            legBotAngleMax, legBotSpeed, legBotTorque);

        /*    Leg Back Left Top    */
        prorokTestUnit2.backLeft.legTop = ImportMotorSetting(prorokTestUnit2.backLeft.legTop, legTopAngleMin,
            legTopAngleMax, legTopSpeed, legTopTorque);
        
        /*    Shoulder Back Left*/
        prorokTestUnit2.backLeft.shoulder = ImportMotorSetting(prorokTestUnit2.backLeft.shoulder, shoulderAngleMin,
            shoulderAngleMax, shoulderSpeed, shoulderTorque);
        
        /*    Leg Front Right Bot    */
        prorokTestUnit2.frontRight.legBot = ImportMotorSetting(prorokTestUnit2.frontRight.legBot, legBotAngleMin,
            legBotAngleMax, legBotSpeed, legBotTorque);
        
        /*    Leg Front Right Top    */
        prorokTestUnit2.frontRight.legTop = ImportMotorSetting(prorokTestUnit2.frontRight.legTop, legTopAngleMin,
            legTopAngleMax, legTopSpeed, legTopTorque);

        /*    Shoulder Front Right    */
        prorokTestUnit2.frontRight.shoulder = ImportMotorSetting(prorokTestUnit2.frontRight.shoulder, shoulderAngleMin,
            shoulderAngleMax, shoulderSpeed, shoulderTorque);

        /*    Leg Front Left Bot    */
        prorokTestUnit2.frontLeft.legBot = ImportMotorSetting(prorokTestUnit2.frontLeft.legBot, legBotAngleMin,
            legBotAngleMax, legBotSpeed, legBotTorque);

        /*    Leg Front Left Top    */
        prorokTestUnit2.frontLeft.legTop = ImportMotorSetting(prorokTestUnit2.frontLeft.legTop, legTopAngleMin,
            legTopAngleMax, legTopSpeed, legTopTorque);

        /*    Shoulder Front Left    */
        prorokTestUnit2.frontLeft.shoulder = ImportMotorSetting(prorokTestUnit2.frontLeft.shoulder, shoulderAngleMin,
            shoulderAngleMax, shoulderSpeed, shoulderTorque);
        
    }

    private static Motor ImportMotorSetting(Motor motor, float xMin, float xMax, float speed, float torque)
    {
        motor.angleMin = xMin;
        motor.angleMax = xMax;
        motor.speed = speed;
        motor.torque = torque;
        return motor;
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
        ImportMotorsSettings();
        InitMotors();
    }

    private void ApplyAskedPositions()
    {
        /*    Refresh the position using the asked position in a Json file    */
        prorokTestUnit2.backRight.legBot.targetPosition = Manager.TargetPositions.legBackRightBot;
        prorokTestUnit2.backRight.legTop.targetPosition = Manager.TargetPositions.legBackRightTop;
        prorokTestUnit2.backRight.shoulder.targetPosition = Manager.TargetPositions.shoulderBackRight;

        prorokTestUnit2.backLeft.legBot.targetPosition = Manager.TargetPositions.legBackLeftBot;
        prorokTestUnit2.backLeft.legTop.targetPosition = Manager.TargetPositions.legBackLeftTop;
        prorokTestUnit2.backLeft.shoulder.targetPosition = Manager.TargetPositions.shoulderBackLeft;

        prorokTestUnit2.frontRight.legBot.targetPosition = Manager.TargetPositions.legFrontRightBot;
        prorokTestUnit2.frontRight.legTop.targetPosition = Manager.TargetPositions.legFrontRightTop;
        prorokTestUnit2.frontRight.shoulder.targetPosition = Manager.TargetPositions.shoulderFrontRight;

        prorokTestUnit2.frontLeft.legBot.targetPosition = Manager.TargetPositions.legFrontLeftBot;
        prorokTestUnit2.frontLeft.legTop.targetPosition = Manager.TargetPositions.legFrontLeftTop;
        prorokTestUnit2.frontLeft.shoulder.targetPosition = Manager.TargetPositions.shoulderFrontLeft;
    }

    private void WalkDemo()
    {
        /*    Show a demo walk    */
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

    public static ProrokTestUnit2 GetProrokUnitTest2()
    {
        return _prorokTestUnit2Copy;
    }

    private void InitWalkDemo()
    {
        /*    Init the demo walk (should be called in the void start)    */
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
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 1 & (int) prorokTestUnit2.backRight.legBot.MotorJoint.angle == 0 &
            (int) prorokTestUnit2.backRight.legTop.MotorJoint.angle == 0 &
            (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = -30;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 2 & prorokTestUnit2.backRight.legBot.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 30;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 3 & prorokTestUnit2.backRight.legBot.MotorJoint.angle >= 27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 0;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 4 & (int) prorokTestUnit2.backRight.legBot.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = -30;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 5 & prorokTestUnit2.backRight.legTop.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = 30;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 6 & prorokTestUnit2.backRight.legTop.MotorJoint.angle >= 27)
        {
            prorokTestUnit2.backRight.legTop.targetPosition = 0;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 7 & (int) prorokTestUnit2.backRight.legTop.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = -20;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 8 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle <= -17)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = 20;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 9 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle >= 17)
        {
            prorokTestUnit2.backRight.shoulder.targetPosition = 0;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 10 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle == 0)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = -30;
            prorokTestUnit2.backRight.legTop.targetPosition = -30;
            prorokTestUnit2.backRight.shoulder.targetPosition = -20;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 11 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle <= -17 &
            prorokTestUnit2.backRight.legBot.MotorJoint.angle <= -27 &
            prorokTestUnit2.backRight.legTop.MotorJoint.angle <= -27)
        {
            prorokTestUnit2.backRight.legBot.targetPosition = 30;
            prorokTestUnit2.backRight.legTop.targetPosition = 30;
            prorokTestUnit2.backRight.shoulder.targetPosition = 20;
            _testStep++;
            if (showDebugMessage) Debug.Log("Step : " + _testStep);
        }

        if (_testStep == 12 & (int) prorokTestUnit2.backRight.shoulder.MotorJoint.angle >= 17 &
            prorokTestUnit2.backRight.legBot.MotorJoint.angle >= 27 &
            prorokTestUnit2.backRight.legTop.MotorJoint.angle >= 27)
        {
            _testStep = 0;
        }
    }
}