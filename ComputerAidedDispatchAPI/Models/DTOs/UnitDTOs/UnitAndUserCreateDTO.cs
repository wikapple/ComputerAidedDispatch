﻿using System.ComponentModel.DataAnnotations;

namespace ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs
{
    public class UnitAndUserCreateDTO
    {
        public RegistrationRequestDTO RegistrationDTO { get; set; }
        public string UnitNumber { get; set; }

        public string Status { get; set; }

        public UnitAndUserCreateDTO()
        {
            Status = "Unavailable";
        }
    }
}
