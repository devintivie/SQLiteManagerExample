using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SQLiteManager.Managers
{
    public class StateManager
    {
        #region Singleton
        private static readonly Lazy<StateManager> instance = new Lazy<StateManager>();
        public static StateManager Instance => instance.Value;
        public StateManager()
        {
            //Needed for mobile
            //basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DatabaseType = DatabaseType.Local;
        }
        #endregion

        #region Fields
        private const string stateFilename = "budgetState.json";
        private string basePath = "";
        #endregion

        #region Properties
        public string DatabaseName { get; set; } = null;
        public DatabaseType DatabaseType { get; set; }
        public List<string> Budgets { get; private set; }

        public string DatabasePath
        {
            get
            {
                var connectionString = $"Data Source={DatabaseName}.sqlite3;Version=3;";
                //var currentFileAndExt = $"{DatabaseName}.db3";
                var fullpath = Path.Combine(basePath, connectionString);
                return fullpath;
            }
        }
        #endregion

        #region Methods
        public async Task SaveState()
        {
            var path = $"{basePath}/{stateFilename}";

            var state = new State
            {
                DatabaseName = DatabaseName
            };
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, state);
            }
        }

        public async Task<string> LoadState()
        {
            var path = Path.Combine(basePath, stateFilename);

            var state = new State();
            try
            {
                var filenames = Directory.GetFiles(basePath);
                foreach (var item in filenames)
                {
                    Console.WriteLine(item);
                }
                var str = File.ReadAllText(path);
                state = JsonConvert.DeserializeObject<State>(str);
                DatabaseName = state.DatabaseName;
            }
            catch (FileNotFoundException e)
            {
                await SaveState();
                var filenames2 = Directory.GetFiles(basePath);
                foreach (var item in filenames2)
                {
                    Console.WriteLine(item);
                }
                var str = File.ReadAllText(path);
                state = JsonConvert.DeserializeObject<State>(str);

                DatabaseName = state.DatabaseName;
            }
            return DatabaseName;
        }


        public async Task<List<string>> FindBudgetFiles()
        {
            await Task.Run(() =>
            {
                var filenames = Directory.GetFiles(basePath, "*.db3");

                Budgets = filenames.Select(fn => Path.GetFileNameWithoutExtension(fn)).ToList();
            });

            return Budgets;
        }

        public async Task DeleteBudgetFile(string budgetName)
        {
            await Task.Run(() =>
            {
                var filename = $"{budgetName}.db3";
                var fullpath = Path.Combine(basePath, filename);
                File.Delete(fullpath);
            });

            if (DatabaseName == budgetName)
            {
                DatabaseName = null;
                await SaveState();
            }
        }


        #endregion

    }

    [Serializable]
    public class State
    {
        public string DatabaseName { get; set; }
    }
}
