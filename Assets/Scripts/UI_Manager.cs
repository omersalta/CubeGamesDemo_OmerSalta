using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    
    //Sliders added on unity Hierarchy Inspector
    [SerializeField] private GameObject HMaxS;
    [SerializeField] private GameObject AnimateSpeedS;
    [SerializeField] private GameObject BouncinessS;
    [SerializeField] private GameObject NoBounceThresholdS;

    private ThrowParam _throwParam;
    private Vector3 _lastClickPos;
    private List<Vector3> _clickPosList = new List<Vector3>();
    [SerializeField] private int clickPosListCapacity;
    
    // Start is called before the first frame update
    void Start() {
        _throwParam = new ThrowParam();
        clickPosListCapacity = 2;
        SetAllSlidersValues();
        //Debug.Log("_throwParam :" + _throwParam.ToString());
    }

    private void SetAllSlidersValues() {
        
        HMaxSlider(20f);
        AnimSpeedSlider(1);
        BouncinnesSlider(0.6f);
        NoBounceThresholdSlider(0.5f);
    }
    
    
    void Update()
    {
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
    
    private void PrintClickList() {
        int i = 0;
        foreach (var item in _clickPosList) {
            
            Debug.Log(i+". "+ item);
            i++;
        }
    }
    
    private void PushClickList() {
        if (_clickPosList.Count < clickPosListCapacity) {
            _clickPosList.Add(_lastClickPos);
            if (_clickPosList.Count == clickPosListCapacity) {
                //Throwing
                //PrintClickList();
                _throwParam.StartPos = _clickPosList[0];
                _throwParam.EndPos = _clickPosList[1];
                Throwing();
                _clickPosList.Clear();
            }
        }
        else {
            Debug.LogWarning("list Over Capacity!!!!");
            PrintClickList();
            _clickPosList.Clear();
        }
    }
    
    private void Throwing() {
        //Debug.Log("throwParam:"+_throwParam);
        FindObjectOfType<BallThrower>().GetBallAndThrow(_throwParam);
    }
    
    public void HMaxSlider (float value) {
        //Debug.Log("coming value:"+value);
        _throwParam.Hmax = value;
        HMaxS.GetComponentInChildren<Text>().text = value.ToString();
    }
    
    public void AnimSpeedSlider(float value) {
        //Debug.Log("coming value:"+value);
        _throwParam.AnimSpeed = (int)value;
        AnimateSpeedS.GetComponentInChildren<Text>().text = value.ToString();
    }
    
    public void BouncinnesSlider(float value) {
        //Debug.Log("coming value:"+value);
        _throwParam.bouncinnes = (float)value;
        BouncinessS.GetComponentInChildren<Text>().text = value.ToString("0.00");
    }
    
    public void NoBounceThresholdSlider(float value) {
        //Debug.Log("coming value:"+value);
        _throwParam.nobounceThreshold = (float)value;
        NoBounceThresholdS.GetComponentInChildren<Text>().text = value.ToString("0.00");
    }
    
}
