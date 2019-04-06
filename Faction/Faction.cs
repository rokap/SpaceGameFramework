using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public List<Standing> standings = new List<Standing>();
    public List<Ship> ships = new List<Ship>();
}

[System.Serializable]
public class Standing
{
    public const int max = 1000, min = -1000;

    public Faction faction;
    public int rating;
}