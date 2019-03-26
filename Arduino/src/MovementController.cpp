#include "MovementController.h"
#include "Arduino.h"
#include <PID_v1.h>

//#define OUTPUT_PID_DATA
//#define OUTPUT_GYROSCOPE_DATA
#define OUTPUT_MOTOR_CONTROL_DATA

MovementController::MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor)
{
  HasStarted = false;
  ZeroPitchOffset = 0;
  gyroscope = inGyroscope;

  PIDForward = new PIDController(0, 225, -225, Kp, Ki, Kd, 10);

  this->leftMotor = leftMotor;
  this->rightMotor = rightMotor;

  direction == None;
}

void MovementController::SetZeroPitch()
{
  ZeroPitchOffset = gyroscope->GetPitch();
}

void MovementController::Start()
{
  HasStarted = true;
  PIDForward->ResetPID();
}

void MovementController::Stop()
{
  leftMotor->SetPwm(0x00);
  rightMotor->SetPwm(0x00);
  PIDForward->ResetPID();
  HasStarted = false;
}

void MovementController::SetPIDProportional(float proportional)
{
  Kp = proportional;
  PIDForward->SetProportional(Kp);
}

void MovementController::SetPIDIntegral(float integral)
{
  Ki = integral;
  PIDForward->SetIntegral(Ki);
}

void MovementController::SetPIDDerivative(float derivative)
{
  Kd = derivative;
  PIDForward->SetDerivative(Kd);
}

void MovementController::MoveForward(int duration)
{
  direction = Forward;
  controlDirectionStopTime = millis() + duration;
}

void MovementController::Loop()
{
  if (!HasStarted)
  {
    return;
  }

  if (millis() > controlDirectionStopTime)
  {
    direction = None;
  }

  PIDForward->UpdateInput(gyroscope->GetPitch() - ZeroPitchOffset);
  int PIDForwardOutput = PIDForward->GetOutput();
  int leftMotorOutput = PIDForwardOutput;
  int rightMotorOutput = PIDForwardOutput;

  if (direction == Forward)
  {
    leftMotorOutput += 40;
    rightMotorOutput += 40;
  }

  if (gyroscope->GetPitch() > 25 || gyroscope->GetPitch() < -25)
  {
    leftMotor->SetPwm(0x00);
    rightMotor->SetPwm(0x00);
    return;
  }
  
  if (leftMotorOutput > 0)
  {
    leftMotor->SetDirection(MotorController::Forward); 
    leftMotor->SetPwm(leftMotorOutput + 30);

    #ifdef OUTPUT_MOTOR_CONTROL_DATA
      Serial.print("Forward with PWM ");
    #endif
  }
  else
  {
    leftMotor->SetDirection(MotorController::Backward);                                                                                                                                                                                                                                                                                                     
    leftMotor->SetPwm((leftMotorOutput - 30) * -1);

    #ifdef OUTPUT_MOTOR_CONTROL_DATA
      Serial.print("Backward. ");
    #endif
  }

  if (rightMotorOutput > 0)
  {
    rightMotor->SetDirection(MotorController::Forward); 
    rightMotor->SetPwm(rightMotorOutput + 30);
  }
  else
  {
    rightMotor->SetDirection(MotorController::Backward);                                                                                                                                                                                                                                                                                                     
    rightMotor->SetPwm((rightMotorOutput - 30) * -1);
  }

  #ifdef OUTPUT_MOTOR_CONTROL_DATA
    Serial.print("Left motor PWM ");
    Serial.print(leftMotor->GetPwm());
    Serial.print("\tRight motor PWM ");
    Serial.println(rightMotor->GetPwm());
  #endif
  
  #ifdef OUTPUT_PID_DATA
    Serial.print("PID: ");
    Serial.println(PIDForwardOutput);
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
