﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LMPlatform.UI.Services.Modules.BTS
{
    [DataContract]
    public class ProjectsResult : ResultViewData
    {
        [DataMember]
        public List<ProjectViewData> Projects { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}