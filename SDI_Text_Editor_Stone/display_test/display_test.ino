int digit_pin = 12;
int digit_clk = 11;
int seg_pin = 10;
int seg_clk = 9;
int count = 0;
byte hexnum[17] = { B00111111,  // 0
                    B00000110,  // 1
                    B01011011,  // 2
                    B01001111,  // 3
                    B01100110,  // 4
                    B01101101,  // 5
                    B01111101,  // 6
                    B00000111,  // 7
                    B01111111,  // 8
                    B01100111,  // 9
                    B01110111,  // A
                    B01111100,  // b
                    B00111001,  // C
                    B01011110,  // d
                    B01111001,  // E
                    B01110001,  // F
                    B00000000}; // blank
void setup()
{
  pinMode(digit_pin,OUTPUT);
  pinMode(digit_clk,OUTPUT);
  pinMode(seg_pin,OUTPUT);
  pinMode(seg_clk,OUTPUT);
  digitalWrite(digit_clk,LOW);
}

void loop()
{
  load_val(count);
  count += 1;
  if(count>=32767)
     count = 0;
}

void load_val(int val)
{
  int count = 0;
  byte hex;
  int dig = val%10;
  val /= 10;
  digitalWrite(digit_pin,LOW); 
  for(int k=0;k<5;k++)
  {
     hex = hexnum[dig];
     for(int i = 0; i<8; i++)
     {
       digitalWrite(seg_pin,bitRead(hex,i));
       digitalWrite(seg_clk,HIGH);
       digitalWrite(seg_clk,LOW);
     }
     delay(5);
     digitalWrite(digit_clk,HIGH);
     digitalWrite(digit_clk,LOW); 
     digitalWrite(digit_pin,HIGH);
     if(val==0)
        dig = 16;
     else
     {
        dig = val%10;
        val /= 10;
     }    
  }
}
