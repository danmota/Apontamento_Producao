using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Telephony;
using Android.Util;
using Android.Content;
using static Android.Provider.Settings;

namespace Apontamento
{
    [Activity(Label = "Apontamento", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource, 
            // and attach an event to it
            Button btn_start = FindViewById<Button>(Resource.Id.start);
            Button btn_finish = FindViewById<Button>(Resource.Id.finish);
            Button btn_reset = FindViewById<Button>(Resource.Id.reset);
            Button btn_record = FindViewById<Button>(Resource.Id.record);

            Chronometer crono = FindViewById<Chronometer>(Resource.Id.chronometer);

            TextView txt_gravacao = FindViewById<TextView>(Resource.Id.txt_gravacao);
            TextView txt_msgrecorded = FindViewById<TextView>(Resource.Id.txt_msgrecorded);

            crono.Enabled = false;

            btn_start.Click += delegate {   crono.Base = SystemClock.ElapsedRealtime();
                                            crono.Enabled = true;
                                            crono.Start();
                                            txt_msgrecorded.Text = "Mensagem Não Gravada";
            };


            btn_finish.Click += delegate {  crono.Enabled = false;
                                            crono.Stop();
                                            txt_msgrecorded.Text = "Mensagem Não Gravada";
            };


            btn_reset.Click += delegate {   crono.Enabled = false;
                                            crono.Base = SystemClock.ElapsedRealtime();
                                            crono.Stop();
                                            txt_msgrecorded.Text = "Mensagem Não Gravada";
            };


            btn_record.Click += delegate {  txt_gravacao.Text = "Tempo: "+crono.Text.ToString()+" Processo: ";
                                            SavetoSd(crono.Text.ToString());
                                            txt_msgrecorded.Text = "Mensagem Gravada";
            };
        }
        private void SavetoSd(String crono_time)
        {

            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = System.IO.Path.Combine(sdCardPath, DateTime.Now.ToString("yyyyMMdd")+ ".txt");
            
            using (System.IO.StreamWriter write = new System.IO.StreamWriter(filePath, true))
            {
                //TelephonyManager tm = (TelephonyManager)this.GetSystemService(Context.TELEPHONY_SERVICE);

                //Log.Debug("ID", "Android ID: " + Secure.GetString(getContentResolver(), Secure.ANDROID_ID));
                //Log.Debug("ID", "Device ID : " + tm.GetDeviceId());

                write.Write(DateTime.Now.ToString() + "," + crono_time.ToString() + "\n");

            }

        }
    }
}

