using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KrisG.TimeTracker.Entities;
using KrisG.TimeTracker.Services;

namespace KrisG.TimeTracker.Repositories
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {
        private IList<T> _data = new List<T>();

        protected abstract string JsonPath { get; }

        protected RepositoryBase()
        {
            LoadData().Wait();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            await LoadData();
            return _data.AsEnumerable();
        }

        public async Task<T> AddAsync(T item)
        {
            item.Id = Guid.NewGuid().ToString("N");
            _data.Add(item);
            await SaveData();

            return item;
        }

        public async Task UpdateAsync(T item)
        {
            var toUpdate = _data.FirstOrDefault(x => x.Id == item.Id);

            if (toUpdate != null)
            {
                _data.Remove(toUpdate);
            }

            _data.Add(item);
            await SaveData();
        }

        public async Task DeleteAllAsync()
        {
            _data.Clear();
            await SaveData();
        }

        private async Task SaveData()
        {
            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(JsonPath, json);
        }

        private async Task LoadData()
        {
            if (File.Exists(JsonPath))
            {
                var json = await File.ReadAllTextAsync(JsonPath);
                _data = JsonSerializer.Deserialize<IList<T>>(json);
            }
        }
    }
}
