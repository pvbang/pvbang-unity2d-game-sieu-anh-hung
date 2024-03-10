using UnityEngine;

public class HeroUnit : MonoBehaviour
{
    public string id = "";
    public string h_id = "";
    public string h_name = "";
    public int h_level = 0;
    public int h_levelMax = 205;
    public int h_exp = 0;
    public int h_expMax = 1000;
    public ulong h_hp = 1000;
    public ulong h_maxHP = 1000;
    public ulong h_damagePysical = 1000;
    public ulong h_damageMagic = 1000;
    public ulong h_tank = 1000;
    public ulong h_speed = 1000;

    // thuộc tính
    public string elements = "land";
    // đất
    public ulong landAttackElements = 1000;
    public ulong landTankElements = 1000;
    // nước
    public ulong waterAttackElements = 0;
    public ulong waterTankElements = 0;
    // gió
    public ulong windAttackElements = 0;
    public ulong windTankElements = 0;
    // lửa
    public ulong fireAttackElements = 0;
    public ulong fireTankElements = 0;
    // ánh sáng
    public ulong lightAttackElements = 0;
    public ulong lightTankElements = 0;
    // bóng tối
    public ulong darkAttackElements = 0;
    public ulong darkTankElements = 0;

    // tiến hóa
    public int evolution = 0;
    // siêu tiến hóa
    public int superEvolution = 0;
    // chuyển sinh
    public int reincarnation = 0;
    // cải tạo
    public ulong renovationHP = 0;
    public ulong renovationDamagePysical = 0;
    public ulong renovationDamageMagic = 0;
    public ulong renovationTank = 0;
    public ulong renovationSpeed = 0;
    // chuyển thế
    public int transformation = 0;


    public Hero ToHero()
    {
        return new Hero
        {
            id = this.id,
            h_id = this.h_id,
            h_name = this.h_name,
            h_level = this.h_level,
            h_levelMax = this.h_levelMax,
            h_exp = this.h_exp,
            h_expMax = this.h_expMax,
            h_hp = this.h_hp,
            h_maxHP = this.h_maxHP,
            h_damagePysical = this.h_damagePysical,
            h_damageMagic = this.h_damageMagic,
            h_tank = this.h_tank,
            h_speed = this.h_speed,
            elements = this.elements,
            landAttackElements = this.landAttackElements,
            landTankElements = this.landTankElements,
            waterAttackElements = this.waterAttackElements,
            waterTankElements = this.waterTankElements,
            windAttackElements = this.windAttackElements,
            windTankElements = this.windTankElements,
            fireAttackElements = this.fireAttackElements,
            fireTankElements = this.fireTankElements,
            lightAttackElements = this.lightAttackElements,
            lightTankElements = this.lightTankElements,
            darkAttackElements = this.darkAttackElements,
            darkTankElements = this.darkTankElements,
            evolution = this.evolution,
            superEvolution = this.superEvolution,
            reincarnation = this.reincarnation,
            renovationHP = this.renovationHP,
            renovationDamagePysical = this.renovationDamagePysical,
            renovationDamageMagic = this.renovationDamageMagic,
            renovationTank = this.renovationTank,
            renovationSpeed = this.renovationSpeed,
            transformation = this.transformation
        };
    }

}
