using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfficeEqApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OfficeEqApp.Views
{
    //
    // Логика взаимодействия с окном редактирования оборудования.
    // Предоставляет список типов и статусов, а также изменяет объект Equipment.
    //
    public partial class EditEquipmentWindow : Window, INotifyPropertyChanged
    {
        //
        // Объект оборудования, который редактируется.
        //
        public Equipment Equipment { get; private set; }

        //
        // Список всех возможных типов оборудования для ComboBox.
        //
        public List<EquipmentType> EquipmentTypes => new(Enum.GetValues(typeof(EquipmentType)) as EquipmentType[] ?? []);

        //
        // Список всех возможных статусов оборудования для ComboBox.
        //
        public List<EquipmentStatus> EquipmentStatuses => new(Enum.GetValues(typeof(EquipmentStatus)) as EquipmentStatus[] ?? []);

        public EditEquipmentWindow(Equipment equipment)
        {
            InitializeComponent();
            Equipment = equipment;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //
        // Сохраняет изменения и закрывает окно.
        //
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        //
        // Отменяет изменения и закрывает окно.
        //
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

