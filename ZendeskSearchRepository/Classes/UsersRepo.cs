using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZendeskSearchRepository.DBModels;
using ZendeskSearchRepository.DBModels.ComplexDTOs;

namespace ZendeskSearchRepository.Classes
{
    public class UsersRepo
    {
        public static List<User> Users { get; protected set; }

        public UsersRepo(List<User> userSet)
        {
            Users = userSet;
        }
    }
}
