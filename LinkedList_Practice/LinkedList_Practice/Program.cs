using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedList_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = { "the", "dog", "jumps", "over", "the", "log" };
            LinkedList<string> sentence = new LinkedList<string>(words);

            DisplayAllWords(sentence, "Original LinkedList values:");
            GetNode(sentence);

            AddNodes(sentence, new string[] { "and", "the", "hog" });
            DisplayAllWords(sentence, "After adding new nodes to the end of the LinkedList:");

            sentence.AddBefore(sentence.Find("dog"), "big");
            DisplayAllWords(sentence, "After adding a node before 'dog':");

            LinkedListNode<string> dogNode = sentence.Find("dog");
            Console.WriteLine($"Value before 'dog' node: {dogNode.Previous.Value}");
            Console.WriteLine($"Value after 'dog' node: {dogNode.Next.Value}");
            
            sentence.AddBefore(dogNode, "ugly");
            sentence.AddAfter(dogNode, "barely");
            Console.WriteLine($"Value after adding 'ugly' before 'dog': {dogNode.Previous.Value}");
            Console.WriteLine($"Value after adding 'barely' after 'dog': {dogNode.Next.Value}\n");

            DisplayAllWords(sentence, "After adding nodes before and after 'dog':");


            LinkedList<string> linkedList2 = new LinkedList<string>(new string[] { "the", "big", "dog" });
            Console.WriteLine("Working on new LinkedList values");
            ReverseSentence(linkedList2, "Reversed LinkedList values:");

            FindAndReplace(linkedList2, "\nFinding and replacing 'dog' with 'cat'");
        }

        static void DisplayAllWords(LinkedList<string> mySentence, string text)
        {
            Console.WriteLine(text);
            foreach (string word in mySentence)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine("\n");
        }

        static void GetNode(LinkedList<string> mySentence)
        {
            LinkedListNode<string> firstNode = mySentence.First;
            LinkedListNode<string> lastNode = mySentence.Last;

            Console.WriteLine("First node value: " + firstNode.Value);
            Console.WriteLine("Last node value: " + lastNode.Value);

            Console.WriteLine("\n");
        }

        static void AddNodes(LinkedList<string> mySentence, params string[] words)
        {
            foreach (string word in words)
            {
                mySentence.AddLast(word);
            }
        }

        static void ReverseSentence(LinkedList<string> mySentence, string text)
        {
            LinkedListNode<string> currentNode = mySentence.Last;
            Console.WriteLine(text + " ");

            while (currentNode != null)
            {
                Console.Write(currentNode.Value + " ");
                currentNode = currentNode.Previous;
            }
            Console.WriteLine();
        }

        static void FindAndReplace(LinkedList<string> mySentence, string text)
        {
            Console.WriteLine(text);

            LinkedListNode<string> findNode = mySentence.Find("dog");

            if (findNode != null)
            {
                string currentNodeValue = findNode.Value;
                findNode.Value = "cat";

                Console.WriteLine($"Replaced '{currentNodeValue}' with '{findNode.Value}'");
                DisplayAllWords(mySentence, "\nUpdated LinkedList values:");
            }
            else
            {
                Console.WriteLine($"Could not find the word in the LinkedList.");
            }
        }
    }
}