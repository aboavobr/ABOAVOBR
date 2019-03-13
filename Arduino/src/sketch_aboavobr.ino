#include "Arduino.h"
#include "helper_3dmath.h"
#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "Wire.h"
#include "Gyroscope.h"
#include "MovementController.h"

Gyroscope *gyroscope;
MovementController *movementController;

void setup() {
  // put your setup code here, to run once:
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
  while(!Serial);
  gyroscope = new Gyroscope();
  movementController = new MovementController(gyroscope);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available())
  {
	  int state = Serial.parseInt();
	  
	  if (state == 0)
	  {
		 digitalWrite(LED_BUILTIN, LOW);
	  }
	  else if(state == 1)
	  {
		  digitalWrite(LED_BUILTIN, HIGH);
	  }
	  else
	  {
		Serial.write("nope");
	  }
  }
  
  gyroscope->Loop();
  movementController->Loop();
}
