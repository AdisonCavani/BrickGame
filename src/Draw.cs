using System;

namespace BrickGame.Draw
{
    // Bricks
    class DrawBrickSpeed
    {
        // Generate random speed (Y axis)

        public int Speed1()
        {
            //Random rnd = new Random();
            //speed1 = rnd.Next(3, 7);

            int speed1 = 4;
            return speed1;
        }

        public int Speed2()
        {
            //Random rnd = new Random();
            //speed2 = rnd.Next(3, 7);

            int speed2 = 5;
            return speed2;
        }

        public int Speed3()
        {
            //Random rnd = new Random();
            //speed3 = rnd.Next(3, 7);

            int speed3 = 6;
            return speed3;
        }
        // End of "Generate random speed (Y axis)"
    }

    class DrawBrickPosition
    {
        // Generate random horizontal position (X axis)

        public int Position1()
        {
            Random rnd = new Random();
            int position1 = rnd.Next(10, 1850);
            return position1;
        }

        public int Position2()
        {
            Random rnd = new Random();
            int position2 = rnd.Next(10, 1850);
            return position2;
        }

        public int Position3()
        {
            Random rnd = new Random();
            int position3 = rnd.Next(10, 1850);
            return position3;
        }
        // End of "Generate random horizontal position (X axis)"
    }

    // Coin
    class DrawCoinSpeed
    {
        // Generate random speed (Y axis)

        public int Speed1()
        {
            //Random rnd = new Random();
            //speed1 = rnd.Next(3, 7);

            int speed1 = 4;
            return speed1;
        }
        // End of "Generate random speed (Y axis)"
    }

    class DrawCoinPosition
    {
        // Generate random horizontal position (X axis)

        public int Position1()
        {
            Random rnd = new Random();
            int position1 = rnd.Next(10, 1850);
            return position1;
        }
        // End of "Generate random horizontal position (X axis)"
    }
}
