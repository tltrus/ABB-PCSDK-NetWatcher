using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics.Controllers;

namespace NetWatcher.Model
{
    internal class IRCcontroller : Model
    {
        private string id = string.Empty;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private IPAddress ipAddress;
        public IPAddress IPAddress
        {
            get => ipAddress;
            set
            {
                ipAddress = value;
                OnPropertyChanged();
            }
        }


        private Availability availability;
        public Availability Availability
        {
            get => availability;
            set
            {
                availability = value;
                OnPropertyChanged();
            }
        }


        private bool isVirtual;
        public bool IsVirtual
        {
            get => isVirtual;
            set
            {
                isVirtual = value;
                OnPropertyChanged();
            }
        }


        private string systemName = string.Empty;
        public string SystemName
        {
            get => systemName;
            set
            {
                systemName = value;
                OnPropertyChanged();
            }
        }


        private string rwVersion = string.Empty;
        public string RWVersion
        {
            get => rwVersion;
            set
            {
                rwVersion = value;
                OnPropertyChanged();
            }
        }


        private string controllerName = string.Empty;
        public string ControllerName
        {
            get => controllerName;
            set
            {
                controllerName = value;
                OnPropertyChanged();
            }
        }
    }
}
