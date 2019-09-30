using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels
{
    public class TeamClass
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public LayoutClass Layout { get; set; }
        public string FilterContent { get; set; }
        public List<MemberClass> Members { get; set; }
    }
}
