#include <SoftwareSerial.h>
//SoftwareSerial BTserial(8, 9);//RX/TX
/*
#define rxPin 10
#define txPin 11
SoftwareSerial BTSerial(rxPin, txPin); // RX, TX

*/


//BTSerial.begin(38400);

#define bt Serial2 

#include<Servo.h>

int PIN_F_TRIGGER=46;
int PIN_F_ECHO=40;
int PIN_R_TRIGGER=26;
int PIN_R_ECHO=36;
long duration;
//long distance=2000;
int distance1;
int distance2;


int motor_PIN= 6;
Servo mymotor;

int SERVO1_PIN= 7;
Servo myservo;

int blueToothReceived=0;



void setup() {
  // put your setup code here, to run once:
mymotor.attach(motor_PIN); 
myservo.attach(SERVO1_PIN);


//mymotor.write(85);
//delay(1000);
Serial.begin(9600);
pinMode(PIN_F_TRIGGER,OUTPUT);
pinMode(PIN_F_ECHO,INPUT);
pinMode(PIN_R_TRIGGER,OUTPUT);
pinMode(PIN_R_ECHO,INPUT);

Serial2.read();
Serial2.println(bt);



}


void loop() {
  // put your main code here, to run repeatedly:
/*

myservo.write(90);
mymotor.write(109);


if(distance2<80){
myservo.write(90);
mymotor.write(109);
delay(1000);
myservo.write(90);
mymotor.write(70);
delay(1000);
myservo.write(45);
mymotor.write(109);
delay(1000);



}
*/
if(bt.available()>0){
  blueToothReceived=bt.read();
  Serial.print(blueToothReceived);
}

if(blueToothReceived=='F'){
MoveFront();
  
  }

if(blueToothReceived=='D'){
MoveBack();
  
  
  }

  if(blueToothReceived=='L'){
MoveLeft();
  
  
  }
  if(blueToothReceived=='R'){
MoveRight();
  
  
  }

if(blueToothReceived=='S'){
Stop();
  
  
  }
  
  
/*
if(distance2<40){
  mymotor.write(60);
  myservo.write(90);
}
*/
else

{
  
//myservo.write(120);
//mymotor.write(120);
//delay(1000);
}


//Serial.println(duration);
distance1=((distance(PIN_F_TRIGGER,PIN_F_ECHO))/58);
distance2=((distance(PIN_R_TRIGGER,PIN_R_ECHO))/58);
Serial.println(distance1);
delay(100);
Serial.println(distance2);
delay(100);
  }


 long distance(int trigger, int echo){
    digitalWrite(trigger,LOW);
    delayMicroseconds(2);
   digitalWrite(trigger,HIGH);
    delayMicroseconds(10);
    digitalWrite(trigger,LOW);
 return pulseIn(echo,HIGH);//*0.034/2;
  delay(1000);
 //duration = pulseIn(echo, HIGH);
  //distance=duration*0.034/2;

 }

void MoveFront(){
  myservo.write(90);
mymotor.write(109);
  
  }

  void MoveBack(){
myservo.write(90);
mymotor.write(70);
  
  }
    void MoveRight(){
myservo.write(45);
mymotor.write(109);
  
  }
   void MoveLeft(){
myservo.write(135);
mymotor.write(109);
  
  }

    void Stop(){
myservo.write(90);
mymotor.write(90);
  
  }

 
