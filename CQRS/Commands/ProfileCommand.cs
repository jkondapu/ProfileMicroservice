using MediatR;
using Newtonsoft.Json.Linq;
using System;

namespace ProfileMicroservice.CQRS.Commands
{
    public class ProfileCommand : IRequest<bool>
    {
       public JObject Profile { get; set; }
       public Guid? ProfileID { get; set; } 
    }
}
