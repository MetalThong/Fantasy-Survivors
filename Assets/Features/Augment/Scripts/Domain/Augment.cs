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
                BuffAugment((BuffInfoSO)augmentInfo);
                break;
            case AugmentType.Add:
                AddAugment((AddInfoSO)augmentInfo);
                break;
            case AugmentType.Upgrade:
                UpgradeAugment((UpgradeInfoSO)augmentInfo);
                break;
        }
    }


    private void BuffAugment(BuffInfoSO info)
    {
        switch (info.Type)
        {       
            case StatType.Health:
                //tăng máu tối đa
                break;
            case StatType.Healing:
                //hồi máu
                break;
            case StatType.MoveSpeed:
                //tăng stat moveSpeed 
                break;
            case StatType.PickupRange:
                //tăng stat pickup range
                break;          
            default:
                return;
        }
    }

    private void AddAugment(AddInfoSO info)
    {
        WeaponManager.Instance.AddWeapon(info.NewWeapon);
        WeaponManager.Instance.UpdateWeapons();
    }

    private void UpgradeAugment(UpgradeInfoSO info)
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