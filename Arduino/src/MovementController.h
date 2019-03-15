/*
  MovementController.h - library for controlling the movement of the robot
*/

#ifndef MovementController_h_
#define MovementController_h_

#include "Arduino.h"
#include "PID_v1.h"
#include "Gyroscope.h"
#include "MotorController.h"

class MovementController
{
  public:
    MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor);
    void Loop();
    
  private:
    //Define Variables we'll be connecting to
    double Setpoint, Input, Output;
    
    //Specify the links and initial tuning parameters
    double Kp=2, Ki=5, Kd=1;
    PID *myPID;
    Gyroscope *gyroscope;
    MotorController* leftMotor;
    MotorController *rightMotor;
};

#endif
