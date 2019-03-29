using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Computer
{
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
    public List<Entity> targets = new List<Entity>();
    private Ship ship;

    public Computer(Ship ship) { this.ship = ship; }

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
    public void AddTarget(Entity target)
    {
        targets.Add(target);
    }
}
