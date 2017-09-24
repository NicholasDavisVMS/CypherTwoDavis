using System;
using System.IO;

namespace CeaserCypher
{
	/// <summary>
	/// This program implements the most basic message encryption technique.  
	/// It is called a Ceaser Cypher (See Wikipedia for more details)
	/// 
	/// Your job is to read and understand this code.
	/// Annotate this example code by commenting on every line that you have not 
	/// seen before in this class.  There are many new things here.  Tell me
	/// what new methods do, and why (you think) they are used here.
	/// 
	/// Run the program, give it some example input, see what happens.
	/// 
	/// There are new methods, opperators, and implicit data conversions here.
	/// 
	/// Make sure to read the rubric on Haiku so you know you have 
	/// covered all the points I'm looking for!
	/// 
	/// </summary>
	class MainClass
	{

		/// <summary>
		/// The alphabet in lower case.
		/// </summary>
		public static String alphabet = "abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// The alphabet in upper case.
		/// </summary>
		public static String ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// Encrypts the character using the given key in the given character set.
		/// </summary>
		/// <returns>The character.</returns>
		/// <param name="charSet">Character set which the given character is a part of.</param>
		/// <param name="ch">The character.</param>
		/// <param name="key">Key for the cypher.</param>
		public static char EncryptCharacter(String charSet, char ch, int key)
		{
            ///This code is repeated for each letter in the message due to the foreach in the main method
			if (charSet.IndexOf(ch) >= 0)
			{
				// Get the position of the character ch in the alphabet.
				int charNumber = charSet.IndexOf(ch);
                ////This Line of code converts the 2 alphabets(depending) on which 
                /// one is passed into the method by the main method based on case of letters(MainMethod)
                /// and turns them into number values

				// add the key to the char number (this is the Ceaser Cipher step!)
				// the % 26 wraps around the alphabet so 'x' + 5 => 'c'
				int encryptedCharNumber = (charNumber + key) % charSet.Length;

				// get the character associated with the encrypted character number in the lowercase alphabet.
				char encryptedChar = charSet[encryptedCharNumber];
                ///This line of code transformed the letter-turned-into-number back into letters
                /// although the letters have been encrypted now so the message 'won't' be the same

				// stick that encrypted character on the end of the encryptedMessage.
				return encryptedChar;
                ///This returns the encrypted letter back to the main method for printing
			}
			else
			{
				// ignore it if they character is not a part of the given character set
				return ch;
                ///Extra characters are just ignored
			}
		}

		/// <summary>
		/// Reads text from file and returns it as a String.
		/// </summary>
		/// <returns>The contents from file.</returns>
		/// <param name="fileName">Full file path.</param>
		public static String ReadFromFile(String fileName)
		{
			String contents = "";
			StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open));

			while (!reader.EndOfStream)
			{
				contents += String.Format("{0}{1}", reader.ReadLine(), Environment.NewLine);
			}

			reader.Close();

			return contents;
		}


		public static char DecryptCharacter(String charDe, char le, int keyTwo)
		{
            ///This decrypting method fights fire with fire by using the same method as 
            /// the encypting method just altered so that it decrypts instead of encrypts
            if (charDe.IndexOf(le) >= 0)
			{
				
                int charNumber = charDe.IndexOf(le);
				
                int encryptedCharNumber = (charNumber + (26 - keyTwo)) % charDe.Length;

				
				char encryptedChar = charDe[encryptedCharNumber];
				
				return encryptedChar;
				
			}
			else
			{
				// ignore it if they character is not a part of the given character set
                return le;
				///Extra characters are just ignored
			}
		}

		public static void Main(string[] args)
		{
			// Get a secret message from the user.
			Console.WriteLine("Enter a message to encrypt:");
			String message = Console.ReadLine();

			// Get the KEY with which to encrypt this secret message.  This must be an INTEGER.
			Console.Write("Enter the KEY to encrypt this message with >> ");
			int key = Convert.ToInt32(Console.ReadLine());

			// Make a place to store the encrypted message while I'm working on building it.
			String encryptedMessage = "";

			// Iterate through the message that the user typed one character at a time.
			foreach (char ch in message.ToCharArray())
			{
				// if this is a lowercase letter, encrypt the character using the lowercase alphabet string.
				// IndexOf returns -1 if the character does not appear in the string!
				if (alphabet.IndexOf(ch) >= 0)
				{

					// stick that encrypted character on the end of the encryptedMessage.
					encryptedMessage += EncryptCharacter(alphabet, ch, key);
                    //the "EncryptCharacter" sends the inputed data to another method,
                    //and that method is encrypting the message.
				}

				// Do the same thing, if the character is a Capital letter.
				else if (ALPHABET.IndexOf(ch) >= 0)
				{
					encryptedMessage += EncryptCharacter(ALPHABET, ch, key);
					//the "EncryptCharacter" sends the inputed data to another method,
					//and that method is encrypting the message. Here it is for capital letters
                    //in the ALPHABET STRING, not the alphabet string
				}

				// If the character is neither a capital nor lowercase, then ignore it and don't try to encrypt.
				else
				{
					encryptedMessage += ch;
                    //For symbols and numbers that aren't in the alphabet, these will just be added to
                    //The encrypted message unaltered. Including Spaces.
				}
			}

			// Print the encrypted message!
			Console.WriteLine("_________________________________________________");
			Console.WriteLine("Your encrypted message is:\n\n{0}", encryptedMessage);
			Console.ReadKey();
			/// the "\n" is a new line

			Console.WriteLine("Enter a message to decrypt");
            String messageTwo = Console.ReadLine();

			Console.Write("Enter the KEY to decrypt this message with >> ");
			int keyTwo = Convert.ToInt32(Console.ReadLine());

            string decryptedMessage = "";

            foreach (char le in encryptedMessage)
            {
                if (alphabet.IndexOf(le) >= 0)
                {
                    decryptedMessage += DecryptCharacter(alphabet, le, key);
                }
                else if (ALPHABET.IndexOf(le) >= 0)
                {
                    decryptedMessage += DecryptCharacter(ALPHABET, le, key);
                }
                else
                {
                    decryptedMessage += le;
                }
            }
            Console.WriteLine("Your decrypted message is:\n\n{0}", decryptedMessage);
			Console.ReadKey();


            ///QUESTIONS: (ask what abstraction is, internet search not helping too well)
			/// This code makes use of abstraction by turning the entire string,
			/// however long, of the message into an array where each alphabetical 
            /// character in the array can then be changed into another character
            /// to encrypt the message.
			/// 
            /// To make a better encryption algorithm, there would have to be 
            /// different key number for each character in the message based around
            /// a list of numbers, for example for a key '24' for the message 'ab'
            /// it would turn the a into c but the b would go to f instead of d.
            /// Obviously that is a easy example, and a more ideal encryption algorithm
            /// would require a longer string of numbers like "356289" for example,
            /// even perhaps akey going into the hundreads and thousands place if possible.
		}
	}
}
