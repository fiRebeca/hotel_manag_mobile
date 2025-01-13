using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using hotel_manag_mobile.Models;

namespace hotel_manag_mobile.Data
{
    public class HotelAppDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public HotelAppDatabase(string dbPtah)
        {
            _database = new SQLiteAsyncConnection(dbPtah);
            _database.CreateTableAsync<Facility>().Wait();
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Room>().Wait();
            _database.CreateTableAsync<Feedback>().Wait();
            _database.CreateTableAsync<Reservation>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if(user.ID != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task DeleteUserAsync(User user)
        {
            await _database.DeleteAsync(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _database.Table<User>().ToListAsync();
        }

        public async Task EditUserAsync(User user)
        {
            await _database.UpdateAsync(user);
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            return await _database.Table<Room>().ToListAsync();
        }


        public async Task SaveRoomAsync(Room room)
        {
            await _database.InsertAsync(room);
        }

        public async Task DeleteRoomAsync(Room room)
        {
            await _database.DeleteAsync(room);
        }

        public async Task<List<Facility>> GetFacilitiesAsync()
        {
            return await _database.Table<Facility>().ToListAsync();
        }

        public async Task<Facility> GetFacilityByIdAsync(int id)
        {
            return await _database.Table<Facility>().Where(f => f.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveFacilityAsync(Facility facility)
        {
            if (facility.ID != 0)
            {
                return await _database.UpdateAsync(facility);
            }
            else
            {
                return await _database.InsertAsync(facility);
            }
        }

        public async Task<int> DeleteFacilityAsync(Facility facility)
        {
            return await _database.DeleteAsync(facility);
        }

        
        public Task<List<Feedback>> GetFeedbacksByUserIdAsync(int userId)
        {
            return _database.Table<Feedback>().Where(f => f.UserID == userId).ToListAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            await _database.UpdateAsync(feedback);
        }
        public Task DeleteFeedbackAsync(Feedback feedback)
        {
            return _database.DeleteAsync(feedback);
        }
        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            await _database.InsertAsync(feedback);
        }

        public async Task<List<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _database.Table<Reservation>()
                                  .Where(r => r.UserID == userId)
                                  .ToListAsync();
        }


        public async Task SaveReservationAsync(Reservation reservation)
        {
            if (reservation.ID == 0)
            {
                await _database.InsertAsync(reservation);
            }
            else
            {
                await _database.UpdateAsync(reservation);
            }
        }






    }
}
