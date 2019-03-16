#include "MotorController.h"
#include "Arduino.h"

MotorController::MotorController(byte enablePin, byte in1, byte in2)
{
  this->enablePin = enablePin;
  this->in1 = in1;
  this->in2 = in2;

  Serial.println(enablePin);
  Serial.println(in1);
  Serial.println(in2);

  pinMode(enablePin, OUTPUT);
  pinMode(in1, OUTPUT);
  pinMode(in2, OUTPUT);
  //Initial rotation direction
  SetDirection(Forward);

  SetPwm(0x00);
}

void MotorController::SetPwm(byte pwm)
{
  pwmOutput = pwm;
  analogWrite(enablePin, pwmOutput);
}

byte MotorController::GetPwm()
{
  return pwmOutput;
}

void MotorController::SetDirection(Direction newDirection)
{
  direction = newDirection;

  switch (direction)
  {
    case Forward:
      digitalWrite(in1, LOW);
      digitalWrite(in2, HIGH);
      break;
    case Backward:
      digitalWrite(in1, HIGH);
      digitalWrite(in2, LOW);
      break;
  }
}
