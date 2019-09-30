using ITG.Brix.Teams.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Definitions
{
    public class UpdateTeamCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }
        public string DriverWait { get; private set; }
        public string Layout { get; private set; }

        public IEnumerable<Guid> Members { get; private set; }
        public string FilterContent { get; set; }
        public int Version { get; private set; }

        public UpdateTeamCommand(Guid id,
                                 string name,
                                 string image,
                                 string description,
                                 string driverWait,
                                 string layout,
                                 IEnumerable<Guid> members,
                                 string filterContent,
                                 int version)
        {
            Id = id;
            Name = name;
            Image = image;
            Description = description;
            DriverWait = driverWait;
            Layout = layout;
            Members = members ?? Enumerable.Empty<Guid>();
            FilterContent = filterContent;
            Version = version;
        }
    }
}
