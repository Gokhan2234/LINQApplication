using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQApplication
{
    class StudentComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (x.StudentName.ToLower() == y.StudentName.ToLower() 
                && x.StudentID == y.StudentID)
                return true;
            return false;
            //throw new NotImplementedException;
        }

        public int GetHashCode(Student obj)
        {
            return obj.StudentID.GetHashCode();
            //throw new NotImplementedException();
        }
    }
}
