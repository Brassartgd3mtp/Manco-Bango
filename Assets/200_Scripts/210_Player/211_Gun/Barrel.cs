using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public List<Color> barrelStock = new List<Color>(6);
    public static int selectedBullet;
    private int selectedForReload
    {
        get => selectedForReload;
        set
        {
            if (selectedForReload > 6)
            {
                selectedForReload = 0;
            }
        }
    }

    private void Awake()
    {
        for (int i = 0; i < barrelStock.Count; i++)
        {
            barrelStock[i] = Color.black;
        }
    }

    public void AddStock(Color _color)
    {
        if (barrelStock[5] != Color.black)
        barrelStock[selectedForReload] = _color;
        selectedForReload++;
    }

    public void NextBullet()
    {
        selectedBullet++;
    }
}