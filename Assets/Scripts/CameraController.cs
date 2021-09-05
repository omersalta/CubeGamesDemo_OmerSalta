using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 25f;
    
    private float rotSpeed = 3.0f;
    
    private float rotX;
    private float rotY;
    
    void Update () {
        
        #region input&Control
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            var currentV = Input.GetAxisRaw("Vertical");
            var currentH = Input.GetAxisRaw("Horizontal");
            Vector3 direction = transform.forward * currentV + transform.right * currentH;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
        if ( Input.GetMouseButton(2) ) {
            rotY += rotSpeed * Input.GetAxis("Mouse X");
            rotX += rotSpeed * -Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(rotX, rotY, 0);
        }
        #endregion
        
    }

}
