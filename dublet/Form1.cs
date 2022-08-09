﻿using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace TwinBrowserWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeBrowsers();
        }

        private async void InitializeBrowsers()
        {
            await wv1.EnsureCoreWebView2Async();
            SetWebView2EventHandlers(wv1);
            
            await wv2.EnsureCoreWebView2Async();
            SetWebView2EventHandlers(wv2);

            await wv3.EnsureCoreWebView2Async();
            SetWebView2EventHandlers(wv3);

            await wv4.EnsureCoreWebView2Async();
            SetWebView2EventHandlers(wv4);

            await wv5.EnsureCoreWebView2Async();
            SetWebView2EventHandlers(wv5);

            btnGo1_Click(null, null);
            btnGo2_Click(null, null);
            btnGo5_Click(null, null);


        }

        private void btnGo1_Click(object sender, EventArgs e)
        {
            wv1.Source = new Uri(textUrl1.Text);
        }

        private void btnGo2_Click(object sender, EventArgs e)
        {
            wv2.Source = new Uri(textUrl2.Text);
        }


        private void btnGo5_Click(object sender, EventArgs e)
        {
            wv5.Source = new Uri(textUrl5.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            wv3.CoreWebView2.NavigateToString(textUrl3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wv4.CoreWebView2.NavigateToString(textUrl4.Text);
        }


        private void btnGoUpwork_Click(object sender, EventArgs e)
        {
            wv5.CoreWebView2.NavigateToString(textUrl5.Text);
        }
        /// <summary>
        /// set event handlers for WebView2
        /// </summary>
        /// <param name="wv"></param>
        /// <exception cref="Exception"></exception>
        private void SetWebView2EventHandlers(WebView2 wv)
        {
            string senderName = wv.Name;
//            LogMsg("SetWebView2EventHandlers() called");

            int step = 10;
            try
            {
                step = 20;
                if (wv.CoreWebView2 != null)
                {
                    step = 30;
                    wv.CoreWebView2.NavigationStarting += EnsureHttps;
                    step = 40;
                    wv.CoreWebView2.SourceChanged += SourceChanged;
                    step = 50;
                    //wv.CoreWebView2.ContentLoading += ContentLoading;
                    step = 60;
                    //wv.CoreWebView2.HistoryChanged += HistoryChanged;
                    step = 70;
                    wv.CoreWebView2.NavigationCompleted += NavigationCompleted;
                    step = 80;
                    wv.CoreWebView2.NewWindowRequested += NewWindowRequested;
                    step = 90;
                    //wv.CoreWebView2.ClientCertificateRequested += ClientCertificateRequested;
                    step = 100;
                    wv.CoreWebView2.NavigationStarting += NavigationStarting;
                    step = 110;
                    //wv.CoreWebView2InitializationCompleted += CoreWebView2InitializationCompleted;
                    step = 120;
                    //wv.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;
                    step = 130;
                    //wv.CoreWebView2.ProcessFailed += ProcessFailed;
                    step = 140;
                    //wv.CoreWebView2.WebResourceRequested += WebResourceRequested;

                    // should be called by CoreWebView2InitializationCompleted
                    step = 150;

                    //wv.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                    //LogMsg($"SetWebView2EventHandlers() WebMessageReceived handler set to [wv.CoreWebView2.WebMessageReceived]");
                    step = 160;
                    //SetUserAgent(wv);

                }
                else
                {
                    step = 80;
                    throw new Exception("WebView2 is null");
                }
            }
            catch (Exception ex)
            {
                string msg = $"InitWebView2.Page() @ [{step}] EXCEPTION {ex.Message}";
                HandleException(msg);
            }

        }

        private void LogMsg(string msg)
        {
            string m = $"{DateTime.Now.ToString("hh:MM:ss.fff")} - {msg}" + Environment.NewLine;
            textLog.AppendText(m);
        }



        private void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            LogMsg($"NavigationStarting() [{e.Uri}]");
        }

        private void SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            
        }

        private void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            LogMsg( $"{Name} : {e.Uri}");

        }

        private void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            
            //textLog.AppendText(e.)
        }

        private void NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;

            //e.NewWindow = null;            
            wv3.Source = new Uri(e.Uri);
            wv3.Refresh();
        }

        private void HandleException(string msg)
        {
            throw new NotImplementedException();
        }
    }



}