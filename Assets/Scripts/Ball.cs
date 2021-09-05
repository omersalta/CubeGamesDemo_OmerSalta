using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Observable
{
    private bool groundLimited;
    private static float gravityForce;
    //groundValue should set every each object according to their mesh,scale ex. couse pivot point
    private float groundValue;
    public Vector3 velocity{ get; set; }
    public float timeScale{ get; set; }
    public float BounceTreshold{ get; set; }
    public float Bounciness{ get; set; }
    
    
    
    internal void Awake() {
        Debug.Log("Awake on physicalObject.cs");
        AlignGroundAutomaticly();
        gravityForce = 9.8f;
        groundLimited = true;
        timeScale = 1f;
        BounceTreshold = 0.2f;
        Bounciness = 0.8f;
        Register(FindObjectOfType<Pool>());
    }
    
    void FixedUpdate() {

        EffectVelocityWithGravity();

        if (groundLimited) {
            LimitedUpdate();
        }
        else {
            UnLimitedUpdate();
        }

    }
    
    public void AlignGroundAutomaticly() {
        //PhysicalObject using Sphare mesh so; we need down ground value couse position pivot is center.
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        Vector3 objSize = mf.sharedMesh.bounds.size;
        float objHeight = objSize.y * transform.lossyScale.y;
        AlignGround(objHeight/2);
    }

    private void AlignGround(float remainderFromPivot) {
        groundValue = remainderFromPivot;
    }

    public void SetGroundValueManually(float value) {
        groundValue = value;
    }
    
    void UnLimitedUpdate() {
        transform.position += timeScale * velocity * Time.deltaTime;
    }

    void BounceYAxis() {
        //Debug.Log("Bouncing now");
        velocity = Vector3.Scale(velocity, new Vector3(1f, -1f * Bounciness, 1f));
        //over position from ground on bottom
        var newPos = new Vector3(transform.position.x, groundValue, transform.position.z);
        transform.position = newPos;
        //Debug.Log("velocity :" + velocity + " when Bouncing");
    }

    void LimitedUpdate() {
        //Debug.Log("velocity:"+velocity);
        if (transform.position.y < groundValue) {
            Debug.LogWarning("ball already in ground what can ı do");
        }
        
        transform.position += timeScale * velocity * Time.deltaTime;
        if (transform.position.y < groundValue) {
            BounceYAxis();
        }
    }
    
    void EffectVelocityWithGravity() {
        velocity += Vector3.down * timeScale * gravityForce * Time.deltaTime;
        isFalling();
    }
    
    void isFalling() {
        if (velocity.y < 0 && velocity.y > -0.1f) {
            //Debug.Log("pos when falling: "+ transform.position);
            if (transform.position.y < BounceTreshold) {
                //Debug.Log("dont bounce ");
                DontBounceMore();
            }
        }
    }
    
    void DontBounceMore() {
        velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, groundValue, transform.position.z);
        Notify(gameObject);
    }
    
}
