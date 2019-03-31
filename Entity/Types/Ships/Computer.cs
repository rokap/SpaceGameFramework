
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
    public List<Software> softwares = new List<Software>();
    public List<Entity> targets = new List<Entity>();

    private Ship ship;

    public Computer(Ship ship) { this.ship = ship; }

    public void Boot()
    {
        Debug.Log(ship.name + " Computer Booting.");
        SetCMD(CMD.FLYTO);
        
    }

    public void Update()
    {
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
               int x = Random.Range(-150, 150);
               int y = Random.Range(-150, 150);
               int z = Random.Range(-150, 150);
                ship.ai.ChangeBehaviour(new AI.Behaviour.Flying(ship, new Vector3(x, y, z)));
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
