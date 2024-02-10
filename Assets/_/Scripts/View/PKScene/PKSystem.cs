using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKSystem : MonoBehaviour
{
    // game object left
    public GameObject LEFT_1;
    public GameObject LEFT_2;
    public GameObject LEFT_3;
    public GameObject LEFT_4;
    public GameObject LEFT_5;
    public GameObject LEFT_6;

    // game object right
    public GameObject RIGHT_1;
    public GameObject RIGHT_2;
    public GameObject RIGHT_3;
    public GameObject RIGHT_4;
    public GameObject RIGHT_5;
    public GameObject RIGHT_6;

    // pos left
    public GameObject LEFT_POS_1;
    public GameObject LEFT_POS_2;
    public GameObject LEFT_POS_3;
    public GameObject LEFT_POS_4;
    public GameObject LEFT_POS_5;
    public GameObject LEFT_POS_6;

    // pos right
    public GameObject RIGHT_POS_1;
    public GameObject RIGHT_POS_2;
    public GameObject RIGHT_POS_3;
    public GameObject RIGHT_POS_4;
    public GameObject RIGHT_POS_5;
    public GameObject RIGHT_POS_6;

    // hero left
    private GameObject HERO_LEFT_1;
    private GameObject HERO_LEFT_2;
    private GameObject HERO_LEFT_3;
    private GameObject HERO_LEFT_4;
    private GameObject HERO_LEFT_5;
    private GameObject HERO_LEFT_6;

    // hero right
    private GameObject HERO_RIGHT_1;
    private GameObject HERO_RIGHT_2;
    private GameObject HERO_RIGHT_3;
    private GameObject HERO_RIGHT_4;
    private GameObject HERO_RIGHT_5;
    private GameObject HERO_RIGHT_6;

    private void Awake()
    {
        if (LEFT_1 != null)
        {
            // tạo object hero left 1 lên pos left 1
            HERO_LEFT_1 = Instantiate(LEFT_1, LEFT_POS_1.transform.position, Quaternion.identity);

            // animation bắt đầu
            HERO_LEFT_1.GetComponent<HeroController>().AnimStart();
        }

        if (RIGHT_1 != null)
        {
            // tạo object hero right 1 lên pos right 1
            GameObject HERO_RIGHT_1 = Instantiate(RIGHT_1, RIGHT_POS_1.transform.position, Quaternion.identity);

            // đảo chiều của hero right 1
            HERO_RIGHT_1.transform.localScale = new Vector3(-(HERO_RIGHT_1.transform.localScale.x), HERO_RIGHT_1.transform.localScale.y, HERO_RIGHT_1.transform.localScale.z);
            Vector3 heroScale = HERO_RIGHT_1.transform.Find("CanvasStatusBar").localScale;
            HERO_RIGHT_1.transform.Find("CanvasStatusBar").localScale = new Vector3(-(heroScale.x), heroScale.y, heroScale.z);

            // animation bắt đầu
            HERO_RIGHT_1.GetComponent<HeroController>().AnimStart();
        }

    }

}
