
//How quickly to rotate the object.
var sensitivityX:float = 15;
var sensitivityY:float = 15;
 
//Camera that acts as a point of view to rotate the object relative to.
var referenceCamera:Transform;
 
//The script in Start() is executed before Update(), so we can use it to
//doublecheck our variables have valid values before we try to run the script in Update().
function Start() {
 
        //Ensure the referenceCamera variable has a valid value before letting this script run.
        //If the user didn't set a camera manually, try to automatically assign the scene's Main Camera.
        if (!referenceCamera) {
                if (!Camera.main) {
                        Debug.LogError("No Camera with 'Main Camera' as its tag was found. Please either assign a Camera to this script, or change a Camera's tag to 'Main Camera'.");
                        Destroy(this);
                        return;
                }
                referenceCamera = Camera.main.transform;
        }
}
 
//Update() is called once every frame, and should be used to run script that
//should be doing something constantly. In this case, we potentially want to
//rotate the object constantly if the user is always moving the mouse.
function Update () {
 
        //Get how far the mouse has moved by using the Input.GetAxis().
        var rotationX:float = Input.GetAxis("Mouse X") * sensitivityX;
        var rotationY:float = Input.GetAxis("Mouse Y") * sensitivityY;
 
        //Rotate the object around the camera's "up" axis, and the camera's "right" axis.
        transform.Rotate( referenceCamera.up         , -Mathf.Deg2Rad * rotationX );
        transform.Rotate( referenceCamera.right      ,  Mathf.Deg2Rad * rotationY );
 
}