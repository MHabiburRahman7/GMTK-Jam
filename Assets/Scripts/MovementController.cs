using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    #region variables
    public float speed;

    private Rigidbody rb;
    private Camera playerCam;
    #endregion

    #region builtins
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + movementInput * speed * Time.deltaTime);

        //make the player look in the direction of the cursor
        Ray cameraRay = playerCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength)) {
            Vector3 playerLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, playerLook, Color.red, 0.1f);

            transform.LookAt(new Vector3(playerLook.x, transform.position.y, playerLook.z));
        }
    }

    #endregion
}
