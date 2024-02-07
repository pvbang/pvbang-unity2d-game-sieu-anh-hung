using System;
using System.Diagnostics;
using System.Linq;

public class RandomStringGenerator 
{
    private static readonly Random random = new Random();
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public string randomString;
    public string randomStringHero;

    static string[] vietnamesePrefixes = { "Siêu", "Huyền", "Thần", "Hoàng", "Anh", "Thiên", "Phi", "Huyền Diệu", "Siêu Phàm", "Phi Phàm", "Phi Vụn", "Siêu Vượt", "Thần Thoại", "Anh Hùng", "Anh Hùng Sáng", "Anh Hùng Dũng Mãnh", "Anh Hùng Huyền Diệu", "Huyền Thoại", "Vô Song", "Phi Thiên", "Siêu Cấp", "Huyền Thần", "Thần Vũ", "Thần Tốc", "Hoàng Kim", "Hoàng Thượng", "Hoàng Tộc", "Hoàng Gia", "Hoàng Đế", "Hoàng Long", "Anh Chàng", "Anh Tài", "Anh Tình", "Thiên Tài", "Thiên Tỉ", "Thiên Thần", "Phiên Bản", "Phi Tưởng", "Phi Độc", "Phi Dị", "Vĩ Đại", "Vĩ Vàng", "Vĩ Thượng", "Vĩ Phong", "Vĩ Tuyến", "Vĩ Hùng", "Vĩ Sư", "Vĩ Hiệp", "Vĩ Cảnh" }; 
    static string[] vietnameseAdjectives = { "Dũng Cảm", "Mạnh Mẽ", "Kiên Cường", "Phi Thường", "Nhanh Nhẹn", "Quyết Liệt", "Tinh Nhuệ", "Vĩ Đại", "Vĩ Vàng", "Vĩ Thượng", "Vĩ Phong", "Vĩ Tuyến", "Vĩ Hùng", "Vĩ Sư", "Vĩ Hiệp", "Vĩ Cảnh", "Dũng Mãnh", "Dũng Ngã", "Dũng Sĩ", "Dũng Thương", "Dũng Tử", "Dũng Nghĩa", "Dũng Sĩ Đạo", "Mạnh Mẽ", "Độc Đáo", "Mênh Mông", "Sáng Tạo", "Đa Dạng", "Thành Đạt", "Sáng Suốt", "Chắc Chắn", "Hoàn Hảo", "Kiên Cường", "Thoải Mái", "Ôn Hòa", "Lập Trường", "Linh Hoạt", "Nhẫn Nại", "Trí Tuệ", "Tự Tin", "Lạc Quan", "Hài Hước", "Hợp Tác", "Tận Tâm", "Tỉ Mỉ", "Chân Thành", "Phi Thường", "Đẳng Cấp", "Hiệu Quả", "Hấp Dẫn", "Tuyệt Vời", "Tiên Tiến", "Lạ Mắt", "Hiện Đại", "Chuyên Nghiệp", "Hiếm Có", "Thú Vị", "Nhanh Nhẹn", "Tinh Tế", "Chính Xác", "Linh Hoạt", "Nhanh Nhạy", "Phản Xạ", "Nắm Bắt", "Thông Minh", "Sáng Suốt", "Sáng Tạo", "Tích Cực", "Lạc Quan" };
    static string[] suffixes = { "Man", "Girl", "Boy", "Hero", "Warrior", "Knight", "Ninja", "Master", "Champion", "Legend", "Guardian", "Savior", "Conqueror", "Protector", "Gladiator", "Crusader", "Avenger", "Hunter", "Assassin", "Ranger", "Samurai", "Pirate", "Mage", "Wizard", "Sorcerer", "Summoner", "Druid", "Warlock", "Paladin", "Barbarian", "Monk", "Archer", "Bard", "Shaman", "Priest", "Alchemist", "Engineer", "Rogue", "Sage", "Mystic", "Oracle", "Prophet", "Scribe", "Jester", "Minstrel", "Brawler", "Berserker", "Centurion", "Vigilante" };

    // ramdom tên nhân vật
    public static string GenerateRandomName()
    {
        string prefix = vietnamesePrefixes[random.Next(vietnamesePrefixes.Length)];
        string adjective = vietnameseAdjectives[random.Next(vietnameseAdjectives.Length)];
        string suffix = suffixes[random.Next(suffixes.Length)];
        return $"{prefix} {adjective} {suffix}";
    }

    // ramdom chuỗi ký tự
    public static string GenerateRandomString(int length)
    {
        if (length <= 0 || length > chars.Length)
        {
            throw new ArgumentOutOfRangeException("length", "Length must be greater than 0 and less than or equal to " + chars.Length);
        }

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }
}
