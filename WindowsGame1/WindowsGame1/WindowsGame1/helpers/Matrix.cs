using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StillDesign.PhysX;
using Microsoft.Xna.Framework;


namespace WindowsGame1.helpers
{
    class MatrixHelpers
    {
        public static Microsoft.Xna.Framework.Matrix Convert(StillDesign.PhysX.MathPrimitives.Matrix input)
        {
            Microsoft.Xna.Framework.Matrix output = new Microsoft.Xna.Framework.Matrix
                (
                    input.M11, input.M12, input.M13, input.M14,
                    input.M21, input.M22, input.M23, input.M24,
                    input.M31, input.M32, input.M33, input.M34,
                    input.M41, input.M42, input.M43, input.M44
                );

            return output;
        }
    }
}
