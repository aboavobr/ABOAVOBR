#include "PIDController.h"

//#define OUTPUT_PID_CONTROLLER_INFO
//#define OUTPUT_PID_CONFIGURATION_INFO

PIDController::PIDController(float target, float max, float min, float kp, float ki, float kd, int refreshInterval)
{
  PID_I = 0;
  PID_D = 0;
  PID_P = 0;
  this->target = target;
  this->kp = kp;
  this->ki = ki;
  this->kd = kd;
  this->max = max;
  this->min = min;
  this->refreshInterval = refreshInterval;
  timePrev = millis();
}

void PIDController::SetTarget(float target)
{
  this->target = target;
}

void PIDController::ResetPID()
{
  PID_I = 0;
  PID_D = 0;
  PID_P = 0;
  timePrev = millis();
  previous_error = 0;
}

void PIDController::SetProportional(float proportional)
{
  kp = proportional;
  PID_P = 0;

  #ifdef OUTPUT_PID_CONFIGURATION_INFO
    Serial.print("New PID Proportional: ");
    Serial.println(kp);
  #endif
}

void PIDController::SetIntegral(float integral)
{
  ki = integral;
  PID_I = 0;

  #ifdef OUTPUT_PID_CONFIGURATION_INFO
    Serial.print("New PID Integral: ");
    Serial.println(ki);
  #endif
}

void PIDController::SetDerivative(float derivative)
{
  kd = derivative;
  PID_D = 0;

  #ifdef OUTPUT_PID_CONFIGURATION_INFO
    Serial.print("New PID Derivative: ");
    Serial.println(kd);
  #endif
}

void PIDController::UpdateInput(float input)
{
  float timeDelta = (millis() - timePrev);

  if (timeDelta < refreshInterval)
  {
    return;
  }
  
  PID_error = target - input;
  PID_P = kp * PID_error;
  PID_I = PID_I + (ki * PID_error * timeDelta);
  PID_D = kd*((PID_error - previous_error)/timeDelta);
  PID_value = PID_P + PID_I + PID_D;

  if (PID_value > max)
  {
    PID_value = max;
  }
  else if (PID_value < min)
  {
    PID_value = min;
  }

  if (PID_P > max)
  {
    PID_P = max;
  }
  else if (PID_P < min)
  {
    PID_P = min;
  }

  if (PID_I > max)
  {
    PID_I = max;
  }
  else if (PID_I < min)
  {
    PID_I = min;
  }

  if (PID_D > max)
  {
    PID_D = max;
  }
  else if (PID_D < min)
  {
    PID_D = min;
  }
  
  previous_error = PID_error;
  timePrev = millis();

  #ifdef OUTPUT_PID_CONTROLLER_INFO
    String buf;
    buf += String(input, 1);
    buf += F("\t");
    buf += String(PID_value, 1);
    buf += F("\t");
    buf += String(PID_P, 1);
    buf += F("\t");
    buf += String(PID_I, 1);
    buf += F("\t");
    buf += String(PID_D, 1);
    Serial.println(buf);
  #endif
}

float PIDController::GetOutput()
{
  return PID_value;
}
