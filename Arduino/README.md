# Arduino Code

The Arduino is used to control the motor and is responsible that our bot doesn't fall on its nose.

The code for that is stored in the sketches in the src folder. Libraries belong into the lib folder. 

## Build and Upload

### Arduino IDE
To code you can use the official Arduino that can be downloaded from the [Arduino project page](https://www.arduino.cc/en/main/software).  
You can build/verify your sketches with it and directly upload it to a connected Arduino.

### Docker
If you don't want to install the Arduino IDE you can as well make use of the Docker container [strm/dev-arduino](https://hub.docker.com/r/strm/dev-arduino).  
After you installed docker, run the following command from this directory to build the sketch:  
  
    docker run --rm -it -v "$(pwd):/src" strm/dev-arduino build  

To upload the sketch run:  

    docker run --rm -it -v "$(pwd):/src" strm/dev-arduino upload  

The respective build and upload scripts in this folder do contain those commands as well.

## Continuous Integration
A CI build to verify the sketch is setup in [Azure Devops](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build?definitionId=34&_a=summary).  
The current state can be seen from the main page via the build badge:  
[![Build status](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_apis/build/status/ABOAVOBR-Arduino-CI)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build/latest?definitionId=34)

