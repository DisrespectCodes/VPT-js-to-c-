// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class CarControleScript : MonoBehaviour {
//@script AddComponentMenu ("CarPhys/Scripts/Car Control Script")

Vector3 centerOfMass;	//Center of mass
WheelCollider dataWheel;	//Wheel Collider from which you want to calculate the speed
float lowestSteerAtSpeed = 50;	//if lowestSteerAtSpeed < currentSpeed the steer Angle = highSpeedSteerAngel
float lowSpeedSteerAngel = 10;	//This could be a high value
float highSpeedSteerAngel = 1;	//This shouldn't be a high value (recomended for stability of car)
float decellarationSpeed = 30;	//How fast the car will decellarate
float maxTorque  = 50;	//Maximum Torque
float currentSpeed;		//Current Speed of car
float topSpeed = 150;		//Highest speed at which the car can go
float maxReverseSpeed = 50; 	//Highest Reverse speed
GameObject backLightObject;	//Mesh for reverse light
Material idleLightMaterial;	//for idle state
Material brakeLightMaterial; 	//Braked state
Material reverseLightMaterial;	//Reverse state
//@HideInInspector	
bool  braked = false;	//Brake trigger
float maxBrakeTorque = 100; 	//Braking speed
Texture2D speedOMeterDial;	//GUI Texture for dial
Texture2D speedOMeterPointer;		//GUI Texture for needle
int[] gearRatio;		//Shift gear at speed
//GameObject spark;		//OnCollision Spark
GameObject collisionSound;	//OnCollision Sound
int minAnglePointer = -90;
int maxAnglePointer = 180;

void  Start (){
GetComponent<Rigidbody>().centerOfMass=centerOfMass; //Center of mass , for this the car should be pointing on z axis
}

void  FixedUpdate (){
HandBrake();
}
void  Update (){
BackLight ();
EngineSound();
CalculateSpeed();
}

//Speed Calculation

void  CalculateSpeed (){
currentSpeed = 2*22/7*dataWheel.radius*dataWheel.rpm*60/1000;
currentSpeed = Mathf.Round(currentSpeed);
}

//Light Control

void  BackLight (){
if (currentSpeed > 0 && Input.GetAxis("Vertical")<0&&!braked){
backLightObject.GetComponent<Renderer>().material = brakeLightMaterial;
}
else if (currentSpeed < 0 && Input.GetAxis("Vertical")>0&&!braked){
backLightObject.GetComponent<Renderer>().material = brakeLightMaterial;
}
else if (currentSpeed < 0 && Input.GetAxis("Vertical")<0&&!braked){
backLightObject.GetComponent<Renderer>().material = reverseLightMaterial;
}
else if (!braked){
backLightObject.GetComponent<Renderer>().material = idleLightMaterial;
}
}

//Brake Trigger

void  HandBrake (){
if (Input.GetButton("Jump")){
braked = true;
}
else{
braked = false;
}
}
//Engine Sound

void  EngineSound (){
for (FIXME_VAR_TYPE i= 0; i < gearRatio.length; i++){
if(gearRatio[i]> currentSpeed){
break;
}
}
float gearMinValue = 0.00f;
float gearMaxValue = 0.00f;
if (i == 0){
gearMinValue = 0;
}
else {
gearMinValue = gearRatio[i-1];
}
gearMaxValue = gearRatio[i];
float enginePitch = ((currentSpeed - gearMinValue)/(gearMaxValue - gearMinValue))+1;
GetComponent<AudioSource>().pitch = enginePitch;
}

//Speedometer

void  OnGUI (){
GUI.DrawTexture( new Rect(Screen.width - 300,Screen.height - 300,300,300),speedOMeterDial);
float speedFactor = currentSpeed / topSpeed;
float rotationAngle;
if (currentSpeed >= 0){
  rotationAngle = Mathf.Lerp(minAnglePointer,maxAnglePointer,speedFactor);
  }
  else {
  rotationAngle = Mathf.Lerp(minAnglePointer,maxAnglePointer,-speedFactor);
  }
GUIUtility.RotateAroundPivot(rotationAngle,Vector2(Screen.width - 150 ,Screen.height - 150));
GUI.DrawTexture( new Rect(Screen.width - 300,Screen.height - 300,300,300),speedOMeterPointer);



}

//CollisioN FX

void  OnCollisionEnter ( Collision other  ){

if (other.transform != transform && other.contacts.length != 0){
for (FIXME_VAR_TYPE i= 0; i < other.contacts.length ; i++){
//Instantiate(spark,other.contacts[i].point,Quaternion.identity);
GameObject clone = Instantiate(collisionSound,other.contacts[i].point,Quaternion.identity);
clone.transform.parent = transform;
}
}
}

void  OnDrawGizmos (){
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere (transform.position+centerOfMass, 0.1f);
    }

}