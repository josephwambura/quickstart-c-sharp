string[] secretWords = { "Daniel", "Margaret", "Joseph", "Peter", "Wambui", "Anne", "Kelsey" };

var random = new Random();

var secretWord = secretWords[random.Next(secretWords.Length)];

var guess = "";

var guessCount = 0;

var guessLimit = 3;

var outOfGuesses = false;

// using a While loop

//while (guess != secretWord && !outOfGuesses)
//{
//    if (guessCount < guessLimit)
//    {
//        Console.Write("Enter your Guess Word: ");

//        guess = Console.ReadLine();

//        guessCount++;
//    }
//    else
//    {
//        outOfGuesses = true;
//    }
//}

// using a do while loop

do
{
    if (guessCount < guessLimit)
    {
        Console.Write("Enter your Guess Word: ");

        guess = Console.ReadLine();

        guessCount++;
    }
    else
    {
        outOfGuesses = true;
    }
} while(guess != secretWord && !outOfGuesses);

if (outOfGuesses)
    Console.WriteLine("You Lose!");
else
    Console.WriteLine("You Win!");

Console.ReadLine();
