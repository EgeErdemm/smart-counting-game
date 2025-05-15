using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSubUI : MonoBehaviour
{
    [SerializeField] private Button AddButton, SubButton;

    public static int AddSubMode;

    private ColorBlock AddBlock;
    private ColorBlock SubBlock;
    private Color grey = new Color32(200, 200, 200, 255);

    private void Start()
    {
        AddSubMode = 1;
        AddBlock = AddButton.colors;
        SubBlock = SubButton.colors;
        //baslangicta sectim
        AddBlock.normalColor = grey;
        AddButton.colors = AddBlock;
        //
    }

    public void AddMode()
    {
        AddSubMode = 1;

        AddBlock.normalColor = grey;
        SubBlock.normalColor = Color.white;

        AddButton.colors = AddBlock;
        SubButton.colors = SubBlock;
    }

    public void SubMode()
    {
        AddSubMode = -1;

        SubBlock.normalColor = grey;
        AddBlock.normalColor = Color.white;

        AddButton.colors = AddBlock;
        SubButton.colors = SubBlock;
    }
}
