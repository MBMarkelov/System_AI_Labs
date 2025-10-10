using SAI.Domain.Entities;
using SAI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Domain.Interfaces
{
    public interface IUserRepository 
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByPhoneNumberNumber(PhoneNumber phoneNumber);
        Task<User?> GetByEmail(Email email);
        Task<User?> GetByTelegramNickName(TelegramNickName telegramNickName);
        Task AddAsync (User user);
        Task UpdateAsync (User user);
        Task DeleteAsync (User user);
        Task<bool> ExistsByPhoneNumber(PhoneNumber phoneNumber);
        Task<bool> ExistsByEmail(Email email);
        Task<bool> ExistByTelegramNickName(TelegramNickName telegramNickName);
    }
}
