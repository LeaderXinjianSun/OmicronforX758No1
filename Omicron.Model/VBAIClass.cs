using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.VBAI;
using NationalInstruments.VBAI.Structures;
using NationalInstruments.VBAI.Enums;
using NationalInstruments.Vision.WindowsForms;
using NationalInstruments.Vision;

namespace Omicron.Model
{
    public class VBAIClass
    {
        private VBAIEngine vBAIEngine;
        public VisionImage VBAIImage;
        public string vbaipath = "";
        public bool OpenEngine()
        {             
            try
            {
                vBAIEngine = new VBAIEngine("LeaderVision", "", true);
                vBAIEngine.OpenInspection(vbaipath);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public void CloseEngine()
        {
            try
            {
                vBAIEngine.StopInspection();
                vBAIEngine.CloseConnection();
            }
            catch { }
        }
        public List<StepMeasurements> InspectEngine()
        {
            string guid = "";
            List<StepMeasurements> listMeasurements = new List<StepMeasurements>();
            try
            {
                bool newImageAvailable;//, inspectionStatus;
                VBAIDateTime timeStamp;
                InspectionMeasurements[] measurements;
                vBAIEngine.EnableInspectionMeasurements();
                vBAIEngine.RunInspectionOnce(-1);

                measurements = vBAIEngine.GetInspectionMeasurements(null, out timeStamp);
                InspectionStep[] steps = vBAIEngine.GetInspectionSteps();
                //     string guid = "";
                foreach (var inspectionstep in steps)
                {
                    if (inspectionstep.stepName == "Select Image 1")
                    {
                        guid = inspectionstep.stepGUID; break;
                    }
                }
                VBAIImage = vBAIEngine.GetInspectionImage(guid, 1, 1, out newImageAvailable);
                foreach (var inspectMeasurements in measurements)
                {
                    listMeasurements.AddRange(inspectMeasurements.measurements);
                }
                return listMeasurements;
            }
            catch(Exception ex) { return null; }

        }

    }
}
