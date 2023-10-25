using System;
using System.Collections.Generic;
using System.IO;

namespace StudentDataApp
{
   public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
    }

   public class Program
    {
       public static void Main()
        {
            List<Student> students = LoadStudentData("StudentData.txt");

            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
            }
            else
            {
                //display the student data
                Console.WriteLine("Student Data:");
                Console.WriteLine("Name\tClass");
                DisplayStudentData(students);

                //display the data after sorting the data
                Console.WriteLine("\nSorting Students Data By Name:");
                QuickSort(students, 0, students.Count - 1);
                DisplayStudentData(students);
                
                //display the student data after searching by name
                Console.WriteLine("\nSearch for a student by name:");
                string searchName = Console.ReadLine();
                int index = BinarySearch(students, searchName);

                if (index != -1)
                {
                    Console.WriteLine($"Student found: {students[index].Name}, Class: {students[index].Class}");
                }
                else
                {
                    Console.WriteLine($"No students found with the name '{searchName}'.");
                }
            }
        }

       public static List<Student> LoadStudentData(string filePath)
        {
            List<Student> students = new List<Student>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        students.Add(new Student
                        {
                            Name = parts[0].Trim(),
                            Class = parts[1].Trim()
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }

            return students;
        }
         //Displays the student data
       public static void DisplayStudentData(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($" {student.Name}\t{student.Class}");
            }
        }

        //Sorting the data by name
        public static void QuickSort(List<Student> students, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(students, low, high);

                QuickSort(students, low, pivotIndex - 1);
                QuickSort(students, pivotIndex + 1, high);
            }
        }

       public static int Partition(List<Student> students, int low, int high)
        {
            string pivot = students[high].Name;
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (string.Compare(students[j].Name, pivot) < 0)
                {
                    i++;
                    Swap(students, i, j);
                }
            }

            Swap(students, i + 1, high);
            return i + 1;
        }

        public static void Swap(List<Student> students, int index1, int index2)
        {
            Student temp = students[index1];
            students[index1] = students[index2];
            students[index2] = temp;
        }

        //Search data by name
        public static int BinarySearch(List<Student> students, string searchName)
        {
            int low = 0;
            int high = students.Count - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int comparison = string.Compare(students[mid].Name, searchName, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                {
                    return mid;
                }
                else if (comparison < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return -1; // Student not found
        }
    }
}
