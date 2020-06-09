using System;
using System.Collections.Generic;
using static System.Console;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            ForegroundColor = ConsoleColor.White;
            string alph = "abcdefghijklmnopqrstuvwxyz";
            char curValR, curValM, curValL;
            Dictionary<char, char> roterR = new Dictionary<char, char>
            {
                {'a','l'},{'b','e'},{'c','y'},{'d','j'},{'e','v'},{'f','c'},{'g','n'},{'h','i'},
                {'i','x'},{'j','w'},{'k','p'},{'l','b'},{'m','q'},{'n','m'},{'o','d'},{'p','r'},
                {'q','t'},{'r','a'},{'s','k'},{'t','z'},{'u','g'},{'v','f'},{'w','u'},{'x','h'},
                {'y','o'},{'z','s'}
            };
            Dictionary<char, char> roterM = new Dictionary<char, char>
            {
                {'a','b'},{'b','d'},{'c','f'},{'d','h'},{'e','j'},{'f','l'},{'g','c'},{'h','p'},
                {'i','r'},{'j','t'},{'k','x'},{'l','v'},{'m','z'},{'n','n'},{'o','y'},{'p','e'},
                {'q','i'},{'r','w'},{'s','g'},{'t','a'},{'u','k'},{'v','m'},{'w','u'},{'x','s'},
                {'y','q'},{'z','o'}
            };
            Dictionary<char, char> roterL = new Dictionary<char, char>
            {
                {'a','f'},{'b','s'},{'c','o'},{'d','k'},{'e','a'},{'f','n'},{'g','u'},{'h','e'},
                {'i','r'},{'j','h'},{'k','m'},{'l','b'},{'m','t'},{'n','i'},{'o','y'},{'p','c'},
                {'q','w'},{'r','l'},{'s','q'},{'t','p'},{'u','z'},{'v','x'},{'w','v'},{'x','g'},
                {'y','j'},{'z','d'}
            };
            Dictionary<char, char> reflector = new Dictionary<char, char>
            {
                {'a','f'},{'b','v'},{'c','p'},{'d','j'},{'e','i'},{'g','o'},{'h','y'},
                {'k','r'},{'l','z'},{'m','x'},{'n','w'},{'t','q'},{'s','u'},
                {'f','a'},{'v','b'},{'p','c'},{'j','d'},{'i','e'},{'o','g'},{'y','h'},
                {'r','k'},{'z','l'},{'x','m'},{'w','n'},{'q','t'},{'u','s'},
            };

            Enigma enigma = new Enigma();
            WriteLine("RoterL:\t\tRoterM:\t\tRoterR");
            for (int i = 0; i < alph.Length; i++)
                WriteLine(i.ToString() + ")" + alph[i] + "-" + roterL[alph[i]] + "\t\t" + i.ToString() + ")" + alph[i] + "-" + roterM[alph[i]] +
                    "\t\t" + i.ToString() + ")" + alph[i] + "-" + roterR[alph[i]]);
            WriteLine("\nReflector: ");
            foreach (var keyValue in reflector)
                Write(keyValue.Key + "->" + keyValue.Value + " ");
            Write("\n\nInput word: ");
            string inputString = ReadLine();
            for (int i = 0; i < 5; i++)
            {
                Write("Input start value for Roter R: ");
                curValR = Convert.ToChar(ReadLine());
                Write("Input start value for Roter M: ");
                curValM = Convert.ToChar(ReadLine());
                Write("Input start value for Roter L: ");
                curValL = Convert.ToChar(ReadLine());
                WriteLine($"Cur value of roters after moving: {enigma.MoveRingRoter(curValL, 4, alph)} " +
                    $"{enigma.MoveRingRoter(curValM, 1, alph)} {enigma.MoveRingRoter(curValR, 1, alph)}");
                WriteLine("\nMessage: " + inputString);
                string shifr = enigma.Shifr(inputString, curValR, curValM, curValL, roterL, roterM,
                    roterR, reflector, alph);
                ForegroundColor = ConsoleColor.Green;
                WriteLine("Shifr: " + shifr);
                ForegroundColor = ConsoleColor.White;
                ForegroundColor = ConsoleColor.Red;
                WriteLine("-------------------------------------");
                ForegroundColor = ConsoleColor.White;
            }
        }

        static int Counter(char c, string s)
        {
            int counter = 0;
            foreach (char ch in s)
                if (c == ch)
                    counter++;
            return counter;
        }
    }
}