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
    double PIDForwardSetpoint, PIDFordwardInput, PIDForwardOutput;
    double PIDBackwardSetpoint, PIDBackwardInput, PIDBackwardOutput;
    double PIDForwardMovement;
    
    //Specify the links and initial tuning parameters
    double Kp=25, Ki=1, Kd=0;
    PID *PIDForward;
    PID *PIDBackward;
    Gyroscope *gyroscope;
    MotorController* leftMotor;
    MotorController *rightMotor;
};

#endif
