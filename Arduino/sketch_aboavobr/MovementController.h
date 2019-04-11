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
    MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor);
    void Loop();
    void SetPIDProportional(double proportional);
    void SetPIDIntegral(double integral);
    void SetPIDDerivative(double derivative);
    void SetZeroPitch();
    void Start();
    void Stop();

  private:
    //Define Variables we'll be connecting to
    double ZeroPitchOffset;
    bool HasStarted;
    float LastPitch = 0;

    //Specify the links and initial tuning parameters
    double Kp = 60, Ki = 0.5, Kd = 2500;
    PIDController *PIDForward;
    Gyroscope *gyroscope;
    MotorController* leftMotor;
    MotorController *rightMotor;
};

#endif
