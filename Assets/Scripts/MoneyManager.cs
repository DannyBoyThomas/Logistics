using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour 
{
    public int Money;
    
    public void AddFunds(int money)
    {
        Money += money;
    }

}
