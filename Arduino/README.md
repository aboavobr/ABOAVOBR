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

    docker run --rm -it -v "$(pwd):/src" --device=/dev/ttyACM0 strm/dev-arduino upload  

**Note:** The uploading only works on a linux device, as passing through devices within Docker for Linux is not supported.  
Also if your device named differently accordingly adjust the path.

## Continuous Integration and Deplyoment
A CI build to verify the sketch is setup in [Azure Devops](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build?definitionId=34&_a=summary).  
The current state can be seen from the main page via the build badge:  
[![Build status](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_apis/build/status/ABOAVOBR-Arduino-CI)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build/latest?definitionId=34)  

After every push to master the sketch is built and a new Docker Image is created and uploaded to [Docker Hub](https://hub.docker.com/r/aboavobr/arduino/).  
This image will automatically flash the Arduino from the Raspberry Pi when executed, this makes it easy to test updates for the Arduino Sketch without having to dismount it, just update your image after the push on the Raspberry with:
  
    docker pull aboavobr/arduino  

And then upload to the Arduino with:  

    docker run --device=/dev/ttyACM0 aboavobr/arduino  

**Disclaimer:** The container is still a work in progress and does not yet function as it should