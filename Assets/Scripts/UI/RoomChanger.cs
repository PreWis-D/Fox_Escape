using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] private List<MiniGame> _miniGames = new List<MiniGame>();
    [SerializeField] private List<GameObject> _wallsRoom = new List<GameObject>();
    [SerializeField] private List<Button> _buttons = new List<Button>();
    [SerializeField] private Canvas _buttonsRightAndLeft;

    private int _TargetMiniGameId;
    private int _currentWallNumber = 0;
    private GameObject _currentWall;
    private GameObject _currentMiniGame;
    private float _transitionDelay = 0.2f;
    private Coroutine _changeRoomInJob;
    private bool _isGoToMiniGame = false;
    private bool _isCurrentWallActive = true;

    public event UnityAction WallChanged;

    protected void Awake()
    {
        for (int i = 0; i < _buttons.Count; i++)
            _buttons[i] = GetComponent<Button>();

        _currentWall = _wallsRoom[_currentWallNumber];
    }

    public void GoToTheRight()
    {
        TryEditListWall(_wallsRoom.Count - 1, 0, true);
    }

    public void GoToTheLeft()
    {
        TryEditListWall(0, _wallsRoom.Count - 1, false);
    }

    private void TryEditListWall(int currentNumber, int targetNumber, bool isRightButton)
    {
        ChangeAccessToButtons(false);

        if (_currentWallNumber == currentNumber)
            _currentWallNumber = targetNumber;
        else if (isRightButton == true)
            _currentWallNumber++;
        else if (isRightButton == false)
            _currentWallNumber--;

        StartChangeRoom(false);
    }

    private void GoToMiniGame()
    {
        _isCurrentWallActive = false;
        _buttonsRightAndLeft.gameObject.SetActive(false);

        for (int i = 0; i < _miniGames.Count; i++)
        {
            if (_miniGames[i].MiniGameId == _TargetMiniGameId)
            {
                _currentWall.SetActive(false);
                _currentMiniGame = _miniGames[i].gameObject;
                _currentMiniGame.SetActive(true);
            }
        }
    }

    private void GoToNextWall(GameObject wall)
    {
        _currentWall.SetActive(false);
        _currentWall = wall;
        _currentWall.SetActive(true);
    }

    private void ExitToMiniGame()
    {
        _currentMiniGame.SetActive(false);
        _isCurrentWallActive = true;
        _buttonsRightAndLeft.gameObject.SetActive(true);
        _currentWall.SetActive(true);
    }

    private IEnumerator ChangeRoom(GameObject wall, bool isGoToMiniGame)
    {
        yield return new WaitForSeconds(_transitionDelay);

        if (isGoToMiniGame)
        {
            GoToMiniGame();
        }
        else
        {
            if (_isCurrentWallActive)
                GoToNextWall(wall);
            else
                ExitToMiniGame();
        }

        ChangeAccessToButtons(true);
    }

    private void StartChangeRoom(bool isGoToMiniGame)
    {
        if (_changeRoomInJob != null)
            StopChangeRoom();

        WallChanged?.Invoke();
        _changeRoomInJob = StartCoroutine(ChangeRoom(_wallsRoom[_currentWallNumber], isGoToMiniGame));
    }

    private void StopChangeRoom()
    {
        StopCoroutine(_changeRoomInJob);
    }

    private void ChangeAccessToButtons(bool isAccess)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_buttons[i] != null)
                _buttons[i].interactable = isAccess;
        }
    }

    public void MiniGameButton(int id)
    {
        _isGoToMiniGame = true;
        _TargetMiniGameId = id;

        StartChangeRoom(_isGoToMiniGame);
    }

    public void ExitMiniGameButton()
    {
        _isGoToMiniGame = false;

        StartChangeRoom(_isGoToMiniGame);
    }
}