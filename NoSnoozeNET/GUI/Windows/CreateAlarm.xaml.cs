﻿using NoSnoozeNET.GUI.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoSnoozeNET.GUI.Windows
{
    /// <summary>
    /// Interaction logic for CreateAlarm.xaml
    /// </summary>
    public partial class CreateAlarm : Window
    {
        public static List<AlarmItem> PreviewItemList = new List<AlarmItem>();
        public AlarmItem PreviewItem = new AlarmItem();
        public AlarmItem OutputAlarmItem;

        public CreateAlarm()
        {
            InitializeComponent();

            var alarmList = new List<AlarmItem>();

            PreviewItem = new AlarmItem()
            {
                AlarmName = "Alarm name",
                AlarmCreated = "Created: " + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                TimeToRing = "Rings in 7h",
            };

            PreviewItem.LayoutTransform = new ScaleTransform(0.75, 0.75);

            alarmList.Add(PreviewItem);

            PreviewContainer.ItemsSource = alarmList;

            List<PluginListItem> plugins = new List<PluginListItem>();

            plugins.Add(new PluginListItem());
            plugins.Add(new PluginListItem());
            plugins.Add(new PluginListItem());
            plugins.Add(new PluginListItem());

            PluginList.ItemsSource = plugins;

            PreviewItemList.Add(PreviewItem);
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            PreviewItem.AlarmName = txtAlarmName.Text;
        }

        private void UpDownBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var now = DateTime.Now;
            double totalHours = 0;
            if (now > TimePicker.Value.Value)
            {
                var tomorrow = now.AddDays(1).Date.AddHours(int.Parse(TimePicker.Value.Value.ToString("hh")));
                totalHours = (tomorrow - now).TotalHours;
            }
            else
            {
                totalHours = (TimePicker.Value.Value - now).TotalHours;
            }


            PreviewItem.TimeToRing = $"Rings at {TimePicker.Value.Value:HH:mm} (in {Math.Round(totalHours)}h)";
            PreviewItem.AlarmCreated = $"Created: {DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            OutputAlarmItem = new AlarmItem()
            {
                AlarmCreated = PreviewItem.AlarmCreated,
                AlarmName = PreviewItem.AlarmName,
                TimeToRing = PreviewItem.TimeToRing
            };
            this.DialogResult = true;
            this.Close();
        }

        public AlarmItem SavedItem => OutputAlarmItem;
    }
}