using System;
using System.Collections.Generic;

namespace HashMap
{
    class Program
    {
        static void Main(string[] args)
        {
            //CORE VARIABLES
            var hashTableSize = 11;
            var arrayNames_String = new string[] { "Mia", "Tim", "Bea", "Zoe", "Sue","Len", "Moe", "Lou", "Rae", "Max", "Tod" };
            var arrayPersons = new Person[] {
                new Person("Mia", 111), new Person("Tim", 222), new Person("Bea", 333),
                new Person("Zoe", 444), new Person("Sue", 555), new Person("Len", 666),
                new Person("Moe", 777), new Person("Lou", 888), new Person("Rae", 999),
                new Person("Max", 1000111), new Person("Tod", 1000222)
                };



            HashTable_Key_Only(hashTableSize, arrayNames_String);



            //####################################################
            // Now the question is, how to implement and retrieve key/value pairs?
            HashTable_KeyValue(hashTableSize, arrayPersons);


        }


        //For simplicity we are not going to use generic type parameters <T>
        static void HashTable_KeyValue(int hashTableSize, Person[] persons)
        {
            var arrayList_HashTable = new List<Person>[hashTableSize];

            //initialize HashTable - it's preferable to initialize it now as it will result in a cleaner code
            for (var i = 0; i < arrayList_HashTable.Length; i++)
            {
                arrayList_HashTable[i] = new List<Person>();
            }

            var index = 0;
            // Insert Elements
            foreach (var i in persons)
            {
                index = HashNumber(i.Name, hashTableSize);
                arrayList_HashTable[index].Add(i);
            }

            // Print results
            Console.WriteLine("----------------------------------------");
            for (var i = 0; i < arrayList_HashTable.Length; i++) //run through the array
            {
                Console.WriteLine($"arrayList_HashTable[{i}]");
                foreach (var listItem in arrayList_HashTable[i]) //run through the List
                {
                    Console.WriteLine($"    Key = {listItem.Name}  |  Value = {listItem.Number}");
                }
                Console.WriteLine();
            }

            //####################################################
            // Can we retrieve data?

            //Looking for Mia
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Can we retrieve data?\nLooking for Mia");

            var mia_Hash = HashNumber("Mia", hashTableSize);


            var sublist1 = arrayList_HashTable[mia_Hash];
            foreach (var item in sublist1)
            {
                if ("Mia".CompareTo(item.Name) == 0)
                {
                    Console.WriteLine($"\nResult from querying arrayList_HashTable[mia_Hash] => one item in the list (strItem) => Key = {item.Name}  |  Value = {item.Number}");
                }
            }

        }



        static void HashTable_Key_Only(int hashTableSize , string[] arrayNames_String)
        {
            var arrayString_HashTable = new string[hashTableSize];

            // Expected output: 4, 1, 0, 5, 4, 1, 3, 7, 5, 8, 9
            // Collisions: 4 (Mia, Sue) , 1 (Tim, Len), 5 (Zoe, Rae) 
            foreach (var i in arrayNames_String)
            { _ = HashNumber(i, hashTableSize, true); }


            var index = 0;

            foreach (var i in arrayNames_String)
            {
                index = HashNumber(i, hashTableSize);
                arrayString_HashTable[index] = i;
            }

            //At this point I expect to see some gaps, as per 'Last-write-wins'
            // we permanently lost some elements
            Console.WriteLine("----------------------------------------");
            for (var i = 0; i < arrayString_HashTable.Length; i++)
            { Console.WriteLine($"stringHashTable[{i}] = {arrayString_HashTable[i]}"); }

            //The solution for this can be an array of lists (List<string>[])
            var arrayList_HashTable = new List<string>[hashTableSize];

            //initialize HashTable - it's preferable to initialize it now as it will result in a cleaner code
            for (var i = 0; i < arrayList_HashTable.Length; i++)
            {
                arrayList_HashTable[i] = new List<string>();
            }


            // Insert Elements
            foreach (var i in arrayNames_String)
            {
                index = HashNumber(i, hashTableSize);
                arrayList_HashTable[index].Add(i);
            }

            // Print results
            Console.WriteLine("----------------------------------------");
            for (var i = 0; i < arrayList_HashTable.Length; i++) //run through the array
            {
                Console.WriteLine($"arrayList_HashTable[{i}]");
                foreach (var listItem in arrayList_HashTable[i]) //run through the List
                {
                    Console.WriteLine($"    listItem = {listItem}");
                }
                Console.WriteLine();
            }


            //####################################################
            // Can we retrieve data?

            //Looking for Mia
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Can we retrieve data?\nLooking for Mia");

            var mia_Hash = HashNumber("Mia", hashTableSize);
            Console.WriteLine($"Result from querying arrayString_HashTable[mia_Hash] = {arrayString_HashTable[mia_Hash]}");

            var sublist1 = arrayList_HashTable[mia_Hash];
            foreach (var strItem in sublist1)
            {
                if ("Mia".CompareTo(strItem) == 0)
                {
                    Console.WriteLine($"\nResult from querying arrayList_HashTable[mia_Hash] => one item in the list (strItem) = {strItem}");
                }
            }

        }

        static int HashNumber(string input, int size , bool print = false )
        {
            var sum = 0; var returnValue = 0;
            foreach (var i in input.ToCharArray()  )
            {
                sum += (int)i;
            }
            returnValue = sum % size;
            if (print == true) { Console.WriteLine($"Value: {returnValue} - input: {input}"); }

            return returnValue;
            
        }


    }

    public class Person
    {
        public string Name;
        public int Number;
        public Person(string Name, int Number)
        {
            this.Name = Name;
            this.Number = Number;
        }
    }
}
