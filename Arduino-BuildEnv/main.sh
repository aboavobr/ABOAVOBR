# First Input whether to compile or upload
# Second is Board Model, default is the key for the arduino uno (arduino:avr:uno)
# Third is the port to use (only needed for upload)
# Check https://github.com/arduino/arduino-cli for Actions etc.
ACTION="compile" #upload
SKETCH=""
BOARD_MODEL="arduino:avr:uno"
PORT=""


if [ -z "$1" ]
  then
    echo "No action specified, will use compile"
else
  echo "Using the specified action: " $1
  ACTION="$1"
fi

if [ -z "$2" ]
  then
    echo "No sketch directory specified, will use current directory"
else
  echo "Following Sketch Directory specified: " $2
  SKETCH="$2"
fi

if [ -z "$3" ]
  then
    echo "No Libraries Directory Specified"
else
  echo "Using specified directory as Libraries Directory: " $3
  echo "Copying libraries to correct location"

  mkdir /root/Arduino
  mkdir /root/Arduino/libraries
  cp -a $3/* /root/Arduino/libraries
fi

if [ -z "$4" ]
  then
    echo "No board model specified - will use uno"
else
  echo "Using specified board model: " $4
  BOARD_MODEL="$4"
fi

if [ -z "$5" ]
  then
    echo "No serial port specified - will use default"
else
  echo "Using specified Port: " $5
  PORT="-p $5"
fi

arduino-cli $ACTION $PORT --fqbn $BOARD_MODEL $SKETCH