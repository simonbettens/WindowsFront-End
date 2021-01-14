using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsFront_end.Controllers;
using WindowsFront_end.Models;
using WindowsFront_end.Models.DTO_s;

namespace WindowsFront_end.ViewModel
{
    public class LoginRegistratieViewModel : INotifyPropertyChanged
    {

        private bool _gotDataNotSuccefull;
        public bool GotDataNotSuccesfull
        {
            get { return _gotDataNotSuccefull; }
            set { _gotDataNotSuccefull = value; RaisePropertyChanged("GotDataNotSuccesfull"); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged("ErrorMessage"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged("Email"); }
        }

        private string _pw;
        public string Passwoord
        {
            get { return _pw; }
            set { _pw = value; RaisePropertyChanged("Passwoord"); }
        }

        private string _pwc;
        public string PasswoordConfirmation
        {
            get { return _pwc; }
            set { _pwc = value; RaisePropertyChanged("PasswoordConfirmations"); }
        }

        private string _fn;
        public string FirstName
        {
            get { return _fn; }
            set { _fn = value; RaisePropertyChanged("FirstName"); }
        }

        private string _ln;
        public string LastName
        {
            get { return _ln; }
            set { _ln = value; RaisePropertyChanged("LastName"); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged("Address"); }
        }


        public ApplicationDataContainer LocalSettings { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public LoginRegistratieViewModel(ApplicationDataContainer localSettings)
        {
            LocalSettings = localSettings;
            LoginCommand = new RelayCommand(async (param) => await LogInPerson());
            RegisterCommand = new RelayCommand(async (param) => await RegistrationPerson());
        }


        public async Task<bool> RegistrationPerson()
        {
            try
            {
                RegisterDTO p = new RegisterDTO
                {
                    Email = Email,
                    Password = Passwoord,
                    Name = LastName,
                    FirstName = FirstName,
                    PasswordConfirmation = PasswoordConfirmation,
                    Address = Address
                };

                LocalSettings.Values["current_user_email"] = Email;

                HttpResponseMessage response = await AccountController.Register(p);
                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    LocalSettings.Values["token"] = token;
                    GotDataNotSuccesfull = false;
                    ErrorMessage = "";
                }
                else
                {
                    ErrorMessage = "Er liep iets mis bij het inloggen. Zijn alle parameters juist ingevuld?";
                    GotDataNotSuccesfull = true;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Er liep iets mis bij het inloggen. Zijn alle parameters juist ingevuld?";
                GotDataNotSuccesfull = true;
            }

            return GotDataNotSuccesfull;
        }

        public async Task<bool> LogInPerson()
        {
            try
            {
                //test
                //https://localhost:5001/person/login

                LoginDTO login = new LoginDTO
                {
                    Email = Email,
                    Password = Passwoord
                };
                LocalSettings.Values["current_user_email"] = Email;
                HttpResponseMessage response = await AccountController.Login(login);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    LocalSettings.Values["token"] = token;
                    Person loggedInUser = await AccountController.GetPersonByEmail(login.Email);
                    if (loggedInUser != null)
                    {
                        LocalSettings.Values["loggedInUser"] = loggedInUser.Name;
                    }
                    GotDataNotSuccesfull = false;
                }
                else
                {
                    GotDataNotSuccesfull = true;
                    ErrorMessage = "Er liep iets mis bij het inloggen. Zijn alle parameters juist ingevuld?";
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Er liep iets mis bij het inloggen. Zijn alle parameters juist ingevuld?";
                GotDataNotSuccesfull = true;
            }
            return GotDataNotSuccesfull;
        }
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
