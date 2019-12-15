# ABOAVOBR - AutoBalancing Obstacle Avoiding Voice Obedient Beer Robot

| |Arduino|Raspberry Pi|App|
|:--:|:--:|:--:|:--:|
**Builds**|[![Build status](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_apis/build/status/ABOAVOBR-Arduino-CI)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build/latest?definitionId=34)|[![Build status](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_apis/build/status/ABOAVOBR-RaspberryPi-CI)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build/latest?definitionId=35)|[![Build status](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_apis/build/status/ABOAVOBR-Xamarin-CI)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_build/latest?definitionId=36)|
**Deployments**|[![Deployment status](https://benjsawesometfstest.vsrm.visualstudio.com/_apis/public/Release/badge/d5640ac3-2cb3-46e7-85c8-61e1c3b9e255/1/1)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_release?view=all&path=%5C)|[![Deployment status](https://benjsawesometfstest.vsrm.visualstudio.com/_apis/public/Release/badge/d5640ac3-2cb3-46e7-85c8-61e1c3b9e255/2/2)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_release?view=all&path=%5C)|[![Deployment status](https://benjsawesometfstest.vsrm.visualstudio.com/_apis/public/Release/badge/d5640ac3-2cb3-46e7-85c8-61e1c3b9e255/3/3)](https://benjsawesometfstest.visualstudio.com/ABOAVOBR/_release?view=all&path=%5C)|
**Release**|[![Docker Hub](https://img.shields.io/docker/pulls/aboavobr/arduino.svg?style=plastic)](https://hub.docker.com/r/aboavobr/arduino/)|[![Docker Hub](https://img.shields.io/docker/pulls/aboavobr/raspberry.svg?style=plastic)](https://hub.docker.com/r/aboavobr/raspberry/)|[![APK Download](https://img.shields.io/badge/APK-Download-success.svg)](https://github.com/aboavobr/Android-Apk-Repo/raw/master/0.0.1/com.aboavobr.phone.apk)|

The intention of this project is to provide you with the necessary details to 3D print, assemble and program your own autobalancing robot with some quite advanced features.

## System Architecture
![](./Documents/SystemDiagram.png)

## Bill of Materials
A detailed list with the materials needed including the 3D Model can be found [here](./Documents/BOM.md)

## Assembly instructions
TODO: Add 3d models and reference them here for printing.
![Img1](./Construction/20190818_162025.jpg)

Fasten the motor mounts in the bottom part of the robot
![](./Construction/20190818_165857.jpg)

![](./Construction/20190818_165904.jpg)

Lengthen the red and white wires (VCC, GND) coming out from the motors
![](./Construction/20190818_172829.jpg)

Mount the motors to the mounts
![](./Construction/20190818_173135.jpg)

![](./Construction/20190818_173138.jpg)

![](./Construction/20190818_173141.jpg)

![](./Construction/20190818_173156.jpg)

![](./Construction/20190818_174238.jpg)

Mount the Arduino board in the following way to avoid putting pressure on the solder points under the board
![](./Construction/20190818_175032.jpg)

Mount the Arduino board in the following position
![](./Construction/20190818_175536.jpg)

![](./Construction/20190818_175544.jpg)

Solder the MPU-6050 connectors like this. Try to make it as straight as possible.
![](./Construction/20190818_175742.jpg)

![](./Construction/20190818_180144.jpg)

Mount the MPU-6050 board in a similar way to the Arduino board to avoid pressure on the solder points.
![](./Construction/20190818_180807.jpg)

![](./Construction/20190818_180953.jpg)

![](./Construction/20190818_181000.jpg)

Mount the motor driver in a similar way.
![](./Construction/20190818_181622.jpg)

Mount the DC to USB converter in a similar way.
![](./Construction/20190818_182159.jpg)

![](./Construction/20190818_182206.jpg)

Mount the camera in the following way.
![](./Construction/20190818_182832.jpg)

![](./Construction/20190818_183507.jpg)

![](./Construction/20190818_183520.jpg)

![](./Construction/20190818_183907.jpg)

![](./Construction/20190818_183912.jpg)

![](./Construction/20190818_183920.jpg)

Mount the Raspberry in the following way.
![](./Construction/20190818_184743.jpg)

![](./Construction/20190818_184845.jpg)

Mount the on/off button like this.
![](./Construction/20191209_201055.jpg)

![](./Construction/20191209_201214.jpg)

![](./Construction/20191209_201232.jpg)

![](./Construction/20191209_201254.jpg)

Isolate all wires except the blue and green ones.
![](./Construction/20191209_201535.jpg)

![](./Construction/20191209_201538.jpg)

The main power connections are described here, but you should check the schematics at the end of the construction for a better overview.
Connect the On/Off button green wire to minus on the battery connector
![](./Construction/20191211_203137.jpg)

Connect the battery positive connector to two wires
One connected to the DC to USB converter +
One connected to the motor controller +
![](./Construction/20191211_201144.jpg)

![](./Construction/20191211_201149.jpg)

![](./Construction/20191211_202011.jpg)

Connect the On/Off button blue wire to
DC to USB converter - (ground)
Motor controller - (ground)
![](./Construction/20191211_202135.jpg)

![](./Construction/20191211_202011.jpg)

Schematics
![](./Construction/ABOAVOBR_schematic_bb.jpg)

![](./Construction/ABOAVOBR_schematic_schem.jpg)
TODO: Add instructions here

Starting point for collecting ideas:
https://www.instructables.com/id/Arduino-Balance-Balancing-Robot-How-to-Make/

