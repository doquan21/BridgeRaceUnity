using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public VariableJoystick joystick;
    public CharacterController controller;
    public float movementSpeed;
    public float rotationSpeed;

    public Canvas inputCanvas;
    public bool isJoystick;

    public GameObject playerGO;
    public Animator playerAnim;
    private Player player;

    private void Start()
    {
        EnableJoystickInput();
        player = playerGO.GetComponent<Player>();
    }

    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (isJoystick)
        {
            var movementDirection = new Vector3(0.0f,0.0f,0.0f);
            //Debug.Log(controller.transform.forward);
            if (player.isMoving == true || joystick.Direction.y < 0)
            {
                movementDirection = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            }
            // else
            // {
            //     movementDirection = new Vector3(joystick.Direction.x, 0.0f, 0.0f);
            // }
            controller.SimpleMove(movementDirection * movementSpeed);
            if (movementDirection.sqrMagnitude <=0)
            {
                playerAnim.SetBool("run",false);
                return ;
            }
            playerAnim.SetBool("run",true);
            var targetDirection = Vector3.RotateTowards(controller.transform.forward, movementDirection, rotationSpeed * Time.deltaTime, 0.0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
