using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotencyTracker : MonoBehaviour
{
    private Player player;
    [SerializeField] private int potencyNumber = 0;
    private TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        potencyNumber = player.potencyAmount;
        text.text = potencyNumber.ToString();
    }
}
