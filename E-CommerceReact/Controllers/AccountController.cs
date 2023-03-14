using E_CommerceReact.Data;
using E_CommerceReact.DTO;
using E_CommerceReact.Entities;
using E_CommerceReact.Extensions;
using E_CommerceReact.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceReact.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> userManager;
        private readonly TokenService tokenService;
        private readonly StoreContext context;

        public AccountController(UserManager<User> userManager, TokenService tokenService,
            StoreContext context)
        {
            this.context = context;
            this.tokenService = tokenService;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO LoginDTO)
        {
            var user = await userManager.FindByNameAsync(LoginDTO.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, LoginDTO.Password))
                return Unauthorized();

            var userBasket = await RetrieveBasket(LoginDTO.Username);
            var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

            if (anonBasket != null)
            {
                if (userBasket != null) context.Baskets.Remove(userBasket);
                anonBasket.BuyerId = user.UserName;
                Response.Cookies.Delete("buyerId");
                await context.SaveChangesAsync();
            }

            return new UserDTO
            {
                Email = user.Email,
                Token = await tokenService.GenerateToken(user),
                Basket = anonBasket != null ? anonBasket.MapBasketToDTO() : userBasket?.MapBasketToDTO()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(RegisterDTO registerDto)
        {
            var user = new User { UserName = registerDto.Username, Email = registerDto.Email };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await userManager.AddToRoleAsync(user, "Member");

            return StatusCode(201);
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var userBasket = await RetrieveBasket(User.Identity.Name);

            return new UserDTO
            {
                Email = user.Email,
                Token = await tokenService.GenerateToken(user),
                Basket = userBasket?.MapBasketToDTO()
            };
        }


        [Authorize]
        [HttpGet("savedAddress")]
        public async Task<ActionResult<UserAddress>> GetSavedAddress()
        {
            return await userManager.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Address)
                .FirstOrDefaultAsync();
        }

        private async Task<Basket> RetrieveBasket(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                Response.Cookies.Delete("buyerId");
                return null;
            }

            return await context.Baskets
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(basket => basket.BuyerId == buyerId);
        }
    }
}