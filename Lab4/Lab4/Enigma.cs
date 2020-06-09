using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    class Enigma
    {
        public string Shifr(string msg, char curValR, char curValM, char curValL, Dictionary<char, char> roterL,
            Dictionary<char, char> roterM, Dictionary<char, char> roterR, Dictionary<char, char> reflector, string alph)
        {
            string res = String.Empty;
            for (int i = 0; i < msg.Length; i++)
            {
                char output;
                char inputSymb = msg[i];
                curValR = MoveRingRoter(curValR, 4, alph);
                curValM = MoveRingRoter(curValM, 1, alph);
                curValL = MoveRingRoter(curValL, 1, alph);
                output = RoterToReflector(roterR, alph, inputSymb, 'a', curValR);
                output = RoterToReflector(roterM, alph, output, curValR, curValM);
                output = RoterToReflector(roterL, alph, output, curValM, curValL);
                output = ToReflector(reflector, output, curValL, alph);
                output = FromReflector(output, curValL, roterL, alph);
                output = RoterFromReflector(roterM, alph, output, curValL, curValM);
                output = RoterFromReflector(roterR, alph, output, curValM, curValR);
                output = GetResult(output, curValR, alph);
                res += output;
            }
            return res;
        }

        char RoterToReflector(Dictionary<char, char> roter, string alph, char input,
            char prevRoterCurVal, char nextRoterCurVal)
        {
            Console.WriteLine($"{nextRoterCurVal}-{prevRoterCurVal}={alph.IndexOf(nextRoterCurVal) - alph.IndexOf(prevRoterCurVal)}");
            int modif = (alph.IndexOf(input) + (alph.IndexOf(nextRoterCurVal)
                - alph.IndexOf(prevRoterCurVal)));
            if (modif < 0)
                while (modif < 0)
                    modif += alph.Length;
            else
                modif = modif % alph.Length;
            char roterInput = alph[modif];
            Console.WriteLine($"{input}+({alph.IndexOf(nextRoterCurVal) - alph.IndexOf(prevRoterCurVal)})={roterInput}");
            Console.WriteLine("Output roter to reflector: " + roter[roterInput]);
            return roter[roterInput];
        }

        char RoterFromReflector(Dictionary<char, char> roter, string alph, char output,
            char prevRouterCurVal, char nextRouterCurVal)
        {
            int modif = (alph.IndexOf(output) - (alph.IndexOf(prevRouterCurVal) - alph.IndexOf(nextRouterCurVal))) % alph.Length;
            if (modif < 0)
                while (modif < 0)
                    modif += alph.Length;
            char modifChar = alph[modif];
            Console.WriteLine("To roter from reflector:");
            Console.WriteLine($"{output}-({alph.IndexOf(prevRouterCurVal) - alph.IndexOf(nextRouterCurVal)})={modifChar}");
            foreach (var keyValue in roter)
                if (keyValue.Value == modifChar)
                {
                    Console.WriteLine($"{modifChar}<-{keyValue.Key}");
                    return keyValue.Key;
                }
            return default(char);
        }

        public char MoveRingRoter(char curValRoter, int move, string alph)
            => alph[(alph.IndexOf(curValRoter) + move) % alph.Length];

        char ToReflector(Dictionary<char, char> reflector, char outputRoterL, char curValRotL, string alph)
        {
            int modif = alph.IndexOf(outputRoterL) - alph.IndexOf(curValRotL);
            int a = modif;
            if (modif < 0)
                while (modif < 0)
                    modif += alph.Length;
            char modifChar = alph[modif];
            Console.WriteLine($"{outputRoterL}-({alph.IndexOf(curValRotL)})={modifChar}");
            Console.WriteLine($"Reflector: {modifChar}->{reflector[modifChar]}");
            return reflector[modifChar];
        }
        char FromReflector(char outputToRelector, char curValRoterL, Dictionary<char, char> roterL, string alph)
        {
            char modifChar = alph[(alph.IndexOf(outputToRelector) + alph.IndexOf(curValRoterL)) % alph.Length];
            foreach (var keyValue in roterL)
                if (keyValue.Value == modifChar)
                {
                    Console.WriteLine("To roter from reflector:");
                    Console.WriteLine($"{outputToRelector}+{alph.IndexOf(curValRoterL)}={keyValue.Value}");
                    Console.WriteLine($"{keyValue.Value}<-{keyValue.Key}");
                    return keyValue.Key;
                }
            return default(char);
        }

        char GetResult(char output, char curValR, string alph)
        {
            int modif = alph.IndexOf(output) - alph.IndexOf(curValR);
            if (modif < 0)
                while (modif < 0)
                    modif += alph.Length;
            Console.WriteLine($"Result:\n{output}-({alph.IndexOf(curValR)})={alph[modif]}");
            Console.WriteLine("-------------------------------------");
            return alph[modif];
        }
    }
}