using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public List<Color> barrelStock = new List<Color>(6);

    private static int selectedBullet = 0;
    private static int selectedForReload = 0;

    public static int SelectedBullet
    {
        get => selectedBullet;
        set
        {
            if (value > 5)
                selectedBullet = 0;
            else
                selectedBullet = value;
        }
    }

    public static int SelectedForReload
    {
        get => selectedForReload;
        set
        {
            if (value > 5)
                selectedForReload = 0;
            else
                selectedForReload = value;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < barrelStock.Capacity; i++)
        {
            barrelStock.Add(Color.black);
        }
    }

    public void AddStock(Color _color)
    {
        barrelStock[SelectedForReload] = _color;

        SelectedForReload++;
    }

    public void NextBullet()
    {
       barrelStock[selectedBullet] = Color.black;
       
       SelectedBullet++;
    }
}