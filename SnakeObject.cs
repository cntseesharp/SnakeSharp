using System.Collections.Generic;

namespace snake
{
    class SnakeObject
    {
        ushort length;
        List<SnakeSector> sectors = new List<SnakeSector>();
        public SnakeObject(Vector2 startCoords, ushort length = 3)
        {
            this.length = length;
            GenerateSnake(startCoords);
        }

        public void AddObject(SnakeSector newObject)
        {
            sectors.Add(newObject);
        }

        public List<SnakeSector> GetObjects()
        {
            return sectors;
        }

        void AddSector(Vector2 pos)
        {
            sectors.Add(new SnakeSector(pos));
        }

        void GenerateSnake(Vector2 startCoords)
        {
            sectors.Add(new SnakeSector(startCoords, true));

            for (ushort i = 1; i < length; i++)
                sectors.Add(new SnakeSector(
                    new Vector2(startCoords.X - i, startCoords.Y)
                    ));
        }
    }
}
