using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Augment : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image itemImage;
    [SerializeField] Image augmentImage;
    private AugmentInfoSO augmentInfo;

    [SerializeField] PlayerEntity player;

    public void Init(AugmentInfoSO info)
    {
        nameText.text = info.name;
        description.text = info.description;
        itemImage.sprite = info.image;
        augmentImage.sprite = info.frame;
        augmentInfo = info;
    }

    private void ApplyAugment()
    {
        switch (augmentInfo.augmentType)
        {
            case AugmentType.Buff:
                Buff((BuffInfoSO)augmentInfo);
                break;
            case AugmentType.Add:
                Add((AddInfoSO)augmentInfo);
                break;
            case AugmentType.Upgrade:
                Upgrade((UpgradeInfoSO)augmentInfo);
                break;
        }
    }


    private void Buff(BuffInfoSO info)
    {
        StatComponent stat = player.GetStatComponent();
        MovementComponent movement = player.GetMovementComponent();
        switch (info.Type)
        {       
            case BuffType.Health:
                stat.IncreaseMaxHealth(info.amount);
                break;
            case BuffType.Healing:
                stat.IncreaseCurrentHealth(info.amount);
                break;
            case BuffType.MoveSpeed:
                movement.IncreaseMoveSpeed(info.amount);
                break;
            case BuffType.PickupRange:
                stat.IncreasePickUpRange(info.amount);
                break;          
            default:
                return;
        }
    }

    private void Add(AddInfoSO info)
    {
        WeaponManager.Instance.AddWeapon(info.NewWeapon);
        WeaponManager.Instance.UpdateWeapons();
    }

    private void Upgrade(UpgradeInfoSO info)
    {
        List<GameObject> weaponObjs = WeaponManager.Instance.WeaponObjects;
        List<Weapon> weapons = WeaponManager.Instance.Weapons;
        for (int i = 0; i < weaponObjs.Count ; i++) 
        {
            GameObject currentWeaponObject = weaponObjs[i];
            Weapon currentWeaponComponent = weapons[i];
            if (info.SourceWeaponID == currentWeaponComponent.Info.ID) 
            {
                Transform parent = WeaponManager.Instance.WeaponPositions[i];
                GameObject upgradedWeapon = Instantiate(info.UpgradedWeapon, parent); 
                WeaponManager.Instance.WeaponObjects[i] = upgradedWeapon;
                Destroy(currentWeaponObject); 
                break;
            }
        }
        WeaponManager.Instance.UpdateWeapons();
    }

    public void OnClick()
    {
        ApplyAugment();
    }
}