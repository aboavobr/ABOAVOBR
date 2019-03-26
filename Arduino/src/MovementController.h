/*
  MovementController.h - library for controlling the movement of the robot
*/

#ifndef MovementController_h_
#define MovementController_h_

#include "Arduino.h"
#include "PIDController.h"
#include "Gyroscope.h"
#include "MotorController.h"

class MovementController
{
  public:
    enum ControlDirection
    {
      None = 0,
      Forward = 1,
      Backward = 2,
      RotateLeft = 3,
      RotateRight = 4
    };
  
    MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor);
    void Loop();
    void SetPIDProportional(float proportional);
    void SetPIDIntegral(float integral);
    void SetPIDDerivative(float derivative);
    void SetZeroPitch();
    void Start();
    void Stop();
    void MoveForward(int duration);

  private:
    //Define Variables we'll be connecting to
    double ZeroPitchOffset;
    bool HasStarted;
    float LastPitch = 0;

    //Specify the links and initial tuning parameters
    float Kp = 60, Ki = 0.5, Kd = 2500;
    PIDController *PIDForward;
    Gyroscope *gyroscope;
    MotorController* leftMotor;
    MotorController *rightMotor;
    ControlDirection direction;
    unsigned long controlDirectionStopTime;
};

#endif
