﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Seminarska
{
    internal class Program
    {
        public static void Meni()
        {
            bool napaka = true;
            do
            {
                Console.WriteLine("\a*************************");
                Console.WriteLine("||\tActivities\t||");
                Console.WriteLine("*************************");
                Console.WriteLine("||\tO - Odpri\t||");
                Console.WriteLine("||\tV - Vnos\t||");
                Console.WriteLine("||\tA - Aktivnosti\t||");
                Console.WriteLine("||\tB - Brisanje\t||");
                Console.WriteLine("||\tI - Iskanje\t||");
                Console.WriteLine("||\tT - Total\t||");
                Console.WriteLine("||\tS - Shrani\t||");
                Console.WriteLine("*************************");
                Console.WriteLine("||\tZ - Izhod\t||");
                Console.WriteLine("*************************");
                Console.Write("Vnesite znak za željeno funkcijo: ");
                char znak = char.Parse(Console.ReadLine());
                switch (znak)
                {
                    case 'o': Odpri(); break;
                    case 'O': Odpri(); break;
                    case 'v': Vnos(); break;
                    case 'V': Vnos(); break;
                    case 'a': Aktivnosti(); break;
                    case 'A': Aktivnosti(); break;
                    case 'b': Brisanje(); break;
                    case 'B': Brisanje(); break;
                    case 'i': Iskanje(); break;
                    case 'I': Iskanje(); break;
                    case 't': Total(); break;
                    case 'T': Total(); break;
                    case 's': Shrani(); break;
                    case 'S': Shrani(); break;
                    case 'z': Console.WriteLine("Izbrali s izhod, program se bo končal"); napaka = false; break;
                    case 'Z': Console.WriteLine("Izbrali s izhod, program se bo končal"); napaka = false; break;
                    default: Console.WriteLine("\nVnesli ste napačen znak, prosim ponovite izbiro"); break;
                }
            } while (napaka);

        }
        //public static string[,] activities;
        public static Activities[] activities;
        public static int counter;
        public static void Odpri()
        {
            string fileName = "c:\\temp\\save";
            Array.Clear(activities, 0, activities.Length);
            //counter = 0;
            //Console.Write("Vnesite pot do datoteke: ");
            //string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                StreamReader reader = File.OpenText(fileName);
                string vrstica;
                while ((vrstica = reader.ReadLine()) != null)
                {
                    string[] entries = vrstica.Split(';');
                    for (int j = 0; j < 5; j++)
                    {
                        activities[counter, j] = entries[j];
                    }
                    counter++;
                }
                reader.Close();
                Console.WriteLine("Datoteka naložena");
            }
            else
            {
                Console.WriteLine("Datoteka ne obstaja!");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
        public static void Vnos()
        {
            Console.WriteLine("*****\tVnos\t*****");
            Console.Write("Vnesite ime vožnje: ");
            activities[counter, 0] = Console.ReadLine();
            Console.WriteLine("Vnesite datum aktivnosti v obliki DD-MM-YYYY: ");
            activities[counter, 1] = Console.ReadLine();
            Console.WriteLine("Vnesite čas trajanja aktivnosti v obliki hh:mm:ss: ");
            activities[counter, 2] = Console.ReadLine();
            Console.WriteLine("Vnesite prevoženo razdaljo (km): ");
            activities[counter, 3] = Console.ReadLine();
            Console.WriteLine("Vnesite prevoženo višinsko razliko (m): ");
            activities[counter, 4] = Console.ReadLine();
            Console.WriteLine("Aktivnost je vnešena");
            Console.ReadLine();
            counter++;
        }
        public static void Aktivnosti()
        {
            Console.WriteLine("*****\tAktivnosti\t*****");
            for (int i = 0; i < counter; i++)
            {
                Console.WriteLine("{0,-3}| {1, -15} | {2, -13} | {3, -12} | {4, 5}km | {5, 5}m", i + 1, activities[i, 0], activities[i, 1], activities[i, 2], activities[i, 3], activities[i, 4]);
            }
            Console.ReadLine();
        }
        public static void Brisanje()
        {
            int i = Iskanje();
            Console.WriteLine(i);
            Console.WriteLine(activities[i - 1, 0]);
            Array.Clear(activities, i - 1, 5);
            Console.WriteLine(activities[i - 1, 0]);

            Console.ReadLine();
        }
        public static int Iskanje()
        {
            Console.WriteLine("Vnesti ime iskane poti: ");
            string search = Console.ReadLine();
            bool found = false;
            int ireturn = 0;
            
            for (int i = 0; i < counter; i++)
            {
                if (activities[i, 0] == search)
                {
                    Console.WriteLine("Iskana pot je najdena");
                    Console.WriteLine("{0,-3}| {1, -15} | {2, -13} | {3, -12} | {4, 5}km | {5, 5}m", i + 1, activities[i, 0], activities[i, 1], activities[i, 2], activities[i, 3], activities[i, 4]);
                    found = true;
                    ireturn = i + 1;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Iskana pot NI najdena");
                Console.ReadLine();
                return -1;
            }
            else
                return ireturn;
        }
        public static void Total()
        {
            Console.WriteLine("\tŠtevilo aktivnosti\t|\tSkupen čas\t|\tSkupna razdalja\t     |\t  Skupen vzpon\t");
            Console.WriteLine("=======================================================================================================");
            int distance = 0;
            int climb = 0;
            TimeSpan ts;
            ts = TimeSpan.Zero;  //Initialize a time span to zero
            int noofactivity = counter;
            for (int i = 0; i < counter; i++)
            {
                ts = ts + TimeSpan.Parse(activities[i, 2]);
            }
            for (int i = 0; i < counter; i++)
            {
                distance = distance + int.Parse(activities[i, 3]);
            }
            for (int i = 0; i < counter; i++)
            {
                climb = climb + int.Parse(activities[i, 4]);
            }
            Console.WriteLine("\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t", noofactivity, ts, distance, climb);
            Console.ReadLine();
        }
        public static void Shrani()
        {
            Console.Write("Ime in pot do datoteke: ");
            string fileName = Console.ReadLine();
            StreamWriter writer = File.CreateText(fileName);
            for (int i = 0; i < counter; i++)
            {
                writer.WriteLine("{0};{1};{2};{3};{4}", activities[i, 0], activities[i, 1], activities[i, 2], activities[i, 3], activities[i, 4]);
            }
            writer.Close();
            Console.WriteLine("Podatki so uspešno shranjeni");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            counter = 0;
            activities = new string[50, 5]; //cilj, date, time, distance, climb
            Meni();
        }
    }
}