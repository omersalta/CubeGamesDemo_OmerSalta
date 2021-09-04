using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrower : MonoBehaviour {
    
    private Vector3 lastClickPos;
    private List<Vector3> clickPosList = new List<Vector3>();
    [SerializeField] private int clickPosListCapacity;
    public Pool ballPool;
    
    void Start() {
        ballPool = FindObjectOfType<Pool>();
    }

    // Update is called once per frame
    void Update() {
        
        #region input
        var mCam = Camera.main;
        if (Input.GetMouseButtonUp(0)) {
            
            Ray mouseRay = mCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit curentHit;
            
            if (Physics.Raycast(mouseRay, out curentHit)) {
                lastClickPos = curentHit.point;
                //Debug.Log("clickPos:"+ lastClickPos);
                PushClickList();
            }
        }
        #endregion
    }

    private void PushClickList() {
        if (clickPosList.Count < clickPosListCapacity) {
            clickPosList.Add(lastClickPos);
            if (clickPosList.Count == clickPosListCapacity) {
                PrintClickList();
                clickPosList.Clear();
                
            }
        }else {
            Debug.LogWarning("list Over Capacity!!!!");
            PrintClickList();
            clickPosList.Clear();
        }
    }
    

    public void GetBallAndThrow() {
        var ball = ballPool.GetNextBall();
        Vector3 kickForce = new Vector3(5, 5, 5);
        ball.KickBall(kickForce);
    }
    
    private void PrintClickList() {
        int i = 0;
        foreach (var item in clickPosList) {
            
            Debug.Log(i+". "+ item);
            i++;
        }
    }
    
    
   
    
    
    
}
