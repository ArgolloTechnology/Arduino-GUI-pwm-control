#include <TimerOne.h>  // Inclua a biblioteca TimerOne

const int pwmPin = 9;  // Pino PWM
String data;
char dl;
int dutyCycle = 460; 
int periodo = 500;
void setup() {
  Serial.begin(9600);
  Timer1.initialize(periodo);  // Inicializa o Timer1 com um período de 500 microsegundos (2 kHz)
  Timer1.pwm(pwmPin, dutyCycle); // Configura o PWM no pino 9 com um duty cycle de 50%
  pinMode(13,OUTPUT);
}

void loop() {
  serialControl();
  Timer1.setPwmDuty(pwmPin, dutyCycle);
}

void serialControl(){
  if(Serial.available()){
    data = Serial.readString();
    dl = data.charAt(0);
    String buffer;
    switch (dl){
      case 'a':
        digitalWrite(13,LOW);
      break;
      case 'A':
        digitalWrite(13,HIGH);
      break;
      case 'f':
        buffer = data.substring(1);
        periodo = 1000000/buffer.toInt();
        Timer1.setPeriod(periodo);
      break;
      case 'd':
        buffer = data.substring(1);
        dutyCycle = buffer.toInt();
      break;
    }
  }
}