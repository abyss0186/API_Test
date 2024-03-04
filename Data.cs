//using AutoMapperDemo01.Model;
using AutoMapperDemo01.Models;
using System.Collections.Generic;

namespace AutoMapperDemo
{
    public class Data
    {
        public static List<Student> ListStudent { get; set; }

        static Data()
        {
            ListStudent = new List<Student>();
            for (int i = 0; i < 3; i++)
            {
                Student student = new Student()
                {
                    ID = i,
                    Name = $"测试_{i}",
                    Age = 20,
                    Gender = "男"
                };
                ListStudent.Add(student);
            }
        }

        public static List<Student> GetList()
        {
            return ListStudent;
        }
        public static void Add(Student entity)
        {
            ListStudent.Add(entity);
        }
    }
}