﻿using Application.Core.Data;

namespace LMPlatform.Models
{
    public class Module : ModelBase
    {
        public string Name
        {
            get; 
            set;
        }

        public string DisplayName
        {
            get; 
            set;
        }
    }
}