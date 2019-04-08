#include "SimPlot.h"

SimPlot::SimPlot()
{
  
}

void SimPlot::Plot(int data1, int data2, int data3, int data4)
{
  int* buffer = new int[20];
  
  int pktSize;
  
  buffer[0] = 0xCDAB;             //SimPlot packet header. Indicates start of data packet
  buffer[1] = 4*sizeof(int);      //Size of data in bytes. Does not include the header and size fields
  buffer[2] = data1;
  buffer[3] = data2;
  buffer[4] = data3;
  buffer[5] = data4;
    
  pktSize = 2 + 2 + (4*sizeof(int)); //Header bytes + size field bytes + data
  
  //IMPORTANT: Change to serial port that is connected to PC
  Serial.write((uint8_t * )buffer, pktSize);

  delete[] buffer;
}
