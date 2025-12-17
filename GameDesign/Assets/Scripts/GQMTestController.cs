using System;
using System.Globalization;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class GQMTestController : MonoBehaviour
{
    private const int testId = 0, location = 1, keyInteractions = 2, extraInteractions = 3, dialogs = 4, totalDialogTime = 5, locationStart = 6, memoryInteractions = 7, endingStatus = 8, totalTimeStill = 9, timesStill = 10, stimuliMemory = 11, stimuli = 12, totalTimeStimuli = 13, datetime = 14, eventActionState = 15, typeofAction = 16, payload = 17;
    private string pathTestName;
    private const string genpath = "Assets/Scripts/csv";
    private const string dayFormat = "yyyy-MM-dd HH-mm-ss";
    private const string timeFormat = @"hh\:mm\:ss\:ff";
    private const string timeFormatZero = "0:0:0:0";

    private string[] parameters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get current time
        DateTime now = DateTime.Now;
        // Get test id
        string[] all_tests = Directory.GetFiles(genpath);
        int testid = 1;
        // Check if there are files
        if (all_tests.Length > 0) {
            if (GameManager.instance.testId == 0)
            {
                string filename = all_tests[all_tests.Length - 1];
                Debug.Log(filename);
                if (filename.Contains(".meta"))
                {
                    filename = all_tests[all_tests.Length - 2];
                }
                StreamReader lastTest = new StreamReader(filename);
                string[] debugSplit = lastTest.ReadLine().Split(',');
                Debug.Log(debugSplit[0]);
                testid = int.Parse(debugSplit[0])+1;
                GameManager.instance.testId = testid;
            } else {
                testid = GameManager.instance.testId;
            }
            Debug.Log(testid);
        } else {
            GameManager.instance.testId = testid;
        }
        if (GameManager.instance.testFile.Length == 0) {
            pathTestName = genpath + "/PlaytestAt_" + now.ToString(dayFormat) + ".csv";
            GameManager.instance.testFile = pathTestName;
        } else {
            pathTestName = GameManager.instance.testFile;
        }
        if (GameManager.instance.testParameters.Length == 0) {
            // Create the new test file
            Debug.Log(pathTestName);
            StreamWriter newTestFile = new StreamWriter(new FileStream(pathTestName, FileMode.Create, FileAccess.Write, FileShare.Read));
            parameters = new string[] { testid.ToString(), "aunt's House", "0", "0", "0", timeFormatZero, "0", "0", "0", timeFormatZero, "0", "0", "0", timeFormatZero, timeFormatZero, "0", "0", "0" };
            GameManager.instance.testParameters = parameters;

            // Write the first line
            foreach (string item in parameters)
            {
                newTestFile.Write(item + ',');
            }
            newTestFile.Write('\n');

            newTestFile.Close();
        }
        else {
            parameters = GameManager.instance.testParameters;
        }
        foreach (string item in parameters)
        {
            Debug.Log(item);
        }
    }

    // Increase value of item as string by 1
    private void IncreaseString(int location) {
        string item = parameters[location];
        int parametersString = int.Parse(item) + 1;
        parameters[location] = parametersString.ToString();
    }

    // Write into the test file
    private bool writeInteraction(string currentTime, string currentEventActionState, string currentTypeAction, string currentPayload)
    {
        StreamWriter newTestFile = File.AppendText(pathTestName);

        parameters[datetime] = currentTime;
        parameters[eventActionState] = currentEventActionState;
        parameters[typeofAction] = currentTypeAction;
        parameters[payload] = currentPayload;

        foreach (string item in parameters) {
            newTestFile.Write(item + ',');
        }
        newTestFile.Write('\n');

        newTestFile.Close();
        return true;
    }

    // Increment time into current
    private void increaseTime(TimeSpan newTime, int location)
    {
        // Convert from string to time
        Debug.Log(parameters[location]);
        string lastTiemStr = parameters[location];
        Debug.Log(lastTiemStr);
        if (lastTiemStr.Equals(timeFormatZero))
        {
            Debug.Log(newTime);
            parameters[location] = newTime.ToString(timeFormat);
        }
        else {
            TimeSpan lastTime = TimeSpan.ParseExact(lastTiemStr, timeFormat, CultureInfo.InvariantCulture);
            // Sum both times
            lastTime = lastTime.Add(newTime);
            parameters[location] = lastTime.ToString(timeFormat);
        }
    }

    // Interactions for the csv

    // In case of interaction
    //<key_interactions>, <extra_interactions>, <memory_interactions>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void addInteraction(bool is_key){
        string currentTime = DateTime.Now.ToString(timeFormat);
        if (parameters[location].Contains("Memory")) {
            IncreaseString(memoryInteractions);
        }
        if (is_key) {
            IncreaseString(keyInteractions);
        }
        else
        {
            IncreaseString(extraInteractions);
        }
        writeInteraction(currentTime, "action", "addInteraction", is_key.ToString());
        GameManager.instance.testParameters = parameters;
    }

    // In case of entering location
    //<location>, <location_start>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void enteringLocation(string newLocation) {
        string currentTime = DateTime.Now.ToString(timeFormat);

        parameters[location] = newLocation;
        parameters[locationStart] = currentTime;

        writeInteraction(currentTime, "event", "enteringLocation", newLocation);
        GameManager.instance.testParameters = parameters;
    }

    // In case of entering location
    //<dialogs>, <total_dialog_time>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void newDialog(TimeSpan newTime)
    {
        DateTime currentTime = DateTime.Now;

        IncreaseString(dialogs);
        increaseTime(newTime, totalDialogTime);

        writeInteraction(currentTime.ToString(timeFormat), "state", "newDialog", newTime.ToString(timeFormat));
        GameManager.instance.testParameters = parameters;
    }

    // In case of entering location
    //<ending_status>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void endingUpdate()
    {
        string currentTime = DateTime.Now.ToString(timeFormat);

        IncreaseString(endingStatus);

        writeInteraction(currentTime, "state", "endingUpdate", parameters[endingStatus]);
        GameManager.instance.testParameters = parameters;
    }

    // In case of entering location
    //<total_time_still>, <times_still>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void stillUpdate(TimeSpan newTime)
    {
        string currentTime = DateTime.Now.ToString(timeFormat);

        increaseTime(newTime, totalTimeStill);
        IncreaseString(timesStill);

        writeInteraction(currentTime, "action", "stillUpdate", newTime.ToString(timeFormat));
        GameManager.instance.testParameters = parameters;
    }

    // In case of entering location
    //<stimuli_memory>, <stimuli>, <total_time_stimuli>, <datetime>, <event/action/state>, <typeof>, <payload>
    public void fellingStimuli(TimeSpan newTime)
    {
        string currentTime = DateTime.Now.ToString(timeFormat);
        if (parameters[location].Contains("Memory"))
        {
            IncreaseString(stimuliMemory);
        }
        IncreaseString(stimuli);
        increaseTime(newTime, totalTimeStimuli);

        writeInteraction(currentTime, "event", "fellingStimuli", newTime.ToString(timeFormat));
        GameManager.instance.testParameters = parameters;
    }
}
