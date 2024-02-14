using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKSystem : MonoBehaviour
{
    // game object left
    public GameObject[] LEFT = new GameObject[9];

    // game object right
    public GameObject[] RIGHT = new GameObject[9];

    // pos left
    public Transform[] LEFT_POS = new Transform[6];

    // pos right
    public Transform[] RIGHT_POS = new Transform[6];

    // hero left
    private GameObject[] LEFT_HERO = new GameObject[9];

    // hero right
    private GameObject[] RIGHT_HERO = new GameObject[9];

    private void Awake()
    {
        SpawnLeftHero(0, LEFT_POS[0]);
        SpawnLeftHero(1, LEFT_POS[1]);
        SpawnLeftHero(2, LEFT_POS[2]);
        SpawnLeftHero(3, LEFT_POS[3]);
        SpawnLeftHero(4, LEFT_POS[4]);
        SpawnLeftHero(5, LEFT_POS[5]);

        SpawnRightHero(0, RIGHT_POS[0]);
        SpawnRightHero(1, RIGHT_POS[1]);
        SpawnRightHero(2, RIGHT_POS[2]);
        SpawnRightHero(3, RIGHT_POS[3]);
        SpawnRightHero(4, RIGHT_POS[4]);
        SpawnRightHero(5, RIGHT_POS[5]);

        
    }

    private void SpawnLeftHero(int numHero, Transform LEFT_POS)
    {
        if (LEFT[numHero] != null)
        {
            // tạo object hero left 1 lên pos left 1
            LEFT_HERO[numHero] = Instantiate(LEFT[numHero], LEFT_POS.position, Quaternion.identity);

            // animation bắt đầu
            LEFT_HERO[numHero].GetComponent<HeroController>().AnimStart();
        }
    }

    private void SpawnRightHero(int numHero, Transform RIGHT_POS)
    {
        if (RIGHT[numHero] != null)
        {
            // tạo object hero right 1 lên pos right 1
            RIGHT_HERO[numHero] = Instantiate(RIGHT[numHero], RIGHT_POS.position, Quaternion.identity);

            // đảo chiều của hero right 1
            RIGHT_HERO[numHero].transform.localScale = new Vector3(-(RIGHT_HERO[numHero].transform.localScale.x), RIGHT_HERO[numHero].transform.localScale.y, RIGHT_HERO[numHero].transform.localScale.z);
            Vector3 heroScale = RIGHT_HERO[numHero].transform.Find("CanvasStatusBar").localScale;
            RIGHT_HERO[numHero].transform.Find("CanvasStatusBar").localScale = new Vector3(-(heroScale.x), heroScale.y, heroScale.z);

            // animation bắt đầu
            RIGHT_HERO[numHero].GetComponent<HeroController>().AnimStart();
        }
    }

}
