// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class CarControleNitro : MonoBehaviour {

//@script AddComponentMenu ("CarPhys/Scripts/CarControleNitro")
FIXME_VAR_TYPE thisPC= false;
private FIXME_VAR_TYPE nitro= false;
private FIXME_VAR_TYPE currentTorque= 0.0f;
FIXME_VAR_TYPE maxNitroAccelaration= 50;
FIXME_VAR_TYPE maxTorque= 20;
FIXME_VAR_TYPE nitroVolume= 80f;
FIXME_VAR_TYPE maxNitrovolume= 100;
private FIXME_VAR_TYPE carTorque= 0;
GameObject particleEffectLeft;
GameObject particleEffectRight;
GUISkin guiSkin;

void  Start (){
particleEffectLeft.GetComponent<ParticleEmitter>().emit = false;
particleEffectRight.GetComponent<ParticleEmitter>().emit = false;
particleEffectLeft.GetComponent<LensFlare>().enabled = false;
particleEffectRight.GetComponent<LensFlare>().enabled = false;
}

void  Update (){
Swipe();
Nitro();
carTorque = gameObject.GetComponent<CarControleScript>().maxTorque = currentTorque;
if(nitroVolume<=0){
nitro = false;
nitroVolume = 0;
currentTorque = maxTorque;
particleEffectLeft.GetComponent<ParticleEmitter>().emit = false;
particleEffectRight.GetComponent<ParticleEmitter>().emit = false;
particleEffectLeft.GetComponent<LensFlare>().enabled = false;
particleEffectRight.GetComponent<LensFlare>().enabled = false;
}
if(nitroVolume> maxNitrovolume){
nitroVolume = maxNitrovolume;
}
if(Input.GetKey(KeyCode.Z)){
nitro = true;
}else{
nitro = false;
}
if(Input.touchCount == 0 ){
nitro = false;
}
}
Vector2 touchFacing;
Vector2 initTouchPos;	

void  Swipe (){
int fingerCount = 0;				
if(fingerCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Ended)
{
touchFacing = (initTouchPos - Input.GetTouch(0).position).normalized;					
if(Vector2.Dot(touchFacing, -Vector2.up) > 0.8f && Vector2.Distance(initTouchPos, Input.GetTouch(0).position) > 20)
{
nitro = true;
}
}
}



void  Nitro (){
if(nitro){
currentTorque = maxNitroAccelaration;
nitroVolume -= 1;
particleEffectLeft.GetComponent<ParticleEmitter>().emit = true;
particleEffectRight.GetComponent<ParticleEmitter>().emit = true;
particleEffectRight.GetComponent<LensFlare>().enabled = true;
particleEffectLeft.GetComponent<LensFlare>().enabled = true;
}
if(!nitro){
currentTorque = maxTorque;
nitroVolume += 1;
particleEffectLeft.GetComponent<ParticleEmitter>().emit = false;
particleEffectRight.GetComponent<ParticleEmitter>().emit = false;
particleEffectLeft.GetComponent<LensFlare>().enabled = false;
particleEffectRight.GetComponent<LensFlare>().enabled = false;
}
}


void  OnGUI (){
GUI.skin = guiSkin;

GUI.Label( new Rect(100,50,300,100),"Current torque : " + currentTorque );
GUI.Label( new Rect(100,80,300,100),"Nitro Volume : " + nitroVolume);
}





}