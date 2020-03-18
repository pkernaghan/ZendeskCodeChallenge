using System;
using System.Collections.Generic;
using System.Text;

namespace ZendeskSearchRepository.DBModels.ComplexDTOs
{
    public class ComplexUserDTO
    {
        public User UserDTO { get; set; }

        public Organization OrganizationDTO { get; set; }

        public ComplexUserDTO()
        {
            UserDTO = new User();
            OrganizationDTO = new Organization();
        }
    }
}
