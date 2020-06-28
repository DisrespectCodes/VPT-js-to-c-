// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class SkiddingScript : MonoBehaviour {
/*
Script Created by FlatTutorials for "Car Controller kit".
*/

//@script AddComponentMenu ("CarPhys/Scripts/SkiddingScript")

private float currentFrictionValue;	//Calculates the current friction on the wheel
float skidAt = 1.5f;	//if current friction < skidAt the generate marks
float soundEmition = 15;	//instantiation of (no. of sound prefab per second)
private float soundWait;	//Time counter
GameObject skidSound;		//Skidsound Prefab
GameObject skidSmoke;		//Smoke Particle system
float smokeDepth = 0.4f;	//instantiate above the ground
float markWidth = 0.2f;	//Skid mark Width
bool  startSkid;		//Burnout FX
private int skidding;		//skidding started or not
private FIXME_VAR_TYPE lastPos= new Vector3[2]; 	//Saves the last position of skidmark
Material skidMaterial;	//Skidmark texture

//Positioning the smoke
void  Start (){
skidSmoke.transform.position = transform.position;
skidSmoke.transform.position.y -= smokeDepth;
}

//Friction calculation and triggers for the effects
void  Update (){
WheelHit hit;
transform.GetComponent<WheelCollider>().GetGroundHit(hit);
currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
FIXME_VAR_TYPE rpm= transform.GetComponent<WheelCollider>().rpm;
if (skidAt <= currentFrictionValue && soundWait <= 0 || rpm < 300 && rpm > 10 && Input.GetAxis("Vertical")>0 && soundWait <= 0 && startSkid && hit.collider){
Instantiate(skidSound,hit.point,Quaternion.identity);
soundWait = 1;
}
soundWait -= Time.deltaTime*soundEmition;
if (skidAt <= currentFrictionValue || rpm < 300 && rpm > 10 && Input.GetAxis("Vertical")>0 && startSkid && hit.collider){
skidSmoke.particleEmitter.emit = true;
SkidMesh();
}
else {
skidSmoke.particleEmitter.emit = false;
skidding = 0;
}
}

//Generates skidmarks

void  SkidMesh (){

WheelHit hit;
transform.GetComponent<WheelCollider>().GetGroundHit(hit);
GameObject mark = new GameObject("Mark");
MeshFilter filter = mark.AddComponent<MeshFilter>();
mark.AddComponent<MeshRenderer>();
Mesh markMesh = new Mesh();
FIXME_VAR_TYPE vertices= new Vector3 [4];
FIXME_VAR_TYPE triangles= new int[6];

if (skidding == 0){
vertices[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(markWidth,0.01f,0);
vertices[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(-markWidth,0.01f,0);
vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(-markWidth,0.01f,0);
vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(markWidth,0.01f,0);
lastPos[0] = vertices[2];
lastPos[1] = vertices[3];
skidding = 1;
}
else {
vertices[1] = lastPos[0];
vertices[0] = lastPos[1];
vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(-markWidth,0.01f,0);
vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*Vector3(markWidth,0.01f,0);
lastPos[0] = vertices[2];
lastPos[1] = vertices[3];
} 

triangles = (0,1,2,2,3,0);
markMesh.vertices = vertices;
markMesh.triangles = triangles;
markMesh.RecalculateNormals();
Vector2[] uvm = new Vector2[4];
uvm[0] = Vector2(1,0);
uvm[1] = Vector2(0,0);
uvm[2] = Vector2(0,1);
uvm[3] = Vector2(1,1);
markMesh.uv = uvm;
filter.mesh = markMesh;
mark.renderer.material = skidMaterial;
mark.AddComponent<DestroyTimerScript>();
}
}