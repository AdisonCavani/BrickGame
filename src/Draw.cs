using System;

namespace BrickGame.Draw
{
    // Bricks
    class DrawBrickSpeed
    {
        // Generate random speed (Y axis)

        public int Speed1()
        {
            int speed = 4;
            return speed;
        }

        public int Speed2()
        {
            int speed = 5;
            return speed;
        }

        public int Speed3()
        {
            int speed = 6;
            return speed;
        }
        // End of "Generate random speed (Y axis)"
    }

    class DrawBrickPosition
    {
        // Generate random horizontal position (X axis)

        public int Position1(Random random)
        {
            int position = random.Next(10, 1850);
            return position;
        }

        public int Position2(Random random)
        {
            int position = random.Next(10, 1850);
            return position;
        }

        public int Position3(Random random)
        {
            int position = random.Next(10, 1850);
            return position;
        }
        // End of "Generate random horizontal position (X axis)"
    }

    // Coin
    class DrawCoinSpeed
    {
        // Generate random speed (Y axis)

        public int Speed1()
        {
            int speed = 4;
            return speed;
        }
        // End of "Generate random speed (Y axis)"
    }

    class DrawCoinPosition
    {
        // Generate random horizontal position (X axis)

        public int Position1(Random random)
        {
            int position = random.Next(10, 1850);
            return position;
        }
        // End of "Generate random horizontal position (X axis)"
    }
}