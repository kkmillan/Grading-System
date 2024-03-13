using System;
using System.Collections.Generic;
using System.Reflection;

namespace Grading_System
{
    internal class Program
    {
        static int numberOfStudents;
        static string[]? studentLists;
        static double[]? scienceGrade;
        static double[]? mathGrade;
        static double[]? englishGrade;
   
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("  Grading System");
            Console.WriteLine("-------------------\n");

            Console.Write("Enter the total number of students: ");
            

            
            while (!int.TryParse(Console.ReadLine(), out numberOfStudents)) // Validate integer input
            {
                Console.Write(" Invalid input. Please enter a number: ");
            }

                
            string action;
            do
            {
                Console.WriteLine("\n---------------------------------------\n Welcome to the Student Grades System!\n What would you like to do?\n---------------------------------------");
                Console.WriteLine("[1] Enroll Students\n[2] Enter Student Grades\n[3] Show Student Grades\n[4] Show Top Student\n[5] Exit\n");

                action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.WriteLine("\nEnroll Students");

                        
                        // Check if there are enrolled students
                        if (studentLists != null)
                        {
                            int currentStudents = studentLists.Count(s => s != null);
                            Console.WriteLine($"currentStudents : {currentStudents}\nstudentLists.Length : {studentLists.Length}");

                            if (currentStudents >= numberOfStudents)
                            {
                                Console.WriteLine($"There are already {currentStudents} enrolled students. The slots are full");
                                break;
                            }
                            else
                            {
                                int slots_available = numberOfStudents - currentStudents;
                                Console.WriteLine($"There are {slots_available} slots available.");
                                EnrollStudents(currentStudents);
                            }

                        }else
                        {
                            //Console.WriteLine("No students have been enrolled yet.");
                            EnrollStudents(0);
                        }


                        Console.WriteLine("\n--------------------------");
                        Console.WriteLine("List of Enrolled Students:");
                        Console.WriteLine("-------------------------");
                        foreach (var student in studentLists)
                        {
                            Console.WriteLine(student);
                        }

                        Console.Write("\nPress any key to go back to the main menu. Press 5 to exit");
                        string response = Console.ReadLine();
                        if (response == "5")
                        {
                            action="5";
                        }
                        else
                        {
                            continue;
                        }

                        break;
                    case "2":
                        Console.WriteLine("Enter Student Grades");
                        EnterStudentGrades();
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("Show Student Grades");
                        ShowStudentGrades();
                        break;
                    case "4":
                        Console.WriteLine("Show Top Student");
                        ShowTopStudent();
                        break;
                    case "5":
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            } while (action != "5");

        }



