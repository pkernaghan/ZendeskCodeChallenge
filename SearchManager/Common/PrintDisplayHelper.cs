using System;
using ZendeskSearchManager.Constants;
using ZendeskSearchManager.Exception;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;
using ZendeskSearchRepository.Models.DataRepoEntity.ComplexDTOs;

namespace ZendeskSearchManager.Common
{
    public static class PrintDisplayHelper
    {
        public static void PrintOrganisationDetails(Organization organization)
        {
            try
            {
                if (organization != null && !string.IsNullOrWhiteSpace(organization.JsonData))
                {
                    Console.WriteLine(PrintSearchResultMessages.OrganisationDetails.Title);
                    ConsoleWriterHelper.PrintPrettyJSON(organization.JsonData);
                }
            }
            catch (System.Exception ex)
            {
                throw new PrintDisplayException(ErrorMessages.PrintDisplay.DisplayOrganisationDetails, ex);
            }
        }

        public static void PrintTicketDetails(ComplexTicketDTO ticket)
        {
            try
            {
                if (ticket != null && ticket.Ticket != null && !string.IsNullOrWhiteSpace(ticket.Ticket.JsonData))
                {
                    Console.WriteLine(PrintSearchResultMessages.TicketDetails.Title);
                    ConsoleWriterHelper.PrintPrettyJSON(ticket.Ticket.JsonData);

                    if (ticket.TicketOrganizationDTO != null &&
                        string.IsNullOrWhiteSpace(ticket.TicketOrganizationDTO.JsonData))
                    {
                        Console.WriteLine(PrintSearchResultMessages.TicketDetails.TicketOrganisationSubtitle);
                        PrintOrganisationDetails(ticket.TicketOrganizationDTO);
                    }

                    if (ticket.TicketSubmitterDTO != null && ticket.TicketSubmitterDTO.UserDTO != null)
                    {
                        Console.WriteLine(PrintSearchResultMessages.TicketDetails.TicketSubmitterSubtitle);
                        PrintUserDetails(ticket.TicketSubmitterDTO);
                    }

                    if (ticket.TicketAssigneeDTO != null && ticket.TicketAssigneeDTO.UserDTO != null)
                    {
                        Console.WriteLine(PrintSearchResultMessages.TicketDetails.TicketAssigneeSubtitle);
                        PrintUserDetails(ticket.TicketAssigneeDTO);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new PrintDisplayException(ErrorMessages.PrintDisplay.DisplayTicketDetails, ex);
            }
        }

        public static void PrintUserDetails(ComplexUserDTO user)
        {
            try
            {
                if (user != null && user.UserDTO != null && !string.IsNullOrWhiteSpace(user.UserDTO.JsonData))
                {
                    Console.WriteLine(PrintSearchResultMessages.UserDetails.Title);
                    ConsoleWriterHelper.PrintPrettyJSON(user.UserDTO.JsonData);

                    if (user.OrganizationDTO != null)
                    {
                        Console.WriteLine(PrintSearchResultMessages.UserDetails.UserOrganisationSubtitle);
                        PrintOrganisationDetails(user.OrganizationDTO);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new PrintDisplayException(ErrorMessages.PrintDisplay.DisplayUserDetails, ex);
            }
        }
    }
}