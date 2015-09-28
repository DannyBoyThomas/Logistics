using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoneyManager : MonoBehaviour 
{
    public int Money;
    public int gain;

    public Text CurrentMoney;
    public Text AverageGain;
    public Color PositiveColor;
    public Color NegativeColor;

    public int LastMoney;
    float timer = 0;
    
    public GameObject floatingText;


    public void Start()
    {
    }
    public void AddFunds(int money)
    {
        gain += money;
        Money += money;
    }

    public void AddFunds(int money, Vector3 pos)
    {
        AddFunds(money);
        SpawnFloatingText(pos, money);
    }

    public void BoughtObject(GameObject go)
    {
        if (!go.GetComponent<WorldObject>())
            return;

        WorldObject obj = go.GetComponent<WorldObject>();

        SpawnFloatingText(obj.transform.position, obj.Cost);
    }


    public void SpawnFloatingText(Vector3 worldPos, int Price)
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject floating = GameObject.Instantiate(floatingText);
        //floating.GetComponent<RectTransform>().anchoredPosition = pos;


        FloatingText text = floating.GetComponent<FloatingText>(); text.worldPos = (Vector3)worldPos;
        if (Price > 0)
            text.color = PositiveColor;
        else
            text.color = NegativeColor;

        text.Text = "$ " + Price;
        
        floating.transform.SetParent(this.transform);
    }
    

    public int GetAverage()
    {
        return gain;
        //return money;
    }

    public void Update()
    {
        if (gain < 0)
            AverageGain.color = NegativeColor;
        else
            AverageGain.color = PositiveColor;

        AverageGain.text = "$ "  + gain;


        if (Money < 0)
            CurrentMoney.color = NegativeColor;
        else
            CurrentMoney.color = PositiveColor;

        CurrentMoney.text = "$ " + Money;

        timer -= Time.deltaTime;

        if (timer < 0)
        {

            if (gain < 0)
                AverageGain.color = NegativeColor;
            else
                AverageGain.color = PositiveColor;

            AverageGain.text = "$ " + gain;

            timer = 3;
            gain = 0;
        }
    }

}
