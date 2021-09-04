using UnityEngine;

public class CameraController : MonoBehaviour {

    private float moveSpeed = 7f;
    private float rotSpeed = 4.0f;
    
    void Update () {
        
        #region input&Control
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
            var currentV = Input.GetAxisRaw("Vertical");
            var currentH = Input.GetAxisRaw("Horizontal");
            Vector3 direction = transform.forward * currentV + transform.right * currentH;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        
        if ( Input.GetMouseButton(2) ) {
            float h = rotSpeed * Input.GetAxis("Mouse X");
            float v = rotSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(-v, h, 0);
        }
        #endregion
        
    }

}
