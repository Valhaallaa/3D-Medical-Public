using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoringSystem : MonoBehaviour
{
    public static ScoringSystem _Instance;

    [SerializeField]
    private int LevelID;
    private int _TotalFuncNum, _FuncNumCompleted;

    private float _TotalPercentage;

    private bool isPenalised, hasPassed;
    [SerializeField]
    TextMeshProUGUI _FeedbackBox, _ResultBox;
    [System.Serializable]
    public enum Task {_CheckAirway, _ClearAirway,_TasksFinished, _ApplyOxygen, _TakeTemp, _CheckPupils,
    _CheckSeizing, _CheckConsciousness, _CheckSkinColour, _CheckAccessoryMuscles, _CheckChestSymmetry, _CheckDepthOfRespiration, _AssessPain, _ListenToChest, _MeasureRespitoryRate,
    _MeasurePulse, _MeasureBloodPressure, _MeasureCapillaryRefill, _InsertIV, _MeasureGlucoseLevel, _TakeBloods, _CheckOxygenSaturation, _BreathingStage, _CirculationStage, _DisabilityStage, _FullAssessment, _InvalidTool, _ApplyFluids, _CheckUrineOutput}; 
    [SerializeField]
    Task _CurrentTask;

   // private Task _NextQuest;
    
    public List<TaskData> TaskOrder = new List<TaskData>();
    public List<Task> GlobalRepeatable = new List<Task>();
    public List<Task> WrongTasks = new List<Task>();



    public void TaskChecker(Task task)
    {

        if (task == Task._InvalidTool)
        {
            Penalty(task);
        }

        for (int i = 0; i < TaskOrder.Count; i++)
        {

            if (TaskOrder[i]._SubTasks.Count > 0) // Check subtasks if it has any
            {
                for (int j = 0; j < TaskOrder[i]._SubTasks.Count; j++)
                {
                    if (task == TaskOrder[i]._SubTasks[j])
                    {
                        // Tasks Complete
                        if (TaskOrder[i]._ThisTask == _CurrentTask)
                        {
                            TaskOrder[i]._SubTasks.RemoveAt(j);
                            if (TaskOrder[i]._RepeatableTasks.Count > 0)
                            {
                                for (int x = 0; x < TaskOrder[i]._RepeatableTasks.Count; x++)
                                {
                                    if (task == TaskOrder[i]._RepeatableTasks[x])
                                        GlobalRepeatable.Add(TaskOrder[i]._RepeatableTasks[x]);
                                }
                            }
                        }
                        else
                        {
                            Penalty(task);

                        }

                        if (TaskOrder[i]._SubTasks.Count == 0) // No more subtasks, Task is complete
                        {
                            if (TaskOrder[i]._ThisTask == _CurrentTask)
                                TaskComplete(i);
                            else
                            {
                                Penalty(task);

                            }
                        }
                    }
                }
            }
            else // no subtasks
            {
                if (task == TaskOrder[i]._ThisTask)
                {
                    // Task Complete
                    if (TaskOrder[i]._ThisTask == _CurrentTask)
                        TaskComplete(i);
                    else
                    {
                        Penalty(task);

                    }
                }
            }
        }

        switch (LevelID)
        {
            default:
            case 1:
                AffectPatient1(task);
                break;
            case 2:
                AffectPatient2(task);
                break;
        }

        // if multiple levels need change this into function
        switch (task)
        {
            default:
                break;
        }
    }


    void AffectPatient1(Task task)
    {
        switch (task)
        {
            default:
                break;
            case Task._ListenToChest:
            case Task._CheckSkinColour:
            case Task._CheckOxygenSaturation:
                PatientSystem._PatientInfo.FindIssue();
                break;
            case Task._ApplyOxygen:
                PatientSystem._PatientInfo._OxygenSaturation = 92;
                PatientSystem._PatientInfo._SkinColourDescription = "Flushed";
                GameObject.FindGameObjectWithTag("OxygenMask").GetComponent<MeshRenderer>().enabled =true;
                break;
            case Task._MeasureRespitoryRate:
                PatientSystem._PatientInfo.FindIssue();
                PatientSystem._PatientInfo._RespitoryRate = 25;
                break;
            case Task._InsertIV:
                GameObject.FindGameObjectWithTag("IVBag").GetComponent<MeshRenderer>().enabled = true;
                break;
            case Task._ApplyFluids:
                if (_CurrentTask == Task._ApplyFluids || _CurrentTask == Task._DisabilityStage)
                {
                    PatientSystem._PatientInfo._HeartBeatsPM = 108;
                    PatientSystem._PatientInfo._BloodPressure = "105/24";
                    PatientSystem._PatientInfo._CapillaryRefillTime = 2;
                    PatientSystem._PatientInfo._Temperature = 39.5f;
                }
                else
                {
                    FeedbackHandler._Handler.SetText("Unable to Apply", "Cant apply Fluids yet");
                }
                break;
        }
    }

    void AffectPatient2(Task task)
    {
        switch (task)
        {
            case Task._CheckAirway:
                TutorialBox._Tutorial.SetText("Well done", " The next step seeing as the airway is clear is to go through the Breathing stage, you can check the steps within the Breathing stage by looking at the information book. Some of these steps require" +
                    "tools which can be found on the table behind you. You can hover over a tool to see what it is and left click to pick it up. You will put the tool you are holding down when when you pickup a new tool.");
                TutorialBox._Tutorial.SetHelper("Complete Breathing Stage");
                break;
            default:
                break;
            case Task._CheckSkinColour:
            case Task._CheckOxygenSaturation:
                PatientSystem._PatientInfo.FindIssue();
                TutorialBox._Tutorial.SetText("Call for Help","Once you have found an issue with the patients you should call for help,calling for help before finding an issue will cause you to be penalized. To call for help click on the button on the wall.");

                break;
            case Task._CheckChestSymmetry:
                TutorialBox._Tutorial.SetText("To Pass", "In order to complete the level once you have finished all your steps you should click on the door behind you and finish your examination, then you will be scored and if you have not been penalized you will pass.");
                break;
            case Task._ListenToChest:
                PatientSystem._PatientInfo.FindIssue();
                TutorialBox._Tutorial.SetText("Tasks Order", "While tasks do have an order they must be completed in, at times you may go back and repeat a task if you've already done it, such as checking the paitents pulse after completing an intervention without being penalized.");
                break;
            case Task._ApplyOxygen:
                PatientSystem._PatientInfo._OxygenSaturation = 92;
                PatientSystem._PatientInfo._SkinColourDescription = "Flushed";
                GameObject.FindGameObjectWithTag("OxygenMask").GetComponent<MeshRenderer>().enabled = true;
                break;
            case Task._MeasureRespitoryRate:
                PatientSystem._PatientInfo._RespitoryRate = 25;
                break;
            case Task._InsertIV:
                GameObject.FindGameObjectWithTag("IVBag").GetComponent<MeshRenderer>().enabled = true;
                break;
            case Task._ApplyFluids:
                if (_CurrentTask == Task._CirculationStage || _CurrentTask == Task._DisabilityStage)
                {
                    PatientSystem._PatientInfo._HeartBeatsPM = 108;
                    PatientSystem._PatientInfo._BloodPressure = "105/24";
                    PatientSystem._PatientInfo._CapillaryRefillTime = 2;
                    PatientSystem._PatientInfo._Temperature = 39.5f;
                }
                else
                {
                    FeedbackHandler._Handler.SetText("Unable to Apply", "Cant apply Fluids yet");
                }
                break;
            case Task._InvalidTool:
                TutorialBox._Tutorial.SetText("Wrong Tool!", "Whatever task you are attempting to do you have not picked up the correct tool for it, go back and try to find the riht one!");
                break;
        }
    }

    void Penalty(Task task)
    {
        Debug.Log("PENALTY FUNC");
        bool FoundRepetable = false;
        for (int i = 0; i < GlobalRepeatable.Count; i++)
        {
            if (task == GlobalRepeatable[i])
                FoundRepetable = true;
        }

        if (!FoundRepetable)
        {
            //Penlize
            Debug.Log(" PENALTY ");
            isPenalised = true;
            bool _InList = false;
            for (int i = 0; i < WrongTasks.Count; i++)
            {
                if (task == WrongTasks[i])
                {
                    _InList = true;
                }
            }
            if (!_InList)
                WrongTasks.Add(task);
        }

    }

    public void FinalResult()
    {
        if (isPenalised || !DataManager._Data.Correct || _CurrentTask != Task._TasksFinished)
        {
            hasPassed = false;
        }
        else
        {
            hasPassed = true;
        }
    }

    void TaskComplete(int TaskPosition)
    {
        
            _CurrentTask = TaskOrder[TaskPosition]._NextMainTask;

        if(TaskOrder[TaskPosition]._RepeatableTasks.Count > 0)
        {
            for (int i = 0; i < TaskOrder[TaskPosition]._RepeatableTasks.Count; i++)
            {
                GlobalRepeatable.Add(TaskOrder[TaskPosition]._RepeatableTasks[i]);
            }
        }


            // Finish
        if(_CurrentTask == Task._TasksFinished)
        {

        }
        
       
    }

    float CalculateTotalPercentage()
    {
        _TotalPercentage = (_TotalFuncNum / _FuncNumCompleted) * 100;

        return _TotalPercentage;
    }

    void Start()
    {
        
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);



        switch (LevelID)
        {
            default:
            case 1:
                CreateTasks1();
                break;
            case 2:
                CreateTasks2();
                    break;
        }
        _CurrentTask = TaskOrder[0]._ThisTask;
    }

    private void CreateTasks1()
    {

        TaskData CheckAirway = new TaskData();
        CheckAirway._ThisTask = Task._CheckAirway;
        CheckAirway._SubTasks = new List<Task>();
        CheckAirway._RepeatableTasks = new List<Task>();
        CheckAirway._NextMainTask = Task._BreathingStage;
        CheckAirway._RepeatableTasks.Add(Task._CheckAirway);
        TaskOrder.Add(CheckAirway);


        TaskData Breathing = new TaskData();
        Breathing._ThisTask = Task._BreathingStage;
        Breathing._SubTasks = new List<Task>();
        Breathing._RepeatableTasks = new List<Task>();
        Breathing._SubTasks.Add(Task._CheckOxygenSaturation);
        Breathing._SubTasks.Add(Task._ApplyOxygen);
        Breathing._SubTasks.Add(Task._MeasureRespitoryRate);
        Breathing._SubTasks.Add(Task._CheckDepthOfRespiration);
        Breathing._SubTasks.Add(Task._CheckChestSymmetry);
        Breathing._SubTasks.Add(Task._CheckAccessoryMuscles);
        Breathing._SubTasks.Add(Task._CheckSkinColour);
        Breathing._SubTasks.Add(Task._ListenToChest);

        Breathing._RepeatableTasks.Add(Task._CheckOxygenSaturation);
        Breathing._RepeatableTasks.Add(Task._MeasureRespitoryRate);
        Breathing._RepeatableTasks.Add(Task._CheckDepthOfRespiration);
        Breathing._RepeatableTasks.Add(Task._CheckChestSymmetry);
        Breathing._RepeatableTasks.Add(Task._CheckAccessoryMuscles);
        Breathing._RepeatableTasks.Add(Task._CheckSkinColour);
        Breathing._RepeatableTasks.Add(Task._ListenToChest);

        Breathing._RepeatableTasks.Add(Task._CheckOxygenSaturation);
        Breathing._NextMainTask = Task._CirculationStage;
        TaskOrder.Add(Breathing);


        TaskData Circulation = new TaskData();

        Circulation._ThisTask = Task._CirculationStage;
        Circulation._NextMainTask = Task._DisabilityStage;
        Circulation._SubTasks = new List<Task>();
        Circulation._RepeatableTasks = new List<Task>();
        Circulation._SubTasks.Add(Task._MeasurePulse);
        Circulation._SubTasks.Add(Task._MeasureBloodPressure);
        Circulation._SubTasks.Add(Task._MeasureCapillaryRefill);
        Circulation._SubTasks.Add(Task._CheckUrineOutput);
        Circulation._SubTasks.Add(Task._TakeTemp);

        Circulation._SubTasks.Add(Task._ApplyFluids);
        Circulation._SubTasks.Add(Task._InsertIV);
        // do Iv Stuff here

        Circulation._RepeatableTasks.Add(Task._MeasurePulse);
        Circulation._RepeatableTasks.Add(Task._MeasureBloodPressure);
        Circulation._RepeatableTasks.Add(Task._MeasureCapillaryRefill);
        Circulation._RepeatableTasks.Add(Task._CheckUrineOutput);

        TaskOrder.Add(Circulation);


        TaskData Disability = new TaskData();

        Disability._ThisTask = Task._DisabilityStage;
        Disability._NextMainTask = Task._FullAssessment;
        Disability._SubTasks = new List<Task>();
        Disability._RepeatableTasks = new List<Task>();
        Disability._SubTasks.Add(Task._CheckConsciousness);
        Disability._SubTasks.Add(Task._MeasureGlucoseLevel);
        Disability._SubTasks.Add(Task._CheckPupils);
        Disability._SubTasks.Add(Task._AssessPain);
        Disability._SubTasks.Add(Task._CheckSeizing);

        Disability._RepeatableTasks.Add(Task._CheckSeizing);
        Disability._RepeatableTasks.Add(Task._AssessPain);
        Disability._RepeatableTasks.Add(Task._MeasureGlucoseLevel);

        TaskOrder.Add(Disability);

        TaskData Exposure = new TaskData();

        Exposure._ThisTask = Task._FullAssessment;
        Exposure._SubTasks = new List<Task>();
        Exposure._RepeatableTasks = new List<Task>();
        Exposure._RepeatableTasks.Add(Task._FullAssessment);
        Exposure._NextMainTask = Task._TasksFinished;
        TaskOrder.Add(Exposure);
    }

    private void CreateTasks2()
    {

        TaskData CheckAirway = new TaskData();
        CheckAirway._ThisTask = Task._CheckAirway;
        CheckAirway._SubTasks = new List<Task>();
        CheckAirway._RepeatableTasks = new List<Task>();
        CheckAirway._NextMainTask = Task._BreathingStage;
        CheckAirway._RepeatableTasks.Add(Task._CheckAirway);
        TaskOrder.Add(CheckAirway);

        TaskData Breathing = new TaskData();
        Breathing._ThisTask = Task._BreathingStage;
        Breathing._SubTasks = new List<Task>();
        Breathing._RepeatableTasks = new List<Task>();
        Breathing._SubTasks.Add(Task._CheckOxygenSaturation);
        Breathing._SubTasks.Add(Task._MeasureRespitoryRate);
        Breathing._SubTasks.Add(Task._CheckDepthOfRespiration);
        Breathing._SubTasks.Add(Task._CheckChestSymmetry);
        Breathing._SubTasks.Add(Task._CheckAccessoryMuscles);
        Breathing._SubTasks.Add(Task._CheckSkinColour);
        Breathing._SubTasks.Add(Task._ListenToChest);

        Breathing._RepeatableTasks.Add(Task._CheckOxygenSaturation);
        Breathing._RepeatableTasks.Add(Task._MeasureRespitoryRate);
        Breathing._RepeatableTasks.Add(Task._CheckDepthOfRespiration);
        Breathing._RepeatableTasks.Add(Task._CheckChestSymmetry);
        Breathing._RepeatableTasks.Add(Task._CheckAccessoryMuscles);
        Breathing._RepeatableTasks.Add(Task._CheckSkinColour);
        Breathing._RepeatableTasks.Add(Task._ListenToChest);

        Breathing._RepeatableTasks.Add(Task._CheckOxygenSaturation);
        Breathing._NextMainTask = Task._CirculationStage;
        TaskOrder.Add(Breathing);

    }

    public void GenerateFeedback()
    {
        //public enum Task
        //{
        //  _CheckAirway, _ClearAirway, _TasksFinished, _ApplyOxygen, _TakeTemp, _CheckPupils,
        //_CheckSeizing, _CheckConsciousness, _CheckSkinColour, _CheckAccessoryMuscles, _CheckChestSymmetry, _CheckDepthOfRespiration, _AssessPain, _ListenToChest, _MeasureRespitoryRate,
        // _MeasurePulse, _MeasureBloodPressure, _MeasureCapillaryRefill, _InsertIV, _MeasureGlucoseLevel, _TakeBloods, _CheckOxygenSaturation, _BreathingStage, _CirculationStage, _DisablityStage, _FullAssessment, _InvalidTool, _ApplyFluids, _CheckUrineOutput
        // };

        FinalResult();
        if (hasPassed)
            _ResultBox.text = " YOU PASSED \n";

        if (!hasPassed)
        {
            string failuretext = "";
            switch (_CurrentTask)
            {
                default:
                    break;
                case Task._CheckAirway:
                    failuretext = "You failed to check the airway";
                    break;
                case Task._BreathingStage:
                    failuretext = "You failed to finish the Breathing stage";
                    break;
                case Task._CirculationStage:
                    failuretext = "You failed to finish the Circulation stage";
                    break;
                case Task._DisabilityStage:
                    failuretext = "You failed to finish the Disability stage";
                    break;
                case Task._ApplyFluids:
                    failuretext = "You failed to apply the fluids to the patient";
                    break;
                case Task._FullAssessment:
                    failuretext = "You failed to finish the Exposure stage";
                    break;

            }



            _ResultBox.text = (" YOU FAILED: \n" + failuretext);



        }

        _FeedbackBox.text = "You got these wrong: \n";
        foreach (Task task in WrongTasks)
        {
            switch (task)
            {

                case Task._CheckAirway:
                    _FeedbackBox.text += "Check Airway \n";
                    break;
                case Task._ApplyOxygen:
                    _FeedbackBox.text += "Apply Oxygen Mask \n";
                    break;
                case Task._ClearAirway:
                    _FeedbackBox.text += "Clear Airway \n";
                    break;
                case Task._TakeTemp:
                    _FeedbackBox.text += "Take Temperature \n";
                    break;
                case Task._CheckPupils:
                    _FeedbackBox.text += "Check Pupils \n";
                    break;
                case Task._CheckSeizing:
                    _FeedbackBox.text += "Check Seizing \n";
                    break;
                case Task._CheckConsciousness:
                    _FeedbackBox.text += "Check Consciousness \n";
                    break;
                case Task._CheckSkinColour:
                    _FeedbackBox.text += "Check Skin Colour \n";
                    break;
                case Task._CheckAccessoryMuscles:
                    _FeedbackBox.text += "Check Acessory Muscles \n";
                    break;
                case Task._CheckChestSymmetry:
                    _FeedbackBox.text += "Check Chest Symmetry \n";
                    break;
                case Task._CheckDepthOfRespiration:
                    _FeedbackBox.text += "Check Depth Of Respiration \n";
                    break;
                case Task._AssessPain:
                    _FeedbackBox.text += "Assess Pain \n";
                    break;
                case Task._ListenToChest:
                    _FeedbackBox.text += "Listen To Chest \n";
                    break;
                case Task._MeasureRespitoryRate:
                    _FeedbackBox.text += "Measure Respitory Rate \n";
                    break;
                case Task._MeasurePulse:
                    _FeedbackBox.text += "Measure Pulse \n";
                    break;
                case Task._MeasureBloodPressure:
                    _FeedbackBox.text += "Measure Blood Pressure \n";
                    break;
                case Task._MeasureCapillaryRefill:
                    _FeedbackBox.text += "Measure Capillary Refill Time \n";
                    break;
                case Task._InsertIV:
                    _FeedbackBox.text += "Insert IV \n";
                    break;
                case Task._MeasureGlucoseLevel:
                    _FeedbackBox.text += "Measure Glucose Level \n";
                    break;
                case Task._TakeBloods:
                    _FeedbackBox.text += "Take Bloods \n";
                    break;
                case Task._CheckOxygenSaturation:
                    _FeedbackBox.text += "Check Oxygen Saturation \n";
                    break;
                case Task._ApplyFluids:
                    _FeedbackBox.text += "Apply IV Fluids \n";
                    break;
                case Task._CheckUrineOutput:
                    _FeedbackBox.text += "Check Urine Output / Fluid Balance \n";
                    break;
                case Task._FullAssessment:
                    _FeedbackBox.text += "Fully Assess Patients Body \n";
                    break;
                default:
                    break;
            }
        }

    }

    [System.Serializable]
    public struct TaskData
    {
        public Task _ThisTask;
        public Task _NextMainTask;

        public List<Task> _SubTasks;
        public List<Task> _RepeatableTasks;
         
        
    }
}
