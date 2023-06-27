using ColleagueInt.RTW.Database;
using ColleagueInt.RTW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ColleagueInt.RTW.Test.Repositories.SeedTestData
{
    public class SeedData
    {

        private readonly RTWContext _context;
        private readonly string _seedDataPath;

        public SeedData(RTWContext context)
        {
            _context = context;
            _seedDataPath = @"Repositories\SeedTestData\TestData\";
        }


        private List<T> Get<T>(string filename)
        {
            var dataPath = Path.Join(_seedDataPath, filename);
            List<T> entities = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(dataPath));
            return entities;
        }

        private void LoadTable<T>(DbSet<T> dbSet, string filename)
            where T : IdentityEntity
        {
            var entities = Get<T>(filename);
            dbSet.AddRange(entities);
            _context.SaveChanges();
        }

        public static void Clear<T>(DbSet<T> dbSet)
            where T : class
        {
            dbSet.RemoveRange(dbSet);
        }


        public void LoadSeedData()
        {
            LoadTable<Stage>(_context.Stage, "Stages.json");
            LoadTable<Status>(_context.Status, "Statuses.json");
            LoadTable<Colleague>(_context.Colleague, "Colleagues.json");
            LoadTable<Incident>(_context.Incident, "Incidents.json");
            LoadTable<IncidentDetail>(_context.IncidentDetail, "IncidentDetails.json");
        }


        public void ClearDatabase()
        {
            Clear(_context.Stage);
            Clear(_context.Status);
            Clear(_context.Colleague);
            Clear(_context.Incident);
            Clear(_context.IncidentDetail);
        }
    }
}
