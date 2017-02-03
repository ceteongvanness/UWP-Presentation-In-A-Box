/* 
Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
Use of this sample source code is subject to the terms of the Microsoft license 
agreement under which you licensed this sample source code and is provided AS-IS.
If you did not accept the terms of the license agreement, you are not authorized 
to use this sample source code.  For the terms of the license, please see the 
license agreement between you and Microsoft.
To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
*/
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Linq;

namespace WeatherForecastSample
{

   public partial class ForecastPage
      : Windows.UI.Xaml.Controls.Page
   {
      Forecast forecast;

      public ForecastPage()
      {
         InitializeComponent();
      }

      /// <summary>
      /// Event handler to handle when this page is navigated to
      /// </summary>
      protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
      {
         var navigationArguments = (e.Parameter ?? "").ToString().Split('&').Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Split('=')).ToDictionary(t => t[0], t => t[1]);
         // get the city, latitude, and longitude out of the query string 
         string cityName = navigationArguments["City"];
         string latitude = navigationArguments["Latitude"];
         string longitude = navigationArguments["Longitude"];
         forecast = new Forecast();
         // get the forecast for the given latitude and longitude
         forecast.GetForecast(latitude, longitude);
         // set data context for page to this forecast
         DataContext = forecast;
         // set source for ForecastList box in page to our list of forecast time periods
         ForecastList.ItemsSource = forecast.ForecastList;
      }

      /// <summary>
      /// Make sure no item can be selected in the forecast list box
      /// </summary>
      private void ForecastList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
      {
         ForecastList.SelectedIndex = -1;
         ForecastList.SelectedItem = null;
      }

   }

}