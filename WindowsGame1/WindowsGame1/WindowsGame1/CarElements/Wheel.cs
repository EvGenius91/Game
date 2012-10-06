using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StillDesign.PhysX;
using Microsoft.Xna.Framework;




namespace WindowsGame1.Objects
{
    public class Wheel : CarElements
    {
        public WheelShapeDescription shapeDesc;
        protected StillDesign.PhysX.MathPrimitives.Vector3 localPosition;

        public Wheel(Game1 game, int id, String nameModel) : base(game, id, nameModel) { }

        float Radius = 10.0f;

        protected override void iniPhysx()
        {
            BodyDescription bodyDesc = new BodyDescription();
            bodyDesc.Mass = 5;
            this.shapeDesc = new WheelShapeDescription();
            this.shapeDesc.Radius = this.Radius;
            shapeDesc.SuspensionTravel = 10;
            //shapeDesc.LocalPosition = localPosition;

            ActorDescription actorDesc = new ActorDescription();
            actorDesc.BodyDescription = bodyDesc;
            actorDesc.GlobalPose = StillDesign.PhysX.MathPrimitives.Matrix.Translation(100,30,0);
            actorDesc.Shapes.Add(shapeDesc);
            this.actor = game.scene.CreateActor(actorDesc);
            this.shape = actor.Shapes[0];
            //this.shape = this.actor.CreateShape(shapeDesc) as WheelShape;

        }

        public override void Update()
        {
            this.actor.WakeUp();
            if (this.actor.IsSleeping)
                Console.Write("");
            //this.actor.BodyFlags.FrozenPositionY = true;

            if (game.stackControl.ContainsKey(id))
            {
                WheelShape shape = this.shape as WheelShape;
                //this.actor.WakeUp();
                if (game.stackControl[id].ContainsKey("MotorTorque"))
                {
                    shape.MotorTorque = game.stackControl[id]["MotorTorque"];
                }
                if (game.stackControl[id].ContainsKey("AxleSpeed"))
                {
                    //shape.AxleSpeed = game.stackControl[id]["AxleSpeed"];
                }

                this.shape = shape;
               
            }
            
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
        }   
       
    }

    
}
