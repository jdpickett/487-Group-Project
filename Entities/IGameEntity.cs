using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entities;

public interface IGameEntity
{
    bool IsAlive {get;}
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}