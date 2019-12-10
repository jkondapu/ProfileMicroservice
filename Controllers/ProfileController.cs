using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ProfileMicroservice.CQRS.Queries.Contracts;
using ProfileMicroservice.Model;
using System.Threading.Tasks;
using ProfileMicroservice.CQRS.Commands;
using Newtonsoft.Json.Linq;

namespace ProfileMicroservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{api-version:apiVersion}/Profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProfileInformationQueries _profileInformationQueries;
        public ProfileController(IMediator mediator, IProfileInformationQueries profileInformationQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _profileInformationQueries = profileInformationQueries ?? throw new ArgumentNullException(nameof(profileInformationQueries));
        }

        /// <summary>
        /// Get Profile based on Profile FirstName, LastName
        /// </summary>
        /// <param name="FirstName">FirstName</param>
        /// <param name="LastName">LastName</param>
        /// <returns></returns>
        [Route("GetAllProfiles")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get([FromQuery]string FirstName, [FromQuery]string LastName)
        {
            var _profiles = await _profileInformationQueries.GetProfiles(FirstName, LastName);
            return _profiles != null ? Ok(_profiles) : (IActionResult)NotFound();
        }

        /// <summary>
        /// Get Profile based on Profile FirstName, LastName
        /// </summary>
        /// <param name="FirstName">FirstName</param>
        /// <param name="LastName">LastName</param>
        /// <returns></returns>
        [Route("GetAllProfiles")]
        [ApiVersion("1.1")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllProfiles([FromQuery]string FirstName, [FromQuery]string LastName)
        {
            var _profiles = await _profileInformationQueries.GetProfiles(FirstName, LastName);
            return _profiles != null ? Ok(_profiles) : (IActionResult)NotFound();
        }

        /// <summary>
        /// Get Specific profile based on ProfileID
        /// </summary>
        /// <param name="profileId">ProfileID</param>
        /// <returns></returns>
        [Route("ProfileById/{profileId:guid}")]
        [HttpGet]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProfileById(Guid profileId)
        {
            var _profiles = await _profileInformationQueries.GetProfileById(profileId);
            return _profiles != null ? Ok(_profiles) : (IActionResult)NotFound();
        }

        /// <summary>
        /// Get Specific profile based on ProfileID
        /// </summary>
        /// <param name="profileId">ProfileID</param>
        /// <returns></returns>
        [Route("ProfileById/{profileId:guid}")]
        [ApiVersion("1.1")]
        [HttpGet]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProfileUsingId(Guid profileId)
        {
            var _profiles = await _profileInformationQueries.GetProfileById(profileId);
            return _profiles != null ? Ok(_profiles) : (IActionResult)NotFound();
        }

        /// <summary>
        /// Create New profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [Route("CreateProfile")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProfileCommand profile)
        {
            return await _mediator.Send(profile) ? Ok() : (IActionResult)BadRequest();
        }

        /// <summary>
        /// Update the Profile by using ProfileID
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [Route("UpdateProfile")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProfileCommand profile)
        {
            return await _mediator.Send(profile) ? Ok() : (IActionResult)BadRequest();
        }

        /// <summary>
        /// Deleting Profile
        /// </summary>
        /// <param name="id"></param>
        [Obsolete]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
