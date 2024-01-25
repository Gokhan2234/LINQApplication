using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQApplication
{
    class Student : IComparable<Student>, IComparer<Student>, IEqualityComparer<Student>
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public int StandardID { get; set; }

        public Student() { }

        public Student(int StudentID, string StudentName, int Age, int StandardID = 0)
        {
            this.StudentID = StudentID;
            this.StudentName = StudentName;
            this.Age = Age;
            this.StandardID = StandardID;
        }


        //  First Interface
        public int CompareTo(Student other)
        {
            if (this.StudentName.Length > other.StudentName.Length)
                return 1;
            return 0;
            //throw new NotImplementedException();
        }


        //  Second Interface
        public int Compare(Student x, Student y)
        {
            throw new NotImplementedException();
        }


        //  Third Interface
        public bool Equals(Student x, Student y)
        {
            if (x.StudentName.ToLower() == y.StudentName.ToLower()
                && x.StudentID == y.StudentID && x.Age == y.Age)
                return true;
            return false;
            //throw new NotImplementedException();
        }

        public int GetHashCode(Student obj)
        {
            return obj.StudentID.GetHashCode();
            //throw new NotImplementedException();
        }
    }
}
