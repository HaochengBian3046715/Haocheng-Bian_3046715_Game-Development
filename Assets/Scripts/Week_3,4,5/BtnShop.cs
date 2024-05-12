using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnShop : MonoBehaviour
{

    public bool IsBuy
    {

        get => PlayerPrefs.GetInt("IsBuy" + gameObject.name, 0) == 0 ? false : true;
        set
        {
            PlayerPrefs.SetInt("IsBuy" + gameObject.name, value?1:0);
            PlayerPrefs.Save();
        }
    }


    [SerializeField] Image imgItem;
    [SerializeField] GameObject txtPrice;
    [SerializeField] GameObject txtSelect;
    [SerializeField] GameObject imgIconPrice;
    public Sprite sprite;



    void Start()
    {
        imgItem.sprite = sprite;
    }

    private void OnEnable()
    {
        Validate();
    }

    private void Validate()
    {
        if (IsBuy)
        {
            txtPrice.SetActive(false);
            imgIconPrice.SetActive(false);
            txtSelect.SetActive(true);
        }
        else
        {
            txtPrice.SetActive(true);
            imgIconPrice.SetActive(true);
            txtSelect.SetActive(false);
        }
    }

    public void  buy()
    {
        IsBuy = true;
        Validate();
    }

}
