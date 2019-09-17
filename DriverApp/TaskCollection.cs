using JNPShuttle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JNPShuttle
{
    public class TaskCollection
    {
        public List<TaskClass> tasks;

        public List<TaskClass> Tasks { get => tasks; set => tasks = value; }
    }
}