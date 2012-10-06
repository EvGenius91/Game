using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WindowsGame1.Objects;
using StillDesign.PhysX;
using WindowsGame1.helpers;

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public Scene scene;
        List<CarElements> elements = new List<CarElements>();
        public myCamera.Camera camera;
        public Output output;
        public Dictionary<int, Dictionary<string, float>> stackControl = new Dictionary<int, Dictionary<string, float>>();

                

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
        }
        
        protected override void Initialize()
        {
            this.iniPhysics();
            //Костыль для плоскости
            PlaneShapeDescription planeDesc = new PlaneShapeDescription();
            planeDesc.Normal = new StillDesign.PhysX.MathPrimitives.Vector3(0, 1, 0);

            ActorDescription actorPlane = new ActorDescription();
            actorPlane.Shapes.Add(planeDesc);
            scene.CreateActor(actorPlane);
            //

            ElementDesc wheelDesc = new ElementDesc();
            wheelDesc.TypeJoint = "Cylindrical";
            wheelDesc.GlobalPosition = new StillDesign.PhysX.MathPrimitives.Vector3(2.0f, 200.0f, 2.0f);
            
            

            camera = new myCamera.Camera(this);
            Car car = new Car(this, 1, "car", new Vector3(2.0f,30.0f,0.0f));
            car.ElementsDesc.Add("wheel1", wheelDesc);
            
            Wheel wheel = new Wheel(this, 2, "wheel");
            car.addElement("Cylindrical", wheel);
            elements.Add(wheel);
            //car.addElement(new Motor(this, 3));

            elements.Add(car);
                        
            Components.Add(camera);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            output = new Output(Content, GraphicsDevice);

            // Create a new SpriteBatch, which can be used to draw textures.
           

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected Core iniPhysics()
        {CoreDescription coreDesc = new CoreDescription();
            Core core = new Core(coreDesc, null);
            core.SetParameter(PhysicsParameter.VisualizationScale, 2.0f);
            core.SetParameter(PhysicsParameter.VisualizeCollisionShapes, true);
            core.SetParameter(PhysicsParameter.VisualizeClothMesh, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLocalAxes, true);
            core.SetParameter(PhysicsParameter.VisualizeJointLimits, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidPosition, true);
            core.SetParameter(PhysicsParameter.VisualizeFluidEmitters, false); // Slows down rendering a bit too much
            core.SetParameter(PhysicsParameter.VisualizeForceFields, true);
            core.SetParameter(PhysicsParameter.VisualizeSoftBodyMesh, true);

            RemoteDebugger debugger = core.Foundation.RemoteDebugger;
            debugger.Connect("localhost");
            if (debugger.IsConnected)
            {
                Console.Write("Debugger connected\n");
            }

            SceneDescription sceneDesc = new SceneDescription();
            sceneDesc.Gravity = new StillDesign.PhysX.MathPrimitives.Vector3(0, -10.0f, 0);
            scene = core.CreateScene(sceneDesc);
            

            return core;
        }

        float v = 50;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();


            if (keyboard.IsKeyDown(Keys.W))
            {
                setControlVal(2, "MotorTorque", v);
                v += 10;
            }

            if (keyboard.IsKeyDown(Keys.Q))
            {
                //setControlVal(2, "AxleSpeed", 1000);
            }




            foreach (CarElements element in elements)
                element.Update();

            scene.Simulate(60.0f);
            scene.FlushStream();
            scene.FetchResults(SimulationStatus.RigidBodyFinished, true);
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            foreach (CarElements element in elements)
                element.Draw();

            base.Draw(gameTime);
        }

        public void setControlVal(int id, String name, float value)
        {
            if (stackControl.ContainsKey(id))
            {
                if (stackControl[id].ContainsKey(name))
                {
                    stackControl[id][name] = value;
                }
                else
                {
                    stackControl[id].Add(name, value);
                }
            }
            else
            {
                Dictionary<String, float> dict = new Dictionary<string, float>();
                dict.Add(name, value);
                stackControl.Add(id, dict);

            }
            
        }

        public float getControlVal(int id, String name)
        {
            return stackControl[id][name];
        }

        public bool isControlVal(int id, String name)
        {
            if (stackControl.ContainsKey(id))
                if (stackControl[id].ContainsKey(name))
                    return true;

            return false;
                    
        }


        

    }
}
