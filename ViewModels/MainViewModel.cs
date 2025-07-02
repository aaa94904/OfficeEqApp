using OfficeEqApp.Helpers;
using OfficeEqApp.Models;
using OfficeEqApp.Views;
using OfficeEqApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace OfficeEqApp.ViewModels
{
    //
    // Главная ViewModel приложения, реализует логику отображения,
    // добавления, редактирования, удаления и сброса оборудования.
    //
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Equipment> Equipments { get; set; }

        private Equipment? _selectedEquipment;
        public Equipment? SelectedEquipment
        {
            get => _selectedEquipment;
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ResetCommand { get; }

        public MainViewModel()
        {
            var loaded = DataService.LoadData();
            Equipments = new ObservableCollection<Equipment>(loaded);

            AddCommand = new RelayCommand(_ => AddEquipment());
            RemoveCommand = new RelayCommand(_ => RemoveEquipment(), _ => SelectedEquipment != null);
            EditCommand = new RelayCommand(_ => EditEquipment(), _ => SelectedEquipment != null);
            ResetCommand = new RelayCommand(_ => ResetData());
        }

        private void AddEquipment()
        {
            var newId = Equipments.Count > 0 ? Equipments[^1].Id + 1 : 1;

            var newEquipment = new Equipment
            {
                Id = newId,
                Name = "Новое оборудование",
                Type = EquipmentType.Printer,
                Status = EquipmentStatus.InStock
            };

            var editWindow = new EditEquipmentWindow(newEquipment)
            {
                Owner = Application.Current.MainWindow
            };

            if (editWindow.ShowDialog() == true)
            {
                Equipments.Add(newEquipment);
                DataService.SaveData(Equipments);
            }
        }

        private void RemoveEquipment()
        {
            if (SelectedEquipment != null)
            {
                Equipments.Remove(SelectedEquipment);
                DataService.SaveData(Equipments);
            }
        }

        private void EditEquipment()
        {
            if (SelectedEquipment == null) return;

            // Создаем копию объекта для редактирования
            var equipmentCopy = new Equipment
            {
                Id = SelectedEquipment.Id,
                Name = SelectedEquipment.Name,
                Type = SelectedEquipment.Type,
                Status = SelectedEquipment.Status
            };

            var editWindow = new EditEquipmentWindow(equipmentCopy)
            {
                Owner = Application.Current.MainWindow
            };

            if (editWindow.ShowDialog() == true)
            {
                SelectedEquipment.Name = equipmentCopy.Name;
                SelectedEquipment.Type = equipmentCopy.Type;
                SelectedEquipment.Status = equipmentCopy.Status;

                OnPropertyChanged(nameof(Equipments));
                DataService.SaveData(Equipments);
            }
        }

        private void ResetData()
        {
            var result = MessageBox.Show(
                "Все данные будут удалены без возможности восстановления. Продолжить?",
                "Подтверждение сброса",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Equipments.Clear();
                DataService.SaveData(Equipments);
            }
        }
    }
}