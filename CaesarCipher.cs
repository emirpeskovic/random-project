using System.Text;

namespace Random;

public static class CaesarCipher
{
    private static readonly List<char> Alphabet = new()
    {
        ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+',
        ',', '-', '.', '/', '0', '1', '2', '3', '4', '5', '6', '7',
        '8', '9', ':', ';', '<', '=', '>', '?', '@',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
        'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
        'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
        'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z',
    };

    public static byte[] Encrypt(string message)
    {
        // We create a new PseudoRand instance with our 'key' as the seed
        var random = new PseudoRand(123, 456, 789);
        
        // We create a new StringBuilder instance to store our encrypted message
        var encryptedMessage = new StringBuilder();
        
        // We loop through each character in the message
        foreach (var character in message)
        {
            // We get the index of the character in the alphabet
            var index = Alphabet.IndexOf(character);
            
            // We get a random number between 0 and the length of the alphabet
            var randomIndex = random.Next(0, Alphabet.Count - 1);
            
            // We get the new index by adding the random number to the index of the character and modding it by the length of the alphabet
            var newIndex = (index + randomIndex) % Alphabet.Count;
            
            // We add the character at the index of the random number to the encrypted message
            encryptedMessage.Append(Alphabet[newIndex]);
        }
        
        // We return the encrypted message as a byte array
        return Encoding.ASCII.GetBytes(encryptedMessage.ToString());
    }

    public static string Decrypt(byte[] encryptedMessage, uint seed1, uint seed2, uint seed3)
    {
        // We create a new PseudoRand instance with our 'key' as the seed
        var random = new PseudoRand(seed1, seed2, seed3);
        
        // We create a new StringBuilder instance to store our decrypted message
        var decryptedMessage = new StringBuilder();
        
        // We loop through each character in the message
        foreach (var character in Encoding.ASCII.GetString(encryptedMessage))
        {
            // We get the index of the character in the alphabet
            var index = Alphabet.IndexOf(character);
            
            // We get a random number between 0 and the length of the alphabet
            var randomIndex = random.Next(0, Alphabet.Count - 1);
            
            // We get the new index by subtracting the random number from the index of the character and modding it by the length of the alphabet
            var newIndex = (index - randomIndex + Alphabet.Count) % Alphabet.Count;
            
            // We add the character at the index of the random number to the decrypted message
            decryptedMessage.Append(Alphabet[newIndex]);
        }
        
        // We return the decrypted message as a string
        return decryptedMessage.ToString();
    }
}