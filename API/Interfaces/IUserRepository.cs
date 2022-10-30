using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void update(AppUser User);

        Task<bool> saveAllAsync();

        Task<IEnumerable<AppUser>> getAppUsersAsync();

        Task<AppUser> getUserByIdAsync(int id);

        Task<AppUser> getUserByUserName(string userName);

        Task<List<MemberDTO>> GetMembersAsync();

        Task<MemberDTO> GetMemberAsync(string userName);
    }
}