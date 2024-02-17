using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardController : MonoBehaviour
{
    public Image image;
    public Image frame;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (image == null)
        {
            image = transform.Find("Reward").Find("Image").GetComponent<Image>();
        }
        if (frame == null)
        {
            frame = transform.Find("Reward").Find("Image").Find("Frame").GetComponent<Image>();
        }
        if (text == null)
        {
            text = transform.Find("Reward").Find("Text").GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetReward(Sprite image, Sprite frame, string rewardText)
    {
        this.image.sprite = image;
        this.frame.sprite = frame;
        this.text.text = rewardText;
    }
}
