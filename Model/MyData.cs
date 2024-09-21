﻿using CommunityToolkit.Mvvm.ComponentModel;
namespace docghifile.Model
{
    public partial class MyData : ObservableObject
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _tuoi;
        [ObservableProperty]
        private string _phoneNumber;
        [ObservableProperty]
        private string _email;
    }
}
