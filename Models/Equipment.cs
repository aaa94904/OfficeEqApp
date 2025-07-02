using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OfficeEqApp.Models
{
    //
    // Представляет офисное оборудование: принтер, сканер или монитор.
    // Реализует INotifyPropertyChanged для поддержки обновлений в интерфейсе.
    //
    public class Equipment : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private EquipmentType _type;
        private EquipmentStatus _status;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public EquipmentType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public EquipmentStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum EquipmentType
    {
        Printer,
        Scanner,
        Monitor
    }

    public enum EquipmentStatus
    {
        InUse,
        InStock,
        InRepair
    }
}