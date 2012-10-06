using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1.Objects;

namespace WindowsGame1
{
    public class Motor : CarElements
    {
        public Motor(Game1 game, int id) : base(game, id) { }

        public override void Update()
        {
            if(game.isControlVal(this.id, "accelerate"))
                game.setControlVal(2, "MotorRPM", game.getControlVal(this.id, "accelerate")*60);

        }

        public override void Draw() { }
    }
}
