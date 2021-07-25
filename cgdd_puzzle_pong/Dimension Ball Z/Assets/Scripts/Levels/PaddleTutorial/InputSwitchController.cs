using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSwitchController : MonoBehaviour
{
   public GameObject mouse;
   public GameObject keyboard;



   public void Switch(bool mouseBool)
   {
      if (mouseBool)
      {
         keyboard.SetActive(false);
         mouse.SetActive(true);
      }
      else
      {
         keyboard.SetActive(true);
         mouse.SetActive(false);
      }
   }
}
