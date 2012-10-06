using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame1.helpers
{
    public class Output
    {
        public ContentManager Content;
        public GraphicsDevice gd;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Output(ContentManager Content, GraphicsDevice gd)
        {
            this.Content = Content;
            this.gd = gd;
            this.spriteBatch = new SpriteBatch(gd);
            this.font = Content.Load<SpriteFont>("Segoe UI Mono");

        }

        public void DrawString(String str)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(this.font, str , new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(this.font, str, new Vector2(0, 0), Color.Black);
            spriteBatch.End();
        }
    }
}
