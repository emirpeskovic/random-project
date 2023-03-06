using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Random;

// Create new Random instance
var random1 = new System.Random();

// Create a list to store numbers from random1
var randomNumbers1 = new List<int>();

// Create new RandomNumberGenerator instance
var random2 = RandomNumberGenerator.Create();

// Create a list to store numbers from random2
var randomNumbers2 = new List<int>();

// Create a new Stopwatch instance
var timer = new Stopwatch();

// Start the timer
timer.Start();
// Generate 1 million random numbers from Random() and put them in the corresponding list
for (var i = 0; i < 1_000_000; i++)
{
    randomNumbers1.Add(random1.Next());
}
// Stop the timer
timer.Stop();

// Write the elapsed time to the console
Console.WriteLine($"Random() took {timer.ElapsedMilliseconds} ms");

// Create our buffer for RandomNumberGenerator
var buffer = new byte[4];

// Restart the timer
timer.Restart();
// Generate 1 million random numbers from RandomNumberGenerator and put them in the corresponding list
for (var i = 0; i < 1_000_000; i++)
{
    random2.GetBytes(buffer);
    randomNumbers2.Add(BitConverter.ToInt32(buffer));
}
// Stop the timer
timer.Stop();

// Write the elapsed time to the console
Console.WriteLine($"RandomNumberGenerator took {timer.ElapsedMilliseconds} ms");

// Write each list to a file for excel to read
File.WriteAllLines("random1.csv", randomNumbers1.Select(x => x.ToString()));
File.WriteAllLines("random2.csv", randomNumbers2.Select(x => x.ToString()));

// Encrypt our message
var encryptedMessage = CaesarCipher.Encrypt("Hello, World!");

// Write the encrypted message to the console
Console.WriteLine(Encoding.ASCII.GetString(encryptedMessage));

// Decrypt our message
var decryptedMessage = CaesarCipher.Decrypt(encryptedMessage, 123, 456, 789);

// Write the decrypted message to the console
Console.WriteLine(decryptedMessage);