using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Transform[] WeaponPositions = new Transform[6];
    public List<GameObject> WeaponObjects = new List<GameObject>();
    public List<Weapon> Weapons = new List<Weapon>();
    bool[] slotUsed = new bool[6];

    private void Start()
    {
        slotUsed[0] = true;
        UpdateWeapons();
    }

    public Transform GetWeaponSlot()
    {
        for (int i = 0; i < 6; i++)
        {
            if (!slotUsed[i])
            {
                slotUsed[i] = true;
                return WeaponPositions[i];
            }
        }
        return null;
    }

    public bool HasFreeSlot()
    {
        for (int i = 0; i < slotUsed.Length; i++)
        {
            if (!slotUsed[i]) return true;
        }
        return false;
    }    

    public void AddWeapon(GameObject weapon)
    {
        Transform parent = GetWeaponSlot();
        if (parent != null)
        {
            WeaponObjects.Add(Instantiate(weapon, parent));
        }
    }

    public void UpdateWeapons()
    {
        Weapons.Clear();
        foreach (var obj in WeaponObjects)
        {
            Weapons.Add(obj.GetComponent<Weapon>());
        }
    }

    public void Tick(List<Enemy> enemies)
    {
        foreach (var weap in Weapons)
        {
            weap?.Tick(enemies);
        }
    }
}
