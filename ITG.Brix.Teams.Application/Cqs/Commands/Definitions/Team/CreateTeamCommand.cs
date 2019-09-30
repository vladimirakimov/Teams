using ITG.Brix.Teams.Application.Bases;
using MediatR;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Definitions
{
    public class CreateTeamCommand : IRequest<Result>
    {
        public string Name { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }
        public string Layout { get; private set; }

        public CreateTeamCommand(string name,
                                 string image,
                                 string description,
                                 string layout)
        {
            Name = name;
            Image = image;
            Description = description;
            Layout = layout;
        }
    }
}
