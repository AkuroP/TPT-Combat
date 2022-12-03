using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    private bool showConsole;
    private bool showhelp;
    private string input = " ";

    public static DebugCommand<int> SETLVL;
    public static DebugCommand<int> SETHP;
    public static DebugCommand<int> SETATK;
    public static DebugCommand<int> SETSATK;
    public static DebugCommand<int> SETDEF;
    public static DebugCommand<int> SETSDEF;
    public static DebugCommand<int> SETSPEED;
    public static DebugCommand HELP;

    public List<object> commandList;

    public Shadow playerStat;


    private void Awake()
    {
        #region StatCommand
 
        SETLVL = new DebugCommand<int>("setLvl", "<amount> - Set the level of the player","setLvl" ,(x) =>
        {
            playerStat.lvl = x;
        });
        
        SETHP = new DebugCommand<int>("sethp", "<amount> - Set the current health of the player","sethp" ,(x) =>
        {
            playerStat.currentHP = x;
        });
        
        SETATK = new DebugCommand<int>("setatk", "<amount> - Set the attack of the player","setatk" ,(x) =>
        {
            playerStat.atk = x;
        });
        
        SETSATK = new DebugCommand<int>("setSatk", "<amount> - Set special attack the of the player","setSatk" ,(x) =>
        {
            playerStat.sAtk = x;
        });
        
        SETDEF = new DebugCommand<int>("setdef", "<amount> - Set the defense of the player","setdef" ,(x) =>
        {
            playerStat.def = x;
        });
        
        SETSDEF = new DebugCommand<int>("setSdef", "<amount> - Set the special defense of the player","setSdef" ,(x) =>
        {
            playerStat.sDef = x;
        });
        
        SETSPEED = new DebugCommand<int>("setspeed", "<amount> - Set the speed (in combat) of the player","setspeed" ,(x) =>
        {
            playerStat.speed = x;
        });
        
        
        
        #endregion
        
        HELP = new DebugCommand("help", "Show the list of commands","help" ,() =>
        {
            showhelp = true;
        });


        commandList = new List<object>
        {
            SETLVL,
            SETHP,
            SETATK,
            SETSATK,
            SETDEF,
            SETSDEF,
            SETSPEED,
            HELP
        };
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
           showConsole = !showConsole; 
        }

        if (Input.GetKeyDown(KeyCode.Return) && showConsole)
        {
            HandleInput();
            input = "";
        }
        
    }

    private Vector2 scroll;
    private void OnGUI()
    {
        if (showConsole)
        {
            float y = 0f;

            if (showhelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");

                Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

                for (int i = 0; i < commandList.Count; i++)
                {
                    DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                    string label = $"{commandBase?.commandFormat} - {commandBase?.commandDescription}";

                    Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                    GUI.Label(labelRect, label);

                }
                
                GUI.EndScrollView();
                
                y += 100;
            }
            
            GUI.Box(new Rect(0, y, Screen.width, 50), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            input = GUI.TextField(new Rect(20f, y + 10f, Screen.width - 10f, 40f), input);

        }

    }

    private void HandleInput()
    {
        string[] properties = input.Split(" ");
        
        for (int i = 0; i < commandList.Count ; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] is DebugCommand)
                {
                    //Cast to this type and invoke the command
                    (commandList[i] as DebugCommand)?.Invoke();
                }
                else if(commandList[i] is DebugCommand<int>)
                {
                    (commandList[i] as DebugCommand<int>)?.Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
