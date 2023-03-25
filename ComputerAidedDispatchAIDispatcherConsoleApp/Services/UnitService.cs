﻿using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services
{
    internal class UnitService : BaseService, IUnitService
    {

        private readonly IHttpClientFactory _clientFactory;

        private string cadUrl;

        public UnitService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.cadUrl = configuration.GetConnectionString("BaseApiUrl");
           
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = cadUrl + "/api/Units",

            });
        }
    }
}