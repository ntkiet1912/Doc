using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using docghifile.Model;
using Microsoft.Win32;
using CsvHelper;
using System.Globalization;

namespace docghifile.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
            [ObservableProperty]
            private string _name;
            [ObservableProperty]
            private string _age;
            [ObservableProperty]
            private string _phoneNumber;
            [ObservableProperty]
            private string _email;
            [ObservableProperty]
            private string _csvFilePath;
            [ObservableProperty]
            public ObservableCollection<MyData> data = new ObservableCollection<MyData>();

  
            [ObservableProperty]
            private MyData selectedData;

            public MainWindowViewModel()
            {
         
            }

            [RelayCommand]
            public void Add()
            {
                Data.Add(new MyData() {
                    Name = _name,
                    Age = _age,
                    PhoneNumber = _phoneNumber,
                    Email = _email,
                });
                
                Name = string.Empty;
                Age = string.Empty;
                PhoneNumber = string.Empty;
                Email = string.Empty;

            }
            [RelayCommand]
            public void Delete()
            {
                Data.Remove(selectedData);
            }
            [RelayCommand]
            public void Save()
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    using (var writer = new StreamWriter(fileName))
                    using (var csv = new CsvWriter(writer,CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(Data);
                    }
                Data.Clear();
                }
            }
            [RelayCommand]
            public void LoadCsv()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    CsvFilePath = openFileDialog.FileName;
                    using (var reader = new StreamReader(CsvFilePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<MyData>().ToList();
                        Data = new ObservableCollection<MyData>(records);
                    }
                }
         }
    }

}


