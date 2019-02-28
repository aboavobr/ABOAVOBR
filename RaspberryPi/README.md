# Asp.Net Core Raspberry REST API

The Raspberry Pi is used to interact between the User and the Arduino.  
For this a REST Interface is offered which is taking commands from external sources (e.g. the Phone Application) and instrcuts the Arduino via the Serial Connection.

## Development
The *aboavobr.raspberry* Solution contains an Asp.Net Core Project that can be built and run via Visual Studio.  

**Important**: When adding new dependencies make sure that they are compliant with .NET Standard 2.0 to support running the application on all platforms.

### REST API
TODO: Add documentation of REST API once it's defined and done

### Architecture
TODO: Add once it's defined and done

### Debug and Test
Debugging works out of the box when using Visual Studio, just put your breakpoints in the Controllers and Services and start it in Debug Mode. You can then either make requests via a normal browser or use a dedicated tool like [Postman](https://www.getpostman.com/).  

Basic functionality should be covered by unit tests, a dedicated project exists in the solution.  

#### Serial Port Configuration
In order to be able to test without an actual Serial Port Connection there is the option to specify whether a Fake Port shall be used. In the *appsettings.Development.json* it can be specified whether to use a Fake Port:  

    "FakePort": {
      "IsActive": false,
      "SerialOutputFile": "bin\\Debug\\serialout.log",
      "SerialInputFile": "bin\\Debug\\serialin.txt"
       },  

If this is active you can specify two files, in the *OutputFile* all messages that are written to the device will be appended, so you can determine whether the correct commands would be sent. In the *InputFile* you can write something and upon Save the *FakePort* implementation will register this as if it would have been input coming from the connected Serial Port.  
Be aware that always the full file is read as input, so be sure to clear previous inputs before saving again.  

By default the files are located in the *bin/Debug* directory of web project, however you can change this path to go wherever you want it.   

**This option is only be available in the Debug Environments!**

## Run and Deploy
You can the application in your Debug Environment normally via Visual Studio. Once it's up you can access it via

    http://localhost:5000/api/debug

If everything went fine you should see some message.  

**Be aware that the Application will fail to startup if there is no Serial Device available! For debugging and testing without connected Arduino see above**

### Specifying Serial Port
If you have multiple serial ports available it will by default just select the first one. In that case you can define which port shall be used using the configuration option *SerialPort* from the *appsettings.json*.  
This is for example needed on Windows as you cannot use the *docker --device* switch as it's not supported so you will just have all ports available.  
Another use case would be if you have to run your docker container in privileged mode and have therefore more than one device available.  

The WebApp configuration can also be set via Environment Variables by setting *ASPNETCORE_VariableName*. This can be used together with *dockers -e* switch to set the variable for a container:  

    docker run -d -p 1337:5000 -e ASPNETCORE_SerialPort=/dev/ttyACM0 aboavobr/raspberry

### Docker
There is also a Docker container that is built and published to [Docker Hub](https://hub.docker.com/r/aboavobr/raspberry/) after every push to master that can be used to run the application on your Raspberry Pi.  

In order to get it up and running first install docker on your Raspberry Pi:  

    curl -fsSL get.docker.com -o get-docker.sh && sh get-docker.sh

To get the latest version of the container run:  

    docker pull aboavobr/raspberry

To start you have to provide a few parameters together with the command:  

    docker run -d -p 1337:5000 --device=/dev/ttyACM0 aboavobr/raspberry

This will run the container in detached mode (*-d*) and will map the requests coming in on port 1337 to the web app that is listening on port 5000 (*-p 1337:5000*). The rest api will then be available on  

    http://<IP-of-Raspberry/>:1337/api/debug

You can choose whatever port you like, but it has to be mapped to 5000 to match with the configuration of the REST API.  
You also have to specify a device (the Arduino USB connection device) as the container can not access the devices otherwise except when run in privileged mode.  
**Your device might be different than the example specified here.**

Last but not least in case you want to do some debugging on the device youc an also run it in foreground mode to observe the logs:  

    docker run -d -it 1337:5000 --device=/dev/ttyACM0 aboavobr/raspberry

### Troubleshooting
Q:How do I fix this "permission denied" message when using docker commands?  
A:Likely you need to add your non root user to the Docker group:  
sudo usermod -aG docker pi  
Where "pi" is your username.

