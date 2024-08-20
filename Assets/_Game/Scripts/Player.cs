using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private LayerMask groundLayer;
    public ColorType colorType { get; private set; }
    public Transform cubeContainer;
    public CharacterBrick playerBrickPrefab;
    public CharacterController controller;

    private int numberOfCube = 0;
    public List<CharacterBrick> newCube = new List<CharacterBrick>();
    public bool isMoving = true;
    private Stair stair;
    public GameObject movementGO;
    private MovementManager movementManager;

    public void OnInit()
    {
        transform.position = new Vector3(0, 1, -3);
        ClearCube();
        colorType = ColorType.Blue;
        ChangeColor(colorType);
        movementManager = movementGO.GetComponent<MovementManager>();
    }


    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay))
        {
            GroundCheck(controller.transform.forward);
            MoveCheck(controller.transform.forward);
        }
    }

    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }

    public void AddCube()
    {
        numberOfCube = newCube.Count;

        CharacterBrick playerCube = Instantiate(playerBrickPrefab, cubeContainer);
        playerCube.transform.localPosition = numberOfCube * 0.35f * Vector3.up;
        playerCube.ChangeColor(colorType);
        newCube.Add(playerCube);
    }

    public void RemoveCube()
    {
        numberOfCube = newCube.Count - 1;

        if (numberOfCube > -1)
        {
            CharacterBrick playerCube = newCube[numberOfCube];
            newCube.Remove(playerCube);
            Destroy(playerCube.gameObject);
            //playerSkin.localPosition = playerSkin.localPosition - 0.25f * Vector3.up;
        }
    }

    public void ClearCube()
    {
        numberOfCube = newCube.Count;
        for (int i = 0; i < numberOfCube; i++)
        {
            Destroy(newCube[i].gameObject);
        }

        newCube.Clear();
    }
    public void MoveCheck(Vector3 nextPos)
    {
        //isMoving = true;
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) + nextPos, Color.red, 0.5f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) + nextPos, out hit, 0.1f, stairLayer))
        {
            if (hit.collider.GetComponent<Stair>())
            {
                stair = hit.collider.GetComponent<Stair>();
            }
            else
            {
                isMoving = false;
            }
            // Debug.Log(stair == null);
            // Debug.Log(newCube == null);
            if (newCube.Count == 0)
            {
                //Debug.Log("Quan b");
                isMoving = false;
            }
            if (stair.colorType != colorType && newCube.Count > 0 || stair.colorType == colorType)
            {
                //Debug.Log("Quan a");
                if (stair.colorType != colorType)
                {
                    stair.ChangeColor(colorType);
                }
                isMoving = true;
            }
        }
    }

    public void GroundCheck(Vector3 nextPos)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down) + nextPos, out hit, 1f, groundLayer))
        {
            if (movementManager.joystick.Direction.y < 0)
            {
                isMoving = true;
                //Debug.Log("Quan c");
            }
            //Debug.Log("Quan c "+ (transform.position + nextPos));
        }
    }
}