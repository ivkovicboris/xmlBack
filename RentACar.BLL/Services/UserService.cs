using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentACar.BLL.Contracts;
using RentACar.BLL.Models;
using RentACar.DAL.Entites;
using RentACar.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.BLL.Services
{
    public class UserService : IUserContract
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Agent> _agentRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;



        public UserService(IRepository<User> userRepository,
                           IRepository<Agent> agentRepository,
                           IRepository<Client> clientRepository,
                           SignInManager<User> signInManager,
                           UserManager<User> userManager,
                           IConfiguration config,
                           IMapper mapper

                            )
        {
            _userRepository = userRepository;
            _agentRepository = agentRepository;
            _clientRepository = clientRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;

        }

        public async Task<object> LoginUser(LoginPOCO model)
        {
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                try
                {
                    if (!user.EmailConfirmed)
                    {

                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                    throw e;
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {

                    var role = (await _userManager.GetRolesAsync(user)).ToList().FirstOrDefault();
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Jti, user.UserId.ToString()),
                    new Claim("Role", role),
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _config["Tokens:Issuer"],
                        _config["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(2),
                        signingCredentials: creds
                    );

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return results;
                }
                return null;
            }
        }

        public async Task<bool> RegisterUser(RegisterPOCO model)
        {
            var newUser = new User()
            {
                Email = model.Email,
                UserName = (model.Email.Split('@')).First(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);
            newUser.UserId = Guid.Parse(newUser.Id);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, model.UserRole);
                if (await _userManager.IsInRoleAsync(newUser, "Client"))
                {
                    var newClient = new Client()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        City = model.City,
                        ClientId = newUser.UserId,
                        Jmbg = model.Jmbg,
                        State = model.State
                    };
                    newUser.EmailConfirmed = false;
                    await _clientRepository.Create(newClient);
                    return true;
                }
                else
                {
                    var newAgent = new Agent()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        AgentId = newUser.UserId,
                        Address = model.Address,
                        City = model.City,
                        State = model.State
                    };
                    try
                    {
                        //await _clinicRepository.Update(clinicToAddEmployee);
                        await _agentRepository.Create(newAgent);
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
            }
            return false;
        }

        public async Task<bool> SentMailForRegistration(MailPOCO mail)
        {
            try
            {
                var client = await _userManager.FindByEmailAsync(mail.Receivers.First());
                client.EmailConfirmed = true;
                await _userManager.UpdateAsync(client);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
