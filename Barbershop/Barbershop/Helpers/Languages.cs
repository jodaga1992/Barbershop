using System;
using System.Collections.Generic;
using System.Text;

namespace Barbershop.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

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

    }
}
