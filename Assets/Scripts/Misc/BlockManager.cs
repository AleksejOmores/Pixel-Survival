using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] public int woodAmount;

    public int woodPerBlock = 10;

    [SerializeField] private TextMeshProUGUI blockText;
    void Start()
    {
        UpdateBlockText();
    }

    private int CalculateAvialableBlocks()
    {
        return woodAmount / woodPerBlock;
    }

    private void UpdateBlockText()
    {
        int availableBlocks = CalculateAvialableBlocks();
        blockText.text = $"{availableBlocks}";
    }

    public void UseWood(int amount)
    {
        woodAmount -= amount;
        UpdateBlockText();
    }

    public void UpWood(int amount)
    {
        woodAmount += amount;
        UpdateBlockText();
    }
}
