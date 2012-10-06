using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StillDesign.PhysX;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WindowsGame1.helpers;
using WindowsGame1;




namespace WindowsGame1.Objects
{
     public class CarElements
    {
        public int id;
        public Actor actor;
        public Shape shape;
        public ShapeDescription shapeDesc;
        protected Model frame;
        protected Game1 game;
        protected List<CarElements> elements = new List<CarElements>();
        public Dictionary<String, ElementDesc> ElementsDesc = new Dictionary<String, ElementDesc>();


        public CarElements(Game1 game, int id, String nameModel = null)
        {
            this.id = id;
            this.game = game;
            if(nameModel != null)
                frame = game.Content.Load<Model>(nameModel);

            this.iniPhysx();
        }

        public void Initialize()
        {

        }

        virtual protected void iniPhysx()
        {
        }

        virtual public void Update()
        {
            foreach(CarElements element in elements)
            {
                element.Update();
            }
        }

        virtual public void Draw()
        {
            Matrix matrix = helpers.MatrixHelpers.Convert(this.actor.GlobalPose);
            foreach (ModelMesh mesh in frame.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = matrix;

                    effect.View = this.game.camera.viewMatrix;
                    effect.Projection = this.game.camera.projectionMatrix;
                }
                mesh.Draw();
            }

            foreach (CarElements element in elements)
            {
                CarElements elem = element as CarElements;
                elem.Draw();
            }
        }

        public void addElement(String name, Wheel Element)
        {
            D6JointDescription jointDesc = null;
            if (name == "Cylindrical")
            {

                jointDesc = new D6JointDescription();
                jointDesc.Actor1 = this.actor;
                jointDesc.Actor2 = Element.actor;
                jointDesc.SetGlobalAnchor(new StillDesign.PhysX.MathPrimitives.Vector3(0,0,0));
                //jointDesc.SetGlobalAxis(new StillDesign.PhysX.MathPrimitives.Vector3(1, 0, 0));
                jointDesc.TwistMotion = D6JointMotion.Locked;
                jointDesc.Swing1Motion = D6JointMotion.Locked;
                jointDesc.Swing2Motion = D6JointMotion.Locked;
                jointDesc.XMotion = D6JointMotion.Free;
                jointDesc.YMotion = D6JointMotion.Free;
                jointDesc.ZMotion = D6JointMotion.Free;
                jointDesc.ProjectionMode = JointProjectionMode.None;
                

            }

            //D6Joint joint = game.scene.CreateJoint(jointDesc) as D6Joint;
            //joint.SetDriveAngularVelocity(new StillDesign.PhysX.MathPrimitives.Vector3(0,0,0));
            MotorDescription motorDesc = new MotorDescription();
            motorDesc.MaximumForce = 200;
            motorDesc.VelocityTarget = 0;
            motorDesc.FreeSpinEnabled = false;
            
            //joint.Motor = motorDesc;
            //joint.BreakableMaxTorque = 100;
            
            
            //Element.shape = this.actor.CreateShape(Element.shapeDesc) as WheelShape;
            //elements.Add(Element);
        }

        public void addElement(Motor Element)
        {
            elements.Add(Element);
        }

    }

}
