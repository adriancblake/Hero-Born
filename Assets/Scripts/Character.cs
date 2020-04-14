using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public int exp = 0;

    public Character()
    {
        this.name = "Not Assigned";
    }
    public Character(string name)
    {
        this.name = name;
    }

    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat("Name: {0} - {1} EXP", name, exp);
    }

    private void Reset() 
    { 
        this.name = "Not assigned"; 
        this.exp = 0; 
    }

}
public class Paladin : Character 
{ 
    public Weapon weapon;
    public Paladin(string name, Weapon weapon) : base(name) 
    { 
        this.weapon = weapon; 
    }
    public override void PrintStatsInfo() 
    { 
        Debug.LogFormat("Hail {0} - take up your {1}!", name, weapon.name); 
    }
}     

public struct Weapon 
{ 
    public string name; 
    public int damage;

    public Weapon(string name, int damage) 
    {
        this.name = name; 
        this.damage = damage; 
    }

    public void PrintWeaponStats()
    {
        Debug.LogFormat("Weapon: {0} - {1} DMG", name, damage);
    }
}

