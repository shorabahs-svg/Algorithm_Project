
using System.Collections.Generic;

internal class Program
{
    enum Grade { Failed, Good, VeryGood, Excellent}
    class StdNode
    {
        public int id;
        public string name;
        public string governorate;
        public float mark1;
        public float mark2;
        public float average;
        public Grade grade;
        public StdNode Next; // Refers to the next node.
        public StdNode Prev; // Refers to the previous node.
    }
    class DLinkList
    {
        public StdNode First;
        public StdNode Last;
        // Add a new student (At the start or at the end.
        public void AddStd(bool End) 
        {
            StdNode n = new StdNode();
            Console.WriteLine("Enter the Data of the new student");
            Console.Write("University Number: ");
            n.id = int.Parse(Console.ReadLine());
            Console.Write("Full Name: ");
            n.name = Console.ReadLine();
            Console.Write("Governorate: ");
            n.governorate = Console.ReadLine();
            Console.Write("Mark 1: ");
            n.mark1 = float.Parse(Console.ReadLine());
            Console.Write("Mark 2: ");
            n.mark2 = float.Parse(Console.ReadLine());
            // Calculate the average.
            n.average = (n.mark1 + n.mark2) / 2;
            // Selection of the grade manually by the user.
            Console.WriteLine("Select the appropriate grade: (0: Failed, 1: Good, 2: Very Good, 3: Excellent)");
            n.grade = (Grade)int.Parse(Console.ReadLine());
            if (First == null) // The menu is empty (No students)
            {
                First = Last = n;
            }
            else if (End) // Adding to the end of the list
            {
                Last.Next = n;
                n.Prev = Last;
                First = n;
            }
            else // Adding to the beginning of the list
            {
                n.Next = First;
                First.Prev = n;
                First = n;
            }
            Console.WriteLine("Student data saved successfully");
        }
        // Recursive search for a specific mark.
        public void RecursiveSearchByScore(StdNode current, float score)
        {
            if (current == null)
            {
                return; // Stopping state (The end of List.
            }
            if (current.average == score)
            {
                Console.WriteLine($"We found a student. Name: {current.name}, Governorate: {current.governorate}");
            }
            // Calling of the same method with the next node.
            RecursiveSearchByScore(current.Next, score);
        }
        public void Sort (bool scoresort) // Bubble sort of the list
        {
            if (First == null) return;
            bool swapped;
            do
            {
                swapped = false;
                StdNode current = First;
                while (current.Next != null)
                {
                    bool swap = false;
                    if (scoresort) // Sort by Average (Ascending).
                    {
                        swap = current.average > current.Next.average;
                    }
                    else // Sort by name (A to Z)
                    {
                        swap = string.Compare(current.name, current.Next.name) > 0;
                    }
                    if (swap) // Swapping the data
                    {
                        SwapData(current, current.Next);
                        swapped = true; 
                    }
                    current = current.Next;
                }
            }
            while (swapped);
            Console.WriteLine("Sorted successfully!");
        }
        private void SwapData(StdNode a,  StdNode b)
        {
            // Swap all of the fields
            (a.id, b.id) = (b.id, a.id);
            (a.name, b.name) = (b.name, a.name);
            (a.governorate, b.governorate) = (b.governorate, a.governorate);
            (a.average, b.average) = (b.average, a.average);
            (a.grade, b.grade) = (b.grade, a.grade);
        }
        public void DisplayList()
        {
            if (First == null)
            {
                Console.WriteLine("The List is empty");
                return;
            }
            StdNode temp = First;
            Console.WriteLine("Now showing the current data of the students");
            while (temp != null)
            {
                Console.WriteLine($"Number: [{temp.id}] / Name: [{temp.name}] / Average: [{temp.average}] / Grade: [{temp.grade}]");
                temp = temp.Next;
            }
        }
    }
    private static void Main(string[] args)
    {
        DLinkList li = new DLinkList();
        Console.WriteLine("Welcome to the student system!");
        for (int i = 1; i<=5; i++)
        {
            Console.WriteLine($"Student data No. [{i}]");
            li.AddStd(true);
        }
        int option;
        do
        {
            Console.WriteLine("**** Main Menu ****");
            Console.WriteLine("Choose the procedure you want...");
            Console.WriteLine("1. Add a new student (Start / End )");
            Console.WriteLine("2. Sort Students (Name / Average )");
            Console.WriteLine("3. Find a student by mark (Recursively)");
            Console.WriteLine("4. Display the Full List");
            Console.WriteLine("0. Exit");
            option = int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.Write("Where do you want to add? (1. Start) , (2. End)");
                    li.AddStd(Console.ReadLine() == "2");
                    break;
                case 2:
                    Console.WriteLine("Sort by... (1. Name 'A to Z') , (Average 'Ascending')");
                    li.Sort(Console.ReadLine() == "2");
                    li.DisplayList();
                    break;
                case 3:
                    Console.WriteLine("Input the Mark To search for...");
                    float score = float.Parse(Console.ReadLine());
                    Console.WriteLine("Search Results:");
                    li.RecursiveSearchByScore(li.First, score);
                    break;
                case 4:
                    li.DisplayList();
                    break;

            }
        }
        while (option != 0);
    }
}
