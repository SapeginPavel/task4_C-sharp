﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace task4.Model;

public class Farm : INotifyPropertyChanged
{
    private int _maxQuantityPerHour;
    private Storage _storage;
    private int _timeToSleep;
    private bool _isWorking;

    public bool IsWorking
    {
        get => _isWorking;
        set
        {
            _isWorking = value;
            OnPropertyChanged(nameof(IsWorking));
        }
    }

    public int MaxQuantityPerHour
    {
        get => _maxQuantityPerHour;
        private set => _maxQuantityPerHour = value;
    }

    public Farm(int maxQuantityPerHour, Storage storage)
    {
        _maxQuantityPerHour = maxQuantityPerHour;
        _storage = storage;
        _isWorking = false;
        _timeToSleep = 1200;

        _storage.PropertyChanged += (sender, e) =>
        {
            switch (e.PropertyName)
            {
                case nameof(_storage.CurrentCapacity):
                    if (_storage.isEmpty())
                    {
                        StartWorking();
                    }
                    break;
            }
        };
    }

    async public void StartWorking()
    {
        IsWorking = true;
        Random random = new Random();
        await Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(_timeToSleep);

                // MessageBox.Show("РАБОТАЕТ! " + _isWorking);

                IsWorking = random.Next(1, 50) != 1; //шанс поломки 2%

                if (!IsWorking)
                {
                    return;
                }
            
                int product = random.Next(1, _maxQuantityPerHour);
                bool wasAdded = _storage.Add(product);

                if (!wasAdded) //кончилось место
                {
                    return;
                }
            }
        });
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}