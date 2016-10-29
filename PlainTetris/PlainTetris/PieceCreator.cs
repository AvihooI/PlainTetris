using System;
using System.Collections.Generic;

namespace PlainTetris
{
    internal static class PieceCreator
    {
        public static List<uint[,]> CreateRandomPiece(out int cTop, out int cLeft)
        {
            var rnd = new Random();
            var num = rnd.Next(0, 6);

            switch (num)
            {
                case 0:
                    return CreateO(out cTop, out cLeft);
                case 1:
                    return CreateI(out cTop, out cLeft);
                case 2:
                    return CreateZ(out cTop, out cLeft);
                case 3:
                    return CreateS(out cTop, out cLeft);
                case 4:
                    return CreateL(out cTop, out cLeft);
                case 5:
                    return CreateJ(out cTop, out cLeft);
                case 6:
                    return CreateT(out cTop, out cLeft);
                default:
                    throw new ArgumentOutOfRangeException();

            }

        }

        public static List<uint[,]> CreateO(out int cTop, out int cLeft)
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

            cTop = -1;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateI(out int cTop, out int cLeft)
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

            cTop = 0;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateZ(out int cTop, out int cLeft)
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

            cTop = -1;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateS(out int cTop, out int cLeft)
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

            cTop = -1;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateL(out int cTop, out int cLeft)
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
                {0,5,5,5},
                {0,5,0,0},
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

            cTop = 0;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateJ(out int cTop, out int cLeft)
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

            cTop = 0;
            cLeft = 3;
            return optionalConfigurations;
        }
        public static List<uint[,]> CreateT(out int cTop, out int cLeft)
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

            cTop = -2;
            cLeft = 3;
            return optionalConfigurations;
        }
    }
}
