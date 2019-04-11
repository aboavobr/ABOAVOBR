/*
  Gyroscope.h - library for interfacing the MPU-6050 sensor to the balancing robot.
  Code mostly borrowed from Jeff Rowberg's MPU6050 lib example  https://github.com/jrowberg/i2cdevlib
*/

#ifndef Gyroscope_h_
#define Gyroscope_h_

#include "Arduino.h"
#include "I2Cdev.h"
#include "Wire.h"
#include "MPU6050_6Axis_MotionApps20.h"

class Gyroscope
{
  public:
    Gyroscope();
    void Loop();
    float GetYaw();
    float GetPitch();
    float GetRoll();
    
  private:
	// class default I2C address is 0x68
	// specific I2C addresses may be passed as a parameter here
	// AD0 low = 0x68 (default for SparkFun breakout and InvenSense evaluation board)
	// AD0 high = 0x69
	MPU6050 mpu;
	//MPU6050 mpu(0x69); // <-- use for AD0 high
};

#endif
