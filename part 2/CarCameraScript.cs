// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class CarCameraScript : MonoBehaviour {
//@script AddComponentMenu ("CarPhys/Scripts/Car Camera Script")

Transform car;	//Car Transform
float distance = 6.4f;		//Distance from car
float height = 1.4f; 	//Value on Y axis according to car transform
float rotationDamping = 3.0f;	 //lower the value , faster the damping will be
float heightDamping = 2.0f;	 //lower the value , faster the damping will be
float zoomRacio = 0.5f;	//Change on FOV
float DefaultFOV = 60;	//Min FOV
bool  rotate = true;	//Look Back While Reversing
private Vector3 rotationVector;	//Rotation Vector

//Positioning

void  LateUpdate (){
float wantedAngel= rotationVector.y; //me
float wantedHeight= car.position.y + height; //me
float myAngel= transform.eulerAngles.y; //me
float myHeight= transform.position.y; //me
myAngel = Mathf.LerpAngle(myAngel,wantedAngel,rotationDamping*Time.deltaTime);
myHeight = Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
float currentRotation= Quaternion.Euler(0,myAngel,0); //me
transform.position = car.position;
transform.position -= currentRotation*Vector3.forward*distance;
transform.position.y = myHeight;
transform.LookAt(car);
}
}


//Rotation And FOV Control

/* void FixedUpdate (){
var localVilocity = car.InverseTransformDirection(car.rigidbody.velocity);
if (localVilocity.z<-0.5 && rotate){
rotationVector.y = car.eulerAngles.y + 180;
}
else {
rotationVector.y = car.eulerAngles.y;
}
var acc = car.rigidbody.velocity.magnitude;
camera.fieldOfView = DefaultFOV + acc*zoomRacio;
} */


