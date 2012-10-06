using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myCamera;

namespace WindowsGame1.Objects
{
    class Building
    {
        Model frame;

        public Building(String nameModel, Game game)
        {
            frame = game.Content.Load<Model>(nameModel);
        }


        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in frame.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.Identity;
                    
                    effect.View = camera.viewMatrix;
                    effect.Projection = camera.projectionMatrix;
                }
                mesh.Draw();
            }
        }
    }
}
