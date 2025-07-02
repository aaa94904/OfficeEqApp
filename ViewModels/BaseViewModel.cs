using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OfficeEqApp.ViewModels
{
    //
    // Базовый класс для всех ViewModel, реализующий механизм уведомления об изменениях свойств.
    //
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //
        // Уведомляет интерфейс о том, что свойство было изменено.
        //
        // propertyName Имя изменённого свойства (подставляется автоматически).
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
