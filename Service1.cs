using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsServiceYape
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer(120000); // 2 minutos en milisegundos
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }
    }
}
