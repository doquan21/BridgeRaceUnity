using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Renderer renderers;
    [SerializeField] ColorDataSO colorDataSO;
    [SerializeField] private LayerMask groundLayer;
    public ColorType colorType { get; private set; }
    public Transform cubeContainer;
    public CharacterBrick enemyBrickPrefab;

    //public Transform[] brickLocations;
    public Transform bridgeTarget;
    public Animator enemyAnim;

    private int numberOfCube = 0;
    public List<CharacterBrick> newCube = new List<CharacterBrick>();
    private UnityEngine.AI.NavMeshAgent agent;
    private int currentBrickIndex = 1;
    private List<GameObject> bricks = new List<GameObject>();
    private int currentFloor = 1;
    private string currentAnim = "";

    private enum BotState
    {
        Idle = 1,
        CollectingBrick = 2,
        FillingBridge = 3
    }

    private BotState currentState = BotState.Idle;
    private bool isRunningBot = false;

    // void Start()
    // {
    //     colorType = ColorType.Red;
    //     ChangeColor(colorType);
    //     agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    //     GoToNextBrick();

    //     bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

    // }
    public IEnumerator Start()
    {
        colorType = ColorType.Red;
        ChangeColor(colorType);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        yield return new WaitForEndOfFrame();
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Debug.Log("Count Brick " + bricks.Count);
        //GoToNextBrick();
        isRunningBot = true;
        yield return null;
    }

    void Update()
    {
        if (GameManager.Ins.IsState(GameState.Gameplay) && isRunningBot)
        {
            FloorCheck();
            switch (currentState)
            {
                case BotState.Idle:
                    ChangAnim("run", false);
                    if (currentFloor == 1)
                    {
                        currentBrickIndex = 1;
                    }
                    else { currentBrickIndex = 17; }
                    Invoke("GoToNextBrick", 1f);
                    break;

                case BotState.CollectingBrick:
                    ChangAnim("run", true);
                    if (Vector3.Distance(transform.position, bricks[currentBrickIndex - 1].transform.position) < 0.8f)
                    {
                        CollectBrick();
                    }
                    break;

                case BotState.FillingBridge:
                    if (newCube.Count == 0)
                    {
                        GetBrick();
                    }
                    break;
            }

        }
    }

    private void ChangAnim(string anim, bool isRun)
    {
        currentAnim = anim;
        enemyAnim.SetBool(anim, isRun);

    }
    public void ChangeColor(ColorType colorType)
    {
        renderers.material = colorDataSO.GetMat(colorType);
    }

    public void AddCube()
    {
        numberOfCube = newCube.Count;

        CharacterBrick playerCube = Instantiate(enemyBrickPrefab, cubeContainer);
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
        }
    }
    void CollectBrick()
    {
        if (newCube.Count > 1)
        {
            currentState = BotState.FillingBridge;
            GoToBridge();
        }
        else
        {
            GoToNextBrick();
        }
    }

    void GetBrick()
    {
        currentState = BotState.Idle;
        GoToNextBrick();
    }

    void GoToNextBrick()
    {
        currentState = BotState.CollectingBrick;
        bool isSameColor = false;
        while (!isSameColor)
        {
            if (bricks[currentBrickIndex].gameObject.GetComponent<Brick>().colorType == colorType)
            {
                isSameColor = true;
            }
            else currentBrickIndex++;
            if (currentBrickIndex > 31)
            {
                currentBrickIndex = 2;
                break;
            }
        }
        //
        if (isSameColor == false)
        {
            currentState = BotState.Idle;
        }
        //
        if (currentFloor == 1)
        {
            if (currentBrickIndex > 15)
            {
                currentBrickIndex = 2;
            }
            //Debug.Log(currentBrickIndex);
            agent.SetDestination(bricks[currentBrickIndex].transform.position);
            currentBrickIndex++;
        }
        else
        {
            if (currentBrickIndex > 31 || currentBrickIndex < 16)
            {
                currentBrickIndex = 16;
            }
            //Debug.Log(currentBrickIndex);
            agent.SetDestination(bricks[currentBrickIndex].transform.position);
            currentBrickIndex++;
        }
    }

    void GoToBridge()
    {
        agent.SetDestination(bridgeTarget.position);
    }
    public void FloorCheck()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 1);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            if (hit.transform.tag == "Floor2")
            {
                Debug.Log("Hit floor2");
                currentFloor = 2;
            }
        }
    }
}
