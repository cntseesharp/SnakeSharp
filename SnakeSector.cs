using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake
{
    class SnakeSector
    {
        bool IsHead;
        Vector2 coords;

        public SnakeSector(Vector2 coords, bool IsHead = false)
        {
            this.IsHead = IsHead;
            this.coords = coords;
        }

        public Vector2 GetCoords()
        {
            return coords;
        }

        public void SetCoords(Vector2 coords)
        {
            this.coords = coords;
        }

        public bool Head()
        {
            return IsHead;
        }
    }
}
