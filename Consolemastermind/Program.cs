using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            var pattern = getRandom();
            bool win = false;
            bool exit = false;
            int attempts = 0;
            Console.WriteLine("Welcome to the matching game.  Try to guess the pre-selected pattern of numbers. Type 'exit' at any time to quit.");
            Console.WriteLine("================================================================================================================");

            while (!exit)
            {
                while (!win)
                {
                    Console.Write("Enter 5 numbers (0-9): ");
                    var input = Console.ReadLine();
                    
                    if (input.ToLower() == "exit")
                    {
                        exit = true;
                        break;
                    }
                    else
                    {
                        attempts++;
                        var numbers = input.ToCharArray().Where(x => int.TryParse(x.ToString(), out int tempInt)).Select(x => int.Parse(x.ToString())).ToArray();
                        
                        //Compare
                        if (numbers.SequenceEqual(pattern))
                        {
                            Console.WriteLine("You win! You have cracked the code in " + attempts + " attempts.");
                            win = true;
                            attempts = 0;
                        }
                        else
                        {
                            //break it down for the user.  How many where in correct spot?  How many where right but not in correct spot?
                            int correct = 0, close = 0;

                            for (var x = 0; x < numbers.Length; x++)
                            {
                                var guess = numbers[x];

                                for (var y = x; y < pattern.Length; y++)
                                {
                                    var answer = pattern[y];

                                    if (guess.Equals(answer) && x == y)
                                    {
                                        //correct and in same position
                                        correct++;
                                    }
                                    else if(pattern.Contains(guess) && x == y)
                                    {
                                        //not correct but it is in the pattern
                                        close++;
                                    }
                                }
                            }

                            Console.WriteLine(String.Format("Great guess, but your are going to have to try again. Your current results are {0} correct and {1} are close.", correct, close));
                            Console.WriteLine("\n");
                        }
                    }
                }

                if (win)
                {
                    Console.WriteLine("Would you like to play again? (y/n)");
                    var input = Console.ReadLine();

                    if (input == "y")
                    {
                        //reset the code
                        pattern = getRandom();
                        //clear the screen
                        Console.Clear();
                        //restart
                        win = false;
                    }
                    else
                        exit = true;
                }
            }
        }

        private static int[] getRandom()
        {
            Random r = new Random();
            int[] numbersArray = new int[5];
            int digitRange = 9;

            for (int i = 0; i < numbersArray.Length; i++)
            {
                numbersArray[i] = r.Next(1, digitRange); //digitRange represents the maximum digit value, such as not exceeding "9"
            }

            return numbersArray;
        }
    }
}
