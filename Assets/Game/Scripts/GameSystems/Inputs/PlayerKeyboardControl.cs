using UnityEngine;

public class PlayerKeyboardControl : MonoBehaviour, IPlayerControl
{
    public float GetHorizontalAxis => Input.GetAxis("Horizontal");
    public float GetVerticalAxis => Input.GetAxis("Vertical");
    public float GetHorizontalLookAxis => Input.GetAxis("Mouse X");
    public float GetVerticalLookAxis => Input.GetAxis("Mouse Y");

    public bool GetRunning => Input.GetKey(KeyCode.LeftShift);
}