﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RentACar.API.Models;
using RentACar.BLL.Contracts;
using RentACar.BLL.Models;
using RentACar.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserContract _userContract;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IAdContract _adContract;

        public AdController(SignInManager<User> signInManager,
                                   UserManager<User> userManager,
                                   IUserContract userContract,
                                   IAdContract adContract,
                                   IMapper mapper,
                                   IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userContract = userContract;
            _mapper = mapper;
            _config = config;
            _adContract = adContract;
        }

        [HttpGet]
        [Route("GetAllAds")]
        public async Task<object> GetAllAds() => await _adContract.GetAllAds();

        [HttpPost]
        [Route("AddAd")]
        public async Task<bool> AddAd([FromBody] AdModel ad)
        {
            var result = await _adContract.AddAd(_mapper.Map<AdModel, AdPOCO>(ad));
            if (result)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("AddAdRequest")]
        public async Task<bool> AddAdRequest([FromBody] AdRequestModel adRequest)
        {
            var result = await _adContract.AddAdRequest(_mapper.Map<AdRequestModel, AdRequestPOCO>(adRequest));
            if (result)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("GetAllAdsByUserId/{userId}")]
        public async Task<object> GetAllAdsByUserId([FromRoute] string userId)
        {
            return await _adContract.GetAllAdsByUserId(Guid.Parse(userId));
        }

    }
}