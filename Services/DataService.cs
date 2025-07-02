using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeEqApp.Models;
using System.IO;
using System.Text.Json;

namespace OfficeEqApp.Services
{
    //
    // Отвечает за сохранение и загрузку данных оборудования в JSON-файл.
    //
    public static class DataService
    {
        private static readonly string FilePath = "equipment_data.json";

        public static List<Equipment> LoadData()
        {
            if (!File.Exists(FilePath))
                return new List<Equipment>();

            var json = File.ReadAllText(FilePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Equipment>>(json, options) ?? new List<Equipment>();
        }

        public static void SaveData(IEnumerable<Equipment> equipments)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(equipments, options);
            File.WriteAllText(FilePath, json);
        }
    }
}