        static void EnrollStudents(int index)
        {
            // Create a new array with the desired size
            string[] newStudentLists = new string[numberOfStudents];

            // Copy existing values from the old array to the new array to retain them
            if (studentLists != null)
            {
                for (int i = 0; i < Math.Min(studentLists.Length, numberOfStudents); i++)
                {
                    newStudentLists[i] = studentLists[i];
                }
            }

            // Replace the old array with the new one
            studentLists = newStudentLists;

            // Proceed with enrolling students as before
            for (int i = index; i < numberOfStudents; i++)
            {
                Console.Write($"Enter Student {i + 1} Name: ");
                string input = Console.ReadLine();

                if (input == "") // Validate input
                {
                    Console.WriteLine("You did not enter anything. Please enter a name.\n");
                    i--;
                    continue;
                }

                studentLists[i] = input;

                Console.WriteLine($"Student {i + 1} {studentLists[i]} has been enrolled.\n");
                int slots = numberOfStudents - i - 1;
                Console.Write($"There are {slots} slots left. Continue?(y/n) ");
                string response = Console.ReadLine();
                
                if (response.ToLower() == "n") // If the user enters "n", break the loop
                {
                    break;
                }
                else if (slots <= 0 && response.ToLower() == "y") // If there are no more slots available and the user enters "y", display a message and break the loop
                {
                    Console.WriteLine("No more slots available. Press any key to continue...");
                    Console.ReadLine();
                    break;
                }
            }
        }


        
        static void EnterStudentGrades()
        {
            if (studentLists != null)
            {

                string input;
                string header;

                do { 
                    int currentStudents = studentLists.Count(s => s != null);

                    // Create a new array with the desired size
                    double[] newScienceGrades = new double[numberOfStudents];
                    double[] newMathGrades = new double[numberOfStudents];
                    double[] newEnglishGrades = new double[numberOfStudents];

                    // Copy existing values from the old array to the new array to retain them
                    if (scienceGrade != null)
                    {
                        for (int i = 0; i < Math.Min(scienceGrade.Length, numberOfStudents); i++)
                        {
                            newScienceGrades[i] = scienceGrade[i];
                        }
                    }
                    // Copy existing values from the old array to the new array to retain them
                    if (mathGrade != null)
                    {
                        for (int i = 0; i < Math.Min(mathGrade.Length, numberOfStudents); i++)
                        {
                            newMathGrades[i] = mathGrade[i];
                        }
                    }
                    // Copy existing values from the old array to the new array to retain them
                    if (englishGrade != null)
                    {
                        for (int i = 0; i < Math.Min(englishGrade.Length, numberOfStudents); i++)
                        {
                            newEnglishGrades[i] = englishGrade[i];
                        }
                    }


                    // Replace the old array with the new one
                    scienceGrade = newScienceGrades;
                    mathGrade = newMathGrades;
                    englishGrade = newEnglishGrades;


                    Console.WriteLine("\n--------------------------");
                    Console.WriteLine("List of Enrolled Students:");
                    Console.WriteLine("--------------------------");
                    header = string.Format("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10}", " ", "Name", "Science", "Math", "English", "Average Grade");
                    Console.WriteLine(header);
                    for (int index = 0; index < studentLists.Length; index++)
                    {
                        double science = (scienceGrade[index] != null) ? scienceGrade[index] : 0;
                        double math = (mathGrade[index] != null) ? mathGrade[index] : 0;
                        double english = (englishGrade[index] != null) ? englishGrade[index] : 0;
                        double rawAverage = (science + math + english) / 3;
                        double average = Math.Round(rawAverage, 2);

                        // If the student name is null, break the loop to save space in formatting
                        if (studentLists[index] == null) 
                        {                         
                            break;
                        }

                        int dispIndex = index + 1;
                        Console.WriteLine($"{"["+ dispIndex + "]",-5} {studentLists[index],-25} {science,-10} {math,-10} {english,-10} {average+"%",-10}");
                    }

                    Console.Write($"\nSelect Student to Enter Grades (1-{currentStudents}): ");
                    int studentIndex;

                    //
                    while (true)
                    {
                        while (!int.TryParse(Console.ReadLine(), out studentIndex)) // Validate integer input
                        {
                            Console.Write(" Invalid input. Please enter only from within the range of the selection :  ");
                        }

                        if (studentIndex < 1 || studentIndex > studentLists.Length) // Validate input within the range of the selection
                        {
                            Console.Write(" Please enter only from within the range of the selection. ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    int actualIndex = studentIndex - 1;
                    Console.WriteLine($"\n--------------------\n {studentLists[actualIndex]}'s Grades\n--------------------");
                    string answer ;
                    string subject;
                    do {
                        Console.WriteLine("Enter subject to input grade\n[1] Science\n[2] Mathematics\n[3] English\n[4] Cancel");
                        subject = Console.ReadLine();

                        switch (subject)
                        {
                            case "1":
                                Console.Write("Enter Science Grade: ");
                                double inputScience;
                                while (!double.TryParse(Console.ReadLine(), out inputScience)) // Validate integer input
                                {
                                    Console.Write(" Invalid input. Please enter a number: ");
                                }
                                scienceGrade[actualIndex] = inputScience;
                                break;
                            case "2":
                                Console.Write("Enter Math Grade: ");
                                double inputMath;
                                while (!double.TryParse(Console.ReadLine(), out inputMath)) // Validate integer input
                                {
                                    Console.Write(" Invalid input. Please enter a number: ");
                                }
                                mathGrade[actualIndex] = inputMath;
                                break;
                            case "3":
                                Console.Write("Enter English Grade: ");
                                double inputEnglish;
                                while (!double.TryParse(Console.ReadLine(), out inputEnglish)) // Validate integer input
                                {
                                    Console.Write(" Invalid input. Please enter a number: ");
                                }
                                englishGrade[actualIndex] = inputEnglish;
                                break;
                            case "4":
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }

                        // Calculate the average grade
                        double rawAverage = ( scienceGrade[actualIndex] + mathGrade[actualIndex] + englishGrade[actualIndex] ) / 3;
                        double average = Math.Round(rawAverage, 2);
                        // Display the student's name and their grades
                        Console.WriteLine($"\n{header}");
                        Console.WriteLine($"{" ",-5} {studentLists[actualIndex],-25} {scienceGrade[actualIndex],-10} {mathGrade[actualIndex],-10} {englishGrade[actualIndex],-10} {average + "%",-10}\n");
                    
                        // Ask the user if they want to continue entering grades for the same student
                        Console.Write($"Do you want to continue inputting {studentLists[actualIndex]}'s grade(y/n): ");
                        answer = Console.ReadLine();

                    } while (answer.ToLower() == "y");

                    // Ask the user if they want to continue entering grades for other students
                    Console.Write("Do you want to continue entering grades for other students(y/n): ");
                    input = Console.ReadLine();

                } while (input.ToLower() == "y") ;


                Console.WriteLine("\n--------------------------");
                Console.WriteLine("List of Enrolled Students:");
                Console.WriteLine("--------------------------");
                header = string.Format("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10}", " ", "Name", "Science", "Math", "English", "Average Grade");
                Console.WriteLine(header);
                for (int index = 0; index < studentLists.Length; index++)
                {
                    double science = (scienceGrade[index] != null) ? scienceGrade[index] : 0;
                    double math = (mathGrade[index] != null) ? mathGrade[index] : 0;
                    double english = (englishGrade[index] != null) ? englishGrade[index] : 0;
                    double rawAverage = (science + math + english) / 3;
                    double average = Math.Round(rawAverage, 2);


                    if (studentLists[index] == null)
                    {
                        break;
                    }

                    int dispIndex = index + 1;
                    Console.WriteLine($"{"[" + dispIndex + "]",-5} {studentLists[index],-25} {science,-10} {math,-10} {english,-10} {average + "%",-10}");
                }


            }

            // If there are no students enrolled, display a message and return to the main menu
            else 
            {                 
                Console.WriteLine("No students have been enrolled yet. Press any key to return to main menu");
                Console.ReadLine();
            }
        }

       
        static void ShowStudentGrades()
        {
            if (studentLists != null)
            {
                int currentStudents = studentLists.Count(s => s != null); // Count the number of students enrolled


                // Create a new array with the desired size
                double[] newScienceGrades = new double[numberOfStudents];
                double[] newMathGrades = new double[numberOfStudents];
                double[] newEnglishGrades = new double[numberOfStudents];

                // Copy existing values from the old array to the new array to retain them
                if (scienceGrade != null)
                {
                    for (int i = 0; i < Math.Min(scienceGrade.Length, numberOfStudents); i++)
                    {
                        newScienceGrades[i] = scienceGrade[i];
                    }
                }
                // Copy existing values from the old array to the new array to retain them
                if (mathGrade != null)
                {
                    for (int i = 0; i < Math.Min(mathGrade.Length, numberOfStudents); i++)
                    {
                        newMathGrades[i] = mathGrade[i];
                    }
                }
                // Copy existing values from the old array to the new array to retain them
                if (englishGrade != null)
                {
                    for (int i = 0; i < Math.Min(englishGrade.Length, numberOfStudents); i++)
                    {
                        newEnglishGrades[i] = englishGrade[i];
                    }
                }


                // Replace the old array with the new one
                scienceGrade = newScienceGrades;
                mathGrade = newMathGrades;
                englishGrade = newEnglishGrades;


                Console.WriteLine("\n--------------------------");
                Console.WriteLine("List of Enrolled Students:");
                Console.WriteLine("--------------------------");
                string header = string.Format("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10}", " ", "Name", "Science", "Math", "English", "Average Grade");
                Console.WriteLine(header);
                
                for (int index = 0; index < studentLists.Length; index++) // Print the list of students and their grades
                {
                    double science = (scienceGrade[index] != null) ? scienceGrade[index] : 0;
                    double math = (mathGrade[index] != null) ? mathGrade[index] : 0;
                    double english = (englishGrade[index] != null) ? englishGrade[index] : 0;
                    double rawAverage = (science + math + english) / 3;
                    double average = Math.Round(rawAverage, 2);

                    if (studentLists[index] == null) // If the student name is null, break the loop to save space in formatting
                    {
                        break;
                    }

                    // Display the list of students and their grades
                    int dispIndex = index + 1;
                    Console.WriteLine($"{"[" + dispIndex + "]",-5} {studentLists[index],-25} {science,-10} {math,-10} {english,-10} {average + "%",-10}");
                }
                
                // Return to the main menu
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
            }

            else
            {
                // If there are no students enrolled, display a message and return to the main menu
                Console.WriteLine("No students have been enrolled yet. Press any key to return to main menu");
                Console.ReadLine();
            }
        }


        static void ShowTopStudent()
        {
               if (studentLists != null)
            {
                int currentStudents = studentLists.Count(s => s != null); // Count the number of students enrolled


                // Create a new array with the desired size
                double[] newScienceGrades = new double[numberOfStudents];
                double[] newMathGrades = new double[numberOfStudents];
                double[] newEnglishGrades = new double[numberOfStudents];

                // Copy existing values from the old array to the new array to retain them
                if (scienceGrade != null)
                {
                    for (int i = 0; i < Math.Min(scienceGrade.Length, numberOfStudents); i++)
                    {
                        newScienceGrades[i] = scienceGrade[i];
                    }
                }

                if (mathGrade != null)
                {
                    for (int i = 0; i < Math.Min(mathGrade.Length, numberOfStudents); i++)
                    {
                        newMathGrades[i] = mathGrade[i];
                    }
                }

                if (englishGrade != null)
                {
                    for (int i = 0; i < Math.Min(englishGrade.Length, numberOfStudents); i++)
                    {
                        newEnglishGrades[i] = englishGrade[i];
                    }
                }


                // Replace the old array with the new one
                scienceGrade = newScienceGrades;
                mathGrade = newMathGrades;
                englishGrade = newEnglishGrades;

                Console.WriteLine("\n-------------------");
                Console.WriteLine(" Top Student/s:");
                Console.WriteLine("-------------------");
                string header = string.Format("{0,-5} {1,-25} {2,-10} {3,-10} {4,-10} {5,-10}", " ", "Name", "Science", "Math", "English", "Average Grade");
                Console.WriteLine(header);

                // Create a Dictionary to store pairs of strings and integers/doubles
                Dictionary<string, List<double>> dictionary = new Dictionary<string, List<double>>();

                for (int index = 0; index < studentLists.Length; index++)
                {
                    // Fetch the grades from the arrays
                    double science = (scienceGrade[index] != null) ? scienceGrade[index] : 0;
                    double math = (mathGrade[index] != null) ? mathGrade[index] : 0;
                    double english = (englishGrade[index] != null) ? englishGrade[index] : 0;

                    double rawAverage = (science + math + english) / 3; // Calculate the average
                    double average = Math.Round(rawAverage, 2); // Round the average to 2 decimal places

                    // If the student name is null, break the loop in case the slots are not filled
                    if (studentLists[index] == null)
                    {
                        break;
                    }

                    // Add the grades to the dictionary  
                    dictionary[studentLists[index]] = new List<double> { (double)average, (double)science, (double)math, (double)english };
                   
                }


                // Sort the dictionary by values in descending order
                var keyValuePairs = dictionary.OrderByDescending(pair => pair.Value[0]);

                // Print the sorted list
                foreach (var pair in keyValuePairs)
                {
                    // Sort grades per rank
                    //Console.WriteLine($"{" ",-5} {pair.Key,-25} {pair.Value[1],-10} {pair.Value[2],-10} {pair.Value[3],-10} {pair.Value[0] + "%",-10}");

                    if (pair.Value[0] == keyValuePairs.First().Value[0])
                    {
                        Console.WriteLine($"{" ",-5} {pair.Key,-25} {pair.Value[1],-10} {pair.Value[2],-10} {pair.Value[3],-10} {pair.Value[0] + "%",-10}");
                    }
                }

                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine(); 

            }
               else
            {   
                // If there are no students enrolled, display a message and return to the main menu
                Console.WriteLine("No students have been enrolled yet. Press any key to return to main menu");
                Console.ReadLine();
            }
        }


    }
}
