using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace Random
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Asd");

            List<ReadData> readDatas = ReadDataFromFile("lajharok.txt");

            Console.WriteLine(GirlSloths(readDatas));
            Console.WriteLine(CubsOfBoySloths(readDatas));
            Console.WriteLine(ListOfSlothsByAge(readDatas));
            Console.WriteLine(SlothCouples(readDatas));
        }

        public static int GirlSloths(List<ReadData> readData)
        {
            int counter = 0;

            foreach (ReadData data in readData)
            {
                if (data.Gender == "nosteny") counter++;
            }

            Console.WriteLine("Ennyi nőstény lajhár van:");
            return counter;
        }

        public static int CubsOfBoySloths(List<ReadData> readData)
        {
            int counter = 0;

            foreach (ReadData data in readData)
            {
                if (data.Gender == "him") counter += data.Childer;
            }

            
            Console.WriteLine("\nA hímeknek összesen ennyi kölykük van:");
            return counter;
        }


        public static string ListOfSlothsByAge(List<ReadData> readData)
        {
            List<KeyValuePair<int, string>> SlothsByAge = new List<KeyValuePair<int, string>>();
            string returnString = null;

            foreach (ReadData data in readData)
            {
                SlothsByAge.Add(new KeyValuePair<int, string>(data.Age, data.Name));
            }

            IOrderedEnumerable<KeyValuePair<int, string>> OrderedSlothsByAge = SlothsByAge.OrderBy(x => x.Key);

            foreach (KeyValuePair<int, string> data in OrderedSlothsByAge)
            {
                returnString += $"- {data.Value} ";
            }
            Console.WriteLine("\nA lajhárok életkor szerinti sorrendben:");
            return returnString;
        }

        public static string SlothCouples(List<ReadData> readData)
        {
            string returnString = "";
            int coupleCounter = 0;

            ReadData[,] allCouples = new ReadData[100, 2];

            for (int i = 0; i < readData.Count; i++)
            {
                ReadData data = readData[i];

                for (int i1 = 0; i1 < readData.Count; i1++)
                {
                    ReadData comparisonData = readData[i1];

                    if (data.Childer == comparisonData.Childer && data.Gender != comparisonData.Gender)
                    {
                        allCouples[coupleCounter, 0] = data;
                        allCouples[coupleCounter, 1] = comparisonData;

                        coupleCounter++;
                    }
                }
            }

            for (int i = 0; i < coupleCounter; i++)
            {
                returnString += $"\n{allCouples[i, 0].Name} és {allCouples[i, 1].Name} nekik {allCouples[i, 0].Childer} gyerekük van ";
            }

            return returnString;

        }

        public static List<ReadData> ReadDataFromFile(string fileLocation)
        {
            StreamReader file = new StreamReader(fileLocation);
            int counter = 0;
            string ln;

            List<ReadData> allReadData = new List<ReadData>();

            ReadData readData = new ReadData();

            while ((ln = file.ReadLine()) != null)
            {
                counter++;

                switch (counter)
                {
                    case 1:
                        {
                            string[] splitString = ln.Split(';');

                            readData.Name = splitString[0];
                            readData.Age = Convert.ToInt32(splitString[1]);
                            break;
                        }
                    case 2:
                        {
                            readData.Weight = ln;
                            break;
                        }
                    case 3:
                        {

                            string[] splitString = ln.Split(';');

                            readData.Gender = splitString[0];
                            readData.Childer = Convert.ToInt32(splitString[1]);

                            allReadData.Add(readData);
                            counter = 0;
                            readData = new ReadData();
                            break;
                        }
                }
            }

            return allReadData;
        }
    }
    class ReadData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Weight { get; set; }
        public string Gender { get; set; }
        public int Childer { get; set; }
    }
}