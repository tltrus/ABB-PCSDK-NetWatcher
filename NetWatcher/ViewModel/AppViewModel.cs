using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Configuration;
using ABB.Robotics.Controllers.Discovery;
using NetWatcher.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NetWatcher.ViewModel
{
    internal class AppViewModel: ViewModel
    {
        public ObservableCollection<IRCcontroller> ControllersList { get; set; }

        private string statusBarContent = string.Empty;
        public string StatusBarContent
        {
            get => statusBarContent;
            set
            {
                statusBarContent = value;
                OnPropertyChanged();
            }
        }

        private NetworkScanner Netscaner {get; set;}
        private ControllerInfoCollection Controllers { get; set; }
        private NetworkWatcher Netwatcher { get; set; }


        public AppViewModel()
        {
            ControllersList = new ObservableCollection<IRCcontroller>();

            NetScan();
            NetWatch();
        }

        private void NetScan()
        {
            ControllersList.Clear();

            Netscaner = new NetworkScanner();
            Netscaner.Scan();
            Controllers = Netscaner.Controllers;

            foreach (ControllerInfo c in Controllers)
            {
                IRCcontroller item = new()
                {
                    IPAddress       = c.IPAddress,
                    Id              = c.Id,
                    Availability    = c.Availability,
                    IsVirtual       = c.IsVirtual,
                    SystemName      = c.SystemName,
                    RWVersion       = c.VersionName,
                    ControllerName  = c.ControllerName
                };

                ControllersList.Add(item);
            }
        }
        private void NetWatch() 
        {
            Netwatcher = new NetworkWatcher(Controllers);
            Netwatcher.Found += new EventHandler<NetworkWatcherEventArgs>(HandleFoundEvent);
            Netwatcher.Lost += new EventHandler<NetworkWatcherEventArgs>(HandleLostEvent);
            Netwatcher.EnableRaisingEvents = true;
        }

        private void HandleFoundEvent(object sender, NetworkWatcherEventArgs e)
        {
            StatusBarContent = "Controllers IRC5 found";
            ScanUpdate();
        }

        private void HandleLostEvent(object sender, NetworkWatcherEventArgs e)
        {
            StatusBarContent = "No IRC5 controllers found";
            ScanUpdate();
        }

        private void ScanUpdate()
        {
            NetScan();
            var timer = new System.Threading.Timer((object? o) => { StatusBarContent = string.Empty; }, null, TimeSpan.FromSeconds(5), TimeSpan.Zero);
        }
    }
}
