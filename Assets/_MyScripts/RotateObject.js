
function Update () {
var speedOfRotation =5;
amtToMove = speedOfRotation * Time.deltaTime;
transform.Rotate(0,0,speedOfRotation* Time.deltaTime);



}