#include "Arduino.h"
#include "helper_3dmath.h"
#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "Wire.h"
#include "Gyroscope.h"
#include "MovementController.h"
#include "MotorController.h"

#define LEFT_MOTOR_ENABLE_PIN 9
#define LEFT_MOTOR_IN_1 5
#define LEFT_MOTOR_IN_2 4

#define RIGHT_MOTOR_ENABLE_PIN 10
#define RIGHT_MOTOR_IN_1 7
#define RIGHT_MOTOR_IN_2 6

#define OUTPUT_COMMAND_INFORMATION

Gyroscope *gyroscope;
MovementController *movementController;
MotorController *leftMotor;
MotorController *rightMotor;
bool HasZeroedMovementController = false;

// Serial communication protocol:
// {COMMAND}:{VALUE}{\n}
// If the command does not have a value, the protocol looks like this:
// {COMMAND}:{\n}
// Or
// {COMMAND}{\n}
char *serialBuf = new char[15];
int serialBufPos = 0;

void setup() {
  // put your setup code here, to run once:
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(115200);
  while(!Serial);
  leftMotor = new MotorController(LEFT_MOTOR_ENABLE_PIN, LEFT_MOTOR_IN_1, LEFT_MOTOR_IN_2);
  rightMotor = new MotorController(RIGHT_MOTOR_ENABLE_PIN, RIGHT_MOTOR_IN_1, RIGHT_MOTOR_IN_2);
  gyroscope = new Gyroscope();
  movementController = new MovementController(gyroscope, leftMotor, rightMotor);
}

void loop() {
  if (!HasZeroedMovementController && millis() > 5000)
  {
    movementController->SetZeroPitch();
    HasZeroedMovementController = true;
    movementController->Start();
  }

  while (Serial.available() > 0)
  {
    serialBuf[serialBufPos] = Serial.read();

    if (serialBuf[serialBufPos] == '\n')
    {
      serialBufPos = 0;
      handleCommand();
    }
    else
    {
      serialBufPos++;
    }
    
	  //int state = Serial.parseInt();
	  //movementController->SetPIDProportional(state);
  }
  
  gyroscope->Loop();
  movementController->Loop();
}

void handleCommand()
{
  //Serial.println("Parsing command");
  String command = "";
  String valueString = "";
  int parsePos = 0;
  while (serialBuf[parsePos] != ':' && serialBuf[parsePos] != '\n')
  {
    command += serialBuf[parsePos];
    parsePos++;
  }
  parsePos++;
  //Serial.println(command);

  while (serialBuf[parsePos] != '\n')
  {
    valueString += serialBuf[parsePos];
    parsePos++;
  }
  //Serial.println(valueString);

  if (command == "PI")
  {
    float value = valueString.toFloat();
    movementController->SetPIDIntegral(value);

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.print("Setting PID Integral: ");
      Serial.println(value);
    #endif    
  }
  else if (command == "PP")
  {
    float value = valueString.toFloat();
    movementController->SetPIDProportional(value);

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.print("Setting PID Proportional: ");
      Serial.println(value);
    #endif
  }
  else if (command == "PD")
  {
    float value = valueString.toFloat();
    movementController->SetPIDDerivative(value);

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.print("Setting PID Derivative: ");
      Serial.println(value);
    #endif
  }
  else if (command == "ZP")
  {
    movementController->SetZeroPitch();

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.println("Setting zero pitch angle");
    #endif
  }
  else if (command == "ST")
  {
    movementController->Start();

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.println("Starting");
    #endif
  }
  else if (command == "SP")
  {
    movementController->Stop();

    #ifdef OUTPUT_COMMAND_INFORMATION
      Serial.println("Stopping");
    #endif
  }
}
