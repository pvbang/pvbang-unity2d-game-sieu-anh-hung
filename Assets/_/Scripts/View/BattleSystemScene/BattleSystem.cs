using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleStase { 
    START, 
    HERO_LEFT_1_TURN, 
    HERO_RIGHT_1_TURN,
    WIN, LOST 
}

public class BattleSystem : MonoBehaviour
{
//    public BattleStase battleStase;
//    public TextMeshProUGUI turn;

//    // left 
//    public Transform heroLeft1Prefab;
//    public Transform heroLeft2Prefab;
//    public Transform heroLeft3Prefab;
//    public Transform heroLeft4Prefab;
//    public Transform heroLeft5Prefab;
//    public Transform heroLeft6Prefab;

//    private Transform heroLeft1Instance;
//    private Transform heroLeft2Instance;
//    private Transform heroLeft3Instance;
//    private Transform heroLeft4Instance;
//    private Transform heroLeft5Instance;
//    private Transform heroLeft6Instance;

//    private Vector3 heroLeft1Pos;

//    private Vector3 heroLeftSkillPos;

//    //private UnitHero heroLeft1Unit;
//    //private UnitHero heroLeft2Unit;
//    //private UnitHero heroLeft3Unit;
//    //private UnitHero heroLeft4Unit;
//    //private UnitHero heroLeft5Unit;
//    //private UnitHero heroLeft6Unit;

//    private Transform heroLeft1Skill1;
//    private Transform heroLeft1Skill2;

//    private Animator heroLeft1AnimSkill1;

//    private int heroLeft1Level;
//    private int heroLeft2Level;
//    private int heroLeft3Level;
//    private int heroLeft4Level;
//    private int heroLeft5Level;
//    private int heroLeft6Level;

//    private ulong heroLeft1CurrentHP;
//    private ulong heroLeft2CurrentHP;
//    private ulong heroLeft3CurrentHP;
//    private ulong heroLeft4CurrentHP;
//    private ulong heroLeft5CurrentHP;
//    private ulong heroLeft6CurrentHP;

//    private ulong heroLeft1TotalHP;
//    private ulong heroLeft2TotalHP;
//    private ulong heroLeft3TotalHP;
//    private ulong heroLeft4TotalHP;
//    private ulong heroLeft5TotalHP;
//    private ulong heroLeft6TotalHP;

//    private ulong heroLeft1Damage;
//    private ulong heroLeft2Damage;
//    private ulong heroLeft3Damage;
//    private ulong heroLeft4Damage;
//    private ulong heroLeft5Damage;
//    private ulong heroLeft6Damage;

//    // right 
//    public Transform heroRight1Prefab;
//    public Transform heroRight2Prefab;
//    public Transform heroRight3Prefab;
//    public Transform heroRight4Prefab;
//    public Transform heroRight5Prefab;
//    public Transform heroRight6Prefab;

//    private Transform heroRight1Instance;
//    private Transform heroRight2Instance;
//    private Transform heroRight3Instance;
//    private Transform heroRight4Instance;
//    private Transform heroRight5Instance;
//    private Transform heroRight6Instance;

//    private Vector3 heroRight1Pos;

//    //private UnitHero heroRight1Unit;
//    //private UnitHero heroRight2Unit;
//    //private UnitHero heroRight3Unit;
//    //private UnitHero heroRight4Unit;
//    //private UnitHero heroRight5Unit;
//    //private UnitHero heroRight6Unit;

//    private int heroRight1Level;
//    private int heroRight2Level;
//    private int heroRight3Level;
//    private int heroRight4Level;
//    private int heroRight5Level;
//    private int heroRight6Level;

//    private ulong heroRight1CurrentHP;
//    private ulong heroRight2CurrentHP;
//    private ulong heroRight3CurrentHP;
//    private ulong heroRight4CurrentHP;
//    private ulong heroRight5CurrentHP;
//    private ulong heroRight6CurrentHP;

//    private ulong heroRight1TotalHP;
//    private ulong heroRight2TotalHP;
//    private ulong heroRight3TotalHP;
//    private ulong heroRight4TotalHP;
//    private ulong heroRight5TotalHP;
//    private ulong heroRight6TotalHP;

//    private ulong heroRight1Damage;
//    private ulong heroRight2Damage;
//    private ulong heroRight3Damage;
//    private ulong heroRight4Damage;
//    private ulong heroRight5Damage;
//    private ulong heroRight6Damage;

//    // status bar
//    private TextMeshProUGUI textLevel;
//    private Image imageHP;

//    public float moveSpeed = 20f;


//    void Start()
//    {
//        battleStase = BattleStase.START;
//        SetupBattle();
//        //StartBattleSystem();
//    }

//    void SetupBattle()
//    {
//        // set position hero
//        heroLeft1Pos = new Vector3(-3.5f, -0.3f, 0f);
//        heroLeft1Instance = Instantiate(heroLeft1Prefab, heroLeft1Pos, Quaternion.identity);

//        heroRight1Pos = new Vector3(3.5f, -0.3f, 0f);
//        heroRight1Instance = Instantiate(heroRight1Prefab, heroRight1Pos, Quaternion.identity);

//        //// set unit hero
//        //heroLeft1Unit = heroLeft1Instance.GetComponent<UnitHero>();

//        //heroRight1Unit = heroRight1Instance.GetComponent<UnitHero>();

//        // get info hero
//        //getInfoHeroLeft1(heroLeft1Unit);

//        //getInfoHeroRight1(heroRight1Unit);

//        // set skill left hero
//        heroLeftSkillPos = new Vector3(4.5f, 0f, 0f);

//        // set level hero
//        updateLevel(heroLeft1Instance, heroLeft1Level);

//        updateLevel(heroRight1Instance, heroRight1Level);

//        // set hp hero
//        updateHP(heroLeft1Instance, heroLeft1CurrentHP / heroLeft1TotalHP);

//        updateHP(heroRight1Instance, heroRight1CurrentHP / heroRight1TotalHP);

//        // set flip right hero
//        //flipHero(heroRight1Unit);

//        // skill left 1
//        heroLeft1AnimSkill1 = heroLeft1Instance.GetComponent<Animator>();

//        // log
//        // Debug.Log(heroLeft1Damage);
//    }

//    async void StartBattleSystem()
//    {
//        await Task.Delay(3000);
//        updateTurn();

//        battleStase = BattleStase.HERO_LEFT_1_TURN;
//    }

///*    void Update()
//    {
//        switch(battleStase)
//        {
//            case BattleStase.HERO_LEFT_1_TURN:
//                battleStase = BattleStase.HERO_LEFT_1_MOVE;
//                playSkill1Left1();
//                break;
//            case BattleStase.HERO_LEFT_1_MOVE:
//                heroLeft1Instance.transform.position = Vector3.MoveTowards(heroLeft1Instance.transform.position, new Vector3(-1.5f, -1.5f, 0f), moveSpeed * Time.deltaTime);
//                break;
//            case BattleStase.HERO_LEFT_1_END_TURN:
//                heroLeft1Instance.transform.position = Vector3.MoveTowards(heroLeft1Instance.transform.position, heroLeft1Pos, moveSpeed * Time.deltaTime);
//                // battleStase = BattleStase.WAIT_TURN;
//                // battleStase = BattleStase.HERO_RIGHT_1_TURN;
//                break;
//            default: break;
//        }

//    }*/

//    void moveHeroLeftToAttackPos(Vector3 firstPos)
//    {
//        firstPos = Vector3.MoveTowards(firstPos, new Vector3(-1.5f, -1.5f, 0f), moveSpeed * Time.deltaTime);
//    }

//    void moveHeroLeftToDefaultPos(Vector3 firstPos, Vector3 defaultPos)
//    {
//        firstPos = Vector3.MoveTowards(firstPos, defaultPos, moveSpeed * Time.deltaTime);
//    }


//    // wait
//    private IEnumerator WaitForSeconds(float seconds)
//    {
//        yield return new WaitForSeconds(seconds);
//        heroLeft1AnimSkill1.SetBool("Skill1", false);
        
//        //battleStase = BattleStase.HERO_LEFT_1_END_TURN;
//    }

//    // turn
//    void updateTurn()
//    {
//        if (turn != null)
//        {
//            if (turn.text == "Giai đoạn chuẩn bị")
//            {
//                turn.text = "1/30";
//            }
//            else if (turn.text == "30/30")
//            {
//                turn.text = "Kết thúc";
//            }
//            else if (turn.text == "Kết thúc")
//            {
                
//            }
//            else
//            {
//                int currentTurn = int.Parse(turn.text.Split('/')[0]);
//                int nextTurn = currentTurn + 1;
//                turn.text = $"{nextTurn}/30";
//            }
//        }
//    }

//    // info
//    //void getInfoHeroLeft1(Hero heroUnit)
//    //{
//    //    if (heroUnit != null)
//    //    {
//    //        heroLeft1Level = getLevel(heroUnit);
//    //        heroLeft1TotalHP = getHP(heroUnit);
//    //        heroLeft1CurrentHP = heroLeft1TotalHP;
//    //        heroLeft1Damage = getDamage(heroUnit);
//    //        heroUnit.playSkill1();
//    //        //heroLeft1Skill1 = getHeroLeft1Skill1(heroUnit);
//    //    }
//    //}

//    //void getInfoHeroRight1(UnitHero heroUnit)
//    //{
//    //    if (heroUnit != null)
//    //    {
//    //        heroRight1Level = getLevel(heroUnit);
//    //        heroRight1TotalHP = getHP(heroUnit);
//    //        heroRight1CurrentHP = heroRight1TotalHP;
//    //        heroRight1Damage = getDamage(heroUnit);
//    //    }
//    //}

//    //// flip
//    //void flipHero(UnitHero heroUnit)
//    //{
//    //    Vector3 heroScale = heroUnit.transform.localScale;
//    //    heroScale.x = heroScale.x * (-1);
//    //    heroUnit.transform.localScale = heroScale;
//    //}

//    //// damage
//    //ulong getDamage(UnitHero unitHero)
//    //{
//    //    ulong heroDamage = 0;
//    //    if (unitHero != null)
//    //    {
//    //        heroDamage = unitHero.damagePysical;
//    //    }

//    //    return heroDamage;
//    //}
//    /*
//    // get skill
//    Transform getHeroLeft1Skill1(UnitHero heroUnit)
//    {
//        Transform transform = null;
//        if (heroUnit != null)
//        {
//            transform = heroUnit.skill1;
//        }
//        return transform;
//    }

//    Transform getHeroLeft1Skill2(UnitHero heroUnit)
//    {
//        Transform transform = null;
//        if (heroUnit != null)
//        {
//            transform = heroUnit.skill2;
//        }
//        return transform;
//    }

//    // play skill
//    void playSkill1Left1()
//    {
//        Instantiate(heroLeft1Skill1, heroLeftSkillPos, Quaternion.identity);

//        heroLeft1AnimSkill1.SetBool("Skill1", true);
//        StartCoroutine(WaitForSeconds(7.7f));
//    }*/

//    // hp
//    //ulong getHP(UnitHero unitHero)
//    //{
//    //    ulong heroHP = 0;
//    //    if (unitHero != null)
//    //    {
//    //        heroHP = unitHero.hp;
//    //    }

//    //    return heroHP;
//    //}
//    void updateHP(Transform heroInstance, ulong fillAmountValue)
//    {
//        if(heroLeft1Instance != null)
//        {
//            Image[] allImages = heroInstance.GetComponentsInChildren<Image>();
//            foreach (Image image in allImages)
//            {
//                if (image.gameObject.name == "HP")
//                {
//                    imageHP = image;
//                    break;
//                }
//            }

//            if (imageHP != null)
//            {
//                imageHP.fillAmount = fillAmountValue;
//            }
//        }
//    }

//    // level
//    //int getLevel(UnitHero unitHero)
//    //{
//    //    int heroLevel = 0;
//    //    if (unitHero != null)
//    //    {
//    //        heroLevel = unitHero.level;
//    //    }

//    //    return heroLevel;
//    //}
//    void updateLevel(Transform heroInstance, int heroLeft1Level)
//    {
//        TextMeshProUGUI text = heroInstance.GetComponentInChildren<TextMeshProUGUI>();

//        textLevel = text;

//        if (textLevel != null)
//        {
//            textLevel.text = heroLeft1Level.ToString();
//        }
//    }

//    //

}
