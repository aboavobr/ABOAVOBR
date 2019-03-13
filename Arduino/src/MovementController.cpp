#include "MovementController.h"
#include "Arduino.h"
#include <PID_v1.h>

#define OUTPUT_PID_DATA
//#define OUTPUT_GYROSCOPE_DATA

MovementController::MovementController(Gyroscope *inGyroscope)
{
  gyroscope = inGyroscope;

  Setpoint = 0;
  myPID = new PID(&Input, &Output, &Setpoint, Kp, Ki, Kd, DIRECT);
  myPID->SetMode(AUTOMATIC);
}

void MovementController::Loop()
{
  #ifdef OUTPUT_GYROSCOPE_DATA
    Serial.print("YPR: ");
    Serial.print(gyroscope->GetYaw());
    Serial.print("\t");
    Serial.print(gyroscope->GetPitch());
    Serial.print("\t");
    Serial.println(gyroscope->GetRoll());
  #endif

  Input = gyroscope->GetPitch();
  myPID->Compute();
  
  #ifdef OUTPUT_PID_DATA
    Serial.print("PID: ");
    Serial.println(Output);
  #endif
}
