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

    public void SetReward(Image image, Image frame, string rewardText)
    {
        this.image.sprite = image.sprite;
        this.frame.sprite = frame.sprite;
        this.text.text = rewardText;
    }
}
