public class Hero
{
    public string id;
    public string name;
    public int level;
    public ulong hp;
    public ulong damagePysical;
    public ulong damageMagic;
    public ulong tank;
    public ulong speed;

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

    public Hero() { }
    
    public Hero(string id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public Hero(string id, string name, int level)
    {
        this.id = id;
        this.name = name;
        this.level = level;
    }

    public Hero(string id, string name, int level, ulong hp, ulong damagePysical, ulong damageMagic, ulong tank, ulong speed)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.damagePysical = damagePysical;
        this.damageMagic = damageMagic;
        this.tank = tank;
        this.speed = speed;
    }

    public Hero(string id, string name, int level, ulong hp, ulong damagePysical, ulong damageMagic, ulong tank, ulong speed, string elements)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.damagePysical = damagePysical;
        this.damageMagic = damageMagic;
        this.tank = tank;
        this.speed = speed;
        this.elements = elements;
    }

    public Hero(string id, string name, int level, ulong hp, ulong damagePysical, ulong damageMagic, ulong tank, ulong speed, string elements, ulong landAttackElements, ulong landTankElements, ulong waterAttackElements, ulong waterTankElements, ulong windAttackElements, ulong windTankElements, ulong fireAttackElements, ulong fireTankElements, ulong lightAttackElements, ulong lightTankElements, ulong darkAttackElements, ulong darkTankElements)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.damagePysical = damagePysical;
        this.damageMagic = damageMagic;
        this.tank = tank;
        this.speed = speed;
        this.elements = elements;
        this.landAttackElements = landAttackElements;
        this.landTankElements = landTankElements;
        this.waterAttackElements = waterAttackElements;
        this.waterTankElements = waterTankElements;
        this.windAttackElements = windAttackElements;
        this.windTankElements = windTankElements;
        this.fireAttackElements = fireAttackElements;
        this.fireTankElements = fireTankElements;
        this.lightAttackElements = lightAttackElements;
        this.lightTankElements = lightTankElements;
        this.darkAttackElements = darkAttackElements;
        this.darkTankElements = darkTankElements;
    }

    public Hero(string id, string name, int level, ulong hp, ulong damagePysical, ulong damageMagic, ulong tank, ulong speed, string elements, ulong landAttackElements, ulong landTankElements, ulong waterAttackElements, ulong waterTankElements, ulong windAttackElements, ulong windTankElements, ulong fireAttackElements, ulong fireTankElements, ulong lightAttackElements, ulong lightTankElements, ulong darkAttackElements, ulong darkTankElements, int evolution, int superEvolution, int reincarnation)
    {
        this.id = id;
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.damagePysical = damagePysical;
        this.damageMagic = damageMagic;
        this.tank = tank;
        this.speed = speed;
        this.elements = elements;
        this.landAttackElements = landAttackElements;
        this.landTankElements = landTankElements;
        this.waterAttackElements = waterAttackElements;
        this.waterTankElements = waterTankElements;
        this.windAttackElements = windAttackElements;
        this.windTankElements = windTankElements;
        this.fireAttackElements = fireAttackElements;
        this.fireTankElements = fireTankElements;
        this.lightAttackElements = lightAttackElements;
        this.lightTankElements = lightTankElements;
        this.darkAttackElements = darkAttackElements;
        this.darkTankElements = darkTankElements;
        this.evolution = evolution;
        this.superEvolution = superEvolution;
        this.reincarnation = reincarnation;
    }
}
