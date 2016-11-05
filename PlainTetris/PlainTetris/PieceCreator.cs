using System;
using System.Collections.Generic;

namespace PlainTetris
{
    internal static class PieceCreator
    {
        public static List<uint[,]> CreateRandomPiece()
        {
            var rnd = new Random();
            var num = rnd.Next(0, 7);

            switch (num)
            {
                case 0:
                    return CreateO();
                case 1:
                    return CreateI();
                case 2:
                    return CreateZ();
                case 3:
                    return CreateS();
                case 4:
                    return CreateL();
                case 5:
                    return CreateJ();
                case 6:
                    return CreateT();
                default:
                    throw new ArgumentOutOfRangeException();

            }

        }

        public static List<uint[,]> CreateO()
        {
            var optionalConfigurations = new List<uint[,]>();

            uint[,] normalBlock =
            {
                {0,0,0,0},
                {0,1,1,0},
                {0,1,1,0},
                {0,0,0,0}
            };

            optionalConfigurations.Add(normalBlock);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateI()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,0,0,0},
                {0,0,0,0},
                {2,2,2,2},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
            {
                {0,2,0,0},
                {0,2,0,0},
                {0,2,0,0},
                {0,2,0,0}
            };

            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateZ()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,3,0,0},
                {0,3,3,0},
                {0,0,3,0},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
{
                {0,0,0,0},
                {0,0,3,3},
                {0,3,3,0},
                {0,0,0,0}
            };

            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateS()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,0,4,0},
                {0,4,4,0},
                {0,4,0,0},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
{
                {0,0,0,0},
                {4,4,0,0},
                {0,4,4,0},
                {0,0,0,0}
            };

            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateL()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,0,0,0},
                {5,5,5,0},
                {0,0,5,0},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
{
                {0,0,5,0},
                {0,0,5,0},
                {0,5,5,0},
                {0,0,0,0}
            };

            uint[,] thirdConfiguration =
{
                {0,0,0,0},
                {0,5,0,0},
                {0,5,5,5},
                {0,0,0,0}
            };

            uint[,] fourthConfiguration =
{
                {0,0,0,0},
                {0,5,5,0},
                {0,5,0,0},
                {0,5,0,0}
            };


            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);
            optionalConfigurations.Add(thirdConfiguration);
            optionalConfigurations.Add(fourthConfiguration);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateJ()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,0,0,0},
                {0,0,6,0},
                {6,6,6,0},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
{
                {0,6,0,0},
                {0,6,0,0},
                {0,6,6,0},
                {0,0,0,0}
            };

            uint[,] thirdConfiguration =
{
                {0,0,0,0},
                {0,6,6,6},
                {0,6,0,0},
                {0,0,0,0}
            };

            uint[,] fourthConfiguration =
{
                {0,0,0,0},
                {0,6,6,0},
                {0,0,6,0},
                {0,0,6,0}
            };


            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);
            optionalConfigurations.Add(thirdConfiguration);
            optionalConfigurations.Add(fourthConfiguration);

            return optionalConfigurations;
        }
        public static List<uint[,]> CreateT()
        {
            var optionalConfigurations = new List<uint[,]>();


            uint[,] normalBlock =
            {
                {0,0,7,0},
                {0,0,7,7},
                {0,0,7,0},
                {0,0,0,0}
            };

            uint[,] secondConfiguration =
{
                {0,0,7,0},
                {0,7,7,7},
                {0,0,0,0},
                {0,0,0,0}
            };

            uint[,] thirdConfiguration =
{
                {0,0,7,0},
                {0,7,7,0},
                {0,0,7,0},
                {0,0,0,0}
            };

            uint[,] fourthConfiguration =
{
                {0,0,0,0},
                {0,7,7,7},
                {0,0,7,0},
                {0,0,0,0}
            };


            optionalConfigurations.Add(normalBlock);
            optionalConfigurations.Add(secondConfiguration);
            optionalConfigurations.Add(thirdConfiguration);
            optionalConfigurations.Add(fourthConfiguration);

            return optionalConfigurations;
        }
    }
}
