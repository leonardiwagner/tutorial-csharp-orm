﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace NHibernateTutorial.Core.Model
{
    public class Student
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual IList<Course> Courses { get; set; }
        public virtual IList<Enrollment> Enrollments { get; set; }

        public Student(){}

        public Student(string name)
        {
            this.Name = name;
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
        }
    }

    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("student");

            Id(i => i.Id);
            Map(x => x.Name);

            HasManyToMany<Course>(x => x.Courses)
                .Table("coursestudent")
                .ParentKeyColumn("studentid")
                .ChildKeyColumn("courseid")
                .Cascade //Decides what does with its relationships
                .All();
                

            HasMany<Enrollment>(x => x.Enrollments)
                .KeyColumn("studentid")
                .Inverse() //Relationship owner
                .Cascade
                .Delete();
        }
    }
}
