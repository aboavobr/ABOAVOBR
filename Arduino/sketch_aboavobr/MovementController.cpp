#include "MovementController.h"
#include "Arduino.h"
#include <PID_v1.h>

//#define OUTPUT_PID_DATA
//#define OUTPUT_GYROSCOPE_DATA
//#define OUTPUT_MOTOR_CONTROL_DATA

MovementController::MovementController(Gyroscope *inGyroscope, MotorController *leftMotor, MotorController *rightMotor)
{
  HasStarted = false;
  ZeroPitchOffset = 0;
  gyroscope = inGyroscope;

  PIDForward = new PIDController(0, 225, -225, Kp, Ki, Kd, 10);

  this->leftMotor = leftMotor;
  this->rightMotor = rightMotor;
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

void MovementController::SetPIDProportional(double proportional)
{
  Kp = proportional;
  PIDForward->SetProportional(Kp);
}

void MovementController::SetPIDIntegral(double integral)
{
  Ki = integral;
  PIDForward->SetIntegral(Ki);
}

void MovementController::SetPIDDerivative(double derivative)
{
  Kd = derivative;
  PIDForward->SetDerivative(Kd);
}

void MovementController::Loop()
{
  if (!HasStarted)
  {
    return;
  }

  PIDForward->UpdateInput(gyroscope->GetPitch() - ZeroPitchOffset);
  float PIDForwardOutput = PIDForward->GetOutput();

  if (gyroscope->GetPitch() > 25 || gyroscope->GetPitch() < -25)
  {
    leftMotor->SetPwm(0x00);
    rightMotor->SetPwm(0x00);
  }
  else if (PIDForwardOutput > 0)
  {
    leftMotor->SetDirection(MotorController::Forward); 
    leftMotor->SetPwm(PIDForwardOutput + 30);
    rightMotor->SetDirection(MotorController::Forward);
    rightMotor->SetPwm(PIDForwardOutput + 30);

    #ifdef OUTPUT_MOTOR_CONTROL_DATA
      Serial.print("Forward with PWM ");
    #endif
  }
  else if (PIDForwardOutput < 0)
  {
    leftMotor->SetDirection(MotorController::Backward);                                                                                                                                                                                                                                                                                                     
    leftMotor->SetPwm((PIDForwardOutput - 30) * -1);
    rightMotor->SetDirection(MotorController::Backward);
    rightMotor->SetPwm((PIDForwardOutput - 30) * -1);

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
