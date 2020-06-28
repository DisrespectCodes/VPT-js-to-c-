// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class DestroyTimerScript : MonoBehaviour {

//@script AddComponentMenu ("CarPhys/Scripts/Destroy Timer Script")

float destroyAfter = 7; 	//Waitin time to destroy a object in seconds
private float timer; 	//Counting time

//Calculation

void  Update (){
timer += Time.deltaTime;
if (destroyAfter <= timer){
Destroy(gameObject);
}
}
}