using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _Mapper;
        public UserRepository(DataContext context, IMapper Mapper){
            _context=context;
            _Mapper=Mapper;
        }
        public async Task<IEnumerable<AppUser>> getAppUsersAsync()
        {
           return await _context.User.Include(p=>p.Photos).ToListAsync();
        }

        public async Task<AppUser> getUserByIdAsync(int id)
        {
           return await _context.User.FindAsync(id);
        }

        public async Task<AppUser> getUserByUserName(string userName)
        {
            return await _context.User.Include(p=>p.Photos).SingleOrDefaultAsync(X => X.UserName==userName);
        }

        public async Task<bool> saveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public  void update(AppUser User)
        {
            _context.Entry(User).State=EntityState.Modified;
        }

        public async Task<MemberDTO> GetMemberAsync(string userName)
        {
            return await  _context.User.Where(x=>x.UserName==userName).ProjectTo<MemberDTO>(_Mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        public async Task<List<MemberDTO>> GetMembersAsync()
        {
            return await _context.User.ProjectTo<MemberDTO>(_Mapper.ConfigurationProvider).ToListAsync();
        }
    }
}