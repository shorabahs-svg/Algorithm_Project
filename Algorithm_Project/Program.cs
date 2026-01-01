using System;

enum Grade { Failed, Good, VeryGood, Excellent }

class StdNode
{
    public int id;
    public string name;
    public string governorate;
    public float mark1;
    public float mark2;
    public float average;
    public Grade grade;
    public StdNode Next;
    public StdNode Prev;
}

class DLinkList
{
    public StdNode First;
    public StdNode Last;

    public void AddStd(bool End)
    {
        StdNode n = new StdNode();
        Console.WriteLine("\nEnter the Data of the new student");
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
        n.average = (n.mark1 + n.mark2) / 2;

        Console.WriteLine("Select grade: (0: Failed, 1: Good, 2: Very Good, 3: Excellent)");
        n.grade = (Grade)int.Parse(Console.ReadLine());

        if (First == null)
        {
            First = Last = n;
        }
        else if (End)
        {
            // FIX: Correctly link to the end and update 'Last'
            Last.Next = n;
            n.Prev = Last;
            Last = n; 
        }
        else
        {
            // FIX: Correctly link to the start and update 'First'
            n.Next = First;
            First.Prev = n;
            First = n;
        }
        Console.WriteLine("Student data saved successfully");
    }

    public void RecursiveSearchByScore(StdNode current, float score)
    {
        if (current == null) return;
        
        if (current.average == score)
        {
            Console.WriteLine($"Found: {current.name}, Gov: {current.governorate}");
        }
        RecursiveSearchByScore(current.Next, score);
    }

    public void Sort(bool scoresort)
    {
        if (First == null || First.Next == null) return;
        bool swapped;
        do
        {
            swapped = false;
            StdNode current = First;
            while (current.Next != null)
            {
                bool shouldSwap = scoresort ? 
                    current.average > current.Next.average : 
                    string.Compare(current.name, current.Next.name) > 0;

                if (shouldSwap)
                {
                    SwapData(current, current.Next);
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped);
        Console.WriteLine("Sorted successfully!");
    }

    private void SwapData(StdNode a, StdNode b)
    {
        (a.id, b.id) = (b.id, a.id);
        (a.name, b.name) = (b.name, a.name);
        (a.governorate, b.governorate) = (b.governorate, a.governorate);
        (a.mark1, b.mark1) = (b.mark1, a.mark1); // Added mark swap
        (a.mark2, b.mark2) = (b.mark2, a.mark2); // Added mark swap
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
        Console.WriteLine("\n--- Current Student Data ---");
        while (temp != null)
        {
            Console.WriteLine($"ID: [{temp.id}] | Name: [{temp.name}] | Avg: [{temp.average}] | Grade: [{temp.grade}]");
            temp = temp.Next;
        }
    }
}

class Program // Wrapped the Main method in a class
{
    static void Main(string[] args)
    {
        DLinkList li = new DLinkList();
        Console.WriteLine("Welcome to the student system!");
        
        // Reduced to 2 for testing purposes, change back to 5 if needed
        for (int i = 1; i <= 2; i++)
        {
            Console.WriteLine($"\nStudent data No. [{i}]");
            li.AddStd(true);
        }

        int option;
        do
        {
            Console.WriteLine("\n**** Main Menu ****");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. Sort Students");
            Console.WriteLine("3. Find a student by mark");
            Console.WriteLine("4. Display List");
            Console.WriteLine("0. Exit");
            
            if (!int.TryParse(Console.ReadLine(), out option)) continue;

            switch (option)
            {
                case 1:
                    Console.Write("1. Start, 2. End: ");
                    li.AddStd(Console.ReadLine() == "2");
                    break;
                case 2:
                    Console.Write("1. Name, 2. Average: ");
                    li.Sort(Console.ReadLine() == "2");
                    li.DisplayList();
                    break;
                case 3:
                    Console.Write("Enter mark: ");
                    float score = float.Parse(Console.ReadLine());
                    li.RecursiveSearchByScore(li.First, score);
                    break;
                case 4:
                    li.DisplayList();
                    break;
            }
        } while (option != 0);
    }
}
