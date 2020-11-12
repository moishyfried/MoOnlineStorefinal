using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoOnlineStore.Api.Errors;
using MoOnlineStore.Api.Extenshions;
using MoOnlineStore.Core.DTO.Email;
using MoOnlineStore.Core.DTO.identity;
using MoOnlineStore.Core.Identity;
using MoOnlineStore.Core.Interfaces;
using MoOnlineStore.Infrastructure.Services;

namespace MoOnlineStore.Api.Controllers
{
  
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,
            ITokenService tokenService,IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GaveCurrentUser()
        {
            var user = await _userManager.GavetheUserByClaimPrincibal(HttpContext.User);
            if(user == null){return null;}
           return new UserDto { 
                DisplayName = user.DisplayName, 
                Email = user.Email, 
                Token = _tokenService.GenarateToken(user)};
        }
        
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GaveUserAddress()
        {
            var user = await _userManager.GavetheUserAddressByClaimPrincibal(HttpContext.User);

            return Ok(_mapper.Map<Address, AddressDto>(user.address));
        }
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var user = await _userManager.GavetheUserAddressByClaimPrincibal(HttpContext.User);

            user.address = _mapper.Map<AddressDto, Address>(addressDto);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(addressDto);

            return BadRequest("Problam Updating The User");
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> EmailExits([FromQuery]string email)
        { 
         
           return await _userManager.FindByEmailAsync(email) != null;
        }
       
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto logindto)
        {
           
            var user = await _userManager.FindByEmailAsync(logindto.Email.ToLower());

            if (user == null){
                return Unauthorized(new ApiResponse(401));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, logindto.Password, true);  
            if(result.IsLockedOut) return Unauthorized(new ApiResponse(401,"lockedout"));
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto { DisplayName = user.DisplayName, 
                Email = user.Email, 
                Token = _tokenService.GenarateToken(user)
            };
        } 
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> resister(RegisterDto registerdto)
        {
            if (EmailExits(registerdto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse()
                { Errors = new[] { "The Email Already Exits" } });
            }
            var user = new AppUser { Email = registerdto.Email, UserName = registerdto.Email,DisplayName = registerdto.DisplayName };

            var result = await _userManager.CreateAsync(user, registerdto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(401));
            return new UserDto { 
                DisplayName = user.DisplayName, 
                Email = user.Email, 
                Token = _tokenService.GenarateToken(user)};
        }
    }
}