using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicIconSwitch : MonoBehaviour
{
    [SerializeField] private int magicIndex;
    [SerializeField] private Image currentIcon;
    [SerializeField] private Sprite[] iconList = new Sprite[4];

    
    public void IconChange(int newMagicIndex)
    {
        magicIndex = newMagicIndex;
        currentIcon.sprite = iconList[magicIndex];
    }

}
