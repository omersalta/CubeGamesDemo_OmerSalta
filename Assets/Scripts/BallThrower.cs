using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour {
    
   
    private Pool _ballPool;
    [SerializeField] 
    private GameObject markPrefab;
    
    void Start() {
        _ballPool = FindObjectOfType<Pool>();
    }

    public void ChangeBallPool(Pool pool) {
        _ballPool = pool;
    }

    public void GetBallAndThrow(ThrowParam _throwParam) {
        
        var ball = _ballPool.GetNextBall();
        
        if (ball == null) {
            Debug.LogWarning("there is no ball available");
            return;
        }
        
        
        ball.transform.position = _throwParam.StartPos;
        ball.timeScale = _throwParam.AnimSpeed;
        ball.Bounciness = _throwParam.bouncinnes;
        ball.BounceTreshold = _throwParam.nobounceThreshold;
        ball.myMark = Instantiate(markPrefab, _throwParam.EndPos, Quaternion.identity);
        ball.AlignGroundAutomaticly();
        float projectileMotionTotalTime = 2*Mathf.Sqrt((_throwParam.Hmax / (Ball.gravityForce)) );
        if (projectileMotionTotalTime == 0f) {
            projectileMotionTotalTime = 1;
        }
        //Debug.Log("projectileMotionTotalTime =" + projectileMotionTotalTime);
        float vXaxis = (_throwParam.EndPos.x - _throwParam.StartPos.x) / projectileMotionTotalTime;
        float vYaxis = projectileMotionTotalTime/2 * Ball.gravityForce;
        float vZaxis = (_throwParam.EndPos.z - _throwParam.StartPos.z) / projectileMotionTotalTime;
        
        Vector3 kickForce = new Vector3(vXaxis, vYaxis, vZaxis);
        ball.SetVelocity(kickForce);
        ball.Register(FindObjectOfType<Pool>());//pool are observing after Register()
    }
    
}



public struct ThrowParam {
    
    public Vector3 StartPos { get; set; }
    public Vector3 EndPos { get; set; }
    public float Hmax { get; set; }
    public float AnimSpeed { get; set; }
    public float bouncinnes { get; set; }
    public float nobounceThreshold { get; set; }
    
    public override string ToString() => $"({StartPos}, {EndPos}, {Hmax}, {AnimSpeed}, {bouncinnes}, {nobounceThreshold}";
}
