using MasterMobile.MVVM;
using Xamarin.Essentials;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    public class EthernetInfoVieModel : BaseViewModel
    {
        public string NetworkState { get; set; }
        public RelyCommand CheckNetworkAccess { get; set; }

        public EthernetInfoVieModel()
        {
            Title = "Ethernet";

            Connectivity.ConnectivityChanged += OnConnectivityChanged;

            CheckNetworkAccess = new RelyCommand(o => CheckConnection());

            CheckConnection();
        }

        #region http connect test

        /// <summary>
        /// обработка изменения состояния подключения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckConnection();
        }

        /// <summary>
        /// проверка состояния сети
        /// </summary>
        private void CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            switch (current)
            {
                case NetworkAccess.Internet:
                    {
                        NetworkState = "локальная сеть и доступ к Интернету";
                        break;
                    }
                case NetworkAccess.ConstrainedInternet:
                    {
                        NetworkState = "ограниченный доступ к Интернету";
                        break;
                    }
                case NetworkAccess.Local:
                    {
                        NetworkState = "только доступ к локальной сети";
                        break;
                    }
                case NetworkAccess.None:
                    {
                        NetworkState = "подключение недоступно";
                        break;
                    }
                case NetworkAccess.Unknown:
                    {
                        NetworkState = "не удается определить режим подключения";
                        break;
                    }
            }
            OnPropertyChanged("NetworkState");
        }

        #endregion
    }
}
