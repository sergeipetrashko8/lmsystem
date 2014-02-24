﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Application.Core.Data;
using LMPlatform.Models.KnowledgeTesting;

namespace LMPlatform.Models
{
    public class Student : ModelBase
    {
        public string Email
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public User User
        {
            get;
            set;
        }

        public int GroupId
        {
            get;
            set;
        }

        public Group Group
        {
            get;
            set;
        }

        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName); }
        }

        public ICollection<ProjectStudent> ProjectStudents
        {
            get;
            set;
        }

        public ICollection<Bug> Bugs
        {
            get;
            set;
        }

        public ICollection<SubjectStudent> SubjectStudents
        {
            get;
            set;
        }

        public ICollection<TestUnlock> TestUnlocks
        {
            get;
            set;
        } 
    }
}