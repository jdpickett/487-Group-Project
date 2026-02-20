using Microsoft.Xna.Framework;

namespace Core;

public static class GameConfig
{
    // Window Size
    public const int ScreenWidth = 960;
    public const int ScreenHeight = 540;

    // Playfield bounds (player movement confined area)
    // adjust to match UI panel thats added later

    public static readonly Rectangle Playfield = new Rectangle(40, 20, ScreenWidth - 80, ScreenHeight - 40);

    public const float PlayerSpeedNormal = 260f;
    public const float PlayerSpeedSlow = 130f;
    public const float BulletSpeedDefault = 180f;
}