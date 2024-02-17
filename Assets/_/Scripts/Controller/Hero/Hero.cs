using UnityEngine;

public class Hero : MonoBehaviour
{
    public string h_name;
    public int h_level = 0;
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
}
