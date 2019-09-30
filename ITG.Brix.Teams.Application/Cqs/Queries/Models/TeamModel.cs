﻿using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Models
{
    public class TeamModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string DriverWait { get; set; }
        public string Layout { get; set; }
        public string FilterContent { get; set; }
        public List<MemberModel> Members { get; set; }
    }
}
