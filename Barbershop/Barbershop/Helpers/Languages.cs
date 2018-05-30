using System;
using System.Collections.Generic;
using System.Text;

namespace Barbershop.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using barbershop.Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string BarbershopTitle
        {
            get { return Resource.BarbershopTitle; }
        }

        public static string ChangeImageLabel
        {
            get { return Resource.ChangeImageLabel; }
        }

        public static string ChangePasswordBtn
        {
            get { return Resource.ChangePasswordBtn; }
        }

        public static string EmailLabel
        {
            get { return Resource.EmailLabel; }
        }

        public static string FirstNameLabel
        {
            get { return Resource.FirstNameLabel; }
        }

        public static string FirstNamePlaceholder
        {
            get { return Resource.FirstNamePlaceholder; }
        }

        public static string LastNameLabel
        {
            get { return Resource.LastNameLabel; }
        }

        public static string LastNamePlaceholder
        {
            get { return Resource.LastNamePlaceholder; }
        }

        public static string LogOutLabel
        {
            get { return Resource.LogOutLabel; }
        }

        public static string MyProfileTitle
        {
            get { return Resource.MyProfileTitle; }
        }

        public static string PerfilLabel
        {
            get { return Resource.PerfilLabel; }
        }

        public static string SaveBtn
        {
            get { return Resource.SaveBtn; }
        }

        public static string SchedulesTitle
        {
            get { return Resource.SchedulesTitle; }
        }

        public static string StatisticsLabel
        {
            get { return Resource.StatisticsLabel; }
        }

        public static string YesMessage
        {
            get { return Resource.YesMessage; }
        }

        public static string LeaveMessage
        {
            get { return Resource.LeaveMessage; }
        }

        public static string NoMessage
        {
            get { return Resource.NoMessage; }
        }

        public static string ConfirmMessage
        {
            get { return Resource.ConfirmMessage; }
        }

        public static string PasswordLabel
        {
            get { return Resource.PasswordLabel; }
        }

        public static string PasswordEnterPlaceholder
        {
            get { return Resource.PasswordEnterPlaceholder; }
        }

        public static string ForgotPasswordLabel
        {
            get { return Resource.ForgotPasswordLabel; }
        }

        public static string LoginBtn
        {
            get { return Resource.LoginBtn; }
        }

        public static string RegisterBtn
        {
            get { return Resource.RegisterBtn; }
        }

        public static string LoadingLabel
        {
            get { return Resource.LoadingLabel; }
        }

        public static string ChangePasswordTitle
        {
            get { return Resource.ChangePasswordTitle; }
        }

        public static string CurrentPasswordLabel
        {
            get { return Resource.CurrentPasswordLabel; }
        }

        public static string CurrentePasswordPlaceholder
        {
            get { return Resource.CurrentePasswordPlaceholder; }
        }

        public static string NewPasswordLabel
        {
            get { return Resource.NewPasswordLabel; }
        }

        public static string NewPasswordPlaceholder
        {
            get { return Resource.NewPasswordPlaceholder; }
        }

        public static string ConfirmPasswordLabel
        {
            get { return Resource.ConfirmPasswordLabel; }
        }

        public static string ConfirmPasswordPlaceholder
        {
            get { return Resource.ConfirmPasswordPlaceholder; }
        }

        public static string MenuTitle
        {
            get { return Resource.MenuTitle; }
        }

        public static string LoginTitle
        {
            get { return Resource.LoginTitle; }
        }

        public static string RegisterTitle
        {
            get { return Resource.RegisterTitle; }
        }

        public static string TouchChangeImage
        {
            get { return Resource.TouchChangeImage; }
        }

        public static string NameLabel
        {
            get { return Resource.NameLabel; }
        }

        public static string NamePlaceholder
        {
            get { return Resource.NamePlaceholder; }
        }

        public static string PhonePlaceholder
        {
            get { return Resource.PhonePlaceholder; }
        }

        public static string YouMustPassword
        {
            get { return Resource.YouMustPassword; }
        }

        public static string ThePasswordHaveSix
        {
            get { return Resource.ThePasswordHaveSix; }
        }

        public static string YouMustConfirm
        {
            get { return Resource.YouMustConfirm; }
        }

        public static string ThePasswordNotMatch
        {
            get { return Resource.ThePasswordNotMatch; }
        }

        public static string ErrorChargingPassword
        {
            get { return Resource.ErrorChargingPassword; }
        }

        public static string ChangeConfirPassword
        {
            get { return Resource.ChangeConfirPassword; }
        }

        public static string YouMostEnterEmail
        {
            get { return Resource.YouMostEnterEmail; }
        }

        public static string SomethingWasWrong
        {
            get { return Resource.SomethingWasWrong; }
        }

        public static string WhereDoYouWantImage
        {
            get { return Resource.WhereDoYouWantImage; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string FromCamera
        {
            get { return Resource.FromCamera; }
        }

        public static string YouMostFirstName
        {
            get { return Resource.YouMostFirstName; }
        }

        public static string YouMostEnterEmailValid
        {
            get { return Resource.YouMostEnterEmailValid; }
        }

        public static string YouMostEnterPhone
        {
            get { return Resource.YouMostEnterPhone; }
        }

        public static string YouMostConfirPassword
        {
            get { return Resource.YouMostConfirPassword; }
        }

        public static string TheUserWasCreate
        {
            get { return Resource.TheUserWasCreate; }
        }

        public static string MyAppointmentsLabel
        {
            get { return Resource.MyAppointmentsLabel; }
        }

        public static string SeparateAppointmentConfirm
        {
            get { return Resource.SeparateAppointmentConfirm; }
        }

        public static string AppointmentWasseparated
        {
            get { return Resource.AppointmentWasseparated; }
        }

        public static string CancelAppointmentConfirm
        {
            get { return Resource.CancelAppointmentConfirm; }
        }

        public static string AppointmentCanceled
        {
            get { return Resource.AppointmentCanceled; }
        }
        
    }
}
