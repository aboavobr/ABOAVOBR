# Expected parameters:
# BoardModel: Defaults to uno
# Serial Port: Defaults to /dev/ttyACM0
BOARD_MODEL="arduino:avr:uno"
PORT="/dev/ttyACM0"

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
  PORT="$2"
fi

sh /main.sh compile sketch_aboavobr/ lib/ $BOARD_MODEL
sh /main.sh upload sketch_aboavobr/ lib/ $BOARD_MODEL $PORT