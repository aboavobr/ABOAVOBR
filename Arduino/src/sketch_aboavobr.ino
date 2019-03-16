#include "Arduino.h"
#include "helper_3dmath.h"
#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "Wire.h"
#include "Gyroscope.h"
#include "MovementController.h"
#include "MotorController.h"

#define LEFT_MOTOR_ENABLE_PIN 9
#define LEFT_MOTOR_IN_1 4
#define LEFT_MOTOR_IN_2 5

#define RIGHT_MOTOR_ENABLE_PIN 10
#define RIGHT_MOTOR_IN_1 6
#define RIGHT_MOTOR_IN_2 7

Gyroscope *gyroscope;
MovementController *movementController;
MotorController *leftMotor;
MotorController *rightMotor;

void setup() {
  // put your setup code here, to run once:
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
  while(!Serial);
  leftMotor = new MotorController(LEFT_MOTOR_ENABLE_PIN, LEFT_MOTOR_IN_1, LEFT_MOTOR_IN_2);
  rightMotor = new MotorController(RIGHT_MOTOR_ENABLE_PIN, RIGHT_MOTOR_IN_1, RIGHT_MOTOR_IN_2);
  gyroscope = new Gyroscope();
  movementController = new MovementController(gyroscope, leftMotor, rightMotor);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available())
  {
	  int state = Serial.parseInt();
	  
	  if (state == 0)
	  {
		 digitalWrite(LED_BUILTIN, LOW);
     //rightMotor->SetDirection(Direction::Forward);
	  }
	  else if(state == 1)
	  {
		  digitalWrite(LED_BUILTIN, HIGH);
      //rightMotor->SetDirection(Direction::Backward);
	  }
	  else
	  {
		Serial.write("nope");
	  }
  }
  
  gyroscope->Loop();
  movementController->Loop();
}
