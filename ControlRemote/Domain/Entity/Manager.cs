﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Manager
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public List<Employer> Employers { get; set; }
    }
}