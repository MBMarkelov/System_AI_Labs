using Microsoft.EntityFrameworkCore;
using SAI.Application.Abstractions;
using SAI.Domain.Entities;
using SAI.Domain.Interfaces;
using SAI.Domain.ValueObjects;
using SAI.Infrastructure.Models;
using SAI.Infrastructure.Persistence;

namespace SAI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private static User ToDomain(ContactEntity entity)
        {
            var user = new User(
                entity.FullName,
                new Email(entity.EmailAddress),
                new PhoneNumber(entity.PhoneNumber.ToString()),
                new TelegramNickName(entity.TelegramNickName ?? "")
            );

            return user;
        }

        private static ContactEntity ToEntity(User user)
        {
            return new ContactEntity
            {
                Id = user.Id,
                FullName = user.FullName,
                EmailAddress = user.Email.Value,
                PhoneNumber = ulong.Parse(user.PhoneNumber.Value),
                TelegramNickName = user.TelegramNickName.Value,
                HouseNumber = 0,
                Street = string.Empty,
                City = string.Empty,
                District = string.Empty,
                Region = 0,
                Country = string.Empty
            };
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Contacts.FindAsync(id);
            return entity == null ? null : ToDomain(entity);
        }

        public async Task<User?> GetByPhoneNumberNumber(PhoneNumber phoneNumber)
        {
            var entity = await _context.Contacts
                .FirstOrDefaultAsync(c => c.PhoneNumber == ulong.Parse(phoneNumber.Value));
            return entity == null ? null : ToDomain(entity);
        }

        public async Task<User?> GetByEmail(Email email)
        {
            var entity = await _context.Contacts
                .FirstOrDefaultAsync(c => c.EmailAddress == email.Value);
            return entity == null ? null : ToDomain(entity);
        }

        public async Task<User?> GetByTelegramNickName(TelegramNickName telegramNickName)
        {
            var entity = await _context.Contacts
                .FirstOrDefaultAsync(c => c.TelegramNickName == telegramNickName.Value);
            return entity == null ? null : ToDomain(entity);
        }

        public async Task AddAsync(User user)
        {
            var entity = ToEntity(user);
            await _context.Contacts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var entity = await _context.Contacts.FindAsync(user.Id);
            if (entity == null) return;

            entity.FullName = user.FullName;
            entity.EmailAddress = user.Email.Value;
            entity.PhoneNumber = ulong.Parse(user.PhoneNumber.Value);
            entity.TelegramNickName = user.TelegramNickName.Value;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Contacts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            var entity = await _context.Contacts.FindAsync(user.Id);
            if (entity == null) return;

            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByPhoneNumber(PhoneNumber phoneNumber) =>
            await _context.Contacts.AnyAsync(c => c.PhoneNumber == ulong.Parse(phoneNumber.Value));

        public async Task<bool> ExistsByEmail(Email email) =>
            await _context.Contacts.AnyAsync(c => c.EmailAddress == email.Value);

        public async Task<bool> ExistByTelegramNickName(TelegramNickName telegramNickName) =>
            await _context.Contacts.AnyAsync(c => c.TelegramNickName == telegramNickName.Value);
    }
}
