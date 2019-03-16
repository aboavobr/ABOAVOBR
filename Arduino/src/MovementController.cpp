#include "MovementController.h"
#include "Arduino.h"
#include <PID_v1.h>

//#define OUTPUT_PID_DATA
//#define OUTPUT_GYROSCOPE_DATA
//#define OUTPUT_MOTOR_CONTROL_DATA

MovementController::MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor)
{
  gyroscope = inGyroscope;

  PIDForwardSetpoint = 0;
  PIDForward = new PID(&PIDFordwardInput, &PIDForwardOutput, &PIDForwardSetpoint, Kp, Ki, Kd, DIRECT);
  PIDForward->SetMode(AUTOMATIC);

  PIDBackwardSetpoint = 0;
  PIDBackward = new PID(&PIDBackwardInput, &PIDBackwardOutput, &PIDBackwardSetpoint, Kp, Ki, Kd, DIRECT);
  PIDBackward->SetMode(AUTOMATIC);

  this->leftMotor = leftMotor;
  this->rightMotor = rightMotor;
}

void MovementController::Loop()
{
  PIDFordwardInput = gyroscope->GetPitch();
  PIDForward->Compute();

  PIDBackwardInput = gyroscope->GetPitch() * -1;
  PIDBackward->Compute();

  PIDForwardMovement = PIDForwardOutput - PIDBackwardOutput;

  if (gyroscope->GetPitch() > 25 || gyroscope->GetPitch() < -25)
  {
    leftMotor->SetPwm(0x00);
    rightMotor->SetPwm(0x00);
  }
  else if (PIDForwardMovement >= 0)
  {
    leftMotor->SetDirection(MotorController::Forward); 
    leftMotor->SetPwm(PIDForwardMovement);
    rightMotor->SetDirection(MotorController::Forward);
    rightMotor->SetPwm(PIDForwardMovement);

    #ifdef OUTPUT_MOTOR_CONTROL_DATA
      Serial.print("Forward with PWM ");
    #endif
  }
  else
  {
    leftMotor->SetDirection(MotorController::Backward);                                                                                                                                                                                                                                                                                                     
    leftMotor->SetPwm(PIDForwardMovement * -1);
    rightMotor->SetDirection(MotorController::Backward);
    rightMotor->SetPwm(PIDForwardMovement * -1);

    #ifdef OUTPUT_MOTOR_CONTROL_DATA
      Serial.print("Backward. ");
    #endif
  }

  #ifdef OUTPUT_MOTOR_CONTROL_DATA
    Serial.print("Left motor PWM ");
    Serial.print(leftMotor->GetPwm());
    Serial.print("\tRight motor PWM ");
    Serial.println(rightMotor->GetPwm());
  #endif
  
  #ifdef OUTPUT_PID_DATA
    Serial.print("PID: ");
    Serial.print(PIDForwardOutput);
    Serial.print("\t");
    Serial.print(PIDBackwardOutput);
    Serial.print("\t");
    Serial.println(PIDForwardMovement);
  #endif

  #ifdef OUTPUT_GYROSCOPE_DATA
    Serial.print("YPR: ");
    Serial.print(gyroscope->GetYaw());
    Serial.print("\t");
    Serial.print(gyroscope->GetPitch());
    Serial.print("\t");
    Serial.println(gyroscope->GetRoll());
  #endif
}
