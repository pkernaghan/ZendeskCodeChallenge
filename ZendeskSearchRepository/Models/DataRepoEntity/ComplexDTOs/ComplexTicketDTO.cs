using System;
using System.Collections.Generic;
using System.Text;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;

namespace ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs
{
    public class ComplexTicketDTO
    {
        public Ticket Ticket { get; set; }
        public Organization TicketOrganizationDTO { get; set; }
        public ComplexUserDTO TicketSubmitterDTO { get; set; }
        public ComplexUserDTO TicketAssigneeDTO { get; set; }

        public ComplexTicketDTO()
        {
            Ticket = new Ticket();
            TicketOrganizationDTO = new Organization();
            TicketSubmitterDTO = new ComplexUserDTO();
            TicketAssigneeDTO = new ComplexUserDTO();
        }
    }
}
