#include <LiquidCrystal.h>//bibliotheque de la aficheur lcd
LiquidCrystal lcd(2,3,4,5,6,7);//Définit les bornes utilisées pour communiquer avec l'écran LCD

const int sensor1=A1 ;//analog pin A1 to variable sensor1 (capteur)
int temp;// variable qui stocke la temperature en degree celsius
int tempMin = 0;   // la temperature de marche de moteur
int tempMax =150;   // la maximun temperature dont la quelle le vitesse 100%
int fanSpeed;
int fanLCD;
int dir1PinA = 10;//for clockwise
int dir2PinA = 9;//anticlickwise
int ana=11; //analog pin to variable ana(moteur)

void setup ()//debut de l'execution de programme-est appelee au demarrage du programme ,est executee q une seule fois 
{
Serial.begin(9600);// initialise le port série à 9600 bauds
pinMode(sensor1,INPUT);//configuration pin A1 comme entree 
pinMode(dir1PinA,OUTPUT);//
pinMode(dir2PinA,OUTPUT); //
pinMode (ana,OUTPUT);// configuartion de pin de moteur  comme sortie
lcd.begin(16,2);  
lcd.setCursor(0,0);
lcd.print("hello ");
lcd.setCursor(0,1);//la cursor dans la 2eme ligne
lcd.print("world");
delay(2000);//attendre 2s 
lcd.clear();
}
int readTemp() {  // permet de converture la temperature mesure en degree celsius
  temp = analogRead(sensor1);
  return temp * 0.48828125;//5*100/1024
}

void loop() {  
   temp = readTemp(); // get the temperature
   
   if((temp >=tempMin ) && (temp <= tempMax)) {  // if temperature is higher than minimum temp
       fanSpeed = map(temp, tempMin, tempMax, 0, 255); // la vitesse réelle du ventilateur
       fanLCD = map(temp, tempMin, tempMax, 0, 100);  // vitesse du ventilateur à afficher sur l'écran LCD
       //    mapper un nombre d’une plage de tempMin à tempMax à une plage de 0 à 100
       analogWrite(ana, fanSpeed);  //faire tourner le ventilateur à la vitesse du ventilateur
       digitalWrite(dir1PinA, HIGH);
       digitalWrite(dir2PinA, LOW);
   } 

   if(temp < tempMin) {   // if temp is lower than minimum temp
    fanSpeed = 0;      // le ventilateur ne tourne pas
    fanLCD = 0; 
    analogWrite(ana, fanSpeed); 
    digitalWrite(dir1PinA, HIGH);
    digitalWrite(dir2PinA, LOW);
   } 
   
   if(temp > tempMax) {        // if temp is higher than tempMax
  analogWrite(ana, 255); 
digitalWrite(dir1PinA, HIGH);
  digitalWrite(dir2PinA, LOW);
   }
   
   lcd.setCursor(0,0);
   lcd.print("T");
   lcd.print(String(temp)); //  afficher la température
   lcd.write(223);//223 est le code du caractère °
  lcd.print("C  ");
   
   lcd.setCursor(0,1); 
   lcd.print("v");
   lcd.print(String(fanLCD)); // afficher la vitesse sur LCD
   lcd.print("%   ");
   
   Serial.print(temp);
   Serial.print (',');
   Serial.print(fanLCD);
   Serial.print (',');
    Serial.println(' ');
    delay(1000); pause de 1s 
   
 

   }
