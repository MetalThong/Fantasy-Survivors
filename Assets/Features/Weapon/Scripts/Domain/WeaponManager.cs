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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }
    public Transform[] WeaponPositions = new Transform[6];
    public List<GameObject> WeaponObjects = new List<GameObject>();
    public List<Weapon> Weapons = new List<Weapon>();

    private void Start()
    {
        UpdateWeapons();
    }

    public Transform GetAvailableWeaponPosition()
    {
        if (WeaponObjects.Count == 6) return null;
        return WeaponPositions[WeaponObjects.Count];
    }

    public bool HasFreeSlot()
    {
        return WeaponObjects.Count < 6;
    }    

    public void AddWeapon(GameObject weapon)
    {
        Transform parent = GetAvailableWeaponPosition();
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

    public void Attack(List<Enemy> enemies)
    {
        foreach (var weap in Weapons)
        {
            weap?.Attack(enemies);
        }
    }

}
