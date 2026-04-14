using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanningSchedule
{
    public partial class OpenSizeReschedule : Form
    {
        public OpenSizeReschedule(object datasource)
        {
            InitializeComponent(); 
            SizeReschedule form = new SizeReschedule(datasource);
            form.ShowDialog(); 
        } 

    }
}
