using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour {
    
    private Vector3 _lastClickPos;
    private List<Vector3> _clickPosList = new List<Vector3>();
    [SerializeField] private int clickPosListCapacity;
    private Pool _ballPool;
    
    void Start() {
        _ballPool = FindObjectOfType<Pool>();
        clickPosListCapacity = 2;
    }

    public void ChangeBallPool(Pool pool) {
        _ballPool = pool;
    }

    // Update is called once per frame
    void Update() {
        
        #region input
        var mCam = Camera.main;
        if (Input.GetMouseButtonUp(0)) {
            
            Ray mouseRay = mCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit curentHit;
            
            if (Physics.Raycast(mouseRay, out curentHit)) {
                _lastClickPos = curentHit.point;
                //Debug.Log("clickPos:"+ _lastClickPos);
                PushClickList();
            }
        }
        #endregion
    }

    private void PushClickList() {
        if (_clickPosList.Count < clickPosListCapacity) {
            _clickPosList.Add(_lastClickPos);
            if (_clickPosList.Count == clickPosListCapacity) {
                PrintClickList();
                _clickPosList.Clear();
            }
        }
        Debug.LogWarning("list Over Capacity!!!!");
        PrintClickList();
        _clickPosList.Clear();
    }
    

    public void GetBallAndThrow(ThrowParam _throwParam) {
        var ball = _ballPool.GetNextBall();
        Vector3 kickForce = new Vector3(5, 5, 5);
        ball.velocity = kickForce;
    }
    
    private void PrintClickList() {
        int i = 0;
        foreach (var item in _clickPosList) {
            
            Debug.Log(i+". "+ item);
            i++;
        }
    }
    
}


public struct ThrowParam
{
    public ThrowParam(Vector3 startPos, Vector3 endPos, float hMax, float animSpeed )
    {
        StartPos = startPos;
        EndPos = endPos;
        Hmax = hMax;
        AnimSpeed = animSpeed;
    }
    
    public Vector3 StartPos { get; }
    public Vector3 EndPos { get; }
    public float Hmax { get; }
    public float AnimSpeed { get; }

    public override string ToString() => $"({StartPos}, {EndPos}, {Hmax}, {AnimSpeed})";
}
