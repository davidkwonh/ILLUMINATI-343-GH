﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionCard : Card
{
    // Public Variables
    public Sprite[] regions;
    public GameObject holder;
    public GameObject treasury;

    // Protected Variables
    protected RegionCard myParent;
    protected RegionCard myChild;
    protected int power;
    protected int transferPower;
    protected int income;
    protected int gold;
    protected GameObject myTreasury;
    protected List<GameObject> openArrows;
    protected List<GameObject> usedArrows;

    protected bool canAttack = true;

    //
    void Start()
    {
        
    }
    //
    public void setParent(RegionCard parent)
    {
        myParent = parent;
        parent.myChild = this;
    }
    // Add gold from income.
    public void gainIncome()
    {
        gold += income;
    }
    public int getGold()
    {
        return gold;
    }
    public void spendGold()
    {
        gold -= 1;
        power += 1;
    }
    public RegionCard getChild()
    {
        return myChild;
    }
    public bool getAttack()
    {
        return canAttack;
    }
    public int getPower()
    {
        return power;
    }
    public void resetPower(int original)
    {
        power = original;
    }

    // Spawn Card Holders for each open arrow on the card.
    protected void spawnArrows()
    {
        openArrows = new List<GameObject>();
        usedArrows = new List<GameObject>();
        GameObject instance;

        // Top Arrow
        instance = Instantiate(myParent.holder, transform.position + new Vector3(15, 0, 0), Quaternion.identity) as GameObject;
        openArrows.Add(instance);
        // Right Arrow
        instance = Instantiate(myParent.holder, transform.position + new Vector3(0, 10, 0), Quaternion.identity) as GameObject;
        openArrows.Add(instance);
        // Bottom Arrow
        instance = Instantiate(myParent.holder, transform.position + new Vector3(0, -10, 0), Quaternion.identity) as GameObject;
        openArrows.Add(instance);
        // Left Arrow
        instance = Instantiate(myParent.holder, transform.position + new Vector3(-15, 0, 0), Quaternion.identity) as GameObject;
        openArrows.Add(instance);

        // Treasury
        instance = Instantiate(myParent.treasury, transform.position, transform.rotation) as GameObject;
        instance.GetComponent<Treasury>().setParent(this);
        myTreasury = instance;
    }

    //
    public GameObject getOpenArrow()
    {
        if (openArrows.Count == 0)
            return null;

        for (int i = 0; i < openArrows.Count; i++)
        {
            if (openArrows[i].GetComponent<CardHolder>().hasCard() == false)
                return openArrows[i];
        }
        return null;
    }
    public void fillArrow()
    {
        usedArrows.Add(openArrows[0]);
        openArrows.RemoveAt(0);
    }
}