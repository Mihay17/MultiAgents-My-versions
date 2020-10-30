using System;

namespace MultiAgents {
    public static class Schedule {
        public struct IdCels
        {
            int Id, Cels;

            public void SetIdCels(int id, int cels)
            {
                Id = id;
                Cels = cels;
            }
            
            public int ID
            {
                get { return Id; }
                set { Id = value; }
            }

            // свойство Y
            public int CELS
            {
                get { return Cels; }
                set { Cels = value; }
            }
        }
        private static int _countOfCells;
        public static int[] arrayOfCells;
        public static IdCels[] arrayOfIdCels;

        public static readonly int FREE = -1;
        public static readonly int OCCUPIED = -2;
        public static readonly int BLOCKED = -3;


                                      // 288
        public static void init(int countOfCells) {
            _countOfCells = countOfCells;
            arrayOfCells = new int[_countOfCells]; // массив из 288 эл-тов
            arrayOfIdCels = new IdCels[_countOfCells];
            for (int i = 0; i < countOfCells; i++) 
            {
                arrayOfCells[i] = FREE;            // свободны     
            }
        }

        public static int getValue(int position) {
            return arrayOfCells[position];
        }

        public static void setValue(int position, int value) {
            arrayOfCells[position] = value;
        }

        public static double timeToDiscrete(DateTime timeBegin, int intervalInMinutes) {
            TimeSpan rez = timeBegin - DateTime.MinValue;
            return Math.Round(rez.TotalMinutes / intervalInMinutes);
        }

        public static DateTime discreteToTime(int discrete, int intervalInMinutes) {
            DateTime dateTimeBegin = DateTime.MinValue;
            DateTime dateTimeEnd = dateTimeBegin.AddMinutes((double)discrete * intervalInMinutes);
            return dateTimeEnd;
        }
                                                  //  288               2                           6                    5
        public static void setTimeToSleep(int timeSlotsNumber, int hoursBeforeMidnight, int hoursAfterMidnight, int timeSlotSizeInMinutes) {
            // Кол-во 5 минутных тактов до полуночи
            int timeSlotsBeforeMidnight = hoursBeforeMidnight * 60 / timeSlotSizeInMinutes;
            // после полуночи
            int timeSlotsAfterMidnight = hoursAfterMidnight * 60 / timeSlotSizeInMinutes;

            for (int i = 0; i < timeSlotsAfterMidnight; i++) 
            {
                Schedule.arrayOfCells[i] = Schedule.BLOCKED;
                Schedule.arrayOfIdCels[i].SetIdCels(-1, Schedule.BLOCKED);
            }

            for (int i = timeSlotsNumber - 1; i >= timeSlotsNumber - timeSlotsBeforeMidnight; i--) 
            {
                Schedule.arrayOfCells[i] = Schedule.BLOCKED;
                Schedule.arrayOfIdCels[i].SetIdCels(-1, Schedule.BLOCKED);
            }
        }

        public static string toString()
        {
            string result = "";
            for (int i = 0; i < _countOfCells; i++)
            {
                if (arrayOfCells[i] == FREE)
                {
                    result += " - ";
                }
                else if (arrayOfCells[i] == OCCUPIED)
                {
                    result +=" "+arrayOfIdCels[i].ID.ToString()+" ";
                }
                else if (arrayOfCells[i] == BLOCKED)
                {
                    result += " " + arrayOfIdCels[i].ID.ToString() + " ";
                    //result += " B ";
                }
            }
            return result;
        }
/*
        public static string toString()
    {
        string result = "";
        for (int i = 0; i < _countOfCells; i++) {
            if (arrayOfCells[i] == FREE) {
                result += "-";
            } else if (arrayOfCells[i] == OCCUPIED) {
                result += "O";
            } else if (arrayOfCells[i] == BLOCKED) {
                result += "B";
            }
        }
        return result;
    }
*/

    }
}
