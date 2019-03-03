# First Input is Board Model, default is uno (see ino build --help)
# For the full list of board names refer to ino build --help
# See here for more help: http://inotool.org/quickstart#tweaking-parameters
BOARD_MODEL="uno"
PORT=""

if [ -z "$1" ]
  then
    echo "No board model specified - will use uno"
else
  echo "Using specified board model: " $1
  BOARD_MODEL="$1"
fi

if [ -z "$2" ]
  then
    echo "No serial port specified - will use default"
else
  echo "Using specified Port: " $2
  PORT="-p $2"
fi



ino build -m $BOARD_MODEL $PORT
ino upload -m $BOARD_MODEL $PORT