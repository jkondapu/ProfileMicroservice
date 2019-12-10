using MediatR;
using ProfileMicroservice.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileMicroservice.CQRS.Commands.Handlers
{
    public class ProfileInformationHandler : IRequestHandler<ProfileCommand, bool>
    {
        private readonly ProfileServiceRepository _profileServiceRepository;

        public ProfileInformationHandler(ProfileServiceRepository profileServiceRepository)
        {
            _profileServiceRepository = profileServiceRepository ?? throw new ArgumentException(nameof(profileServiceRepository));
        }
        public async Task<bool> Handle(ProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.ProfileID == null)
                return await _profileServiceRepository.CreateProfile(request.Profile);
            else
                return await _profileServiceRepository.UpdateProfile(request.Profile, Guid.Parse(request.ProfileID.ToString()));
        }
    }
}
