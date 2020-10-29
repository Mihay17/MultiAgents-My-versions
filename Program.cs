using System;
using System.IO;

namespace MultiAgents {
    public class Program {
        static void Main(string[] args) {
            //string path = "c:\\Users\\Mihay\\OneDrive\\MultyAgent\\MultiAgent5\\agents.txt";
            string path = "agents.txt";
            //File.Create(path);
            //размер "кванта времени" - 5 минут 
            int timeSlotSizeInMinutes = 5;

            // сколько "квантов времени" помещается в 24-часовых сутках
            int timeSlotsNumber = 24 * 60 / timeSlotSizeInMinutes;  // 288

            // время для сна пришлось делить на 2 части, т. к. экипаж ложится спасть 10:00, а просыпается в 6:00 уже следующих суток
            // время сна помечено на линейном индикаторе Schedule.toString() буквами "B", а время работы - буквами "O"
            int sleepHoursBeforeMidnight = 2;
            int sleepHoursAfterMidnight = 6;
            // 288
            Schedule.init(timeSlotsNumber);
            //288                  2                        6                       5
            Schedule.setTimeToSleep(timeSlotsNumber, sleepHoursBeforeMidnight, sleepHoursAfterMidnight, timeSlotSizeInMinutes);
            int sleep1 = sleepHoursBeforeMidnight * 60 / timeSlotSizeInMinutes;
            int sleep2 = sleepHoursAfterMidnight * 60 / timeSlotSizeInMinutes;
            int[,] rasp = new int[50, 288];
            //результат расстановки хранится в этом массиве

            Agent[,] agentArray = new Agent[50, 100];

            Agent agent = new Agent(-1, 0, 6, timeSlotsNumber, 0);
            Agent agent2 = new Agent(-1, 22, 2, timeSlotsNumber, 0); 
            for (int t = 1; t <= 5; t++)
            {
                agentArray[t, 53] = agent;
                agentArray[t, 54] = agent2;
                for (int i = 0; i < sleep2; i++)
                {
                    rasp[t, i] = -1;
                }
                for (int i = timeSlotsNumber - sleep1; i < timeSlotsNumber; i++)
                {
                    rasp[t, i] = -1;
                }
            }
            float spos1 = 6, spos2 = 13, spos3 = 21.5f; // cтарт затрака, обеда и ужина
            float len1 = 0.5f, len2 = 1, len3 = 0.5f; ; // продолжительность завтрака, обеда и ужина
            int sposall = (int)(spos1 * 60 / timeSlotSizeInMinutes); // Старт позиции завтрака в тиках
            int lenall = (int)(len1 * 60 / timeSlotSizeInMinutes);         // Длина в тиках


            //результат расстановки хранится в этом массиве
            //Agent[,] agentArray = new Agent[50, 100];

            agent = new Agent(50, spos1, len1, timeSlotsNumber, 0);
            for (int t = 1; t <= 5; t++)
            {
                agentArray[t, 50] = agent; // работа №50 - завтрак
                //agentArray[t, 50].setAgentID(100);
                for (int i = sposall; i < sposall + lenall; i++)
                {
                    rasp[t, i] = 50;
                }
            }
            sposall = (int)(spos2 * 60 / timeSlotSizeInMinutes); // Старт позиции обеда в тиках
            lenall = (int)(len2 * 60 / timeSlotSizeInMinutes);         // Длина в тихах
            agent = new Agent(51, spos2, len2, timeSlotsNumber, 0);
            for (int t = 1; t <= 5; t++)
            {
                agentArray[t, 51] = agent;               // работа №51 - обед
                //agentArray[t, 51].setAgentID(100);
                for (int i = sposall; i < sposall + lenall; i++)
                {
                    rasp[t, i] = 51; 

                }
            }
            sposall = (int)(spos3 * 60 / timeSlotSizeInMinutes); // Старт позиции ужина в тиках
            lenall = (int)(len3 * 60 / timeSlotSizeInMinutes);         // Длина в тиках
            agent = new Agent(52, spos3, len3, timeSlotsNumber, 0);
            for (int t = 1; t <= 5; t++)
            {
                agentArray[t, 52] = agent;             // работа №52 - ужин
                //agentArray[t, 52].setAgentID(100);
                for (int i = sposall; i < sposall + lenall; i++)   
                {
                    rasp[t, i] = 52;
                }
            }

            //результат расстановки хранится в этом массиве
            //Agent[,] agentArray = new Agent[50, 100];

            Random randomLenght = new Random();
            Random randomStartPosition = new Random();
            Random randomIterup = new Random();
            File.WriteAllText(path, "\n");
            for (int t = 1; t <= 5; t++)
            {
                for (int i = 1; i <= 15; i++)
                {
                    // случайное число - длина очередной задачи
                    float agentLenght = randomLenght.Next(1, 3) - 0.5f;
                    float agentStartPosition = randomStartPosition.Next(8, 21)- 0.5f;
                    int agentIterup = randomIterup.Next(0, 3);
                    // длина          // 288
                    agent = new Agent(i, agentStartPosition, agentLenght, timeSlotsNumber, agentIterup);
                    //agent.runForward(i);
                    // здесь хранится результат - случайным образом сформированные агенты-задачи 
                    agentArray[t, i] = agent;
                    File.AppendAllText(path, t.ToString()+"\n");
                    File.AppendAllText(path, agentArray[t, i].toString() + "\n");
                    Console.WriteLine(t.ToString());
                    Console.WriteLine(agentArray[t, i].toString());
                }
            }
            /*
            int agentLenght = 2;
            int agentStartPosition = 6;
            int agentIterup = 0;
            Agent agent = new Agent(4, agentStartPosition, agentLenght, timeSlotsNumber, agentIterup);
            agentArray[4] = agent;
            Console.WriteLine(agentArray[4].toString());
            */
            int spos, len;
            for (int t = 1; t <= 5; t++)
            {
                for (int k = 0; k < 3; k++) // перебор всех приоритетов
                {
                    for (int i = 1; i <= 15; i++)  // Перебор всех Агентов
                    {
                        if (agentArray[t, i].getIterup() == k)  // Приоритет агента соответсует k
                        {
                            spos = (int)(agentArray[t, i].getStartPosition() * 60 / timeSlotSizeInMinutes); // Старт позиции Данного Агента
                            len = (int)(agentArray[t, i].getLength() * 60 / timeSlotSizeInMinutes);         // Длина 
                            if ((spos <= timeSlotsNumber - sleep1) && (spos >= sleep2))
                            {   // Если старт и конец агента попадают в диапазон не сна
                                if ((spos + len <= timeSlotsNumber - sleep1) && (spos + len >= sleep2))
                                {
                                    int pr2 = -1; // признак цикла сдвига
                                    int pr3 = 0; // признак цикла сдвига назад
                                    int pr = -1; // признак занятости слота
                                    int l = 0;   // сдвиг позиции
                                    while (pr2 == -1)
                                    {   // Сдвиг вперед =================================
                                        if (spos + len + l >= timeSlotsNumber)
                                        {
                                            pr3 = -1;
                                            l = 0;
                                            break;
                                        }
                                        for (int j = spos + l; j < spos + len + l; j++)
                                        {   //Пробегаем по массиву rasp
                                            if (rasp[t, j] == 0)
                                            {
                                                pr = -1;
                                            }
                                            else
                                            {
                                                pr = 0; // Обнаружился занятый слот
                                                break;
                                            }
                                        }

                                        if (pr == -1)
                                        {
                                            pr2 = 0;
                                            // если слоты пустые заполняем
                                            agentArray[t, i].setCurrentPosition((float)((spos+l)*timeSlotSizeInMinutes)/60.0f);
                                            File.AppendAllText(path, "CurPos " + t.ToString() + " " + i.ToString() + " "
                                                           + agentArray[t, i].getCurrentPosition().ToString() + "    " 
                                                           + agentArray[t, i].getStartPosition().ToString() + "\n");
                                            for (int j = spos + l; j < spos + len + l; j++)
                                            {
                                                rasp[t, j] = agentArray[t, i].getAgentID();
                                            }
                                        }
                                        l++; // сдвиг

                                    }
                                    // сдвиг назад =======================================
                                    while (pr3 == -1)
                                    {
                                        /*
                                        if (spos + len - l <= 0)
                                        {
                                            break;
                                        }
                                        */
                                        if (spos - l <= 0)
                                        {
                                            break;
                                        }
                                        for (int j = spos - l; j < spos + len - l; j++)
                                        {   //Пробегаем по массиву rasp
                                            if (rasp[t, j] == 0)
                                            {
                                                pr = -1;
                                            }
                                            else
                                            {
                                                pr = 0; // Обнаружился занятый слот
                                                break;
                                            }
                                        }

                                        if (pr == -1)
                                        {
                                            pr3 = 0;
                                            // если слоты пустые заполняем
                                            agentArray[t, i].setCurrentPosition((float)((spos - l) * timeSlotSizeInMinutes) / 60.0f);
                                            File.AppendAllText(path, "CurPos "+t.ToString()+" "+ i.ToString()+ " " 
                                                           + agentArray[t, i].getCurrentPosition().ToString() + "\n");
                                            for (int j = spos - l; j < spos + len - l; j++)
                                            {
                                                rasp[t, j] = agentArray[t, i].getAgentID();
                                            }
                                        }
                                        l++; // сдвиг
                                    }
                                }
                            }
                        }
                    }
                }
                string result = "\n";
                for (int i = 0; i < 288; i++)
                {
                    result += rasp[t, i].ToString() + " ";
                }
                Console.WriteLine(result);
                File.AppendAllText(path, result+"\n");
                Console.ReadKey();
            }

        }
    }
}

