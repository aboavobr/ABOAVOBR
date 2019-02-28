void setup() {
  // put your setup code here, to run once:
  pinMode(LED_BUILTIN, OUTPUT);
  Serial.begin(9600);
  while(!Serial);
}

void loop() {
  // put your main code here, to run repeatedly:
  if (Serial.available())
  {
	  int state = Serial.parseInt();
	  
	  if (state == 0)
	  {
		 digitalWrite(LED_BUILTIN, LOW);
	  }
	  else if(state == 1)
	  {
		  digitalWrite(LED_BUILTIN, HIGH);
	  }
	  else
	  {
		Serial.write("nope");
	  }
  }
  
  delay(1000);
}
