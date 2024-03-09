public class Hero
{
    public string id;
    public string h_id;
    public string h_name;
    public int h_level;
    public int h_levelMax;
    public int h_exp;
    public int h_expMax;
    public ulong h_hp;
    public ulong h_maxHP;
    public ulong h_damagePysical;
    public ulong h_damageMagic;
    public ulong h_tank;
    public ulong h_speed;

    // thuộc tính
    public string elements;
    // đất
    public ulong landAttackElements;
    public ulong landTankElements;
    // nước
    public ulong waterAttackElements;
    public ulong waterTankElements;
    // gió
    public ulong windAttackElements;
    public ulong windTankElements;
    // lửa
    public ulong fireAttackElements;
    public ulong fireTankElements;
    // ánh sáng
    public ulong lightAttackElements;
    public ulong lightTankElements;
    // bóng tối
    public ulong darkAttackElements;
    public ulong darkTankElements;

    // tiến hóa
    public int evolution;
    // siêu tiến hóa
    public int superEvolution;
    // chuyển sinh
    public int reincarnation;
    // cải tạo
    public ulong renovationHP;
    public ulong renovationDamagePysical;
    public ulong renovationDamageMagic;
    public ulong renovationTank;
    public ulong renovationSpeed;
    // chuyển thế
    public int transformation;
}
