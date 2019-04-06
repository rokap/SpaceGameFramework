using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Station : Entity, IStation
{
    public List<Storage> storage = new List<Storage>();
    public List<Product> products = new List<Product>();

    // Use this for initialization
    void Start()
    {
        foreach (Product item in products)
        {
            item.Startup(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Produce();
    }

    private void Produce()
    {
        foreach (Product item in products)
        {
            item.Produce();
        }
    }
}

[System.Serializable]
public class Storage
{
    public int quantity = 0;
    public Ware ware;
}

[System.Serializable]
public class Product
{
    public Station station;
    bool canBuild = true;
    public int productionTime = 300;
    public float _currentCycle = 0;

    public List<Resource> resources = new List<Resource>();
    public int quantity = 0;
    public Ware ware;

    public void Startup(Station station)
    {
        this.station = station;
        _currentCycle = productionTime;
    }
    public void Produce()
    {
        if (_currentCycle <= 0)
        {

            Storage storage = station.storage.Find(x => x.ware == this.ware);

            foreach (Resource resource in resources)
            {

                Storage resourceStorage = station.storage.Find(x => x.ware == resource.ware);

                if (resourceStorage.quantity < resource.quantity)
                {
                    canBuild = false;
                    break;
                }
                else
                {
                    canBuild = true;
                    resourceStorage.quantity -= resource.quantity;
                }
            }

            if (canBuild)
            {
                if (storage == null)
                {
                    storage = new Storage();
                    storage.quantity = quantity;
                    storage.ware = ware;
                    station.storage.Add(storage);
                }
                else
                {
                    storage.quantity += quantity;
                }
            }
            
            _currentCycle = productionTime;
        }
            _currentCycle -= Time.deltaTime;

    }
}

[System.Serializable]
public class Resource
{
    public int quantity = 0;
    public Ware ware;
}