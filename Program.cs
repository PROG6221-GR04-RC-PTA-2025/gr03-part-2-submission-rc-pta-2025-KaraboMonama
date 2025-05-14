using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

class Program
{
    static Dictionary<string, string> keywordResponses = new Dictionary<string, string>
    {
        { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details." },
        { "scam", "Be cautious of offers that seem too good to be true; they often are." },
        { "privacy", "Regularly review your privacy settings on social media platforms." }
    };
    // List of phishing tips for random selection
    static List<string> phishingTips = new List<string>
    {
        "Be cautious of emails asking for personal information.",
        "Look for spelling mistakes in emails claiming to be from trusted organizations.",
        "Never click on links from unknown sources."
    };

    static string userName; // Variable to store the user's name
    static string favoriteTopic;// Variable to store the user's favorite topic 

    static void Main()
    {
        
        PlayGreeting();
        DisplayAsciiArt();

        // Ask for the user's name and store it
        Console.Write("What's your name? ");
        userName = Console.ReadLine();

        // Ask for the user's favorite cybersecurity topic
        Console.Write("What's your favorite cybersecurity topic? ");
        favoriteTopic = Console.ReadLine();
        Console.WriteLine($"Great! I'll remember that you're interested in {favoriteTopic}.");

        string lastTopic = "";// Variable to keep track of the last topic discussed

        while (true)
        {
            // Prompt the user for input
            Console.Write("You can ask me anything: ");
            string userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                // Check if the input is empty or whitespace
                Console.WriteLine("Please enter a question.");
                continue;
            }

            // Check for any emotional sentiment in the user's input
            string sentimentResponse = DetectSentiment(userInput);
            if (sentimentResponse != null)
            {
                ShowThinking();// Show a thinking message while processing
                Console.WriteLine(sentimentResponse); //Display the sentiment response
                continue; // restart the loop
            }

            
            if (userInput.Contains("more about"))
            {
                if (lastTopic != "")
                {
                    Console.WriteLine($"Continuing on the topic of {lastTopic}...");
                    continue;
                }
                else
                {
                    Console.WriteLine("Could you clarify which topic you want to know more about?");
                    continue;
                }
            }

            // Get the response based on user input and remember the last topic
            lastTopic = RespondToUser(userInput);
        }
    }

   
    static void PlayGreeting()
    {
        using (SoundPlayer player = new SoundPlayer("greeting.wav"))
        {
            player.PlaySync();
        }
    }

    // Method to display ASCII art
    static void DisplayAsciiArt()
    {
        string asciiArt = @"
        ____   ____       _               _             
       |  _ \ / __ \     | |             | |            
       | |_) | |  | |    | |__   ___  ___| |_ ___  _ __ 
       |  _ <| |  | |    | '_ \ / _ \/ __| __/ _ \| '__|
       | |_) | |__| |    | | | |  __/ (__| || (_) | |   
       |____/ \____/     |_| |_|\___|\___|\__\___/|_|   
        Cybersecurity Awareness Bot
        ";
        Console.WriteLine(asciiArt);
        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine("Hello, Bo Hector here!");
        Console.ResetColor(); // Reset back to default color
    }

    static string RespondToUser(string userInput)
    {
        foreach (var keyword in keywordResponses.Keys)
        {
            if (userInput.Contains(keyword))
            {
                ShowThinking();
                Console.WriteLine(keywordResponses[keyword]);
                return keyword; // Return the recognized keyword
            }
        }

        if (userInput.Contains("phishing tips"))
        {
            Random rand = new Random();
            int index = rand.Next(phishingTips.Count);
            ShowThinking();
            Console.WriteLine(phishingTips[index]);
            return "phishing"; // Indicate the last topic
        }

        if (userInput.Contains("privacy"))
        {
            ShowThinking();
            Console.WriteLine($"As someone interested in {favoriteTopic}, you might want to check your privacy settings.");
            return "privacy"; // Indicate the last topic
        }

        Console.WriteLine("I'm not sure I understand. Can you try rephrasing?");
        return ""; // No specific topic recognized
    }

    //Method to simulate thinking and response delay
    static void ShowThinking()
    {
        Console.Write("THINKING...");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(1000); // Wait for 1 second
            Console.Write(".");
        }
        Console.WriteLine(); // New line after thinking
    }

    static string DetectSentiment(string userInput)
    {
        if (userInput.Contains("worried"))
        {
            ShowThinking();
            return "It's completely understandable to feel that way. Scammers can be very convincing. Here are some tips to help you stay safe:\n" +
                   "- Always verify the sender of emails.\n" +
                   "- Use two-factor authentication.\n" +
                   "- Keep your software updated.";
        }
        else if (userInput.Contains("curious"))
        {
            ShowThinking();
            return "That's great! I'm here to provide you with information. Feel free to ask me anything!";
        }
        return null; // No sentiment detected
    }
}