# Arduino Build Environment

This container is used in order to build and upload the Arduino Sketches.  
It can be used to build locally, on a build server and serves also as a base for the aboavobr/arduino image, so that it automatically buids and uploads the newest sketch upon execution.

The image is making use of the [arduino-cli](https://github.com/arduino/arduino-cli).

## Docker
The image is automatically built and pushed to docker as soon as any change is made to it.  

It can be found in the docker hub under *aboavobr/arduino-buildenv*  

## Usage
To get the latest image, just pull it from docker:  

    docker pull aboavobr/arduino-buildenv
    
To build a sketch its easiest if you specify the folder your sketch is in as volume and call the compile command:  

    docker run -it -v "$(pwd):/src" aboavobr/arduino-buildenv compile sketch_aboavobr/ lib/  
    
This will map the current directory to the src folder in the container, call the compile command of the arduino-cli and specify the folder in which the sketch is located (sketch_aboavobr).  
If there are any extra libraries that are used, that folder has to be specified too.

Additional arguments that could be passed are the arduino board model (it will default to *arduino:avr:uno* if nothing else specified).  
You can see more information on the board models on the [arduino-cli github page](https://github.com/arduino/arduino-cli).  

In case you want to upload a sketch you have to specify also the Port where the Arduino is connected to:  

    docker run -it -v "$(pwd):/src" --device=/dev/ttyACM0 aboavobr/arduino-buildenv upload sketch_aboavobr/ lib/ arduino:avr:uno /dev/ttyACM0
    
**Make sure that you specify the *device* parameter when you run the container so that it's available to the arduino-cli**

Uploading is not possible on a Windows machine, as docker for windows does not support specifying the device parameter
