using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake
{
    class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2()
        {
            X = 0;
            Y = 0;
        }

        public Vector2(int value)
        {
            X = value;
            Y = value;
        }
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Vector2 source, Vector2 comparer)
        {
            return (source.X == comparer.X) && (source.Y == comparer.Y);
        }

        public static bool operator !=(Vector2 source, Vector2 comparer)
        {
            return (source.X != comparer.X) || (source.Y != comparer.Y);
        }
    }
}
