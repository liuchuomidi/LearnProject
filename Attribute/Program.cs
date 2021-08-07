using System;
using System.Reflection;
namespace AttributeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new Student() { Age = 10, Name = "s1" };
            Student s2 = new Student() { Age = 10, Name = "s2" };
            var t = typeof(Student);
            var propertys = t.GetProperty("Age").GetValue(s1);



        }
        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
