using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F_EPM_Maintennace_Plan.bean
{
    public class PlanInfo
    {

        private string orgId;
        private string orgName;
        private string deviceNo;
        private string deviceName;
        private string planNo;
        private string planName;
        private string startDate;
        private string endDate;
        private string period;
        private string interval;
        private string frequency;
        private string status;

        public string FinishDate { get; set; }
        public string FrequencyInfo { get; set; }

        public string OrgId
        {
            get
            {
                return orgId;
            }
            set
            {
                orgId = value;
            }
        }
        public string OrgName {
            get {
                return orgName;
            }
            set {
                orgName = value;
            }
        }
        
        public string DeviceName
        {
            get {
                return deviceName;
            }
            set {
                deviceName = value;
            }
        }

        public string DeviceNo
        {
            get
            {
                return deviceNo;
            }
            set
            {
                deviceNo = value;
            }
        }

        public string PlanNo
        {
            get
            {
                return planNo;
            }
            set
            {
                planNo = value;
            }
        }

        public string PlanName
        {
            get
            {
                return planName;
            }
            set
            {
                planName = value;
            }
        }

        public string StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }
        public string Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }
        public string Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }
        public string Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        } 
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
    }
}
