using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{

void Update(){
   if( Keyboard.current.escapeKey.isPressed){
    Debug.Log("bye");
    Application.Quit();

   }
}
}
