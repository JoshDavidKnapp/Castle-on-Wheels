using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum unitState
{
    inactive,
    spawning,
    moving,
    combat
}
public class _UNIT : MonoBehaviour
{
    public NavMeshAgent unitMesh;
    [Header("Enemies Per Unit")]
    public int unitSize;

    [SerializeField]
    protected unitState _currentState;
    protected bool _waitToRepath = false;



    public GameObject[] characters;
    public GameObject[] inactiveCharacters;

    private int[] _charecterPos;
    private int[] _charecterRank;
    private int _rank;
    private int _pos;

    public GameObject soldier;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    protected IEnumerator waitToRepath()
    {
        _waitToRepath = true;
        yield return new WaitForSeconds(2f);
        _waitToRepath = false;
    }

    public void createUnitInactive()
    {
        inactiveCharacters = null;
        characters = new GameObject[unitSize];
        _charecterPos = new int[unitSize];
        _charecterRank = new int[unitSize];
        for (int i = 0; i < unitSize; i++)
        {
            characters[i] = Instantiate(soldier);
            characters[i].transform.parent = transform;
            characters[i].SetActive(false);
        }
        _currentState = unitState.inactive;
    }
    public void setPosAndRank(int amountPerRow)
    {
        int holdAmount;
        holdAmount = amountPerRow;
        for (int i = 0; i < _charecterPos.Length; i++)
        {
            if (i >= holdAmount)
            {
                holdAmount += amountPerRow;
                _pos = 0;
                _rank++;
            }
            _charecterPos[i] = _pos;
            _pos++;
            _charecterRank[i] = _rank;


        }


    }
    public void moveToPos(Vector3 movePos)
    {

        for (int i = 0; i < characters.Length; i++)
        {
            // Debug.Log("Moving");
            characters[i].GetComponent<NavMeshAgent>().SetDestination(new Vector3(movePos.x + _charecterPos[i], movePos.y, movePos.z + _charecterRank[i]));

        }



    }
    public void stopUnit()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            Debug.Log("stop");
            characters[i].GetComponent<NavMeshAgent>().isStopped = true;
        }

    }
    public void changeCurrentStateMove()
    {
        _currentState = unitState.moving;
    }
    public void changeCurrentStateCombat()
    {
        _currentState = unitState.combat;
    }
    private void setNewMovePos()
    {


    }
    protected void setSoldierUnitPos()
    {


    }
    public Vector3 unitAverageVector()
    {
        Vector3 average = Vector3.zero;
        for (int i = 0; i < characters.Length; i++)
        {
            average += characters[i].transform.position;
        }
        average = new Vector3(average.x / characters.Length, average.y / characters.Length, average.z / characters.Length);

        //Debug.Log(average);
        return average;
    }
    public void resizeArrayAfterDeath(GameObject character)
    {
        int loopStart;
        if (inactiveCharacters == null)
        {
            inactiveCharacters = new GameObject[1];
            inactiveCharacters[0] = character;
            for (int i = 0; i < characters.Length; i++)
            {
                if (inactiveCharacters[inactiveCharacters.Length - 1].gameObject == characters[i])
                {
                    characters[i] = null;

                    for (loopStart = i; loopStart < characters.Length; loopStart++)
                    {
                        if (loopStart + 1 < characters.Length)
                        {
                            characters[loopStart] = characters[loopStart + 1];
                            characters[loopStart + 1] = null;
                        }
                    }
                    GameObject[] temp = new GameObject[characters.Length - 1];
                    for (int s = 0; s < temp.Length; s++)
                    {
                        temp[s] = characters[s];


                    }
                    characters = temp;
                    break;

                }
            }
        }
        else
        {
            GameObject[] temp = new GameObject[inactiveCharacters.Length + 1];
            inactiveCharacters.CopyTo(temp, 0);
            temp[temp.Length - 1] = character;
            inactiveCharacters = temp;
            for (int z = 0; z < characters.Length; z++)
            {
                if (inactiveCharacters[inactiveCharacters.Length - 1].gameObject == characters[z])
                {
                    characters[z] = null;

                    for (loopStart = z; loopStart < characters.Length; loopStart++)
                    {
                        if (loopStart + 1 < characters.Length)
                        {
                            characters[loopStart] = characters[loopStart + 1];
                            characters[loopStart + 1] = null;
                        }
                    }
                    temp = new GameObject[characters.Length - 1];
                    for (int s = 0; s < temp.Length; s++)
                    {
                        temp[s] = characters[s];


                    }
                    characters = temp;
                    break;

                }

            }

        }

    }
    public void checkIfAllDone()
    {
        int numOutCombat=0;
        for (int i = 0; i < characters.Length; i++)
        {
            if (!characters[i].GetComponent<_CHARACTERS>().retFoundEnemy())
            {
                numOutCombat++;
            }
            else
                break;
        }
        if (numOutCombat == characters.Length)
        {
            for (int x = 0; x < characters.Length; x++)
            {
                characters[x].GetComponent<_CHARACTERS>().agent.isStopped = false;
            }
            if(gameObject.tag=="Enemy Unit")
            {
                ENEMY_UNIT_LIST.S.removeEnemyFromCombat(gameObject);
                if (characters.Length == 0)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    ENEMY_UNIT_LIST.S.addToArray(gameObject, "Active Enemies");
                }
                
            }
            if (characters.Length == 0)
            {
                gameObject.SetActive(false);
            }
            changeCurrentStateMove();
            _waitToRepath = false;
            Debug.Log("we fucking did it boys");
        }
       


    }



}


