using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    public bool touchMoon;
    public void OnMouseDown() { touchMoon = true; }
        public void OnMouseUp() { touchMoon = false; }
    
}
