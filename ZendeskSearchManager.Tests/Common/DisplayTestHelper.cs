using System;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;

namespace ZendeskSearchManager.Tests
{
    public static class DisplayTestHelper
    {
        public static Organization CreateTestOrganization()
        {
            var testOrgId = 55;
            var testGuid = new Guid("9270ed79-35eb-4a38-a46f-35725197ea8d");
            var testJson = @"{
                '_id': 55,
                'external_id': '9270ed79-35eb-4a38-a46f-35725197ea8d',
                'name': 'Enthaze',
                'created_at': '2016-05-21T11:10:28 -10:00',
                'details': 'MegaCorp',
                'shared_tickets': false,
            }";

            return new Organization(testOrgId, testGuid, testJson);
        }

        public static User CreateTestUserDTO()
        {
            var userDTO = new User();

            var testUserJson = @"{
                    '_id': 123,
                    'external_id': 'c9995ea4-ff72-46e0-ab77-dfe0ae1ef6c2',
                    'name': 'Cross Barlow',
                    'alias': 'Miss Joni',
                    'created_at': '2016-06-23T10:31:39 -10:00',
                    'active': true,
                    'verified': true,
            }";

            userDTO.Id = 123;
            userDTO.OrganizationId = 55;
            userDTO.JsonData = testUserJson;

            return userDTO;
        }

        public static Ticket CreateTestTicketDTO()
        {
            var ticketDTO = new Ticket();

            var testTicketJson = @"{
                '_id': '2217c7dc-7371-4401-8738-0a8a8aedc08d',
                'url': 'http://initech.zendesk.com/api/v2/tickets/2217c7dc-7371-4401-8738-0a8a8aedc08d.json',
                'external_id': '3db2c1e6-559d-4015-b7a4-6248464a6bf0',
                'created_at': '2016-07-16T12:05:12 -10:00',
                'type': 'problem',
                'subject': 'A Catastrophe in Hungary',
                'description': 'Ipsum fugiat voluptate reprehenderit cupidatat aliqua dolore consequat. Consequat ullamco minim laboris veniam ea id laborum et eiusmod excepteur sint laborum dolore qui.',
                'priority': 'normal',
                'status': 'closed',
                'submitter_id': 9,
                'assignee_id': 65,
                'organization_id': 599,
            }";

            ticketDTO.Id = new Guid("2217c7dc-7371-4401-8738-0a8a8aedc08d");
            ticketDTO.AssigneeId = 65;
            ticketDTO.OrganizationId = 599;
            ticketDTO.JsonData = testTicketJson;

            return ticketDTO;
        }

        public static ComplexUserDTO CreateTestComplexUserDTO()
        {
            var returnDTO = new ComplexUserDTO();
            
            returnDTO.OrganizationDTO = CreateTestOrganization();
            returnDTO.UserDTO = CreateTestUserDTO();

            return returnDTO;
        }

        public static ComplexTicketDTO CreateTestComplexTicketDTO()
        {
            var returnDTO = new ComplexTicketDTO();

            returnDTO.TicketOrganizationDTO = CreateTestOrganization();
            returnDTO.TicketAssigneeDTO = CreateTestComplexUserDTO();
            returnDTO.TicketSubmitterDTO = CreateTestComplexUserDTO();
            returnDTO.Ticket = CreateTestTicketDTO();

            return returnDTO;
        }
    }
}