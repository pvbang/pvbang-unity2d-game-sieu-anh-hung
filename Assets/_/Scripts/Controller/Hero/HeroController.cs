using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Animator animator;
    private HeroUnit hero;
    private HP hp;
    private Level level;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        hero = GetComponent<HeroUnit>();
        hp = transform.Find("CanvasStatusBar").Find("StatusBar").Find("HP").GetComponent<HP>();
        level = transform.Find("CanvasStatusBar").Find("StatusBar").Find("Level").GetComponent<Level>();
    }

    private void Start()
    {
        hp.UpdateHP(hero.h_hp, hero.h_maxHP);
        level.UpdateLevel(hero.h_level.ToString());
    }

    // update thanh máu
    public void UpdateHP(float health, float maxHealth)
    {
        hp.UpdateHP(health, maxHealth);
    }

    // update level
    public void UpdateLevel(string lv)
    {
        level.UpdateLevel(lv);
    }

    // animation bắt đầu
    public void AnimStart()
    {
        animator.SetTrigger("start");
    }

    // chết
    public IEnumerator AnimDeath()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    // skill 1
    public void Skill1()
    {
        animator.SetTrigger("skill1");
    }
}
