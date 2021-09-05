using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pool : Observer {
    
    
    public GameObject ballPrefab;
    private Stack<Ball> ballStack = new Stack<Ball>();
    [SerializeField] private int poolTotalCapacity;
    [SerializeField] private int poolStartBallCount;
    [SerializeField] private int poolExtraBallCount;
    private bool destroyOverabundantBall;
    
    // Start is called before the first frame update
    void Start() {
        destroyOverabundantBall = true;
        CreateBalls(ballStack,poolStartBallCount);
    }
    
    public override void OnNotify(GameObject observable) {
        //PhysicalObject PutBack Notifying
        Ball physicalObject = observable.GetComponent<Ball>();
        if (physicalObject != null) {
            PutBackBall(physicalObject);
        }
    }

    public void PutBackBall (Ball physicalObject) {
        if (destroyOverabundantBall && !(ballStack.Count < poolTotalCapacity)) {
            Destroy(physicalObject);
        }else {
            ballStack.Push(physicalObject);
        }
    }

    public Ball GetNextBall() {
        
        if (ballStack.Count > 0) {
            return ballStack.Pop();
        }
        else {
            return AskForExtraBall();
        }
        
    }

    private Ball AskForExtraBall() {
        if (poolExtraBallCount > 0) {
            var item = InstantiateBall();
            item.name = "BALL_EXTRA" ;
            poolExtraBallCount--;
            return item;
        }else {
            Debug.LogWarning("no more can create extra physicalObject");
            return null;
        }
    }
    
    private void CreateBalls (Stack<Ball> poolStack, int count) {
        for (int i = 0; i < count; i++) {
            var item = InstantiateBall();
            item.name = "Ball_" + count;
            poolStack.Push(item);
        }

    }

    private Ball InstantiateBall() {
        var newBall = GameObject.Instantiate(ballPrefab);
        newBall.transform.position = getRandomPosInPool();
        return newBall.GetComponent<Ball>();
    }

    private Vector3 getRandomPosInPool() {
        /*we assume that pool ground is a plane so; it has x,y,z pos in world
         and it has width (x-axis) and depth (z-axis) we calculate that with scale
            transform.pos = centerPos and scale.x*10 = total width and scale.z*10 = total depth
            */
        var x0 = transform.position.x - transform.lossyScale.x * 5;
        var x1 = transform.position.x + transform.lossyScale.x * 5;
        var z0 = transform.position.z - transform.lossyScale.z * 5;
        var z1 = transform.position.z + transform.lossyScale.z * 5;
        
       return new Vector3(Random.Range(x0,x1),transform.position.y,Random.Range(z0,z1));
    }
    
    private void PrintCurrentBallStack() {
        //this func. for observing stack with debug log 
        Debug.Log("Pool Stack Info: ");
        int i = 0;
        foreach (var item in ballStack) {
            Debug.Log(i+". "+ item);
            i++;
        }
    }
    
    
}
