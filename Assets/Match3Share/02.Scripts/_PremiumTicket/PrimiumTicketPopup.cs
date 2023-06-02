using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class AnimItemValue
{
    public string item_name;
    public Sprite item_img;
}

public class PrimiumTicketPopup : PopupSetting
{
    bool IsBuyTicket;
    private int StarCount;
    private int MaxStarCount;
    public GameObject gobDummy;
    public Transform trSlotParent;

    [SerializeField] Text need_starCounttxt;
    [SerializeField] Text next_Leveltxt;
    [SerializeField] Image GuageIMG;
    [SerializeField] ScrollRect contentPos;

    [Header("아이템 애니메이션")]
    [SerializeField] List<Image> IMG = new List<Image>();
    [SerializeField] Animator GetItemAnim;
    [SerializeField] AnimItemValue[] itemValue;

    private List<PrimiumTicketEntity> ticketEntityList = new List<PrimiumTicketEntity>();

   


    private void Start()
    {
        if(PrimiumTicketSystem.GetInstance != null) IsBuyTicket = PrimiumTicketSystem.GetInstance.ISBUYPRIMIUMTICKET;
        gobDummy.SetActive(false);
        
        OnPopupSetting();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(contentPos.verticalNormalizedPosition);
        }
    }

    public override void OnPopupSetting()
    {
        //레벨, 별획득 상태
        next_Leveltxt.text = (PrimiumTicketSystem.GetInstance.LEVEL + 1).ToString();
        StarCount = PrimiumTicketSystem.GetInstance.PRIMIUMTICKETSTAR;
        MaxStarCount = PrimiumTicketSystem.GetInstance.LEVEL;
        need_starCounttxt.text = $"{StarCount} / {MaxStarCount}";
        
        float amount = (float)StarCount / MaxStarCount;
        GuageIMG.fillAmount = amount;

        int nowLevel = 0;
        for (int i = 0; i < PrimiumTicketSystem.GetInstance.ItemValue.Count / 2; i++)
        {
            var loadedDailyMission = GameObject.Instantiate(gobDummy, trSlotParent);
            loadedDailyMission.SetActive(true);
            loadedDailyMission.name = $"Level_{i + 1}";

            var PTEntity = loadedDailyMission.GetComponent<PrimiumTicketEntity>();
            bool isthisLevelForLock = false;
            bool isReceiveLevel_primium = false;
            bool isReceiveLevel_free = false;

            if (PrimiumTicketSystem.GetInstance.LEVEL.Equals(i+1))
            {
                nowLevel = i;
                isthisLevelForLock = true;
            }

            if (PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM.Equals(i + 1)) isReceiveLevel_primium = true;
            if (PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE.Equals(i + 1)) isReceiveLevel_free = true;



            PTEntity.SetTicketEntity(i, IsBuyTicket, isthisLevelForLock, isReceiveLevel_free, isReceiveLevel_primium);

            ticketEntityList.Add(PTEntity);
        }

        //현재 레벨 위치 잡기(다시 보기)
        var levelpercent = (float)nowLevel / 25;
        contentPos.verticalNormalizedPosition = 1f - levelpercent;
    }

    public override void OffPopupSetting()
    {
        this.GetComponent<Animator>().SetTrigger("Off");
    }

    public void GetItem(bool _Isprimium)
    {
        
        if (_Isprimium)
        {
            int EntityTarget = PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM - 1;

            var level = EntityTarget + PrimiumTicketSystem.MAXLEVEL;
            GetLevelItemValue(level);
            
            //두번 되어야함
            ticketEntityList[EntityTarget].ChangeEntity(false, true);
            PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM++;
            EntityTarget++;
            ticketEntityList[EntityTarget].ChangeEntity(true, true);
        }
        else
        {
            int EntityTarget = PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE - 1;

            GetLevelItemValue(EntityTarget);
            
            ticketEntityList[EntityTarget].ChangeEntity(false);
            PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE++;
            EntityTarget++;
            ticketEntityList[EntityTarget].ChangeEntity(true);
        }

        var obj = transform.parent.gameObject;
        obj.GetComponent<PopupManager>().GoldRefresh(true);

    }

    public void GetLevelItemValue(int _level)
    {
        if (PrimiumTicketSystem.GetInstance.ItemValue[_level].itemCount > 1)
        {
            for (int i = 0; i < PrimiumTicketSystem.GetInstance.ItemValue[_level].itemCount; i++)
            {
                string item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
                int item_count = 0;

                switch (i)
                {
                    case 0:
                        item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
                        item_count = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count;
                        break;

                    case 1:
                        item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item_1;
                        item_count = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count_1;
                        break;
                }

                switch (item_name)
                {
                    case "None":
                        PlayerData.GetInstance.Gold += item_count;
                        break;

                    case "Hammer":
                        PlayerData.GetInstance.ItemHammer += item_count;
                        break;

                    case "Bomb":
                        PlayerData.GetInstance.ItemBomb += item_count;
                        break;

                    case "Color":
                        PlayerData.GetInstance.ItemColor += item_count;
                        break;

                    case "Hammer+Bomb":
                        PlayerData.GetInstance.ItemHammer += item_count;
                        PlayerData.GetInstance.ItemBomb += item_count;
                        break;

                    case "Hammer+Color":
                        PlayerData.GetInstance.ItemHammer += item_count;
                        PlayerData.GetInstance.ItemColor += item_count;
                        break;

                    case "Bomb+Color":
                        PlayerData.GetInstance.ItemBomb += item_count;
                        PlayerData.GetInstance.ItemColor += item_count;
                        break;
                }

                foreach (var img in itemValue)
                {
                    if (img.item_name.Equals(item_name)) IMG[i].sprite = img.item_img;
                }
            }
            GetItemAnim.SetTrigger("GetItemAnim_2");
        }
        else
        {
            string item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
            int item_count = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count;

            switch (item_name)
            {
                case "None":
                    PlayerData.GetInstance.Gold += item_count;
                    break;

                case "Hammer":
                    PlayerData.GetInstance.ItemHammer += item_count;
                    break;

                case "Bomb":
                    PlayerData.GetInstance.ItemBomb += item_count;
                    break;

                case "Color":
                    PlayerData.GetInstance.ItemColor += item_count;
                    break;

                case "Hammer+Bomb":
                    PlayerData.GetInstance.ItemHammer += item_count;
                    PlayerData.GetInstance.ItemBomb += item_count;
                    break;

                case "Hammer+Color":
                    PlayerData.GetInstance.ItemHammer += item_count;
                    PlayerData.GetInstance.ItemColor += item_count;
                    break;

                case "Bomb+Color":
                    PlayerData.GetInstance.ItemBomb += item_count;
                    PlayerData.GetInstance.ItemColor += item_count;
                    break;
            }

            foreach (var img in itemValue)
            {
                if (img.item_name.Equals(item_name)) IMG[0].sprite = img.item_img;
            }

            GetItemAnim.SetTrigger("GetItemrAnim_1");
            
        }
    }

    public void BuyTicket()
    {
        //사면
        
    }
}
