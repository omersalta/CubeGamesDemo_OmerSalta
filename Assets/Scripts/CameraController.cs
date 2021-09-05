using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 7f;
    private float RotSpeed= 4.0f;
    private float rotSpeedH = 4.0f;
    float rotX;
    float rotY;
    
    void Update () {
        
        #region input&Control
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            var currentV = Input.GetAxisRaw("Vertical");
            var currentH = Input.GetAxisRaw("Horizontal");
            Vector3 direction = transform.forward * currentV + transform.right * currentH;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
        if ( Input.GetMouseButton(2) ) {
            
            rotX += Input.GetAxis("Mouse X")*RotSpeed;
            rotY += Input.GetAxis("Mouse Y") * RotSpeed;
            rotY = Mathf.Clamp(rotY, -90f, 90f);
            Camera.main.transform.localRotation = Quaternion.Euler(-rotY, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, rotX, 0f);
        }
        #endregion
        
    }

}
