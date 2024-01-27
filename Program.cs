using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LINQApplication
{
    class Program
    {
        private static bool isTeenAger(Student s)
        {
            return s.Age > 12 && s.Age < 20;
        }

        static void Main(string[] args)
        {
            IList<Student> studentList = new List<Student>()
            {
                new Student() { StudentID = 1, StudentName = "John", Age = 13, StandardID = 1 },
                new Student() { StudentID = 2, StudentName = "Steve", Age = 21, StandardID = 1 },
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18, StandardID = 2 },
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20, StandardID = 2 },
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 },
                new Student() { StudentID = 6, StudentName = "Ram", Age = 18 }
            };

            IList<Standard> standardList = new List<Standard>()
            {
                new Standard() { StandardID = 1, StandardName = "Standard 1" },
                new Standard() { StandardID = 2, StandardName = "Standard 2" },
                new Standard() { StandardID = 3, StandardName = "Standard 3" }
            };


            var lowerCaseStudentNames = from s in studentList
                                        where s.StudentName.ToLower().StartsWith("r")
                                        select s.StudentName.ToLower();

            foreach (var name in lowerCaseStudentNames)
                Console.WriteLine(name);
            Console.WriteLine();




            //  let     is used to introduce an new variable

            var lowerCaseStudentNames2 = from s in studentList
                                         let lowerCaseStudentName = s.StudentName.ToLower()
                                         where lowerCaseStudentName.StartsWith("r")
                                         select lowerCaseStudentName;

            foreach (var name in lowerCaseStudentNames2)
                Console.WriteLine(name);
            Console.WriteLine();

            
            
            
            //  let     is used to introduce an new variable

            var upperCaseStudentNames3 = from s in studentList
                                         let upperCaseStudentName = s.StudentName.ToUpper()
                                         where upperCaseStudentName.StartsWith("R")
                                         select upperCaseStudentName;

            foreach (var name in upperCaseStudentNames3)
                Console.WriteLine(name);
            Console.WriteLine();
            Console.ReadKey();
















            //  into        is used to form a group (by giving a name)
            //              or to continue a query after a select clause
            
            var teenAgeStudents = from s in studentList
                                  where s.Age > 12 && s.Age < 20
                                  select s
                                  into teenStudents
                                  where teenStudents.StudentName.StartsWith("B")
                                  select teenStudents;

            foreach (var teen in teenAgeStudents)
                Console.WriteLine(teen.StudentName);
            Console.WriteLine();


            //  Mulitple Select and Where Operator
            var studentNames = studentList
                                .Where(s => s.Age > 18)
                                .Select(s => s)
                                .Where(st => st.StandardID > 0)
                                .Select(s => s);


            foreach (var student in studentNames)
                Console.WriteLine(student.StudentName);
            Console.WriteLine();

            //  Mulitple Select and Where Operator
            var studentNames2 = from s in studentList
                                where s.Age > 18
                                select s
                                into ss
                                where ss.StandardID > 0
                                select ss;


            foreach (var student in studentNames2)
                Console.WriteLine(student.StudentName);
            Console.WriteLine();



            //  LINQ Query returns a Enumerable of anonymous object that has only StudentName property
            var teenStudentNames = from s in studentList
                                   where s.Age > 12 && s.Age < 20
                                   select new { SName = s.StudentName };

            foreach (var student in teenStudentNames)
                Console.WriteLine(student.SName);
            Console.WriteLine();

            teenStudentNames.ToList().ForEach(s => Console.WriteLine(s.SName));
            Console.WriteLine();








            //  LINQ STANDARD QUERY OPERATORS

            //  where       Returns values from the collection based on a predicate function.

            var filteredResults = from s in studentList
                                  where s.Age > 12 && s.Age < 20
                                  select s.StudentName;

            filteredResults.ToList().ForEach(s => Console.Write(s + " "));
            Console.WriteLine();


            //  Using Func<TSource, bool> with delegate Function
            Func<Student, bool> isTeen = delegate (Student s) {
                return s.Age > 12 && s.Age < 20;
            };

            var filteredResults2 = from s in studentList
                                   where isTeen(s)
                                   select s;

            filteredResults2.ToList().ForEach(s => Console.Write("Name 2 " + s.StudentName + " "));
            Console.WriteLine();


            //  Using member function
            var filteredResults3 = from s in studentList
                                   where isTeenAger(s)
                                   select s;

            filteredResults3.ToList().ForEach(s => Console.Write("Name 3 " + s.StudentName + " "));
            Console.WriteLine();


            var filteredResults4 = studentList.Where(s => s.Age > 12 && s.Age < 20);
            filteredResults4.ToList().ForEach(s => Console.Write("Name 4 :" + s.StudentName + " "));
            Console.WriteLine();


            var filteredResults5 = studentList.Where((s, i) => {
                if (i % 2 == 0) return true;
                return false;
            });

            filteredResults5.ToList().ForEach(s => Console.Write("Name 5 :" + s.StudentName + " "));
            Console.WriteLine();


            //  Mulitple Where Clauses
            var filtered = from s in studentList
                           where s.Age > 12
                           where s.Age < 20
                           select s;

            var filtered2 = studentList.Where(s => s.Age > 12).Where(s => s.Age < 20).Select(s => s);


            filtered.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine();
            filtered2.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine();








            //  OfType      Returns values from the collection based on a specified type.
            //              However, it will depend on their ability to cast to a specified type.

            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentName = "Bill", StudentID = 1 });

            var stringResult = from s in mixedList.OfType<string>()
                               select s;

            var intResult = from s in mixedList.OfType<int>()
                            select s;

            var studentResult = from s in mixedList.OfType<Student>()
                                select s;

            foreach (var s in stringResult)
                Console.Write(s + " ");
            Console.WriteLine();

            foreach (var s in intResult)
                Console.Write(s + " ");
            Console.WriteLine();

            intResult.ToList().ForEach(s => Console.Write(s + " "));
            Console.WriteLine();

            studentResult.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine("\n");






            //  OrderBy     sorts the values of a collection in ascending or descending order.
            //              It sorts the collection in ascending order by default
            //              because ascending keyword is optional here.

            var orderByResult = from s in studentList
                                orderby s.StudentName
                                select s;

            orderByResult.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine();


            //  Orderby In method Syntax

            var studentsInAscOrder = studentList.OrderBy(s => s.StudentName);
            studentsInAscOrder.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine();




            //  OrderByDescending   Use descending keyword to sort collection in descending order.
            //                      Only valid in method syntax

            var orderByDescendingResult = from s in studentList
                                          orderby s.StudentName descending
                                          select s;

            orderByDescendingResult.ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine();

            studentList.OrderByDescending(s => s.StudentName).ToList().ForEach(s => Console.Write(s.StudentName + " "));
            Console.WriteLine("\n");






            //  Multiple Sorting
            var orderByResult2 = from s in studentList
                                 orderby s.StudentName, s.Age
                                 select new { s.StudentName, s.Age };

            foreach (var s in orderByResult2)
                Console.Write(s.StudentName + " " + s.Age + " ");

            Console.WriteLine("\n");





            //  ThenBy              sorts the collection for another field after OrderBy method sorts
            //                      it first.  ReSorts the resulted collection after OrderBy sorts it.

            //  ThenByDescending    Seconday sorting after OrderByDescending

            var thenByResult = studentList.OrderBy(s => s.StudentName).ThenBy(s => s.Age);
            var thenByDscResult = studentList
                                    .OrderByDescending(s => s.StudentName)
                                    .ThenByDescending(s => s.Age);

            foreach (var s in thenByResult)
                Console.Write(s.StudentName + " " + s.Age + "  ");
            Console.WriteLine();

            foreach (var s in thenByDscResult)
                Console.Write(s.StudentName + " " + s.Age + "  ");
            Console.WriteLine("\n");







            IList<Student> studentListA = new List<Student>()
            {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };

            //  GroupBy     The grouping operators create a group of elements based on the given key.
            //              The GroupBy operator returns groups of elements based on some key value.
            //              Each group is represented by IGrouping < TKey, TElement > object.
            //              A LINQ query can only end with a GroupBy or Select clause.


            //  groups the data based on Age property
            var groupedResult = from s in studentListA
                                group s by s.Age;

            //  Iterate each age group
            foreach (var ageGroup in groupedResult)
            {
                //  Prints the age group of the students
                //  Each group has a key
                Console.WriteLine("Age Group " + ageGroup.Key);

                //  Prints all the students in the same age group mentioned above
                //  Each group has inner collection
                foreach (Student s in ageGroup)
                    Console.Write(s.StudentName + "  ");

                Console.WriteLine();
            }

            Console.WriteLine();




            //  GroupBy     Method Version

            //  groups the data based on Age property
            var groupedResult2 = studentListA.GroupBy(s => s.Age);

            //  Iterate each age group
            foreach (var ageGroup in groupedResult2)
            {
                //  Prints the age group of the students
                //  Each group has a key
                Console.WriteLine("Age Group " + ageGroup.Key);

                //  Prints all the students in the same age group mentioned above
                //  Each group has inner collection
                foreach (Student s in ageGroup)
                    Console.Write(s.StudentName + "  ");

                Console.WriteLine();
            }

            Console.WriteLine("\n");







            //  ToLookup    ToLookup is the same as GroupBy; the only difference is the execution of
            //              GroupBy is deferred whereas ToLookup execution is immediate.

            //  GroupBy     Method Version
            
            //  groups the data based on Age property
            var lookUpResult = studentListA.ToLookup(s => s.Age);

            //  Iterate each age group
            foreach (var group in lookUpResult)
            {
                //  Each group has a key
                Console.WriteLine("Age Group " + group.Key);

                //  Each group has an inner collection
                foreach (Student s in group)
                    Console.Write(s.StudentName + "  ");

                Console.WriteLine();
            }

            Console.WriteLine("\n");







            IList<string> strList1 = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four"
            };

            IList<string> strList2 = new List<string>()
            {
                "One",
                "Two",
                "Five",
                "Six"
            };


            //  Join    The Join operator joins two sequences (collections) based on a key and
            //          returns a resulted sequence.
            //          The Join operator operates on two collections, ab inner collection &
            //          an outer collection.  It returns a new collection that contains elements
            //          from both the collections which satisfies specified expression.
            //          It is the same as inner join of SQL.

            var inJoin = strList1.Join(strList2,
                       str1 => str1,
                       str2 => str2,
                       (str1, str2) => str1);

            inJoin.ToList<string>().ForEach(s => Console.WriteLine(s));
            Console.WriteLine();


            var innerJoinResult = studentList.Join(
                standardList,
                student => student.StandardID,
                standard => standard.StandardID,
                (student, standard) => new
                {
                    StudentFullName = student.StudentName,
                    StandardFullName = standard.StandardName
                });


            foreach (var obj in innerJoinResult)
                Console.WriteLine("Student Name " + obj.StudentFullName + "\tStandard Name " + obj.StandardFullName);
            Console.WriteLine();





            //  Join in query syntax

            /*
            from... in outerSequence

            join ... in innerSequence

            on outerKey equals innerKey

            select ...
            */

            var inerJoin = from s in studentList
                           join std in standardList
                           on s.StandardID equals std.StandardID
                           select new
                           {
                               StudentFullName = s.StudentName,
                               StandardFullName = std.StandardID
                           };

            foreach (var obj in inerJoin)
                Console.WriteLine("Student Name " + obj.StudentFullName + "\tStandard Name " + obj.StandardFullName);
            Console.WriteLine("\n");









            //  GroupJoin       The GroupJoin operator joins two sequences based on keys and
            //                  returns groups of sequences. It is like Left Outer Join of SQL.

            /*
            from... in outerSequence

            join ... in innerSequence

            on outerKey equals innerKey

            into groupedCollection

            select ...
            */


            var gJoin = standardList.GroupJoin(
                                    studentList,
                                    std => std.StandardID,
                                    s => s.StandardID,
                                    (std, studentsGroup) => new
                                    {
                                        Students = studentsGroup,
                                        StandardFullName = std.StandardName
                                    });

            foreach (var item in gJoin)
            {
                Console.WriteLine(item.StandardFullName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }

            Console.WriteLine();





            //  Using LINQ Syntax

            var grJoin = from std in standardList
                         join s in studentList
                         on std.StandardID equals s.StandardID
                         into studentGroup
                         select new
                         {
                             Students = studentGroup,
                             StandardFullName = std.StandardName
                         };


            foreach (var item in grJoin)
            {
                Console.WriteLine(item.StandardFullName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }

            Console.WriteLine();








            //  Select      It can be used to return a collection of custom class or anonymous type
            //              which includes properties as per our need.

            //  The following example of the select clause returns a collection of anonymous type
            //  containing the Name and Age property.

            var selectResult1 = from s in studentList
                                select new
                                {
                                    Name = s.StudentName,
                                    Age = s.Age
                                };

            foreach (var item in selectResult1)
                Console.WriteLine("Name is {0} and Age is {1}", item.Name, item.Age);
            
            Console.WriteLine();




            //  Using Method Syntax
            var selectResult2 = studentList.Select(s => new { Name = s.StudentName, Age = s.Age });

            foreach (var item in selectResult2)
                Console.WriteLine("Name is {0} and Age is {1}", item.Name, item.Age);

            Console.WriteLine();



            //  or use Fluent LINQ till the end
            studentList
                .Select(s => new { Name = s.StudentName, Age = s.Age })
                .ToList()
                .ForEach(s => Console.WriteLine("Name is" + s.Name + ", Age is " + s.Age));

            Console.WriteLine("\n");







            //  SelectMany      Projects sequences of values that are based on a
            //                  transform function and then flattens them into one sequence.

            List<string> phrases = new List<string>() { "an apple a day", "the quick brown fox" };

            var query = from phrase in phrases
                        from word in phrase.Split(' ')
                        select word;

            foreach (string s in query)
                Console.WriteLine(s);
            Console.WriteLine();





            //  Quantifier Operators (All, Any, Contains)
            //  The quantifier operators evaluate elements of the sequence on some condition and
            //  return a boolean value to indicate that some or all elements satisfy the condition.



            //  All     Checks if all the elements in a sequence satisfies the specified
            //          condition and returns True if all the elements satisfy a condition.

            //  Checks if all the students are teenagers
            bool areAllStudentsTeenAger = studentList.All(s => s.Age > 12 && s.Age < 20);
            Console.WriteLine("All the students are teenager  " + areAllStudentsTeenAger);






            //  Any     Checks if any of the elements in a sequence satisfies the 
            //          specified condition and returns True if does


            //  Checks if any of the students is a teenager
            bool isAnyStudentsTeenAger = studentList.Any(s => s.Age > 12 && s.Age < 20);
            Console.WriteLine("Is any of the students teenager  " + isAnyStudentsTeenAger);






            //  Contains    Checks whether a specified element exists in the collection
            //              or not and returns a boolean accordingly.

            IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };

            bool result = intList.Contains(10);  // returns false
            Console.WriteLine("intList Contains 10 ?  " + result);


            //  Add a student to the list to check it is there
            Student bill = new Student(3, "Bill", 18, 2);

            //  returns false.  Contains() method cannot match User Defined Types
            //  because it only compares the reference of an object not the actual
            //  value of the object
            Console.WriteLine(studentListA.Contains(bill));
            

            //  To compare User-Define Types, need to create a class by implementing the
            //  IEqualityComparer interface, that then compares the values of the two
            //  Students objects and returns boolean
            //  returns true
            Console.WriteLine(studentListA.Contains(bill, new StudentComparer()));


            //  Using Student class itself with IEqualityComparer interface
            //  returns true
            Console.WriteLine(studentListA.Contains(bill, new Student()));


            //  Using Studen class, but changing the StudentID of Bill from 3 to 2
            //  returns false
            Student newBill = new Student(2, "Bill", 18, 2);
            Console.WriteLine(studentListA.Contains(newBill, new Student()));

            Console.WriteLine(studentListA.Contains(new Student(3, "Bill", 18, 2), new Student()));







            //  Aggregation Operators (Aggregate, Average, Count, LongCount, Max, Min, Sum)
            //  Aggregate Performs a custom aggregation operation on the values in the collection.
            //  Average calculates the average of the numeric items in the collection.
            //  Count Counts the elements in a collection.
            //  LongCount Counts the elements in a collection.
            //  Max Finds the largest value in the collection.
            //  Min Finds the smallest value in the collection.
            //  Sum Calculates sum of the values in the collection.



            //  Aggregate Method
            var commaSeperatedList = strList1.Aggregate((s1, s2) => s1 + " , " + s2);
            Console.WriteLine(commaSeperatedList);



            //  Aggregate Method with a seed value ("Student Names are " is the seed value)
            //  This seed value will be acumulated with each student name
            var commaSeperatedStudentNames = studentList
                  .Aggregate<Student, string>("Student Names are  ", (str, s) => str += s.StudentName + " , ");
            Console.WriteLine(commaSeperatedStudentNames);



            //  Aggregate Method with a seed value (" 0 " is the seed value)
            //  This seed value will be acumulated with each student agee
            var addedStudentAges = studentList
                  .Aggregate<Student, int>(0, (totalAge, s) => totalAge += s.Age);
            Console.WriteLine("Total age of all students is " + addedStudentAges);



            //  Aggregate Method with a seed value and a third parameter of Func delegate
            //  The result selector removes the last comma (str => str.Substring(0, str.Length - 1)
            var commaSeperatedStudentNames2 = studentList
                  .Aggregate<Student, string, string>(String.Empty,
                  (str, s) => str += s.StudentName + " , ",   //  return result using seed value
                  str => str.Substring(0, str.Length - 2));   //  result selector removes last comma
            Console.WriteLine("Student names are  " + commaSeperatedStudentNames2);
            Console.WriteLine();










            //  Average     Average extension method calculates the average of the numeric items
            //              in the collection.  Average method returns nullable or non-nullable
            //              decimal, double or float value.
            //              The Average operator in query syntax is Not Supported in C#.


            IList<int> nums = new List<int>() { 10, 20, 30, 40, 50 };

            var avg = nums.Average();
            Console.WriteLine("Average of the nums is  " + avg);



            var avgAge = studentList.Average(s => s.Age);
            Console.WriteLine("Average of the students' ages  " + avgAge);
            Console.WriteLine();











            //  Count   The Count operator returns the number of elements in the collection or
            //          number of elements that have satisfied the given condition.

            IList<int> intNumbers = new List<int>() { 10, 21, 30, 45, 50 };
            var totalNumbers = intNumbers.Count();
            Console.WriteLine("Count of numbers in the intNumbers is " + totalNumbers);


            var moreThan20 = intNumbers.Count(s => s > 20);
            Console.WriteLine("Count of numbers which are greater than 20 in the intNumbers is " + moreThan20);


            var evenNumbers = intNumbers.Count(s => s % 2 == 0);
            Console.WriteLine("Number of even numbers in the inyNumbers is " + evenNumbers);


            var studentCount = studentList.Count();
            Console.WriteLine("Number of students in the studentList is " + studentCount);


            var adultStudents = studentList.Count(s => s.Age >= 18);
            Console.WriteLine("Number of adult students in the studentList is " + adultStudents);


            var youngStudents = studentList.Count(s => s.Age < 18);
            Console.WriteLine("Number of young students in the studentList is " + youngStudents);



            //  C# Query Syntax doesn't support aggregation operators.  However, you can wrap
            //  the query into brackets and use an aggregation functions as shown below.

            var ageCount = (from s in studentList
                            select s.Age).Count();

            Console.WriteLine("Total students in the studentList is " + ageCount);


            var ageCount2 = (from s in studentList
                             where s.Age < 18
                             select s.Age).Count();

            Console.WriteLine("Total students under 18 in the studentList is " + ageCount2);
            Console.WriteLine();








            //  Max     Returns the largest numeric element from a collection.

            IList<int> intMaxMinList = new List<int>() { 10, 21, 30, 45, 50, 87 };
            IList<int> randomList = new List<int>() { 30, 45, 10, 87, 50, 21 };

            var max = intMaxMinList.Max();
            Console.WriteLine("Max of the List is  " + max);


            var maxEvenNumber = intMaxMinList.Max(i => {
                                                            if (i % 2 == 0)
                                                                return i;
                                                            return 0;
                                                        });

            Console.WriteLine("Max of the even numbers in the List is  " + maxEvenNumber);


            var maxEvenNumber2 = (from n in randomList
                                 where n % 2 == 0
                                 select n).Max();

            Console.WriteLine("Maximum of the even numbers in the Random List is  " + maxEvenNumber2);


            var maxOddNumber = intMaxMinList.Max(i => {
                                                        if (i % 2 == 1)
                                                            return i;
                                                        return 0;
                                                  });

            Console.WriteLine("Max of the odd numbers in the List is  " + maxOddNumber);


            var maxOddNumber2 = (from n in randomList
                                where n % 2 == 1
                                select n).Max();

            Console.WriteLine("Maximum of the odd numbers in the Random List is  " + maxOddNumber2);


            var maxAge = studentList.Max(s => s.Age);
            Console.WriteLine("Oldest Student Age is  " + maxAge);


            //  Uses First Interface (CompareTo() method)
            var studentWithLongestName = studentList.Max();
            Console.WriteLine("Student ID {0} and Longest Student Name is {1}",
                studentWithLongestName.StudentID, studentWithLongestName.StudentName);
            Console.WriteLine();









            //  Min     Returns the smallest numeric element from a collection.

            IList<int> intMinMaxList = new List<int>() { 87, 50, 45, 30, 21, 10 };

            var min = intMaxMinList.Min();
            Console.WriteLine("Min of the List is  " + min);


            var minEvenNumber = intMaxMinList.Min(i => {
                if (i % 2 == 0)
                    return i;
                return 100;
            });

            Console.WriteLine("Min of the even numbers in the List is  " + minEvenNumber);


            var minEvenNumber3 = intMinMaxList.Min(i => {
                if (i % 2 == 0)
                    return i;
                return 100;
            });

            //  True!  Because if the number is not even, returns a very large value
            Console.WriteLine("Min of the even numbers in the Reversed sorted List is  " + minEvenNumber3);


            var minEvenNumber2 = (from n in randomList
                                  where n % 2 == 0
                                  select n).Min();

            Console.WriteLine("Minimum of the even numbers in the Random List is  " + minEvenNumber2);


            var minOddNumber = intMaxMinList.Min(i => {
                if (i % 2 == 1)
                    return i;
                return 1000;
            });

            Console.WriteLine("Min of the odd numbers in the List is  " + minOddNumber);


            var minOddNumber2 = (from n in randomList
                                 where n % 2 == 1
                                 select n).Min();

            Console.WriteLine("Minimum of the odd numbers in the Random List is  " + minOddNumber2);


            var minAge = studentList.Min(s => s.Age);
            Console.WriteLine("Youngest Student Age is  " + minAge);


            /*
            //  Uses First Interface (CompareTo() method)
            var studentWithShortestName = studentList.Min();
            Console.WriteLine("Student ID {0} and Shortest Student Name is {1}",
                studentWithShortestName.StudentID, studentWithShortestName.StudentName);
            */
            Console.WriteLine();








            //  Sum     calculates the sum of numeric items in the collection.
            
            var total = intMaxMinList.Sum();
            Console.WriteLine("Sum of the list is " + total);

            var sumOfEvens = intMaxMinList.Sum(i =>
            {
                if (i % 2 == 0)
                    return i;
                return 0;
            });

            Console.WriteLine("Sum of the even numbers in the list is " + sumOfEvens);


            var sumOfOdds = intMaxMinList.Sum(i =>
            {
                if (i % 2 == 1)
                    return i;
                return 0;
            });

            Console.WriteLine("Sum of the odd numbers in the list is " + sumOfOdds);
            Console.WriteLine("Sum of even and odd numbers is " + (sumOfEvens + sumOfOdds));


            Console.WriteLine();
            foreach (Student student in studentList)
                Console.WriteLine("ID " + student.StudentID + ",  Name " + student.StudentName + ",  Age " + student.Age);
            Console.WriteLine();


            var sumOfSomeAges = (from s in studentList
                                 where s.StudentID > 3
                                 select s).Sum(s => s.Age);

            Console.WriteLine("Sum of the ages in the studentList is " + sumOfSomeAges);


            var sumOfStudentAges = studentList.Sum(s => s.Age);
            Console.WriteLine("Sum of the ages in the studentList is " + sumOfStudentAges);


            var sumOfAdults2 = (from s in studentList
                                where s.Age >= 18
                                select s).Sum(s => s.Age);

            Console.WriteLine("Sum of the adults in the studentList is " + sumOfAdults2);


            var sumOfAdults3 = studentList.Sum(s =>
            {
                if (s.Age >= 18)
                    return s.Age;
                return 0;
            });

            Console.WriteLine("Sum of the adults in the studentList is " + sumOfAdults3);


            var numberOfAdults = studentList.Sum(s =>
            {
                if (s.Age >= 18)
                    return 1;
                return 0;
            });

            Console.WriteLine("Sum of the adults in the studentList is " + numberOfAdults);
            Console.WriteLine();










            //  Element Operators (ElementAt, ElementAtOrDefault, First, FirstOrDefault,
            //                      Last, LastOrDefault, Single, SingleOrDefault)
            //  Return a particular element from a sequence (collection).




            //  ElementAt   returns an element from the specified index from a given collection.
            //              If the specified index is out of the range of a collection then
            //              it will throw an Index out of range exception.

            //  ElementAtOrDefault  returns an element from the specified index from a
            //                      collaction and if the specified index is out of range
            //                      of a collection then it will return a default value of
            //                      the data type instead of throwing an error.

            IList<int> intElmList = new List<int>() { 10, 21, 30, 45, 50, 87 };
            IList<string> strElmList = new List<string>() { "One", "Two", null, "Four", "Five" };

            Console.WriteLine("1st Element in intElmList: {0}", intElmList.ElementAt(0));
            Console.WriteLine("1st Element in strElmList: {0}", strElmList.ElementAt(0));

            Console.WriteLine("2nd Element in intElmList: {0}", intElmList.ElementAt(1));
            Console.WriteLine("2nd Element in strElmList: {0}", strElmList.ElementAt(1));

            Console.WriteLine("3rd Element in intElmList: {0}", intElmList.ElementAtOrDefault(2));
            Console.WriteLine("3rd Element in strElmList: {0}", strElmList.ElementAtOrDefault(2));

            Console.WriteLine("10th Element in intElmList: {0} - default int value (0)",
                            intElmList.ElementAtOrDefault(9));
            Console.WriteLine("10th Element in strElmList: {0} - default string value (null)",
                             strElmList.ElementAtOrDefault(9));


            Console.WriteLine("intElmList.ElementAt(9) throws an exception: Index/Argument out of range");
            Console.WriteLine("------------------------------------------------------------------------");
            try
            {
                Console.WriteLine(intElmList.ElementAt(9));
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine("Index ouf of range Exception \n" + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Argument out of range Exception \n" + e.Message);
            }

            //  Thus, it is advisable to use the ElementAtOrDefault extension method
            //  to eliminate the possibility of a runtime exception.  Otherwise need
            //  to use a try - catch block.
            Console.WriteLine();










            //  First           Returns the first element of a collection, or the first
            //                  elelment that satisfies a condition.

            IList<int> intList3 = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            IList<string> strList3 = new List<string>() { null, "Two", "Three", "Four", "Five" };
            IList<string> emptyList = new List<string>();

            Console.WriteLine("1st Element in the intList3  {0}", intList3.First());
            Console.WriteLine("1st Even Element in the intList3  {0}", 
                intList3.First(s => s % 2 == 0));

            Console.WriteLine("1st Element in the strList3  {0}", strList3.First());

            Console.WriteLine("emptyList.First() throws an InvalidOperationException");
            Console.WriteLine("-----------------------------------------------------");
            try
            {
                Console.WriteLine(emptyList.First());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid Operation Exception \n" + e.Message);
            }

            try
            {
                Console.WriteLine("1st Element which is greater than 250 is  {0}", intList3.First());
                Console.WriteLine("1st Element in the intList3 is  {0}", intList3.First(s => s > 250));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception \n" + e.Message);
            }

            Console.WriteLine();









            //  FirstOrDefault    Returns the first element of a collection, or the first
            //                    element that satisfies a condition.  Returns a defauly
            //                    value if index is out of range.

            Console.WriteLine("1st Element in the intList3  {0}", intList3.FirstOrDefault());
            Console.WriteLine("1st Even Element in the intList3  {0}",
                intList3.FirstOrDefault(s => s % 2 == 0));

            Console.WriteLine("1st Element in the strList3  {0}", strList3.FirstOrDefault());
            Console.WriteLine("First Element in the empty List  {0}", emptyList.FirstOrDefault());

            Console.WriteLine("1st Element which is greater than 250 is  {0}", intList3.FirstOrDefault(s => s > 250));


            //  If a collection includes null element then FirstOrDefault() throws an
            //  exception while evaluting the specified condition.
            /*
            Console.WriteLine("1st Element in the intList3 is  {0}",
                                        strList3.FirstOrDefault(s => s.Contains("T")));
            */

            Console.WriteLine("emptyList.First() throws an InvalidOperationException");
            Console.WriteLine("----------------------------------------------------");
            try
            {
                Console.WriteLine(emptyList.First());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid Operation Exception \n" + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception \n" + e.Message);
            }

            Console.WriteLine();











            //  Last()      Returns the last element from a collection, or the last
            //              element that satisfies a condition using lambda expression
            //              or Func delegate.  If a given collection is empty or does not
            //              include any element that satisfied the condition then, throws
            //              InvalidOperationException if no element found.

            Console.WriteLine("Last Element in intList3: {0}", intList3.Last());

            Console.WriteLine("Last Even Element in intList3: {0}", intList3.Last(i => i % 2 == 0));

            Console.WriteLine("Last Element in strList3: {0}", strList3.Last());

            Console.WriteLine("emptyList.Last() throws an InvalidOperationException");
            Console.WriteLine("----------------------------------------------------");
            try
            {
                Console.WriteLine(emptyList.First());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Invalid Operation Exception \n" + e.Message);
            }
            Console.WriteLine();











            //  LastOrDefault()     Returns the last element from a collection, or the last
            //                      element that satisfies a condition. Returns a default
            //                      value if no element found.
            //                      If a collection includes null element then the
            //                      LastOrDefault() throws an exception while evaluting the
            //                      specified condition.
            IList<string> strList4 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            IList<string> strList5 = new List<string>() { null, "Two", "Three" };

            Console.WriteLine("Last Element in intList3: {0}", intList3.LastOrDefault());

            Console.WriteLine("Last Even Element in intList3: {0}",
                                             intList3.LastOrDefault(i => i % 2 == 0));

            Console.WriteLine("Last Element in strList3: {0}", strList3.LastOrDefault());

            Console.WriteLine("Last Element in emptyList: {0}", emptyList.LastOrDefault());

            Console.WriteLine("Last containing T in strList4  " + strList4.LastOrDefault(s => s.Contains("T")));

            try
            {
                Console.WriteLine(strList5.LastOrDefault(s => s.Contains("T")));
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine("Null Reference Exception " + e.Message);
                Console.WriteLine("------------------------------------------------------------------------------");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception is " + e.Message);
            }
            Console.WriteLine();









            //  Single              Returns the only element from a collection, or the only
            //                      element that satisfies a condition.  If Single() found no
            //                      elements or more than one elements in the collection then
            //                      throws InvalidOperationException.
            IList<int> oneElement = new List<int>() { 12 };

            Console.WriteLine("The only element is " + oneElement.Single());
            Console.WriteLine("The only element is " + oneElement.SingleOrDefault());

            Console.WriteLine("Element in empty list " + emptyList.SingleOrDefault());
            Console.WriteLine("The only element which is less than 10 is " + intList3.Single(s => s < 10));

            Console.WriteLine();

            //  SingleOrDefault     The same as Single, except that it returns a default
            //                      value of a specified generic type, instead of throwing an
            //                      exception if no element found for the specified condition.
            //                      However, it will throw InvalidOperationException if it
            //                      found more than one element for the specified condition
            //                      in the collection.

            try
            {
                //  following throws error because list contains more than one element which is less than 100
                Console.WriteLine("Element less than 100 in intList: {0}", intList3.Single(i => i < 100));

                //  following throws error because list contains more than one element which is less than 100
                Console.WriteLine("Element less than 100 in intList: {0}",
                                                    intList3.SingleOrDefault(i => i < 100));

                //  following throws error because list contains more than one elements
                Console.WriteLine("The only Element in intList: {0}", intList3.Single());

                //  following throws error because list contains more than one elements
                Console.WriteLine("The only Element in intList: {0}", intList3.SingleOrDefault());

                //  following throws error because list does not contains any element
                Console.WriteLine("The only Element in emptyList: {0}", emptyList.Single());
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR  " + e.Message);
            }
            Console.WriteLine();











            //  SequenceEqual       There is only one equality operator: SequenceEqual.
            //                      The SequenceEqual method checks whether the number of
            //                      elements, value of each element and order of elements
            //                      in two collections are equal or not.

            //                      If the collection contains elements of primitive data
            //                      types then it compares the values and number of elements,
            //                      whereas collection with complex type elements, checks
            //                      the references of the objects. So, if the objects have
            //                      the same reference then they are considered as equal
            //                      otherwise they are considered not equal.

            IList<string> strList6 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            IList<string> strList7 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            IList<string> strList8 = new List<string>() { "One", "Three", "Two", "Four", "Three" };

            //  It returns false
            bool isEqual = strList6 == strList7;
            Console.WriteLine("Both Lists are equal  " + isEqual);


            //  It returns true
            bool isEqual2 = strList6.SequenceEqual(strList7);
            Console.WriteLine("Both Lists are equal  " + isEqual2);


            //  It returns false
            bool isEqual3 = strList6.SequenceEqual(strList8);
            Console.WriteLine("Both Lists are equal  " + isEqual3);
            

            //  returns false
            Student tom = new Student() { StudentID = 6, Age = 12, StandardID = 3, StudentName = "Tom" };
            Student tim = new Student() { StudentID = 6, Age = 12, StandardID = 3, StudentName = "Tom" };
            Student mike = new Student() { StudentID = 8, Age = 28, StandardID = 2, StudentName = "Mike" };
            bool isEqual4 = tom.Equals(tim);
            Console.WriteLine("Both students are equal " + isEqual4);


            //  returns false
            IList<Student> stdList1 = new List<Student>() { tom };
            IList<Student> stdList2 = new List<Student>() { tim };
            IList<Student> stdList3 = new List<Student>() { mike };

            bool isEqual5 = stdList1.SequenceEqual(stdList2);
            Console.WriteLine("Both lists are equal " + isEqual5);


            //  retursns true
            IList<Student> stdList4 = new List<Student>() { tom };
            bool isEqual6 = stdList1.SequenceEqual(stdList4);
            Console.WriteLine("Both lists are equal " + isEqual6);


            //  returns true - using IEqualityComparer in the Stduent Class (Third Interface)
            bool isEqual7 = stdList1.SequenceEqual(stdList2, new StudentComparer());
            Console.WriteLine("Both lists are equal " + isEqual7);
            Console.WriteLine();












            //  Concatenation Operator (Concat)
            //  Concat      appends two sequences of the same type and returns a new sequence
            //              (collection).

            IList<string> collection1 = new List<string>() { "One", "Two", "Three" };
            IList<string> collection2 = new List<string>() { "Four", "Five", "Six", "Seven" };

            var collection3 = collection1.Concat(collection2);

            foreach (string item in collection3)
                Console.Write(item + "  ");
            Console.WriteLine();



            //  Combining collections
            IList<int> collection4 = new List<int>() { 1, 2, 3 };
            IList<int> collection5 = new List<int>() { 4, 5, 6 };

            var collection6 = collection4.Concat(collection5);
            foreach (int item in collection6)
                Console.Write(item + "  ");
            Console.WriteLine();


            //  Combining Student Objects
            var stdList5 = stdList1.Concat(stdList2);
            stdList5 = stdList5.Concat(stdList3);

            foreach (var item in stdList5)
                Console.WriteLine("Student Name " + item.StudentName + "\tStudent ID " + item.StudentID);
            Console.WriteLine();















            //  Generation Operator (DefaultIfEmpty, Empty, Range, Repeat)

            //  DefaultIfEmpty  returns a new collection with the default value if the given
            //                  collection on which DefaultIfEmpty() is invoked is empty.

            //                  Another overload method of DefaultIfEmpty() takes a value
            //                  parameter that should be replaced with default value.

            IList<int> emptyIntList = new List<int>();
            var newList1 = emptyIntList.DefaultIfEmpty();
            var newList2 = emptyIntList.DefaultIfEmpty(100);

            Console.WriteLine("Count: {0}", newList1.Count());
            Console.WriteLine("Value: {0}", newList1.ElementAt(0));

            Console.WriteLine("Count: {0}", newList2.Count());
            Console.WriteLine("Value: {0}", newList2.ElementAt(0));
            Console.WriteLine();


            //  For Complex Type Collections
            IList<Student> emptyStudentList = new List<Student>();

            var newStudentList1 = studentList.DefaultIfEmpty(new Student());

            var newStudentList2 = studentList.DefaultIfEmpty(new Student()
            {
                StudentID = 0,
                StudentName = ""
            });

            Console.WriteLine("Count: {0} ", newStudentList1.Count());
            Console.WriteLine("Student ID: {0} ", newStudentList1.ElementAt(0));

            Console.WriteLine("Count: {0} ", newStudentList2.Count());
            Console.WriteLine("Student ID: {0} ", newStudentList2.ElementAt(0).StudentID);
            Console.WriteLine();












            //  Empty       Returns an empty collection
            //              is not an extension method of IEnumerable or IQueryable like
            //              other LINQ methods.  It is a static method included in Enumerable
            //              static class.  So, you can call it the same way as other static
            //              methods like Enumerable.Empty<TResult>().  The Empty() method
            //              returns an empty collection of a specified type as shown below.

            var emptyCollection1 = Enumerable.Empty<string>();
            var emptyCollection2 = Enumerable.Empty<Student>();

            Console.WriteLine("Count: {0} ", emptyCollection1.Count());
            Console.WriteLine("Type: {0} ", emptyCollection1.GetType().Name);

            Console.WriteLine("Count: {0} ", emptyCollection2.Count());
            Console.WriteLine("Type: {0} ", emptyCollection2.GetType().Name);
            Console.WriteLine();











            //  Range   Generates collection of IEnumerable<T> type with specified number
            //          of elements with sequential values, starting from first element.

            //          returns a collection of IEnumerable<T> type with specified number
            //          of elements and sequential values starting from the first element.

            //  Crrates a list of 10 elements whose values starting from 10 and ending at 19
            var intCollection = Enumerable.Range(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection.Count());

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));
            Console.WriteLine();
















            //  Repeat      Generates a collection of IEnumerable< T > type with specified
            //              number of elements and each element contains same specified
            //              value.

            //              generates a collection of IEnumerable<T> type with specified
            //              number of elements and each element contains same specified
            //              value.


            //  Creates a list of 10 elements with the value of 10
            intCollection = Enumerable.Repeat<int>(10, 10);
            Console.WriteLine("Total Count: {0} ", intCollection.Count());

            for (int i = 0; i < intCollection.Count(); i++)
                Console.WriteLine("Value at index {0} : {1}", i, intCollection.ElementAt(i));

            Console.WriteLine();












            //  Set Operators (Distinct, Except, Intercept, Union)

            IList<string> strSetColl1 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            IList<string> strSetColl2 = new List<string>() { "Five", "Six", "One", "Three", "Six" };
            IList<int> intSetColl1 = new List<int>() { 1, 2, 3, 2, 4, 4, 3, 5 };
            IList<int> intSetColl2 = new List<int>() { 1, 4, 6, 6, 4, 7, 3, 9 };


            //  Distinct        Returns distinct values from a collection.  Returns a new
            //                  collection of unique elements from the given collection.

            var distinctCollection1 = strSetColl1.Distinct();
            var distinctCollection2 = intSetColl1.Distinct();

            Console.WriteLine("Distict String and int Values");
            foreach (string s in distinctCollection1)
                Console.Write(s + " ");     //  returns One, Two, Three, Four
            Console.WriteLine();
            foreach (int i in distinctCollection2)
                Console.Write(i + " ");     //  returns 1, 2, 3, 4, 5
            Console.WriteLine("\n");










            //  The Distinct extension method doesn't compare values of complex type objects.
            //  You need to implement IEqualityComparer<T> interface in order to compare the
            //  values of complex types.

            //  In the following example, StudentComparer class implements
            //  IEqualityComparer<Student> to compare Student< objects.

            //  Now, you can pass an object of the above StudentComparer class in the
            //  Distinct() method as a parameter to compare the Student objects as shown
            //  below.

            IList<Student> studentSetList = new List<Student>() {
                    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
                };

            IList<Student> studentSetList1 = new List<Student>() {
                    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
                };

            IList<Student> studentSetList2 = new List<Student>() {
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
                };

            //  does work, since we added IEqualityComparer<Student> interface
            //  into the Student class as a Third Interface,
            //  so we just add "new Student()" into the method
            var distinctStudents = studentSetList.Distinct(new Student());
            foreach (Student std in distinctStudents)
                Console.WriteLine(std.StudentName);
            Console.WriteLine();

            //  does work, since we are using IEqualityComparer<Student> interface
            var distinctStudents2 = studentSetList.Distinct(new StudentComparer());
            foreach (Student std in distinctStudents2)
                Console.WriteLine(std.StudentName);
            Console.WriteLine();





            //  The Distinct operator is Not Supported in C# Query syntax.
            //  However, you can use Distinct method of query variable or wrap whole query
            //  into brackets and then call Distinct().

            var intDisLis = (from i in intSetColl1
                             select i).Distinct();
            foreach (int i in intDisLis)
                Console.Write(i + " ");
            Console.WriteLine();

            var intLess5 = (from i in intSetColl1
                            where i < 5
                            select i).Distinct();
            foreach (int i in intLess5)
                Console.Write(i + " ");
            Console.WriteLine();










            //  Except          Returns the difference between two sequences, which means
            //                  the elements of one collection that do not appear in the
            //                  second collection.
            //                  Requires two collections.  It returns a new collection with
            //                  elements from the first collection which do not exist in the
            //                  second collection (parameter collection).

            var exceptCollection1 = strSetColl1.Except(strSetColl2);
            var exceptCollection2 = intSetColl1.Except(intSetColl2);
            var exceptCollection3 = intSetColl2.Except(intSetColl1);

            Console.WriteLine("\nExcept String and int Values");
            foreach (string s in exceptCollection1)
                Console.Write(s + " ");     //  returns One, Two, Four
            Console.WriteLine();

            foreach (int i in exceptCollection2)
                Console.Write(i + " ");     //  returns 2, 5
            Console.WriteLine();

            foreach (int i in exceptCollection3)
                Console.Write(i + " ");     //  returns 6, 7, 9
            Console.WriteLine("\n");








            //  Except extension method doesn't return the correct result for the collection
            //  of complex types.  You need to implement IEqualityComparer interface in
            //  order to get the correct result from Except method.
            
            //  Using StudentComparer Class whixh implements IEqualityComparer
            var exceptStudents = studentSetList1.Except(studentSetList2, new StudentComparer());
            foreach (Student s in exceptStudents)
                Console.WriteLine(s.StudentName);
            
            //  Using Student Class which implements IEqualityComparer
            Console.WriteLine();
            var exceptStudents2 = studentSetList.Except(studentSetList2, new Student());
            foreach (Student s in exceptStudents2)
                Console.WriteLine(s.StudentName);










            //  The Except operator is Not Supported in C# & VB.Net Query syntax.
            //  However, you can use Distinct method on query variable or wrap whole query
            //  into brackets and then call Except().

            var intExcLis = (from i in intSetColl1
                             select i).Except(intSetColl2);
            foreach (int i in intExcLis)
                Console.Write(i + " ");     //  returns 2, 5
            Console.WriteLine();

            var intExcLis2 = (from i in intSetColl2
                             select i).Except(intSetColl1);
            foreach (int i in intExcLis2)   //  returns 6, 7, 9
                Console.Write(i + " ");
            Console.WriteLine("\n");












            //  Intersect       Returns the intersection of two sequences, which means
            //                  elements that appear in both the collections.
            //                  Requires two collections.  It returns a new collection that
            //                  includes common elements that exists in both the collections.

            var inter1 = strSetColl1.Intersect(strSetColl2);
            foreach (var item in inter1)
                Console.Write(item + "  ");
            Console.WriteLine();





            //  The Intersect extension method doesn't return the correct result for the
            //  collection of complex types.  You need to implement IEqualityComparer
            //  interface in order to get the correct result from Intersect method.

            var inter2 = studentSetList1.Intersect(studentSetList2, new Student());
            foreach (var item in inter2)
                Console.Write(item.StudentName + "  ");
            Console.WriteLine();


            var inter3 = studentSetList1.Intersect(studentSetList2, new StudentComparer());
            foreach (var item in inter3)
                Console.Write(item.StudentName + "  ");
            Console.WriteLine("\n");










            //  Union           Returns unique elements from two sequences, which means
            //                  unique elements that appear in either of the two sequences.

            var union1 = strSetColl1.Union(strSetColl2);
            foreach (var item in union1)
                Console.Write(item + "  ");
            Console.WriteLine();






            //  The Union extension method doesn't return the correct result for the
            //  collection of complex types.  You need to implement IEqualityComparer
            //  interface in order to get the correct result from Union method.
            
            var union2 = studentSetList1.Union(studentSetList2, new Student());
            foreach (var item in union2)
                Console.Write(item.StudentName + "  ");
            Console.WriteLine();


            var union3 = studentSetList1.Union(studentSetList2, new StudentComparer());
            foreach (var item in union3)
                Console.Write(item.StudentName + "  ");
            Console.WriteLine("\n");











            //  Partitioning Operators (Skip, SkipWhile,Take & TakeWhile)
            //  Partitioning Operators split the sequence (collection) into two parts and
            //  return one of the parts.


            //  Skip    skips the specified number of element starting from first element
            //          and returns rest of the elements.

            Console.WriteLine("\nSKIP");
            Console.WriteLine("Full List");
            foreach (var item in union1)
                Console.Write(item + " ");


            Console.WriteLine("\nFirst 2 elements skiped / removed from the above list");
            var skip1 = union1.Skip(2);
            foreach (var item in skip1)
                Console.Write(item + " ");


            Console.WriteLine("\nFirst 2 elements skiped / removed from the complex type list");
            var skip2 = union3.Skip(2);
            foreach (var item in skip2)
                Console.Write(item.StudentName + " ");

            Console.WriteLine();










            //  SkipWhile   As the name suggests, the SkipWhile() extension method in LINQ
            //              skip elements in the collection till the specified condition is
            //              true.  It returns a new collection that includes all the
            //              remaining elements once the specified condition becomes false for
            //              any element.

            //              The SkipWhile() method has two overload methods.  One method
            //              accepts the predicate of Func<TSource, bool> type and other
            //              overload method accepts the predicate Func<TSource, int, bool>
            //              type that pass the index of an element.

            //  It skips all the elements until it finds one element with a string length of
            //  4 or more

            Console.WriteLine("\nSKIP WHILE");
            var sWhile1 = union1.SkipWhile(s => s.Length <= 3);
            foreach (var item in sWhile1)
                Console.Write(item + " ");
            Console.WriteLine();


            var sWhile2 = union1.SkipWhile(s => s.Length < 4);
            foreach (var item in sWhile2)
                Console.Write(item + " ");
            Console.WriteLine();

            //  In the above example, SkipWhile() skips first two elements because their
            //  length is less than 3 and finds third element whose length is equal or more
            //  than 4.  Once it finds any element whose length is equal or more than 4
            //  characters then it will not skip any other elements even if they are less
            //  than 4 characters.




            //  SkipWhile() does not skip any elements because the specified condition is
            //  false for the first element.

            IList<string> strRevList = new List<string>() { "Three", "One", "Two", "Four", "Five", "Six" };
            
            var sWhile3 = strRevList.SkipWhile(s => s.Length < 4);
            foreach (var item in sWhile3)
                Console.Write(item + " ");
            Console.WriteLine();




            //  The Lambda Expression includes element and index of an elements as a
            //  parameter.  It skips all the elements till the length of a string element
            //  is greater than it's index.

            var sWhile4 = union1.SkipWhile((s, i) => s.Length > i);
            foreach (var item in sWhile4)
                Console.Write(item + " ");
            Console.WriteLine();




            //  For complex types

            var sWhile5 = union3.SkipWhile(s => s.Age < 20);
            foreach (var item in sWhile5)
                Console.Write(item.StudentName + " ");
            Console.WriteLine();








            //  Take    returns the specified number of elements starting from the
            //          first element.

            Console.WriteLine("\nTAKE\nTakes the first 3 elements from the list");
            var take1 = union1.Take(3);

            foreach (var item in take1)
                Console.Write(item + " ");
            Console.WriteLine();




            //  Take & TakeWhile operator is Not Supported in C# query syntax.
            //  However, you can use Take/TakeWhile method on query variable or wrap
            //  the whole query into brackets and then call Take/TakeWhile() method.

            Console.WriteLine("\nTakes the first 2 students from the studentListA");
            var take2 = (from s in studentListA
                         select s).Take(2);

            foreach (var s in take2)
                Console.Write(s.StudentName + "  ");
            Console.WriteLine();


            Console.WriteLine("\nTakes the first 3 students from the student union list");
            var take3 = (from s in union2
                         select s).Take(3);

            foreach (var s in take3)
                Console.Write(s.StudentName + "  ");
            Console.WriteLine();









            //  TakeWhile   returns elements from the given collection until the specified
            //              condition is true.  If the first element itself doesn't satisfy
            //              the condition then returns an empty collection.

            //              The TakeWhile method has two overload methods.  One method
            //              accepts the predicate of Func<TSource, bool> type and the other
            //              overload method accepts the predicate Func<TSource, int, bool>
            //              type that passes the index of element.




            //  In the following example, TakeWhile() method returns a new collection that
            //  includes all the elements till it finds a string whose length less than 4
            //  characters.

            IList<string> twhileList1 = new List<string>() { 
                                                        "Three", "Four", "Five", "Hundred" };


            Console.WriteLine("\nTAKE WHILE");
            Console.WriteLine("\nTakes all the elements from the list whose length is more than 4");
            var twhile1 = twhileList1.TakeWhile(s => s.Length > 4);

            foreach (var item in twhile1)
                Console.Write(item + " ");
            Console.WriteLine("\nOnly returns Three because length of 2nd string is less than 4");



            Console.WriteLine("\nTakes all the elements from the list whose length is more than 6");
            var twhile2 = twhileList1.TakeWhile(s => s.Length > 6);

            if (twhile2.Count() == 0)
                Console.WriteLine("Empty List");
            foreach (var item in twhile2)
                Console.Write(item + " ");
            Console.WriteLine("\nReturns an empty list because 1st string's length is less than 6");




            //  TakeWhile also passes an index of current element in predicate function.
            //  Following example of TakeWhile method takes elements till length of string
            //  element is greater than it's index

            Console.WriteLine("\nTakes elements till length of string element is greater than it's index");
            var twhile3 = union1.TakeWhile((s, i) => s.Length > i);

            if (twhile3.Count() == 0)
                Console.WriteLine("Empty List");
            foreach (var item in twhile3)
                Console.Write(item + " ");
            Console.WriteLine("\nReturns first 4 elements because the 5th one's length is less than 5\n");













            //  Conversion Operators
            //  The Conversion operators in LINQ are useful in converting the type of the
            //  elements in a sequence (collection).

            //  There are three types of conversion operators:
            //      As operators (AsEnumerable and AsQueryable),
            //      To operators (ToArray, ToDictionary, ToList and ToLookup), and
            //      Casting operators (Cast and OfType).

            //  AsEnumerable	Returns the input sequence as IEnumerable<t>

            //  AsQueryable     Converts IEnumerable to IQueryable, to simulate a remote 
            //                  query provider

            //  Cast            Coverts a non-generic collection to a generic collection
            //                  (IEnumerable to IEnumerable<T>)

            //  OfType          Filters a collection based on a specified type

            //  ToArray         Converts a collection to an array

            //  ToDictionary    Puts elements into a Dictionary based on key selector
            //                  function

            //  ToList          Converts collection to List

            //  ToLookup        Groups elements into an Lookup<TKey, TElement>






            //  AsEnumerable & AsQueryable
            //  The AsEnumerable and AsQueryable methods cast or convert a source object
            //  to IEnumerable<T> or IQueryable<T> respectively.

            Student[] studentArray = {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 31 } ,
            };

            Console.WriteLine("AsEnumerable & AsQueryable");
            ReportTypeProperties(studentArray);
            ReportTypeProperties(studentArray.AsEnumerable());
            ReportTypeProperties(studentArray.AsQueryable());

            //  As you can see in the above example AsEnumerable and AsQueryable methods
            //  convert compile time type to IEnumerable and IQueryable respectively.







            //  Cast    Cast does the same thing as AsEnumerable<T>. It cast the source
            //          object into IEnumerable<T>.

            Console.WriteLine("CAST");
            ReportTypeProperties(studentArray);
            ReportTypeProperties(studentArray.Cast<Student>());

            //  studentArray.Cast<Student>() is the same as
            //  (IEnumerable<Student>)studentArray but Cast<Student>() is more readable.












            //  To Operators (ToArray(), ToList() & ToDictionary())

            //  As the name suggests, ToArray(), ToList(), ToDictionary() method converts
            //  a source object into an Array, List or Dictionary respectively.

            //  To operators force the execution of the query.  It forces the remote query
            //  provider to execute a query and get the result from the underlying data
            //  source e.g. SQL Server database.

            IList<string> strConvList = new List<string>()
            {
                "One",
                "Two",
                "Three",
                "Four",
                "Three"
            };


            Console.WriteLine("\nConverts List to string Array");
            string[] stringArray = strConvList.ToArray();
            foreach (string s in stringArray)
                Console.WriteLine(s + "  ");


            Console.WriteLine("\nConverts Array to string List");
            IList<string> stringList = stringArray.ToList();
            foreach (string s in stringList)
                Console.WriteLine(s + "  ");



            IList<Student> studentDicList = new List<Student>() {
                    new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                    new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                    new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                    new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                    new Student() { StudentID = 5, StudentName = "Ron" , Age = 21 }
                };

            Console.WriteLine("\nConverts Generic List into Dictionary List based on StudentID");
            IDictionary<int, Student> studentDic = studentDicList
                                .ToDictionary<Student, int>(s => s.StudentID);
            foreach (var key in studentDic.Keys)
                Console.WriteLine(key + "  " + (studentDic[key] as Student).StudentName);

            Console.WriteLine();
            foreach (var s in studentDic)
                Console.WriteLine(s.Key + "  " + s.Value.StudentName);


            Console.ReadKey();
        }

        private static void ReportTypeProperties<T>(T obj)
        {
            Console.WriteLine("Compile time type {0}", typeof(T).Name);
            Console.WriteLine("Actual type {0}\n", obj.GetType().Name);
        }
    }
}
