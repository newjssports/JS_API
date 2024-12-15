using Microsoft.EntityFrameworkCore;
using SportsOrderApp.Data;

namespace SportsOrderApp.Core
{
    public class DbHelper
    {
        private readonly JSDBContext _context;

        public DbHelper(JSDBContext context)
        {
            _context = context;
        }

        public async Task<int> GetNextMaxValueAsync(string tableName, string columnName)
        {
            var query = $"SELECT ISNULL(MAX([{columnName}]), 0) FROM [{tableName}]";
            var maxValue = await _context.Database.ExecuteSqlRawAsync(query);

            // Increment the max value by 1
            return maxValue + 1;
        }
    }

}
