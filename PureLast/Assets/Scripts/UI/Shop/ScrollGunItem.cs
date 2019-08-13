using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollGunItem : MonoBehaviour
{
    Image gunImage;
    Image bulletImage;
    Text damage;
    Text gunName;
    Text description;
    Text bpm;
    Text price;
    Button buyButton;
    Image shadow;
    Image buyButtonImage;
    Image equiped;

    int currentIndex;

    void Awake()
    {
        bulletImage = transform.Find("BulletImage").GetComponent<Image>();
        gunImage = transform.Find("GunImage").GetComponent<Image>();        
        gunName = transform.Find("GunName").GetComponent<Text>();
        description = transform.Find("Description").GetComponent<Text>();
        damage = transform.Find("Damage").GetComponent<Text>();     
        bpm = transform.Find("Bpm").GetComponent<Text>();
        price = transform.Find("Price").GetComponent<Text>();
        buyButton = transform.Find("BuyButton").GetComponent<Button>();
        shadow = transform.Find("ScrollGunPanelShadow").GetComponent<Image>();
        buyButtonImage = transform.Find("BuyButton").GetComponent<Image>();
        equiped = transform.Find("Equiped").GetComponent<Image>();
    }

    public void ItemFilling(int Index)
    {
        currentIndex = Index;
        if (currentIndex != GameController.EquipedGun)
        {
            equiped.enabled = false;
        }
        if (GameController.gunStatsList[currentIndex].Owned)
        {
            price.enabled = false;
            buyButton.enabled = false;
            buyButtonImage.enabled = false;
            shadow.enabled = false;
            gunImage.sprite = Resources.Load<Sprite>(GameController.gunStatsList[Index].IconPath);
            bulletImage.sprite = Resources.Load<Sprite>(GameController.gunStatsList[Index].BulletIconPath);
            damage.text = GameController.gunStatsList[Index].Damage.ToString();
            description.text = GameController.gunStatsList[Index].Description.ToString();
            gunName.text = GameController.gunStatsList[Index].GunName.ToString();
            bpm.text = GameController.gunStatsList[Index].BulletPerMinute.ToString();
        }
        else
        {
            gunImage.sprite = Resources.Load<Sprite>(GameController.gunStatsList[Index].IconPath);
            bulletImage.sprite = Resources.Load<Sprite>(GameController.gunStatsList[Index].BulletIconPath);
            damage.text = GameController.gunStatsList[Index].Damage.ToString();
            description.text = GameController.gunStatsList[Index].Description.ToString();
            gunName.text = GameController.gunStatsList[Index].GunName.ToString();
            bpm.text = GameController.gunStatsList[Index].BulletPerMinute.ToString();
            price.text = "Buy for" + GameController.gunStatsList[Index].Price.ToString();
            GetComponent<Button>().enabled = false;
        }
    }

    public void TryToBuy()
    {
        if (GameController.Money >= GameController.gunStatsList[currentIndex].Price)
        {
            GameController.Money -= GameController.gunStatsList[currentIndex].Price;

            price.enabled = false;
            buyButton.enabled = false;
            shadow.enabled = false;
            buyButtonImage.enabled = false;
            GetComponent<Button>().enabled = true;

            GameController.gunStatsList[currentIndex].Owned = true;
            GameController.Save();
        }
    }

    public void Deequip()
    {
        equiped.enabled = false;
    }

    public void Equip()
    {
        if (GameController.gunStatsList[currentIndex].Owned)
        {
            SnapScrolling.instPans[GameController.EquipedGun].GetComponent<ScrollGunItem>().Deequip();
            equiped.enabled = true;
            GameController.EquipedGun = currentIndex;
        }
    }
}
