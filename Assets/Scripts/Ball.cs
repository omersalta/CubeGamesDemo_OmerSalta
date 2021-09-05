using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Observable
{
    public GameObject myMark { get; set; }
    private Vector3 velocity{ get; set; }
    public float timeScale{ get; set; }
    public float BounceTreshold{ get; set; }
    public float Bounciness{ get; set; }
    
    //groundValue should set every each object according to their mesh,scale ex. couse pivot point
    private float groundValue;
    
    public static float gravityForce;
    private bool groundLimited;
    private bool isMoving;
    private float hMax;
    
    
    internal void Awake() {
        //Debug.Log("Awake on physicalObject.cs");
        AlignGroundAutomaticly();
        gravityForce = 9.8f;
        timeScale = 1f;
        BounceTreshold = 0.2f;
        Bounciness = 0.8f;
        groundLimited = true;
        isMoving = false;
        hMax = 0;
    }
    
    private float GetHeigth() {
        //PhysicalObject using Sphare mesh so; we need down ground value couse position pivot is center.
        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        Vector3 objSize = mf.sharedMesh.bounds.size;
        float objHeight = objSize.y * transform.lossyScale.y;
        return objHeight;
    }

    private void AlignGround(float remainderFromPivot) {
        groundValue = remainderFromPivot;
        transform.position = new Vector3(transform.position.x, groundValue, transform.position.z);
    }
    
    public void AlignGroundAutomaticly () {
        Debug.Log("automaticAligned");
        AlignGround(GetHeigth()/2);
    }
    
    void FixedUpdate() {
        
        if (!isMoving) {
            return;
        }
        
        EffectVelocityWithGravity();
        
        if (groundLimited) {
            LimitedUpdate();
        }
        else {
            UnLimitedUpdate();
        }
    }
    
    void UnLimitedUpdate() {
        transform.position += timeScale * velocity * Time.deltaTime;
    }

    void LimitedUpdate() {
        //Debug.Log("velocity:"+velocity);
        if (transform.position.y < groundValue) {
            Debug.LogWarning("ball already bottom of ground...!");
            var pos = transform.position;
            transform.position = new Vector3(pos.x, groundValue + GetHeigth() / 2, pos.z);
            return;
        }
        
        transform.position += timeScale * velocity * Time.deltaTime;
        if (transform.position.y < groundValue) {
            BounceYAxis();
        }
    }
    
    void BounceYAxis() {
        //Debug.Log("Bouncing now");
        velocity = Vector3.Scale(velocity, new Vector3(1f, -1f * Bounciness, 1f));
        //over position from ground on bottom
        var newPos = new Vector3(transform.position.x, groundValue, transform.position.z);
        transform.position = newPos;
        //Debug.Log("velocity :" + velocity + " when Bouncing");
    }

    public void SetVelocity(Vector3 value) {
        isMoving = true;
        velocity = value;
    }
    
    void EffectVelocityWithGravity() {
        bool dir1 = velocity.y > 0;
        velocity += Vector3.down * timeScale * gravityForce * Time.deltaTime;
        if (velocity.y > 0 != dir1 && dir1) {
            Falling();
        }
    }
    
    void Falling() {
        var yPos = transform.position.y;
        hMax = hMax < yPos ? yPos : hMax;
        if (yPos < BounceTreshold + groundValue) {
            DontBounceMore();
        }
    }
    
    void DontBounceMore() {
        Debug.Log("dont bounce");
        Debug.Log(name+"'s hMax: " + hMax);
        hMax = 0;
        velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, groundValue, transform.position.z);
        isMoving = false;
        Destroy(myMark);
        Notify(gameObject);
    }
    
}
