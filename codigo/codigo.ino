#include <TimerOne.h>  // Inclua a biblioteca TimerOne

const int pwmPin = 9;  // Pino PWM
String data;
char dl;
int dutyCycle = 460;
int periodo = 500;

void setup() {
  Serial.begin(9600);  // Inicializa a comunicação serial com a taxa de 9600 bps
  Timer1.initialize(periodo);  // Inicializa o Timer1 com um período de 500 microsegundos (2 kHz)
  Timer1.pwm(pwmPin, dutyCycle); // Configura o PWM no pino 9 com um duty cycle de 50%
  pinMode(13, OUTPUT);  // Define o pino 13 como saída
}

void loop() {
  serialControl();  // Chama a função para controlar a comunicação serial
  Timer1.setPwmDuty(pwmPin, dutyCycle);  // Ajusta o duty cycle do PWM
}

void serialControl() {
  if (Serial.available()) {  // Verifica se há dados disponíveis na porta serial
    data = Serial.readString();  // Lê a string da porta serial
    dl = data.charAt(0);  // Obtém o primeiro caractere da string
    String buffer;  // Declara uma variável para armazenar o valor do buffer
    switch (dl) {
      case 'a':  // Caso 'a'
        digitalWrite(13, LOW);  // Define o pino 13 como LOW (desliga)
        break;
      case 'A':  // Caso 'A'
        digitalWrite(13, HIGH);  // Define o pino 13 como HIGH (liga)
        break;
      case 'f':  // Caso 'f'
        buffer = data.substring(1);  // Extrai o valor após o 'f'
        periodo = 1000000 / buffer.toInt();  // Calcula o período em microsegundos
        Timer1.setPeriod(periodo);  // Ajusta o período do Timer1
        break;
      case 'd':  // Caso 'd'
        buffer = data.substring(1);  // Extrai o valor após o 'd'
        dutyCycle = buffer.toInt();  // Converte o valor para inteiro e ajusta o duty cycle
        break;
      case 'H':  // Caso especial para "Hello"
        Serial.println("Hello");  // Responde com "Hello"
        break;
    }
  }
}
