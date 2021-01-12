using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsFront_end.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsFront_end.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvitesPage : Page
    {
        public InvitesViewModel ViewModel { get; set; }
        public ApplicationDataContainer LocalSettings { get; set; }

        public InvitesPage()
        {
            this.InitializeComponent();
            ViewModel = new InvitesViewModel(ApplicationData.Current.LocalSettings);
            DataContext = ViewModel;
        }



        protected void AcceptInvite(int tripId)
        {
            ViewModel.AcceptInviteToTrip(tripId);
        }

        protected void DeclineInvite(int tripId)
        {
            ViewModel.DeclineInviteToTrip(tripId);
        }
    }
}
