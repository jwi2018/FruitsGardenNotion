using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrimiumTicketEntity : MonoBehaviour
{
    [SerializeField] Text next_Leveltxt;
    [SerializeField] List<Sprite> Item_IMG = new List<Sprite>();

    [SerializeField] GameObject Lockobj;
    [SerializeField] GameObject[] receiveItemCheck;
    [Header("프리미엄 존")]
    [SerializeField] GameObject primium_obj;
    [SerializeField] List<Image> primium_img = new List<Image>();
    [SerializeField] List<Text> primium_txt = new List<Text>();
    [SerializeField] GameObject primium_LockObj;
    [SerializeField] GameObject primiumGetBtn;

    [Header("무료 존")]
    [SerializeField] GameObject Free_obj;
    [SerializeField] List<Image> Free_img = new List<Image>();
    [SerializeField] List<Text> Free_txt = new List<Text>();
    [SerializeField] GameObject freeGetBtn;



    public void SetTicketEntity(int _level, bool _isBuyTicket, bool _Isthislevelforlock, bool _isReceiveLevel_free, bool _isReceiveLevel_primium)
    {
        //프리미엄쪽 자물쇠
        if (_isBuyTicket) primium_LockObj.SetActive(false);

        //레벨 자물쇠
        if (_Isthislevelforlock) Lockobj.SetActive(true);

        //받은 상품들 체크표시
        if (PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM > _level+1) receiveItemCheck[0].SetActive(true);
        if (PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE > _level+1) receiveItemCheck[1].SetActive(true);
        

        //프리쪽 받기 버튼
        if (_isReceiveLevel_free)
        {
            if(_Isthislevelforlock) freeGetBtn.SetActive(false);
            else freeGetBtn.SetActive(true);
            
        }

        //프리미엄쪽 받기 버튼
        if (_isReceiveLevel_primium)
        {
            if (_Isthislevelforlock)
            {
                if (_isBuyTicket) primiumGetBtn.SetActive(false);
            }   
            else
            {
                if (_isBuyTicket) primiumGetBtn.SetActive(true);
            }
        }


        next_Leveltxt.text = (_level + 1).ToString();
        if(PrimiumTicketSystem.GetInstance != null)
        {
            //프리미엄쪽
            {
                int level = PrimiumTicketSystem.MAXLEVEL + _level;
                
                if(PrimiumTicketSystem.GetInstance.ItemValue[level].itemCount > 1)
                {
                    primium_obj.SetActive(true);
                    for (int i=0; i< PrimiumTicketSystem.GetInstance.ItemValue[level].itemCount; i++)
                    {
                        var item_name = PrimiumTicketSystem.GetInstance.ItemValue[level].Item;
                        switch (i)
                        {
                            case 0:
                                item_name = PrimiumTicketSystem.GetInstance.ItemValue[level].Item;
                                primium_txt[i].text = PrimiumTicketSystem.GetInstance.ItemValue[level].Count.ToString();
                                break;

                            case 1:
                                item_name = PrimiumTicketSystem.GetInstance.ItemValue[level].Item_1;
                                primium_txt[i].text = PrimiumTicketSystem.GetInstance.ItemValue[level].Count_1.ToString();
                                break;
                        }

                        switch (item_name)
                        {
                            case "None":
                                primium_img[i].sprite = Item_IMG[0];
                                break;

                            case "Hammer":
                                primium_img[i].sprite = Item_IMG[1];
                                break;

                            case "Bomb":
                                primium_img[i].sprite = Item_IMG[2];
                                break;

                            case "Color":
                                primium_img[i].sprite = Item_IMG[3];
                                break;

                            case "Hammer+Bomb":
                                primium_img[i].sprite = Item_IMG[4];
                                break;

                            case "Hammer+Color":
                                primium_img[i].sprite = Item_IMG[5];
                                break;

                            case "Bomb+Color":
                                primium_img[i].sprite = Item_IMG[6];
                                break;
                        }
                    }
                        
                }
                else
                {
                    var item_name = PrimiumTicketSystem.GetInstance.ItemValue[level].Item;
                    switch (item_name)
                    {
                        case "None":
                            primium_img[0].sprite = Item_IMG[0];
                            break;

                        case "Hammer":
                            primium_img[0].sprite = Item_IMG[1];
                            break;

                        case "Bomb":
                            primium_img[0].sprite = Item_IMG[2];
                            break;

                        case "Color":
                            primium_img[0].sprite = Item_IMG[3];
                            break;

                        case "Hammer+Bomb":
                            primium_img[0].sprite = Item_IMG[4];
                            break;

                        case "Hammer+Color":
                            primium_img[0].sprite = Item_IMG[5];
                            break;

                        case "Bomb+Color":
                            primium_img[0].sprite = Item_IMG[6];
                            break;



                    }

                    primium_txt[0].text = PrimiumTicketSystem.GetInstance.ItemValue[level].Count.ToString();
                }
                
            }


            //프리쪽
            {
                if (PrimiumTicketSystem.GetInstance.ItemValue[_level].itemCount > 1)
                {
                    Free_obj.SetActive(true);
                    for (int i = 0; i < PrimiumTicketSystem.GetInstance.ItemValue[_level].itemCount; i++)
                    {
                        var item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
                        switch (i)
                        {
                            case 0:
                                item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
                                Free_txt[i].text = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count.ToString();
                                break;

                            case 1:
                                item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item_1;
                                Free_txt[i].text = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count_1.ToString();
                                break;
                        }

                        switch (item_name)
                        {
                            case "None":
                                Free_img[i].sprite = Item_IMG[0];
                                break;

                            case "Hammer":
                                Free_img[i].sprite = Item_IMG[1];
                                break;

                            case "Bomb":
                                Free_img[i].sprite = Item_IMG[2];
                                break;

                            case "Color":
                                Free_img[i].sprite = Item_IMG[3];
                                break;

                            case "Hammer+Bomb":
                                Free_img[i].sprite = Item_IMG[4];
                                break;

                            case "Hammer+Color":
                                Free_img[i].sprite = Item_IMG[5];
                                break;

                            case "Bomb+Color":
                                Free_img[i].sprite = Item_IMG[6];
                                break;
                        }
                    }

                }
                else
                {
                    var item_name = PrimiumTicketSystem.GetInstance.ItemValue[_level].Item;
                    switch (item_name)
                    {
                        case "None":
                            Free_img[0].sprite = Item_IMG[0];
                            break;

                        case "Hammer":
                            Free_img[0].sprite = Item_IMG[1];
                            break;

                        case "Bomb":
                            Free_img[0].sprite = Item_IMG[2];
                            break;

                        case "Color":
                            Free_img[0].sprite = Item_IMG[3];
                            break;

                        case "Hammer+Bomb":
                            Free_img[0].sprite = Item_IMG[4];
                            break;

                        case "Hammer+Color":
                            Free_img[0].sprite = Item_IMG[5];
                            break;

                        case "Bomb+Color":
                            Free_img[0].sprite = Item_IMG[6];
                            break;
                    }

                    Free_txt[0].text = PrimiumTicketSystem.GetInstance.ItemValue[_level].Count.ToString();
                }
            }


        }


    }

    public void ChangeEntity(bool _isActive, bool _isPrimium = false)
    {
        //획득하기 버튼 다시 보기

        if(_isPrimium)
        {
            //여기서 비교해줘야하나?
            if (PrimiumTicketSystem.GetInstance.LEVEL > PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM)
            {
                primiumGetBtn.SetActive(_isActive);
                receiveItemCheck[0].SetActive(!_isActive);
            }
            else if(PrimiumTicketSystem.GetInstance.LEVEL.Equals(PrimiumTicketSystem.GetInstance.RECEIVELEVEL_PRIMIUM))
            {
                primiumGetBtn.SetActive(!_isActive);
                receiveItemCheck[0].SetActive(!_isActive);
            }
        }
        else
        {
            if (PrimiumTicketSystem.GetInstance.LEVEL > PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE)
            {
                freeGetBtn.SetActive(_isActive);
                receiveItemCheck[1].SetActive(!_isActive);

            }
            else if (PrimiumTicketSystem.GetInstance.LEVEL.Equals(PrimiumTicketSystem.GetInstance.RECEIVELEVEL_FREE))
            {
                freeGetBtn.SetActive(!_isActive);
                receiveItemCheck[1].SetActive(!_isActive);
            }

        }
        
        
    }
}
