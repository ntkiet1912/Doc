using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using docghifile.Model;
using Microsoft.Win32;

namespace docghifile.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
            [ObservableProperty]
            private string _name;
            [ObservableProperty]
            private string _tuoi;
            [ObservableProperty]
            private string _phoneNumber;
            [ObservableProperty]
            private string _email;
            [ObservableProperty]
            private string _csvFilePath;
            public ObservableCollection<MyData> Data { get; set; }

            [ObservableProperty]
            private ObservableCollection<MyData> _newdata;
            [ObservableProperty]
            private MyData selectedData;

            public MainWindowViewModel()
            {
                Data = new ObservableCollection<MyData>();
                _newdata = new ObservableCollection<MyData>();
            }

            [RelayCommand]
            public void Add()
            {
                Data.Add(new MyData() {
                    Name = _name,
                    Tuoi = _tuoi,
                    PhoneNumber = _phoneNumber,
                    Email = _email,
                });
                _newdata.Add(new MyData() {
                    Name = _name,
                    Tuoi = _tuoi,
                    PhoneNumber = _phoneNumber,
                    Email = _email,
                });
                Name = string.Empty;
                Tuoi = string.Empty;
                PhoneNumber = string.Empty;
                Email = string.Empty;

            }
            [RelayCommand]
            public void Delete()
            {
                if (selectedData != null && !string.IsNullOrEmpty(CsvFilePath))
                {
                    

                    if (File.Exists(CsvFilePath))
                    {
                        var allLines = File.ReadAllLines(CsvFilePath).ToList();
                        var lineToRemove = $"{selectedData.Name},{selectedData.Tuoi},{selectedData.PhoneNumber},{selectedData.Email}";
                        allLines.Remove(lineToRemove);
                        
                        File.WriteAllLines(CsvFilePath, allLines);
                        
                    
                    }
                    Data.Remove(selectedData);
                    _newdata.Remove(selectedData);
                    selectedData = null;
                }
                
                
            }
            [RelayCommand]
            public void Save()
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    bool hasHeader = false;
                    if (File.Exists(fileName))
                    {
                        using(StreamReader reader = new StreamReader(fileName))
                        {
                            if (!reader.EndOfStream)
                            {
                                string firstLine = reader.ReadLine();
                                if(firstLine == "Name,Age,PhoneNumber,Email")
                                {
                                    hasHeader = true;

                                }
                            }
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName,true))
                    {


                        if (!hasHeader) writer.WriteLine("Name,Age,PhoneNumber,Email");
                    
                    
                        foreach (MyData data in _newdata)
                        {
                            writer.WriteLine($"{data.Name},{data.Tuoi},{data.PhoneNumber},{data.Email}");
                        }
                        _newdata.Clear();
                    }
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
                    var csvData = File.ReadAllLines(CsvFilePath);
                    Data.Clear();
                    _newdata.Clear();
                    foreach(var row in csvData.Skip(1))
                    {
                        var columns = row.Split(',');
                        Data.Add(new MyData
                        {
                            Name = columns[0],
                            Tuoi = columns[1],
                            PhoneNumber = columns[2],
                            Email = columns[3]
                        });
                    }
                }
         }
    }

}


