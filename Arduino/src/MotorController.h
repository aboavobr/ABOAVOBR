/*
  MotorController.h - library for interfacing to the motors of the robot.
*/

#ifndef MotorController_h_
#define MotorController_h_

#include "Arduino.h"

enum Direction
{
  Forward = 0,
  Backward = 1
};

class MotorController
{
  public:

  
    MotorController(byte enablePin, byte in1, byte in2);
    void SetPwm(byte pwm);
    byte GetPwm();
    void SetDirection(Direction newDirection);
    
  private:
    byte enablePin, in1, in2;
    byte pwmOutput;
    Direction direction;
};

#endif
