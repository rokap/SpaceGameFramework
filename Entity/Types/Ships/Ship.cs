using UnityEngine;
using System.Collections;
using System;

public class Ship : Entity, IShip
{
    // AI commands toggle behavioral states
    public enum CMD
    {
        IDLE = 0,
        ENGAGE,
        DOCK,
        MINE,
        TRADE,
        WARP,
        FLYTO,
        PATROL,
        PROTECT
    }

    public CMD cmd = CMD.IDLE;
    public AI ai = new AI();

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ai.Update();
        if(ai.target != null)
        Debug.Log(ai.target.GetType());
    }

    private void ProcessCmd()
    {
        switch (cmd)
        {
            case CMD.IDLE:
                break;
            case CMD.ENGAGE:
                break;
            case CMD.DOCK:
                break;
            case CMD.MINE:
                break;
            case CMD.TRADE:
                break;
            case CMD.WARP:
                break;
            case CMD.FLYTO:
                break;
            case CMD.PATROL:
                break;
            case CMD.PROTECT:
                break;
            default:
                Debug.LogError("Invalid Command");
                break;
        }
    }

    public void SetCMD(CMD cmd)
    {
        Debug.Log("Set Ship Command: " + cmd);
        this.cmd = cmd;
        ProcessCmd();
    }
}
