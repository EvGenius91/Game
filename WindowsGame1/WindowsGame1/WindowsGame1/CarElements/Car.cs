using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myCamera;
using StillDesign.PhysX;
using WindowsGame1.helpers;
using WindowsGame1;

namespace WindowsGame1.Objects
{
    public class Car : CarElements
    {
        public bool Control = false;

        public Car(Game1 game, int id, string nameModel, Vector3 startPosition) : base(game, id, nameModel)
        {

            this.iniPhysx(startPosition);
        }

        protected void iniPhysx(Vector3 startPosition)
        {
            BodyDescription boxBody = new BodyDescription();
            boxBody.AngularVelocity = new StillDesign.PhysX.MathPrimitives.Vector3(1, 0, 0);
            boxBody.Mass = 2000;

            //Material
            MaterialDescription materialDesc = new MaterialDescription();
            materialDesc.Restitution = 0.001f;
            materialDesc.StaticFriction = 0.5f;
            materialDesc.DynamicFriction = 0.5f;
            materialDesc.Name = "Test";
            this.game.scene.CreateMaterial(materialDesc);


            BoxShapeDescription boxShapeDesc = new BoxShapeDescription();
            boxShapeDesc.Dimensions = new StillDesign.PhysX.MathPrimitives.Vector3(20.5f, 6.4f, 34.0f);
            boxShapeDesc.Material = this.game.scene.Materials[1];
            boxShapeDesc.Mass = 2000;

            ActorDescription actor = new ActorDescription();
            
            actor.GlobalPose = StillDesign.PhysX.MathPrimitives.Matrix.Translation(startPosition.X, startPosition.Y, startPosition.Z);
            actor.BodyDescription = boxBody;
            actor.Shapes.Add(boxShapeDesc);
            
            this.actor = this.game.scene.CreateActor(actor);
            this.shape = this.actor.CreateShape(boxShapeDesc) as BoxShape;

        }

        

        public void remoteElement()
        {
            
        }


    }
}
