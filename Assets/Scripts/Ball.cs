using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : physicalObject  
{
    
    
    public void KickBall(Vector3 startVelocity) {
        SetVelocity(startVelocity);
    }
    
    public Vector3 GetCurrentVelocity() {
        return GetVelocity();
    }
}