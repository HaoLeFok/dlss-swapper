﻿using AsyncAwaitBestPractices;
using DLSS_Swapper.Data.TechPowerUp;
using DLSS_Swapper.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DLSS_Swapper
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            Title = "DLSS Swapper";
            this.InitializeComponent();
        }

        void MainNavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //FrameNavigationOptions navOptions = new FrameNavigationOptions();
            //navOptions.TransitionInfoOverride = args.RecommendedNavigationTransitionInfo;

            if (args.InvokedItem is String invokedItem)
            {
                GoToPage(invokedItem);
            }
        }

        void GoToPage(string page)
        {
            Type pageType = null;

            if (page == "Games")
            {
                pageType = typeof(GameGridPage);
            }
            else if (page == "Download")
            {
                pageType = typeof(TechPowerUpDownloadPage);
            }

            foreach (NavigationViewItem navigationViewItem in MainNavigationView.MenuItems)
            {
                if (navigationViewItem.Tag.ToString() == page)
                {
                    MainNavigationView.SelectedItem = navigationViewItem;
                    break;
                }
            }

            if (pageType != null)
            {
                ContentFrame.Navigate(pageType);
            }
        }

        async void MainNavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            GoToPage("Games");

            if (Settings.HasShownWarning == false)
            {
                var dialog = new ContentDialog()
                {
                    Title = "Warning",
                    CloseButtonText = "Okay",
                    Content = @"Replacing dlls on your computer can be dangerous.

Placing a malicious dll into a game is just as bad as running Linking_park_-_nUmB_mp3.exe that you just downloaded from LimeWire.

This tool will download dlls from TechPowerUp, it's up to you if you consider them a trusted source.

Use at your own risk.",
                    XamlRoot = MainNavigationView.XamlRoot,
                };
                await dialog.ShowAsync();

                Settings.HasShownWarning = true;
            }



        }
    }
}
