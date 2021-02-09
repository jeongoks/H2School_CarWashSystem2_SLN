using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H1School_CarWashSystem1
{
    public class SystemMenu
    {
        public static void Menu()
        {
            WashingSystem washingHall = new WashingSystem(3);

            // Creates all of the Washing Types with Processes.
            washingHall.CreateTypesAndProcesses(); 

            do
            {
                int menuSelect = 0;

                Console.Clear();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("    What can we do for you today?   ");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("1. Check for available Washing Hall.");
                Console.WriteLine("------------------------------------");
                do
                {
                    Console.WriteLine("Enter your Selection:");
                } while (!int.TryParse(Console.ReadLine(), out menuSelect));

                switch (menuSelect)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("-------------------------------------------------------");
                        int inputProgram = 0;
                        int input = 0;
                        CancellationTokenSource cancelToken1 = new CancellationTokenSource();
                        CancellationTokenSource cancelToken2 = new CancellationTokenSource();
                        CancellationTokenSource cancelToken3 = new CancellationTokenSource();
                        CancellationToken token1 = cancelToken1.Token;
                        CancellationToken token2 = cancelToken2.Token;
                        CancellationToken token3 = cancelToken3.Token;
                        Console.Write($"These are the available wash halls: ");
                        foreach (WashingHall item in washingHall.CheckAvailableWashHall())
                        {
                            Console.Write(item.Id + " ");
                        }
                        Console.WriteLine();
                        Console.WriteLine("-------------------------------------------------------");
                        do
                        {
                            Console.WriteLine("Choose which of the available Halls you wanna use:");
                        } while (!int.TryParse(Console.ReadLine(), out input));
                        if (input == 1) // Washing Hall 1
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Which program would you like to use?");
                                washingHall.ShowProgramTypes();
                            } while (!int.TryParse(Console.ReadLine(), out inputProgram));
                            Task task1 = washingHall.StartWash(input, inputProgram, token1);
                        }
                        if (input == 2) // Washing Hall 2
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Which program would you like to use?");
                                washingHall.ShowProgramTypes();
                            } while (!int.TryParse(Console.ReadLine(), out inputProgram));
                            Task task2 = washingHall.StartWash(input, inputProgram, token2);
                        }
                        if (input == 3) // Washing Hall 3
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Which program would you like to use?");
                                washingHall.ShowProgramTypes();
                            } while (!int.TryParse(Console.ReadLine(), out inputProgram));
                            Task task3 = washingHall.StartWash(input, inputProgram, token3);
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown Input. Try again.");
                        break;
                }
            } while (true);
        }
    }
}
