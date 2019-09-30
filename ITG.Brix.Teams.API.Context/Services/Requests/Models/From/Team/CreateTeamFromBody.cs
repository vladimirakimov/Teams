using System.Runtime.Serialization;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Models.From
{
    public class CreateTeamFromBody
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Layout { get; set; }
    }
}
