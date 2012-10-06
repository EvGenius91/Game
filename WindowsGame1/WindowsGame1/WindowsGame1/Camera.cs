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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace myCamera
{
    public struct ControlCam
    {
        public Vector3 TranslateDirection;
        public Matrix Rotation;
    }

    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Matrix viewMatrix;
        public Matrix projectionMatrix;
        public ControlCam controlCam;
        Vector2 CenterScreen;
        float _Pitch;
        float _Yaw;
        float distancePerFrame = 10;

        public Camera(Game game)
            : base(game)
        {
            CenterScreen = new Vector2(game.GraphicsDevice.Viewport.Width / 2,
                                       game.GraphicsDevice.Viewport.Height / 2);
            viewMatrix = Matrix.CreateLookAt(new Vector3(5, 5, 500),
                                             new Vector3(0, 0, 0),
                                             new Vector3(0, 1, 0));
            projectionMatrix =
            Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45),
                                                game.GraphicsDevice.Viewport.Width /
                                                game.GraphicsDevice.Viewport.Height,
                                                0.1f, 100000.0f);
            controlCam.TranslateDirection = Vector3.Zero;
            controlCam.Rotation = Matrix.Identity;
            CenteredCursor();
        }

        bool centered = false;
        public override void Update(GameTime gameTime)
        {
            KeyboardState states = Keyboard.GetState();

            if (!states.IsKeyDown(Keys.LeftControl))
            {
                base.Update(gameTime);
                this.centered = false;
                return;
            }
            if (!this.centered)
            {
                CenteredCursor();
                this.centered = true;
            }
            Vector2 cursorPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 deltaDampened = (cursorPosition - CenterScreen) * 0.0015f;
            _Yaw -= deltaDampened.X;
            _Pitch -= deltaDampened.Y;
            if (_Pitch < -1.55f)
            {
                _Pitch = -1.55f;
            }
            if (_Pitch > 1.55f)
            {
                _Pitch = 1.55f;
            }

            Matrix cameraRotation = Matrix.CreateFromYawPitchRoll(_Yaw, _Pitch, 0.0f);
            cameraRotation *= controlCam.Rotation;
            Vector3 translateDirection = Vector3.Zero;

            if (states.IsKeyDown(Keys.Up))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Forward, cameraRotation);

            if (states.IsKeyDown(Keys.Down))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Backward, cameraRotation);

            if (states.IsKeyDown(Keys.Left))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Left, cameraRotation);

            if (states.IsKeyDown(Keys.Right))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Right, cameraRotation);

            if (states.IsKeyDown(Keys.PageUp))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Up, cameraRotation);

            if (states.IsKeyDown(Keys.PageDown))
                translateDirection +=
                  Vector3.TransformNormal(Vector3.Down, cameraRotation);

            

            Vector3 newPosition = Matrix.Invert(viewMatrix).Translation;
            
            if (translateDirection != Vector3.Zero)
            {
                newPosition += Vector3.Normalize(translateDirection) * distancePerFrame;
            }
            newPosition -= controlCam.TranslateDirection;
            
            Vector3 newForward = Vector3.TransformNormal(Vector3.Forward, cameraRotation);
            viewMatrix = Matrix.CreateLookAt(newPosition, newPosition + newForward, Vector3.Up);
            CenteredCursor();
            base.Update(gameTime);
        }

        private void CenteredCursor()
        {
            Mouse.SetPosition((int)CenterScreen.X, (int)CenterScreen.Y);
        }
    }
}