using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public List<Color> barrelStock = new List<Color>(6);

    public void AddStock(Color _color)
    {
        if(barrelStock.Count < 6)
        {
            barrelStock.Add(_color);
        }
    }

    public void RemoveStock()
    {
        barrelStock.RemoveAt(0);
    }
}