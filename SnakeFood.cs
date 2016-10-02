using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake
{
    class SnakeFood
    {
        Vector2 coords;
        public SnakeFood(Vector2 coords)
        {
            this.coords = coords;
        }

        public void SetCoords(Vector2 coords)
        {
            this.coords = coords;
        }

        public Vector2 GetCoords()
        {
            return coords;
        }
    }
}
