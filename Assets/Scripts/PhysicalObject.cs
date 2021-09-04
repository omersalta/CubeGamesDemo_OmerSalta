using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicalObject : MonoBehaviour {

    private Vector3 velocity;
    private float groundValue;
    private float minBounceTreshold;
    private float Bounciness;
    private float gravityEffect;
    private bool groundLimited;

    void FixedUpdate() {

        EffectVelocityWithGravity();

        if (groundLimited) {
            LimitedUpdate();
        }
        else {
            UnLimitedUpdate();
        }

    }

    void UnLimitedUpdate() {
        transform.position += velocity * Time.deltaTime;
    }

    void bounceYAxis() {
        velocity = Vector3.Scale(velocity, new Vector3(1f, -1f * Bounciness, 1f));
    }

    void LimitedUpdate() {
        if (transform.position.y < groundValue) {
            Debug.LogWarning("ball already in ground what can ı do");
        }

        transform.position += velocity * Time.deltaTime;
        if (transform.position.y < groundValue) {
            bounceYAxis();
        }
    }

    public void SetVelocity(Vector3 givenV) {
        velocity = givenV;
    }

    public Vector3 GetVelocity() {
        return velocity;
    }

    void EffectVelocityWithGravity() {
        velocity += Vector3.down * gravityEffect * Time.deltaTime;
        if (velocity.y < 0) {
            ImFalling();
        }
    }
    
    void ImFalling() {
        if (transform.position.y < minBounceTreshold) {
            velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, groundValue, transform.position.z);
        }
    }
}
