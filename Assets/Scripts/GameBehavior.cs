using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System;

public class GameBehavior : MonoBehaviour, IManager
{

    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;

    public bool showWinScreen = false;
    public bool showLossScreen = false;

    private string _state;

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    private int _itemsCollected = 0;

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;

                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected) + " more to go !";

            }

            Debug.LogFormat("Items {0}", _itemsCollected);
        }
    }

    private int _playerLives = 3;
    public int Lives
    {
        get { return _playerLives; }
        set
        {
            _playerLives = value;
            if (_playerLives <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... that's gotta hurt";
            }
            Debug.LogFormat("Lives: {0}", _playerLives);
        }
    }

    private int _playerHP = 3;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "Manager initialized...";
        _state.FancyDebug();

        debug(_state);

        LogWithDelegate(debug);

        GameObject player = GameObject.Find("Player");

        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();

        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }




    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    private void LogWithDelegate(DebugDelegate debugDelegate)
    {
        debugDelegate("Delegating the debug task...");
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerLives);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);
        
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 100), "YOU WIN!"))
            {
                Utilities.RestartLevel(0);
            }
        }

        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                try
                { 
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene 0: " + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");

                }
            }
        }
    }
}
