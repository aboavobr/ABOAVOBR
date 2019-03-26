#ifndef PIDController_h_
#define PIDController_h_

#include "Arduino.h"

class PIDController
{
  public:
    PIDController(float target, float max, float min, float kp, float ki, float kd, int refreshInterval);
    void UpdateInput(float input);
    float GetOutput();
    void SetProportional(float proportional);
    void SetIntegral(float integral);
    void SetDerivative(float derivative);
    void ResetPID();

  private:
    float target, max, min, kp, ki, kd;
    float PID_P, PID_I, PID_D;
    float PID_error, previous_error;
    float elapsedTime;
    float PID_value = 0;
    int refreshInterval;
    unsigned long timePrev;
};

#endif
